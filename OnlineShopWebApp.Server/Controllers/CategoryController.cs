using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.API.CustomActionFilter;
using OnlineShop.API.Data;
using OnlineShop.API.Models.Domain;
using OnlineShop.API.Models.DTO;
using OnlineShop.API.Repositories;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Data.WarehouseDButils _db;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryController(WarehouseDButils db, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._db = db;
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

        //POST create a new category
        //POST : localhost:7106/api/category
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddCategoryDTO addCategoryDto)
        {
            var categoryModel = _mapper.Map<Category>(addCategoryDto);

            categoryModel = await _categoryRepository.CreateAsync(categoryModel);

            var categoryDTO = _mapper.Map<CategoryDTO>(categoryModel);

            return CreatedAtAction(nameof(GetById), new { id = categoryDTO.Id }, categoryDTO);
        }



        //GET  all categories
        //GET : localhost:7106/api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoriesModel = await _categoryRepository.GetAllAsync();

           var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categoriesModel);

            return Ok(categoriesDTO);
        }


        //GET category by id
        //GET : localhost:7106/api/category/{id}    
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoryModel = await _categoryRepository.GetByIdAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            var categoryDTO = _mapper.Map<CategoryDTO>(categoryModel);

            return Ok(categoryDTO);
        }



        //PUT update a category
        //PUT : localhost:7106/api/category/{id}
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddCategoryDTO addCategoryDTO)
        {
            var categoryModel = _mapper.Map<Category>(addCategoryDTO);

            categoryModel = await _categoryRepository.UpdateAsync(id, categoryModel);
            if (categoryModel == null)
            {
                return NotFound();
            }
           
            var categoryDTO = _mapper.Map<CategoryDTO>(categoryModel);

            return Ok(categoryDTO);
        }


        //DELETE a category
        //DELETE : localhost:7106/api/category/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var categoryModel =await _categoryRepository.DeleteAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            var categoryDTO = _mapper.Map<CategoryDTO>(categoryModel);

            return Ok(categoryDTO);
        }
    }
}