using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Models.DTO;
using OnlineShop.API.Repositories;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
       
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO) {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                //add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User created successfully");
                    }
                }
            }
            return BadRequest(identityResult.Errors);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if(user!= null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (checkPassword)
                {
                   var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                       var  jwtToken= _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);

                    }
                }
            }
            return BadRequest("Invalid login attempt");
        }
    }
}
