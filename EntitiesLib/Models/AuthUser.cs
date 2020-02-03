using EntitiesLib.Models;

namespace SampleApp.Models
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public int Year { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}