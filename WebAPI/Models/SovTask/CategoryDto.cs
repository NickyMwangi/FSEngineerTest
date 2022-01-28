using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.SovTask
{
    public class CategoryDto : BaseDto<BaseDtoLine>
    {
        #region fields
        //public string JokeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        #endregion
        #region lists
        public IEnumerable<JokesDto> Jokes { get; set; }
        #endregion

    }
}
