using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.API.Models;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/customers")]
    [Authorize]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _custemerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDTO> _customerValidator;

        public CustomersController(
            CustomerRepository customerRepository,
            IMapper mapper,
            IValidator<CustomerDTO> customerValidator)
        {
            _custemerRepository = customerRepository;
            _mapper = mapper;
            _customerValidator = customerValidator;
        }
        [HttpGet]
        [Produces("application/xml")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustemersAsync()
        {
            try
            {
                var customers = await _custemerRepository.GetAllAsync();
                var customerDTOs = _mapper.Map<List<CustomerDTO>>(customers);
                return Ok(customerDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerAsync(int id)
        {
            var CustomerToReturn = await _custemerRepository.GetByIdAsync(id);
            var CustomerToReturnDTO = _mapper.Map<CustomerDTO>(CustomerToReturn);
            if (CustomerToReturn == null)
                return NotFound();
            return Ok(CustomerToReturnDTO);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync(CustomerDTO customerDTO)
        {
            var validationResult = _customerValidator.Validate(customerDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var customer = _mapper.Map<Customer>(customerDTO);
                await _custemerRepository.CreateAsync(customer);
                var createdCustomerDTO = _mapper.Map<CustomerDTO>(customer);
                return Ok(createdCustomerDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                await _custemerRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Customer not found")
                {
                    return NotFound(); 
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync(int id, CustomerDTO updatedCustomerDto)
        {
            var validationResult = _customerValidator.Validate(updatedCustomerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var updatedCustomer = _mapper.Map<Customer>(updatedCustomerDto);
                updatedCustomer.CustomerId = id;

                await _custemerRepository.UpdateAsync(updatedCustomer);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateCustomer(int id,JsonPatchDocument<CustomerDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest();
                }
                var existingCustomer = await _custemerRepository.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                var customerDTO = _mapper.Map<CustomerDTO>(existingCustomer);
                patchDocument.ApplyTo(customerDTO);
                var updatedCustomer = _mapper.Map(customerDTO, existingCustomer);
                await _custemerRepository.UpdateAsync(updatedCustomer);

                return Ok(_mapper.Map<CustomerDTO>(updatedCustomer));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            /*
             [
                { "op": "replace", "path": "/FirstName", "value": "John" },
                { "op": "replace", "path": "/LastName", "value": "Doe" },
                { "op": "add", "path": "/Email", "value": "johndoe@example.com" },
             ] 

            */
        }
    }
}
