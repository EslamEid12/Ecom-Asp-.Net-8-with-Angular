using AutoMapper;
using Ecom.Api.Hellper;
using Ecom.Core.Dto;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var product = await _work.ProductRepository.GetAllAsync(x => x.Category, x => x.photos);
                var Result=_mapper.Map<List<ProductDto>>(product);
                if (product is null)
                {
                    return BadRequest(new ResponsingApi(400));
                }
                return Ok(Result);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(new ResponsingApi(400));
                }
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult>getbyId(int id) 
        {

            try
            {
                var product= await _work.ProductRepository.GetByIDAsync(id, x => x.Category, x => x.photos);
                var Result=_mapper.Map<ProductDto>(product);
                if(product is null) return BadRequest(new ResponsingApi(400));
                return Ok(Result);
            }
            catch(Exception ex)
            {

                return BadRequest(new ResponsingApi(400,ex.Message));
            }
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> Add(AddProductDto productDto)
        {
            try
            {
                await _work.ProductRepository.AddAsync(productDto);
                return Ok(new ResponsingApi(200));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponsingApi(400, ex.Message));
            }
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            try
            {
                await _work.ProductRepository.UpdateAsync(updateProductDto);
                return Ok(new ResponsingApi(200));

            }
            catch (Exception ex)
            {

                return BadRequest(new ResponsingApi(400, ex.Message));
            }
        }
        [HttpDelete("Delete-Product/{Id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try
            {
            var product=await _work.ProductRepository.GetByIDAsync(Id,x=>x.photos,x=>x.Category);
                await _work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponsingApi(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponsingApi(400, ex.Message));
            }
        }
    }
}
