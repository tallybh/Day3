using Day3.Contracts;
using Day3.Models;
using Day3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Day3
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private AppDbContext _context;
        private IUserRepository _userRepository;

        public LoginController(IConfiguration config, AppDbContext context, IUserRepository userRepository)
        {
            _configuration = config;
            _context = context;
            _userRepository  = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

               // if (user != null)
               // {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", _userData.Id.ToString()),
                        new Claim("DisplayName", _userData.DisplayName),
                        new Claim("UserName", _userData.UserName),
                        new Claim("Email", _userData.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
               // }
               // else
               // {
               //     return NotFound("Invalid credentials");
               // }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return  Ok(await _userRepository.GetAllAsync());
        }


        private async Task<UserModel> GetUser(string email, string password)
        {
            return await _userRepository.GetByUserNameAndOasswordAsync(email, password);
        }

    }
}
