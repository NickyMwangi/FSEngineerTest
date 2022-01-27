using Core.Entities.SovTask;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.SovTask;

namespace WebAPI.Controllers.SovTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : DbCrudere<People, PeopleDto, BaseDtoLine>
    {
        public PeopleController(ICrudereService repo, IMapperService _mapper) : base(repo, _mapper)
        {
        }
    }
}
