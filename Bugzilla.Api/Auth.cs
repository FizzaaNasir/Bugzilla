namespace IdentityApi
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthRequirement : IAuthorizationRequirement { }

    public class CustomAuthorizationHandler : AuthorizationHandler<AuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Role" && c.Value == "Manager"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
