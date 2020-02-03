using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using static System.Int32;

namespace SampleApp.Requirement
{
    public class AgeHandler : AuthorizationHandler<AgeRequirement>
    {
        private ILogger<AgeHandler> _logger;

        public AgeHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AgeHandler>();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                var year = 0;
                if (TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value, out year))
                {
                    if ((DateTime.Now.Year - year) >= requirement.Age)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            _logger.LogCritical("Miss");
            return Task.CompletedTask;
        }
    }
}