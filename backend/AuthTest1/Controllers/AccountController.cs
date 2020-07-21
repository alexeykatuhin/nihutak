using AuthTest.Core.Abstract;
using AuthTest.Core.Constants;
using AuthTest.Core.DTO.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AuthTest.API.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
      

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return Ok(await GenerateJwtToken(model.Email, appUser));
            }

            throw new AuthenticationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterWithConfirm([FromBody] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, Constants.UserRole);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
                var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                var link = $"{_configuration["ClientUrl"]}/confirmemail?code={codeEncoded}&id={user.Id}"; 
                await _emailService.SendEmailAsync(user.Email, "Confirm email",
                    $"Для подтверждения эмайла перейди по <a href='{link}'>ссылке</a>.");
                await _signInManager.SignInAsync(user, false);
                return Ok(await GenerateJwtToken(model.Email, user));
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }
        
        [HttpGet]
        public async Task ConfirmEmail(string code, string userId)
        {

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (!result.Succeeded)
                throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.UserRole);
                await _signInManager.SignInAsync(user, false);
                return Ok(await GenerateJwtToken(model.Email, user));
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterConfirm([FromBody] RegisterConfirmDto registerConfirmDto)
        {
            var user = await _userManager.FindByIdAsync(registerConfirmDto.UserId);
            var result = await _userManager.ConfirmEmailAsync(user, registerConfirmDto.Code);
            await _signInManager.SignInAsync(user, false);
            return Ok(await GenerateJwtToken(user.Email, user));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_userManager.Users.Select(x=>_mapper.Map<UserInfoDto>(x)));
        }

        private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var isAdmin = (await _userManager.GetRolesAsync(user)).Any(x=>x == Constants.AdminRole);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new UserDto{ token = new JwtSecurityTokenHandler().WriteToken(token), username = user.UserName, isAdmin = isAdmin, email = email };
        }


        [HttpPost]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.userName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userDto.userName,
                    Email = userDto.email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, Constants.UserRole);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok(await GenerateJwtToken(user.UserName, user));
                }
            }
            else
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(await GenerateJwtToken(user.UserName, user));
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);


            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            var link = $"{_configuration["ClientUrl"]}/resetconfirm?code={codeEncoded}&id={user.Id}";
            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Для сброса пароля перейди по <a href='{link}'>ссылке</a>.");

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto model)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            var user = await _userManager.FindByIdAsync(model.UserId);
            var result = await _userManager.ResetPasswordAsync(user, codeDecoded, model.Password);
            if (result.Succeeded)
                return Ok(await GenerateJwtToken(user.UserName, user));


            throw new ApplicationException("UNKNOWN_ERROR");
        }

    }
}
