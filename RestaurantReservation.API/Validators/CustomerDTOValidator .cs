using FluentValidation;
using RestaurantReservation.API.Models;

namespace RestaurantReservation.API.Models
{
    public class CustomerDTOValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerDTOValidator()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(dto => dto.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(dto => dto.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{10}$");
        }
    }
}
