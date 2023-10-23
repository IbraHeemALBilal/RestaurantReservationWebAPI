using FluentValidation;
using RestaurantReservation.API.Models;

namespace RestaurantReservation.API.Validators
{
    public class ReservationDTOValidator : AbstractValidator<ReservationDTO>
    {
        public ReservationDTOValidator()
        {
            RuleFor(dto => dto.CustomerId)
                .NotEmpty();

            RuleFor(dto => dto.RestaurantId)
                .NotEmpty();

            RuleFor(dto => dto.TableId)
                .NotEmpty();

            RuleFor(dto => dto.ReservationDate)
                .NotEmpty();

            RuleFor(dto => dto.PartySize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
