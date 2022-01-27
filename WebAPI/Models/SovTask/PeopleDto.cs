using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.SovTask
{
    public class PeopleDto : BaseDto<BaseDtoLine>
    {
        #region fields
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string HomeWorld { get; set; }
        #endregion
    }
}
