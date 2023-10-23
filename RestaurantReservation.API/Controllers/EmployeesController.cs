using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.API.Models;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/employees")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmployeeRepository _employeeRepository;
        private readonly IValidator<EmployeeDTO> _employeeValidator;  

        public EmployeesController(IMapper mapper, EmployeeRepository employeeRepository, IValidator<EmployeeDTO> employeeValidator)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator; 
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEployeesAsync()
        {

            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);
                return Ok(employeeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeAsync(int id)
        {
            var employeeToReturn = await _employeeRepository.GetByIdAsync(id);
            var employeeToReturnDto = _mapper.Map<EmployeeDTO>(employeeToReturn);
            if (employeeToReturn == null)
                return NotFound();
            return Ok(employeeToReturnDto);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var validationResult = _employeeValidator.Validate(employeeDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);

                await _employeeRepository.CreateAsync(employee);

                var createdEmployeeDTO = _mapper.Map<EmployeeDTO>(employee);

                return Ok(createdEmployeeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Employee not found")
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
        public async Task<ActionResult> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);
                employee.EmployeeId = id; 

                await _employeeRepository.UpdateAsync(employee);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("managers")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllManagers()
        {
            try
            {
                var managers = await _employeeRepository.ListManagersAsync();
                var managerDTOs = _mapper.Map<List<EmployeeDTO>>(managers);
                return Ok(managerDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{employeeId}/average-order-amount")]
        public async Task<ActionResult<decimal>> GetAverageOrderAmount(int employeeId)
        {
            try
            {
                decimal averageOrderAmount = await _employeeRepository.CalculateAverageOrderAmountAsync(employeeId);

                return Ok(averageOrderAmount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
