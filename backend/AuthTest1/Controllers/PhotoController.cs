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

            var list = query.Include(x => x.City).ThenInclude(x => x.Country).Include(X => X.PhotoTags).ThenInclude(x => x.Tag)
                .Skip(filter.Page * pageSize).Take(pageSize);


            var lstDto = list.Select(x => _mapper.Map<PhotoDto>(x)).ToList();
            return Ok(lstDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilterData()
        {
            var res = new FilterSourceDto();
            res.Tags = _context.Tags.Select(x => _mapper.Map<SimpleDto>(x)).ToList().OrderBy(x => x.Name).ToList();

            res.Countries = _context.Countries.Include(x => x.Cities).Select(x => _mapper.Map<CoutryCitiesDto>(x)).ToList().OrderBy(x => x.Name).ToList();
            return (Ok(res));

        }
        //=> Ok(_context.Tags.Select(x => _mapper.Map<SimpleDto>(x)).ToList().OrderBy(x=>x.Name));

    }
}
