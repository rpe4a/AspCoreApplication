using System;
using System.Collections.Generic;
using System.Text;
using SampleApp.Models;

namespace EntitiesLib.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AuthUser> Users { get; set; }
        public Role()
        {
            Users = new List<AuthUser>();
        }
    }
}
