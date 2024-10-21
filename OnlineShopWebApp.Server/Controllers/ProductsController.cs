using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.CustomActionFilter;
using OnlineShop.API.Data;
using OnlineShop.API.Models.Domain;
using OnlineShop.API.Models.DTO;
using OnlineShop.API.Repositories;
using System.Net;
using System.Text.Json;

namespace OnlineShop.API.Controllers
{
    //https://localhost:7106/api/product
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly Data.WarehouseDButils _db;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;


        public ProductsController(WarehouseDButils db, IProductRepository productRepository, IMapper _mapper, ILogger<ProductsController> logger)
        {
            this._db = db;
            this._productRepository = productRepository;
            this._mapper = _mapper;
            this._logger=logger;
        }


        // get all products
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy , [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000 )
        {
            
                //_logger.LogInformation("Get all products was invoked");
                var productsModel = await _productRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
                //_logger.LogInformation($"Finished getting all products{JsonSerializer.Serialize(productsModel)}");
                var productsDTO = _mapper.Map<List<ProductDTO>>(productsModel);
          //      throw new Exception("New exc");
                return Ok(productsDTO);
        }


        //get from id 
        // "/{id}"
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productModel = await _productRepository.GetByIdAsync(id);

            if(productModel == null)
            {
                return NotFound();
            }

            var productDTO = _mapper.Map<ProductDTO>(productModel);
            return Ok(productDTO);
        }


        //create product
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddProductDTO addProductDTO)
        { 
            var productsModel = _mapper.Map<Products>(addProductDTO);
            productsModel =  await _productRepository.CreateAsync(productsModel);

            var productDTO = _mapper.Map<ProductDTO>(productsModel);

            return Ok(productDTO);
        }



        [HttpPut]
        [Route("{id}")]
        [ValidateModel]     
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddProductDTO addProductDTO)
        {
            var productsModel = _mapper.Map<Products>(addProductDTO);

            productsModel = await _productRepository.UpdateAsync(id, productsModel);
            if (productsModel == null)
            {
                return NotFound();
            }

            var productDTO = _mapper.Map<ProductDTO>(productsModel);

            return Ok(productDTO);
         }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var productsModel = await _productRepository.DeleteAsync(id);

            if (productsModel == null)
            {
                return NotFound();
            }

            var productDTO = _mapper.Map<ProductDTO>(productsModel);
            return Ok(productDTO);
        }
    }
}