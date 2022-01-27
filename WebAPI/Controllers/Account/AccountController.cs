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

        [HttpPost]
        [Route("Register")]
        public async Task<object> Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.MobileNo,
                        Gender = model.Gender,
                        IsActive = true
                    };
                    string host = HttpContext.Request.Host.Value;

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            //await SendEmailConfirmationLink(user, model.ClientURI);
                        }
                        await _userManager.AddToRoleAsync(user, "Applicant");
                        return Ok(new
                        {
                            message = "Registration Successful. Check your email to confirm",
                            model.Email,
                            user.ProfileId,
                            success = true
                        });
                    }
                    List<string> errorMgs = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorMgs.Add(error.Description);
                    }
                    string message = string.Join(",", errorMgs.ToArray());
                    return BadRequest(new { message, status = false });
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
            var regUser = await _userManager.FindByNameAsync(model.Email);
            if (regUser != null)
                await _userManager.DeleteAsync(regUser);
            return BadRequest(new { message = ModelState.AlertMessage(true) });
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
                    return BadRequest(new { message = "Invalid Request", success = false });
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return BadRequest(new { message = "Invalid Email Confirmation Request", success = false });

                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    if (result.Errors == null)
                        errors = "Invalid Email Confirmation Request";
                    return BadRequest(new { message = errors, success = false });
                }

                return Ok();
            }
            catch (CustomException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ExceptionExtension.AlertMessage(ex, true));
            }
            return BadRequest(new { message = ModelState.AlertMessage(true) });
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.AlertMessage(true) });
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new { message = "Invalid Request. An account with this email address doesnt exist" });
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var param = new Dictionary<string, string>
            //{
            //    {"token", token },
            //    {"email", model.Email }
            //};
            //var callback = QueryHelpers.AddQueryString(model.ClientURI, param);
            //await _emailservice.SendEmailResetPasswordAsync(user.FirstName, user.Email, callback);
            return Ok(new
            {
                message = "Reset link sent successfully. Check your Email to reset",
                success = true
            });
        }


        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState.AlertMessage(true) });
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new { message = "Invalid Request. An account with this email address doesnt exist" });
            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok(new
            {
                message = "Password reset successfully. Proceed to login",
                success = true
            });
        }

        //public async Task<IActionResult> SendEmailConfirmationLink(ApplicationUser user, string clientURI)
        //{
        //    string msg = "Open your mail to confirm your account";

        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //    var param = new Dictionary<string, string>
        //    {
        //        {"token", code },
        //        {"email", user.Email }
        //    };
        //    var callbackUrl = QueryHelpers.AddQueryString(clientURI, param);
        //    var sent = await _emailservice.SendEmailConfirmationAsync(user.FirstName, user.Email, callbackUrl.ToString());

        //    if (!Convert.ToBoolean(sent[0].ToString()))
        //        msg = "An error occurred while processing the request.";
        //    return BadRequest(new { message = msg, success = false });
        //}

        [HttpPost]
        [Route("Assignrole")]
        public async Task AssignToRoleAsync([FromBody] ConfirmModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                await _userManager.AddToRoleAsync(user, model.RoleName);
        }

    }
}
