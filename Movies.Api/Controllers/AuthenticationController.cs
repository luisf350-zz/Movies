using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Movies.Common.Dto;
using Movies.Domain.Contract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IUserDomain _userDomain;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserDomain userDomain, IConfiguration configuration, ILogger<AuthenticationController> logger, IMapper mapper) : base(logger, mapper)
        {
            _userDomain = userDomain;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var result = await _userDomain.Register(register);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _userDomain.Login(login);

            if (result.HasErrors)
            {
                return BadRequest(string.Join("- ", result.Errors));
            }

            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, result.Result.Id.ToString()),
                new(ClaimTypes.Name, result.Result.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Jwt:Token").Value));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
