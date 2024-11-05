using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelChain.Service.Controllers.Entities.UserEntities;

public class RegisterUserRequest : IValidatableObject
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }

    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public DateTime BirthDate { get; set; }

    public int PermissionId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (!Regex.IsMatch(Login, @"[\w_]+"))
            errors.Add(new ValidationResult("Login contains invalid symbols"));

        if (PassportSeries is < 1000 or > 9999)
            errors.Add(new ValidationResult("Invalid passport series"));

        if (PassportNumber is < 100000 or > 999999)
            errors.Add(new ValidationResult("Invalid passport number"));

        if (!Regex.IsMatch(PhoneNumber, "[+]7[0-9]{10}"))
            errors.Add(new ValidationResult("Invalid phone number"));

        if (!Regex.IsMatch(Email, @"\w+@\w+[.]\w+"))
            errors.Add(new ValidationResult("Invalid email"));

        if (!Regex.IsMatch(Name, @"[A-Z][a-z]+"))
            errors.Add(new ValidationResult("Name contains invalid symbols"));

        if (!Regex.IsMatch(Surname, @"[A-Z][a-z]+"))
            errors.Add(new ValidationResult("ValidationResult contains invalid symbols"));

        if (Patronymic != null && !Regex.IsMatch(Patronymic, @"[A-Z][a-z]+"))
            errors.Add(new ValidationResult("Patronymic contains invalid symbols"));

        if (BirthDate > DateTime.UtcNow.AddYears(-18))
            errors.Add(new ValidationResult("Under the age of 18"));

        return errors;
    }
    
    public ValidationResult Validate2(ValidationContext validationContext)
    {
        var errorMessages = new StringBuilder();
        var errorFiledNames = new List<string>();

        if (!Regex.IsMatch(Login, @"[\w_]+"))
        {
            errorFiledNames.Add("Login");
            errorMessages.AppendLine("Login contains invalid symbols");
        }

        if (PassportSeries is < 1000 or > 9999)
        {
            errorFiledNames.Add("PassportSeries");
            errorMessages.AppendLine("Invalid passport series");
        }
        
        if (PassportNumber is < 100000 or > 999999)
        {
            errorFiledNames.Add("PassportNumber");
            errorMessages.AppendLine("Invalid passport number");
        }

        if (!Regex.IsMatch(PhoneNumber, "[+]7[0-9]{10}"))
        {
            errorFiledNames.Add("PhoneNumber");
            errorMessages.AppendLine("Invalid phone number");
        }

        if (!Regex.IsMatch(Email, @"\w+@\w+[.]\w+"))
        {
            errorFiledNames.Add("Email");
            errorMessages.AppendLine("Invalid email");
        }
        
        if (!Regex.IsMatch(Name, @"[A-Z][a-z]+"))
        {
            errorFiledNames.Add("Name");
            errorMessages.AppendLine("Name contains invalid symbols");
        }
        
        if (!Regex.IsMatch(Surname, @"[A-Z][a-z]+"))
        {
            errorFiledNames.Add("Surname");
            errorMessages.AppendLine("ValidationResult contains invalid symbols");
        }
        
        if (Patronymic != null && !Regex.IsMatch(Patronymic, @"[A-Z][a-z]+"))
        {
            errorFiledNames.Add("Patronymic");
            errorMessages.AppendLine("Patronymic contains invalid symbols");
        }
        
        if (BirthDate > DateTime.UtcNow.AddYears(-18))
        {
            errorFiledNames.Add("BirthDate");
            errorMessages.AppendLine("Under the age of 18");
        }
        
        
        var errors = new ValidationResult(errorMessages.ToString(),errorFiledNames);
        return errors;
    }
}