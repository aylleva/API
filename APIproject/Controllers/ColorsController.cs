using APIproject.DTO.Color;
using APIproject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIproject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorsController(IColorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1,int take = 2)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> Get(int Id)
        {
            if(Id<1) return BadRequest();
            var colordto=await _service.GetByIdAsync(Id);
            if(colordto == null) return NotFound();
            return Ok(colordto);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateColorDTO colordto)
        {
           await _service.CreateAsync(colordto);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id,[FromForm]UpdateColorDTO colordto)
        {
            if(Id<1) return BadRequest();
            await _service.UpdateAsync(Id, colordto);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if(Id<1) return BadRequest();   
            await _service.DeleteAsync(Id);
            return NoContent();
        }
    }
}
