using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.SovTask
{
    public class JokesDto : BaseDto<BaseDtoLine>
    {
        #region fields
        public string CategoryId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        #endregion
        #region references
        public string JokeCategoryDescription { get; set; }
        #endregion
    }
}
