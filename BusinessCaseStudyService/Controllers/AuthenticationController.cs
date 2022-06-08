using BusinessCaseStudyService.Models;
using BusinessCaseStudyService.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCaseStudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static string username,password;
        public AuthenticationController (IConfiguration config)
        {
            _config = config;
            username = _config.GetSection("Authorize").GetSection("UserName").Value;
            password = _config.GetSection("Authorize").GetSection("Password").Value;
        }
        [Route("login")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseObject<string>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Authorize(Authorize model)
        {
            var logId = $"{Guid.NewGuid()}";
            try
            {
                if (!ModelState.IsValid)
                {
                    var invalidRes = new ErrorResponse
                    {
                        Message = $"{ConstMessage.INVALID_REQ_OBJ}",
                        ProcessId = logId
                    };
                    return StatusCode(400, invalidRes);
                }
                if (model.UserName == username && model.Password == password)
                {
                   var token =  await GenerateJwtToken(username);
                    return Ok(new ResponseObject<string>
                    {
                        Data = token,
                        Message = $"{ConstMessage.SUCCESS}",
                        StatusCode = "200",
                        StatusMessage = $"{ConstMessage.SUCCESS}",
                    });
                }
                else
                {
                    return Ok(new ResponseObject<string>
                    {
                        Data = $"{ConstMessage.INCORRECT_ACCESS}",
                        Message = $"{ConstMessage.INCORRECT_ACCESS}",
                        StatusCode = "200",
                        StatusMessage = $"{ConstMessage.FAIL}",
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        private async Task<string> GenerateJwtToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, userName),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(8)).ToUnixTimeSeconds().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:SigningKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
