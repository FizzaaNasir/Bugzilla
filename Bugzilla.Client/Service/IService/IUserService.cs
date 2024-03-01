using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;

namespace Bugzilla.Client.Service.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersByRole(string role);
        Task<string> GetUserById(string Id);
        Task<Dictionary<string, string>> GetClaimsFromToken(string tokenStr);
        Task<bool> IsUserExist(string uname);
    }
}
