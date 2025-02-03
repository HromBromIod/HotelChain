using FluentValidation;
using HotelChain.Service.Controllers.Users.Entities;

namespace HotelChain.Service.Validator.User;

public class UpdateUsersPermissionsRequestValidator : AbstractValidator<UpdateUsersPermissionsRequest>
{
    public UpdateUsersPermissionsRequestValidator()
    {
        RuleFor(x => x.Permissions)
            .NotEmpty()
            .Must(x => x.Count > 0 && x.Distinct().Count() == x.Count)
            .WithMessage("Permissions is required");
    }
}