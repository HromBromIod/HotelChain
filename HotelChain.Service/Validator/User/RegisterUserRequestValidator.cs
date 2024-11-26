using FluentValidation;
using HotelChain.Service.Controllers.Users.Entities;

namespace HotelChain.Service.Validator.User;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .Matches(@"[\w_]+")
            .WithMessage("UserName is required");
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.PassportSeries)
            .NotEmpty()
            .GreaterThan(999)
            .LessThan(10000)
            .WithMessage("Passport series is required");
        RuleFor(x => x.PassportNumber)
            .NotEmpty()
            .GreaterThan(99999)
            .LessThan(1000000)
            .WithMessage("Passport number is required");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches("[+]7[0-9]{10}")
            .WithMessage("Phone number is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Name is required");
        RuleFor(x => x.Surname)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Surname is required");
        RuleFor(x => x.Patronymic)
            .MinimumLength(0)
            .MaximumLength(200)
            .WithMessage("Patronymic is required");
        RuleFor(x => x.BirthDate)
            .Must(y => y == null || y < DateTime.UtcNow.AddYears(-18))
            .WithMessage("Birth date is required");
    }
}