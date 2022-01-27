using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Account
{
    public class AccountModel
    {
        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            [Required]
            public string ClientURI { get; set; }
        }

        public class RegisterModel
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string FirstName { get; set; }
            //[Required]
            public string MiddleName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string MobileNo { get; set; }
            [Required]
            public string Salutation { get; set; }
           /// [Required]
            public string IdPassport { get; set; }
            [Required]
            public string CountryId { get; set; }
            public string Gender { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string City { get; set; }
            [Required]
            public string ClientURI { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string ConfirmPassword { get; set; }
        }

        public class ConfirmModel
        {
            public string UserId { get; set; }
            public string Code { get; set; }
            [Required]
            public string ClientURI { get; set; }
            public string RoleName { get; set; }
            public string Email { get; set; }
        }

        public class ResetPasswordModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string Token { get; set; }
            public string Code { get; set; }
        }

        public class ForgotPasswordModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string ClientURI { get; set; }
        }
    }
}
