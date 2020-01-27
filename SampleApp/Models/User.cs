using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SampleApp.Model
{
    [Bind("Name")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [BindNever]
        public bool HasRight { get; set; }
    }
}