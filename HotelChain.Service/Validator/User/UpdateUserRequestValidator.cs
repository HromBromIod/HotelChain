using FluentValidation;
using HotelChain.Service.Controllers.Users.Entities;
using DateTime = System.DateTime;

namespace HotelChain.Service.Validator.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        RuleFor(x => x.Login)
            .Matches(@"[\w_]+")
            .WithMessage("Login is required");
        // RuleFor(x => x.PasswordHash)
        //     .NotEmpty()
        //     .WithMessage("Password is required");
        RuleFor(x => x.PassportSeries)
            .GreaterThan(999)
            .LessThan(10000)
            .WithMessage("Passport series is required");
        RuleFor(x => x.PassportNumber)
            .GreaterThan(99999)
            .LessThan(1000000)
            .WithMessage("Passport number is required");
        RuleFor(x => x.PhoneNumber)
            .Matches("[+]7[0-9]{10}")
            .WithMessage("Phone number is required");
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is required");
        RuleFor(x => x.FullName)
            .MinimumLength(0)
            .MaximumLength(600)
            .WithMessage("Name is required");
        RuleFor(x => x.BirthDate)
            .Must(y => y == null || y < DateTime.UtcNow.AddYears(-18))
            .WithMessage("Birth date is required");
    }
}