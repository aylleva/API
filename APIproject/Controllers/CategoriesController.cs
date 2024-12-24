
using APIproject.DTO.Category;
using APIproject.Repositories.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace APIproject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
       
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=2)
        {
            List<Category> categories =await _repository.GetAll(c=>c.Name.Contains("o"),c=>c.Name,true,true,(page-1)*take,take,"Products").ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id < 1) return BadRequest();

            Category category = await _repository.GetbyIdAsync(id);
            if(category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDTO categorydto)
        {
            Category category = new Category { Name=categorydto.Name };

            bool result = await _repository.Check(category);
            if (result) return BadRequest();
          
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return Created();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int? id,[FromForm]UpdateCategoryDTO categorydto)
        {
            if(id < 1) return BadRequest();
            Category existed = await _repository.GetbyIdAsync(id);
            if(existed == null) return NotFound();

            Category category = new() { Name = categorydto.Name };

            bool result=await _repository.Check(category);
            if (result) return BadRequest();

            existed.Name =categorydto.Name;
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id < 1) return BadRequest();
            Category category = await _repository.GetbyIdAsync(id);
            if (category == null) return NotFound();

           _repository.Delete(category);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
