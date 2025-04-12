using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("Not-Found")]
        public async Task<IActionResult> GetNotFound() 
        {
            var category = await _work.CategoryRepository.GetByIDAsync(100);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet("Server-Erorr")]
        public async Task<IActionResult> GetServerErorr()
        {
            var category = await _work.CategoryRepository.GetByIDAsync(100);
            category.Name = "";
            return Ok(category);
        }

        [HttpGet("Bad-Request/{id}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {
            return Ok();
        }

        [HttpGet("Bad-Request/")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
