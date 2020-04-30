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


        [HttpGet]
        public async Task<IActionResult> GetMany(int page = 0)
        {
            int pageSize = 3;
            var list = _context.Photos.Include(x=>x.City).ThenInclude(x=>x.Country).Include(X=>X.PhotoTags).ThenInclude(x=>x.Tag).Skip(page*pageSize).Take(pageSize);

            var lstDto = list.Select(x => _mapper.Map<PhotoDto>(x)).ToList();
            return Ok(lstDto);
        }
    }
}
