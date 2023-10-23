using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/reservations")]
    [Authorize]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ReservationDTO> _reservationValidator; 
        public ReservationController(
            IMapper mapper,
            ReservationRepository reservationRepository,
            IValidator<ReservationDTO> reservationValidator)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationValidator = reservationValidator; // Assign it to the private member
        }
        [HttpGet]
        public async Task<ActionResult<List<ReservationDTO>>> GetAllReservationsAsync()
        {
            try
            {
                var reservations = await _reservationRepository.GetAllAsync();
                var reservationDTOs = _mapper.Map<List<ReservationDTO>>(reservations);
                return Ok(reservationDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReservationAsync(int id)
        {
            var reservationToReturn = await _reservationRepository.GetByIdAsync(id);
            var reservationToReturnDTO = _mapper.Map<ReservationDTO>(reservationToReturn);
            if (reservationToReturn == null)
                return NotFound();
            return Ok(reservationToReturnDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservationAsync(ReservationDTO reservationDTO)
        {
            var validationResult = _reservationValidator.Validate(reservationDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationDTO);
                await _reservationRepository.CreateAsync(reservation);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservationAsync(int id, ReservationDTO reservationDTO)
        {
            var validationResult = _reservationValidator.Validate(reservationDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationDTO);
                reservation.ReservationId = id;

                await _reservationRepository.UpdateAsync(reservation);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservationAsync(int id)
        {
            try
            {
                await _reservationRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Reservation not found")
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpGet("custemer/{id}")]
        public async Task<ActionResult<List<Customer>>> GetReservationsByCustemerAsync(int id)
        {
            try
            {
                var reservations = await _reservationRepository.GetReservationsByCustomerAsync(id);
                var reservationDTOs = _mapper.Map<List<ReservationDTO>>(reservations);
                return Ok(reservationDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}/orders-menu-items")]
        public async Task<ActionResult> ListOrdersAndMenuItems(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetReservationWithOrdersAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                var orders = reservation.Orders;
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}/menu-items")]
        public async Task<ActionResult<List<MenuItem>>> GetOrderedMenuItems(int id)
        {
            try
            {
                var orderedMenuItems = await _reservationRepository.GetOrderedMenuItemsForReservationAsync(id);

                if (orderedMenuItems == null || !orderedMenuItems.Any())
                {
                    return NotFound(); 
                }
                return Ok(orderedMenuItems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
