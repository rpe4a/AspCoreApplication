using Microsoft.AspNetCore.Authorization;

namespace SampleApp.Requirement
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public AgeRequirement(int age)
        {
            Age = age;
        }

        protected internal int Age { get; set; }
    }
}