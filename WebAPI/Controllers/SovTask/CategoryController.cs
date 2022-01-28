﻿using Core.Entities.SovTask;
using Microsoft.AspNetCore.Authorization;
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
    [Route("jokes/[controller]")]
    [Authorize]
    public class CategoryController : DbCrudere<Category, CategoryDto, BaseDtoLine>
    {
        public CategoryController(ICrudereService repo, IMapperService _mapper) : base(repo, _mapper)
        {
        }
    }
}
