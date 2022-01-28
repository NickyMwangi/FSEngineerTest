using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using Service.Utility.Extensions;
using System.Web;
using Infra;
using WebApi.Models;
using static WebAPI.Models.Account.AccountModel;

namespace API.Controllers.Account
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _appSettings;
        public AccountController(UserManager<ApplicationUser> userManager,IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;_appSettings = appSettings.Value;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                string host = HttpContext.Request.Host.Value;
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                    return BadRequest(new { message = "Invalid Email address or username", success = false });
                else
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        //await SendEmailConfirmationLink(user, model.ClientURI);
                        return BadRequest(new { message = "Open your mail to confirm your account", success = false });
                    }
                    else if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var role = await _userManager.GetRolesAsync(user);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_appSettings.JWT_Secret);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim(ClaimTypes.Email,user.Email),
                            new Claim(ClaimTypes.Name,user.Email),
                            new Claim("ProfileName" ,user.FirstName.ToString() +" "+ user.LastName.ToString()),
                            }),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);
                        return Ok(new { user, token, success = true });
                    }
                    else
                    {
                        return BadRequest(new { message = "Invalid password ", success = false });
                    }
                }
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message, success = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, success = false });
            }
        }
    }
}
