using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleApp.ValidationAttributes;

namespace SampleApp.Models
{
    [NamePasswordEqual]
    public class Person : IValidatableObject
    {
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(20, ErrorMessage = "NameLength", MinimumLength = 6)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "AgeRequired")]
        [Range(10, 100, ErrorMessage = "AgeRange")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add(new ValidationResult("Введите имя!", new List<string> {"Name"}));
            if (string.IsNullOrWhiteSpace(Email)) errors.Add(new ValidationResult("Введите электронный адрес!"));
            if (Age < 0 || Age > 120) errors.Add(new ValidationResult("Недопустимый возраст!"));

            return errors;
        }
    }
}