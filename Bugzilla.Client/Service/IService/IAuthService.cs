using Bugzilla.Shared;
using System.Security.Claims;

namespace Bugzilla.Client.Service.IService
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterModel input);
        Task<string> Login(LoginModel input);
        Task<ClaimsPrincipal> GetClaimsPrincipalUser();
    }
}
