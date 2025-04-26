using AutoMapper;
using ECommerceApi.Models.Dtos.Products;
using ECommerceApi.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductReadDto>>> GetProducts()
        {
            Console.WriteLine("--> Getting all products...");
            var products = await _service.GetAllProducts();
            if (products == null)
                return NotFound("No products found");
            return Ok(_mapper.Map<List<ProductReadDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            Console.WriteLine("--> Getting a single product...");
            var product = await _service.GetProductById(id);
            if (product == null)
                return NotFound($"No product found with id {id}");
            return Ok(_mapper.Map<ProductReadDto>(product));
        }
    }
}
