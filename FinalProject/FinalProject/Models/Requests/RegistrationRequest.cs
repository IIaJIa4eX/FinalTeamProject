using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models.Requests
{
    public class RegistrationRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Nickname должен быть заполнен!")]
        [Display(Name = "Никнейм")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Максимальная длина строки 20 символов, минимальная длина строки 3 символа.")]
        public string? Nickname { get; set; }


        [Required(ErrorMessage = "Почта должна быть указана!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Почта имеет неправильный формат")]
        [Display(Name = "Почта")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Пароль должен быть указан!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Максимальная длина пароля 20 символов, минимальная длина пароля 6 символов.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Пароль для подтверждения должен быть указан")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Пароль для подтверждения")]
        public string? ConfirmPassword { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nickname == "aaaaaa")
            {
                yield return new ValidationResult("Плохой ник", new[] { nameof(Nickname) });
            }
            yield return ValidationResult.Success!;
        }
    }
}
