using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Service.Interfaces;
using Service.Utility;
using WebAPI.Models;

namespace WebAPI.Controllers
{

    [ApiController]
    [Produces("application/json")]
    public abstract class DbCrudere<TEntity, TDto, TDtoLine> : ControllerBase
        where TDto : BaseDto<TDtoLine>, new()
        where TDtoLine : BaseDtoLine, new()
        where TEntity : BaseEntity, new()
    {
        protected readonly ICrudereService _repo;
        protected readonly IMapperService mapper;

        public DbCrudere(ICrudereService repo, IMapperService _mapper)
        {
            _repo = repo;
            mapper = _mapper;
        }

        public virtual TDto DefaultValuesGet(TDto dto, bool isNew)
        {
            return dto;
        }

        public virtual TDto DefaultValuesPost(TDto dto, bool isNew)
        {
            return dto;
        }

        public virtual TDtoLine DefaultValuesLine(TDtoLine dtoLine)
        {
            return dtoLine;
        }

        public virtual TDto PopulateSelectLists(TDto dto)
        {
            return dto;
        }

        public virtual IList<TDtoLine> PopulateSelectLists(IList<TDtoLine> dtoLines)
        {
            if (dtoLines == null)
                dtoLines = new List<TDtoLine>();
            foreach (var dtoline in dtoLines)
            {
                PopulateSelectLists(dtoline);
            }
            return dtoLines;
        }

        public virtual TDtoLine PopulateSelectLists(TDtoLine dtoLine)
        {
            return dtoLine;
        }

        public virtual TEntity LogicAfterPost(TEntity entity, bool isNew)
        {
            return entity;
        }

        public virtual IEnumerable<TEntity> FilterQuery(string exp)
        {
            var results = _repo.Where<TEntity>(exp);
            return results;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var entity = await _repo.GetAllAsync<TEntity>();
            var model = mapper.MapConfig<List<TEntity>, List<TDto>>(entity);
            return Ok(model);
        }

        [HttpGet("{exp}/filtered")]
        public virtual IActionResult Filtered(string exp)
        {
            var entity = FilterQuery(exp).ToList();
            var model = mapper.MapConfig<List<TEntity>, List<TDto>>(entity);
            return Ok(model);
        }

        [Route("new")]
        [HttpGet]
        public virtual IActionResult New()
        {
            var dto = new TDto()
            {
                Id = Guid.NewGuid().ToString(),
                //CreatedOn = DateTime.Now,
                //CreatedBy = User.Identity.Name,
            };
            dto = DefaultValuesGet(dto, true);
            dto = PopulateSelectLists(dto);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Find(string id)
        {
            var entity = await _repo.GetByIdAsync<TEntity>(id);
            if (entity == null)
                return NotFound();
            var dto = mapper.MapConfig<TEntity, TDto>(entity);
            dto = PopulateSelectLists(dto);
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            dto = DefaultValuesPost(dto, true);
            dto.Id = Guid.NewGuid().ToString();
            //dto.CreatedOn = DateTime.Now;
            //dto.CreatedBy = User.Identity.Name;
            var entity = mapper.MapConfig<TDto, TEntity>(dto);

            _repo.Insert(entity);
            await _repo.SaveAsync();

            dto = mapper.MapConfig(entity, dto);
            LogicAfterPost(entity, true);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(string id, [FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //dto.ModifiedOn = DateTime.Now;
            //dto.ModifiedBy = User.Identity.Name;

            var entity = await _repo.GetByIdAsync<TEntity>(id);
            if (entity == null)
                return NotFound();

            entity = mapper.MapConfig(dto, entity);
            _repo.Update(entity);
            if (await _repo.SaveAsync() == 0)
                return BadRequest();

            dto = mapper.MapConfig(entity, dto);
            LogicAfterPost(entity, false);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var record = await _repo.GetByIdAsync<TEntity>(id);

            if (record == null)
                return NotFound();

            if (await _repo.DeleteAsync(record) == 0)
                return BadRequest();

            return NoContent();
        }

    }
}
