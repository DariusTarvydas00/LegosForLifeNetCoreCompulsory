using System.Collections.Generic;
using System.IO;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InnoTech.LegosForLife.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new InvalidDataException("ProductService Cannot Be Null");
        }
        
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _productService.GetProducts();
        }

        [HttpGet("id")]
        public ActionResult<ProductByIdDTo> GetProductById(int id)
        {
            var productFromDto = _productService.GetProductById(id);
            return Ok(new ProductByIdDTo()
            {
                Name = productFromDto.Name
            });
        }

        [HttpPost]
        public ActionResult<PostProductDto> PostProduct([FromBody] PostProductDto dto)
        {
            var postProductDto = _productService.CreateProduct(dto.Name);
            return Ok(new PostProductDto()
            {
                Name = postProductDto.Name
            });
        }
    }
}