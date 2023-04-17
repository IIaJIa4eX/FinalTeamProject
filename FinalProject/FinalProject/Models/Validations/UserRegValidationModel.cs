using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinalProject.Models.Validations
{
    public class UserRegValidationModel
    {

        [Required(ErrorMessage = "Nickname должен быть заполнен!")]
        [Display(Name = "Никнейм")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Максимальная длина строки 100 символов, минимальная длина строки 2 символа.")]
        [RegularExpression(@"([А-ЯЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат.")]
        public string? Nickname { get; set; }


        [Required(ErrorMessage = "Почта должна быть указана!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Почта имеет неправильный формат")]
        [Display(Name = "Почта")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль должен быть указан!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Почта имеет неправильный формат")]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Пароль для подтврждения должен быть указан")]
        [Display(Name = "Пароль для подтверждения")]
        public string? ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
                yield return new ValidationResult("Пароли не совпадают!", new[] { nameof(ConfirmPassword) });

            yield return ValidationResult.Success!;
        }
    }
}
