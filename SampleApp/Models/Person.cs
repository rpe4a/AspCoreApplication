using System.ComponentModel.DataAnnotations;

namespace SampleApp.Models
{
    public class Person
    {
        [Required] public string Name { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        public int Age { get; set; }
    }
}