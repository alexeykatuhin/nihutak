using AuthTest.Core.DTO;
using AuthTest.Core.DTO.Photo;
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
        public async Task<IActionResult> GetFilterData()
        {
            var res = new FilterSourceDto();
            res.Tags = _context.Tags.Select(x => _mapper.Map<SimpleDto>(x)).ToList().OrderBy(x => x.Name).ToList();

            res.Countries = _context.Countries.Include(x => x.Cities).Select(x => _mapper.Map<CoutryCitiesDto>(x)).ToList().OrderBy(x => x.Name).ToList();

            res.Years = _context.Photos.Select(x => x.CreatedDt.Year).GroupBy(x => x).Select(x => x.Key).OrderByDescending(x=>x).ToList();
            
            return (Ok(res));

        }
        //=> Ok(_context.Tags.Select(x => _mapper.Map<SimpleDto>(x)).ToList().OrderBy(x=>x.Name));

    }
}
