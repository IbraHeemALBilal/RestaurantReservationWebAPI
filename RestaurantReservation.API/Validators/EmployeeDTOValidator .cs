using FluentValidation;

namespace RestaurantReservation.API.Models
{
    public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidator()
        {
            RuleFor(dto => dto.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(dto => dto.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(dto => dto.Position)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(dto => dto.RestaurantId)
                .NotEmpty()
                .GreaterThan(0);

        }
    }
}
