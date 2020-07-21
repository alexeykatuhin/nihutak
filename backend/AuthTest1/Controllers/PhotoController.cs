using AuthTest.Core.DTO;
using AuthTest.Core.DTO.Photo;
using AuthTest.Data.Comparers;
using AuthTest.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTest.API.Controllers
{
    [Route("[controller]/[action]")]
    public class PhotoController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PhotoController(ApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpPost]
        public async Task<IActionResult> GetMany([FromBody]PhotoFilterDto filter)
        {
            int pageSize = 3;
            var query = _context.Photos.AsQueryable(); 
            if (filter.Tags != null && filter.Tags.Any())
                query = query.Where(x => x.PhotoTags.Any(y => filter.Tags.Contains(y.TagId)));
            if (filter.Countries != null && filter.Countries.Any())
                query = query.Where(x => filter.Countries.Contains(x.City.CountryId));
            if (filter.Cities != null && filter.Cities.Any())
                query = query.Where(x => filter.Cities.Contains(x.City.Id));
            if (filter.Years != null && filter.Years.Any())
                query = query.Where(x => filter.Years.Contains(x.CreatedDt.Year));

            switch (filter.Order)
            {
                case Core.Enums.OrderEnum.Random:
                    query = query.Where(x=> !filter.AlreadyShownPhotos.Contains(x.Id)).OrderBy(x => Guid.NewGuid());
                    break;
                case Core.Enums.OrderEnum.Desc:
                    query = query.OrderByDescending(x => x.CreatedDt);
                    break;
                case Core.Enums.OrderEnum.Asc:
                    query = query.OrderBy(x => x.CreatedDt);
                    break;
            }

            var list = query.Include(x => x.City).ThenInclude(x => x.Country).Include(X => X.PhotoTags).ThenInclude(x => x.Tag)
               .Take(pageSize);


            var lstDto = list.Select(x => _mapper.Map<PhotoDto>(x)).ToList();
            filter.AlreadyShownPhotos.AddRange(lstDto.Select(x => x.Id));
            var res = new PhotoExtDto { Photos = lstDto, AlreadyShownPhotos = filter.AlreadyShownPhotos };
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) 
        {
            var item =   await _context.Photos
            .Include(x => x.City)
            .ThenInclude(x => x.Country)
            .Include(X => X.PhotoTags)
            .ThenInclude(x => x.Tag).FirstOrDefaultAsync(x=>x.Id == id);

            var itemDto = _mapper.Map<PhotoDto>(item);
            return Ok(itemDto);
        }


        [HttpGet]
        public async Task<IActionResult> GetFilterData(bool empty = false)
        {
            var res = new FilterSourceDto();
            var tags = _context.Tags.AsQueryable();
            if (!empty)
                tags = tags.Include(x => x.PhotoTags).Where(x => x.PhotoTags.Any());
            res.Tags = tags.Select(x => _mapper.Map<SimpleDto>(x)).ToList().OrderBy(x => x.Name).ToList();

            var countries = _context.Countries.Include(x => x.Cities).AsQueryable();
            if (!empty)
                countries = countries.Where(x => x.Cities.Any(y => y.Photos.Any()));


            var c = countries.ToList();

            res.Countries = _context.Countries.ToList().Select(x => _mapper.Map<CoutryCitiesDto>(x)).OrderBy(x => x.Name).ToList();

            res.Years = _context.Photos.Select(x => x.CreatedDt.Year).GroupBy(x => x).Select(x => x.Key).OrderByDescending(x=>x).ToList();

            
            return (Ok(res));
        }

        [HttpPost]
        public async Task AddCountry([FromBody]CountryDto countryDto)
        {
            _context.Countries.Add(new Country { Code = countryDto.Code, Name = countryDto.Name });
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task AddCity([FromBody]SimpleDto simpleDto)
        {
            _context.Cities.Add(new City { CountryId = simpleDto.Id.Value, Name = simpleDto.Name });
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task AddPhoto([FromBody]PhotoDto photo)
        {
            await TagProcees(photo);
            var photoItem = _mapper.Map<Photo>(photo);
            _context.Photos.Add(photoItem);
            await _context.SaveChangesAsync();

            foreach (var item in photo.Tags)
            {
                _context.PhotoTags.Add(new PhotoTag { PhotoId = photoItem.Id, TagId = item.Id.Value });
            }
            
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task UpdatePhoto([FromBody]PhotoDto photo)
        {
            try
            {

                var dbItem = await _context.Photos.Include(x=>x.PhotoTags).FirstOrDefaultAsync(x=>x.Id == photo.Id);
                dbItem.Url = photo.Url;
                dbItem.CityId = photo.City.Id.Value;
                dbItem.CreatedDt = DateTime.Parse(photo.Date);
                dbItem.Description = photo.Description;

                await TagProcees(photo);


                var photoTagsNew = photo.Tags.Select(x => new PhotoTag { PhotoId = dbItem.Id, TagId = x.Id.Value }).ToList();

                var deleted = dbItem.PhotoTags.Except(photoTagsNew, new TagComparer());
                var added = photoTagsNew.Except(dbItem.PhotoTags, new TagComparer());
                _context.PhotoTags.RemoveRange(deleted);
                _context.PhotoTags.AddRange(added);
                //dbItem.PhotoTags = photo.Tags.Select(x => new PhotoTag { PhotoId = dbItem.Id, TagId = x.Id.Value }).ToList();

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
        }

        private async Task TagProcees(PhotoDto photo)
        {
            var tags = _context.Tags.ToList();
            foreach (var item in photo.Tags)
            {
                var existed = tags.FirstOrDefault(x => x.Name.ToLower() == item.Name.ToLower());
                if (existed != null)
                {
                    item.Id = existed.Id;
                }
                else
                {
                    var newTag = new Tag { Name = item.Name };
                    _context.Tags.Add(newTag);
                    await _context.SaveChangesAsync();
                    item.Id = newTag.Id;
                }
            }
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            var item = _context.Photos.Single(x => x.Id == id);
            _context.Photos.Remove(item);
        }
    }
}
