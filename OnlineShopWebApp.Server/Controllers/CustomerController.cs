using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.CustomActionFilter;
using OnlineShop.API.Data;
using OnlineShop.API.Models.Domain;
using OnlineShop.API.Models.DTO;
using OnlineShop.API.Repositories;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Data.WarehouseDButils _db;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(WarehouseDButils db, ICustomerRepository customerRepository, IMapper mapper)
        {
            this._db = db;
            this._customerRepository = customerRepository;
            this._mapper = mapper;
        }


        //POST create a new customer
        //POST : localhost:7106/api/customer
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddCustomerDTO addCustomerDTO)
        {
            var customerModel = _mapper.Map<Customer>(addCustomerDTO);
            customerModel = await _customerRepository.CreateAsync(customerModel);

            var customerDTO = _mapper.Map<CustomerDTO>(customerModel);
            return CreatedAtAction(nameof(GetById), new { id = customerDTO.Id }, customerDTO);
        }


        //GET all customers
        //GET : localhost:7106/api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customerModel = await _customerRepository.GetAllAsync();
            var customerDTO = _mapper.Map<List<CustomerDTO>>(customerModel);
            return Ok(customerDTO);
        }

        //GET customer by id
        //GET : localhost:7106/api/customer/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customerModel = await _customerRepository.GetByIdAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }
            var customerDTO = _mapper.Map<CustomerDTO>(customerModel);
            return Ok(customerDTO);
        }


        //PUT update a customer
        //PUT : localhost:7106/api/customer/{id}
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddCustomerDTO addCustomerDTO)
        { 
         var customerModel = _mapper.Map<Customer>(addCustomerDTO);
         customerModel = await _customerRepository.UpdateAsync(id, customerModel);
        if(customerModel == null)
            {
                return NotFound();
            }
            var customerDTO = _mapper.Map<CustomerDTO>(customerModel);
            return Ok(customerDTO);
        }


        //DELETE a customer
        //DELETE : localhost:7106/api/customer/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var customerModel = await _customerRepository.DeleteAsync(id);

            if(customerModel == null)
            {
                return NotFound();
            }

            var customerDTO = _mapper.Map<CustomerDTO>(customerModel);
            return Ok(customerDTO);
        }
    }
}
