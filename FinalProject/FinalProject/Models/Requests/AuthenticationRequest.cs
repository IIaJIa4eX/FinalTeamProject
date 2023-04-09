using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Requests
{
    public class AuthenticationRequest : IValidatableObject
    {


        [Required(ErrorMessage = "Почта должна быть указана!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Почта имеет неправильный формат")]
        [Display(Name = "Почта")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Пароль должен быть указан!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success!;
        }
    }
}
