using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public partial class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileId { get; set; }
        public string UniveristyId { get; set; }
        public string IdPassport { get; set; }
        public string Gender { get; set; }
        public bool? IsActive { get; set; }
        public string UserRole { get; set; }
    }
}
