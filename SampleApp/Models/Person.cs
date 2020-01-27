using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SampleApp.ValidationAttributes;

namespace SampleApp.Models
{
    [NamePasswordEqual]
    public class Person
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [PersonName(new[] { "Tom", "Sam", "Alice" }, ErrorMessage = "Недопустимое имя")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [Remote(action: "CheckEmail", controller: "Home", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
    }
}