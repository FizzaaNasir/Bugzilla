using AutoMapper;
using Bugzilla.Shared;
using Bugzilla.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace Bugzilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor) { 
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
       
        [Route("{role}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByRole(string role)
        {
            var usersInRoleList = _mapper.Map<IEnumerable<UserDTO>>(await _userManager.GetUsersInRoleAsync(role));
           
            if (usersInRoleList == null)
            {
                return Enumerable.Empty<UserDTO>().ToList();
            }
            return Ok(usersInRoleList);
        }

        [HttpGet]
        [Route("{Id}/User")]
        public async Task<string> GetUsernameById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                return user.UserName;
            }
            return null; // Or handle not found scenarios
        }
    
        [HttpGet]
        [Route("{token}/claim")]
        public async Task<Dictionary<string, string>> GetClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claims = jwt.Claims.ToDictionary(c => c.Type, c => c.Value);
            return claims;
        }
        [HttpGet]
        [Route("{uname}/Userexists")]
        public async Task<ActionResult<bool>> IsEmailAvailable(string uname)
        {
            
            var IsUserExist = await _userManager.FindByNameAsync(uname);
            if (IsUserExist!=null)
            {
              return Ok(true);

            }
            return Ok(false);
        }
    }
}
