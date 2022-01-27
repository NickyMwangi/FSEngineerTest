using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class BaseDto<T>
        where T : BaseDtoLine, new()
    {
        public string Id { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime? ModifiedOn { get; set; }
        //public string ModifiedBy { get; set; }
    }
}
