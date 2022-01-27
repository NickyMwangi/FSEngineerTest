using Mapster;
using Service.Utility.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class BaseDtoLine 
    {
        
        //[AdaptMember("Id")]
        public string LineId { get; set; }
        public virtual string HeaderId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get => DateTime.Now; set { } }
        public string ModifiedBy { get; set; }
    }
}
