using AutoMapper;
using Bugzilla.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bugzilla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser { UserName = input.Email, Email = input.Email };

            var result = await _userManager.CreateAsync(user, input.Password!);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new { Successful = false, Errors = errors });

            }
           
            if (!await _roleManager.RoleExistsAsync(SD.ManagerRole))
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ManagerRole)).GetAwaiter().GetResult();
            }
            if (!await _roleManager.RoleExistsAsync(SD.DevRole))
            {
                _roleManager.CreateAsync(new IdentityRole(SD.DevRole)).GetAwaiter().GetResult();
            }
            if (!await _roleManager.RoleExistsAsync(SD.QARole))
            {
                _roleManager.CreateAsync(new IdentityRole(SD.QARole)).GetAwaiter().GetResult();
            }
            if (result.Succeeded)
            {
                //Assigning roles
                String role = input.Role;
                if (role == SD.ManagerRole)
                {
                    await _userManager.AddToRoleAsync(user, SD.ManagerRole);
                }
                else if (role == SD.DevRole)
                {
                    await _userManager.AddToRoleAsync(user, SD.DevRole);
                }
                else if (role == SD.QARole)
                {
                    await _userManager.AddToRoleAsync(user, SD.QARole);
                }
            }

                if (!result.Succeeded)
                {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest("");
                }
            return Ok();
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email!, login.Password!, false, false);
            Claim[] claims;
            if (!result.Succeeded) return BadRequest("Email or password are Incorrect.");
            else
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(login.Email!);
                var userRole = await _signInManager.UserManager.GetRolesAsync(user!);
                claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id),                   
                        new Claim("Email",  user.Email),
                        new Claim (ClaimTypes.Role, userRole.Single()),
                        new Claim("Role", userRole.Single())
               };

            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                 _configuration["Jwt:Issuer"],
                 _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"])),
                signingCredentials: creds
            );

            return Ok( new JwtSecurityTokenHandler().WriteToken(token));
        }

     
    }

}
