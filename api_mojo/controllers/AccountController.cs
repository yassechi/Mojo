using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using core_mojo.models;
using core_mojo.interfaces;
using System.Text;
using core_mojo.Dtos;

namespace api_mojo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration config;
        public AccountController(UserManager<User> _userManager, IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }
        [HttpPost("regiter")]
        public async Task<IActionResult> RegisterNewUser(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    FirstName = userAddDto.FirstName,
                    LasttName = userAddDto.LasttName,
                    Email = userAddDto.Email,
                    PhoneNumber = userAddDto.PhoneNumber,
                    Password = userAddDto.Password,
                    Role = userAddDto.Role,
                    TailleCm = userAddDto.TailleCm ?? 0,
                    IsActif = userAddDto.IsActif,
                    OrganisationId = userAddDto.OrganisationId,
                    UserName = userAddDto.UserName,
                };
                IdentityResult result = await userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(login.UserName);
                if (user is not null)
                {
                    if (await userManager.CheckPasswordAsync(user, login.Password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        // var roles = await userManager.GetRolesAsync(user);
                        // foreach (var item in roles)
                        // {
                        //     claims.Add(new Claim(ClaimTypes.Role, item));
                        // }
                        claims.Add(new Claim("role", user.Role.ToString()));
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]!));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: config["JWT:Issuer"],
                            audience: config["JWT:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: sc
                        );
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var stringToken = tokenHandler.WriteToken(token);

                        return Ok(new
                        {
                            token = stringToken,
                            expiration = token.ValidTo
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name is Invalid !");
                }
            }
            return BadRequest("User Name Invalid !");
        }
    }
}

