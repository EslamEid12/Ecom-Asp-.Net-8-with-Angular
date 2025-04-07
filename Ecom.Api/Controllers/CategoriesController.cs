using System.Linq.Expressions;
using AutoMapper;
using Ecom.Api.Hellper;
using Ecom.Core.Dto;
using Ecom.Core.Entitiy.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult>get()
        {
            try
            {
                var category =await _work.CategoryRepository.GetAllAsync();
                if (category is null)
                    return BadRequest(new ResponsingApi(400));
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>getbyid(int id)
        {
            try
            {
                var category = await _work.CategoryRepository.GetByIDAsync(id);
                if (category is null)
                    return BadRequest(new ResponsingApi(400,$"Not Found Category Id {id}"));
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult>Add(CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _work.CategoryRepository.AddAsync(category);
                return Ok(new ResponsingApi(200, "Item has been Added"));
            }
            catch (Exception ex) 
            {
            return BadRequest(new { message = ex.Message });
            }
           
           
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(updateCategoryDto);
                await _work.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponsingApi(200, "Item has been Updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
        [HttpDelete("delete-category")]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                await _work.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponsingApi(200, "Item has been Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
