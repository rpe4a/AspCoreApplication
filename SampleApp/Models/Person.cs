using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleApp.ValidationAttributes;

namespace SampleApp.Models
{
    [NamePasswordEqual]
    public class Person : IValidatableObject
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
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