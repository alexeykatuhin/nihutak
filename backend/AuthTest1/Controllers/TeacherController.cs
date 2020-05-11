using AuthTest.API.Filters;
using AuthTest.Core.Constants;
using AuthTest.Core.DTO;
using AuthTest.Core.DTO.Photo;
using AuthTest.Core.DTO.Teacher;
using AuthTest.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthTest.API.Controllers
{
    [Route("[controller]/[action]")]
    public class TeacherController: ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeacherController(ApplicationDbContext context, IMapper mapper, RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager) 
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize]
        [HasRole(Constants.AdminRole)]
        [HttpGet]
        public async Task<IActionResult> GetAnswers(int id)
        { 
            var answersDb = await _context.Answers.Where(x => x.OptionId == id).ToListAsync();

            var resDto = new List<AnswerDto>();
            for (int i = 1; i < 21; i++)
            {
                var answ = answersDb.FirstOrDefault(x => x.AnswerId == i);
                if (answ != null)
                    resDto.Add(new AnswerDto { Id = i, Answer = answ.AnswerValue?.ToString() });
                else
                    resDto.Add(new AnswerDto { Id = i });
            }

            return  Ok(resDto);

        }

        [Authorize]
        [HasRole(Constants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> SetAnswers([FromBody]SetAnswerDto dto)
        {
            var answersDb = await _context.Answers.Where(x => x.OptionId == dto.OptionId).ToListAsync();

            var resDto = new List<AnswerDto>();

            foreach (var item in dto.Answers)
            {
                var answ = answersDb.FirstOrDefault(x => x.AnswerId == item.Id);
                var d = double.TryParse(item.Answer, out var dd) ? (double?)dd : null;
                if (answ != null)
                {
                    answ.AnswerValue = d;
                }
                else
                    _context.Answers.Add(new Answer { AnswerId = item.Id, OptionId = dto.OptionId, AnswerValue = d });


            }

            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> CheckAnswers([FromBody]SetAnswerDto dto)
        {
            var answersDb = await _context.Answers.Where(x => x.OptionId == dto.OptionId).ToListAsync();

            var resDto = new List<AnswerDto>();

            foreach (var item in dto.Answers)
            {
                var answ = answersDb.FirstOrDefault(x => x.AnswerId == item.Id);
                var d = double.TryParse(item.Answer, out var dd) ? (double?)dd : null;
                if (answ != null && answ.AnswerValue.HasValue && d != answ.AnswerValue)
                {
                    item.ErrorMessage = $"Ошибка! Правильное значение {answ.AnswerValue}";

                }
                else
                    item.ErrorMessage = null;
            }

            return Ok(dto.Answers);

        }



    }
}
