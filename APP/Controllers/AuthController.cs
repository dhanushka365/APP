using APP.JWT;
using APP.Models.Domain;
using APP.Models.DTO;
using APP.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IUserRepository userRepository, JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null || string.IsNullOrEmpty(loginRequestDTO.UserName) || string.IsNullOrEmpty(loginRequestDTO.Password))
            {
                return BadRequest("Invalid request. Username and password are required.");
            }

            var authenticatedUser = _userRepository.GetUser(loginRequestDTO.UserName, loginRequestDTO.Password);

            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey ?? throw new InvalidOperationException("JWT key is not configured."));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, authenticatedUser.UserName ?? throw new InvalidOperationException("User username is null.")),
                    new Claim(ClaimTypes.Role, authenticatedUser.Role ?? "User")  // Add this line
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return _userRepository.GetUsers();
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid request. Username ,password and Role are required.");
            }

            if (_userRepository.GetUser(user.UserName, user.Password) != null)
            {
                return BadRequest("User already exists.");
            }

            _userRepository.AddUser(user);
            return Ok("User registered successfully.");
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (user.UserId == null) // Ensure UserId is not null
            {
                return BadRequest("UserId is required.");
            }

            var existingUser = _userRepository.GetUserById(user.UserId.Value); // Use .Value to access the int value
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            _userRepository.UpdateUser(user);
            return Ok("User updated successfully.");
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            _userRepository.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
    }
}
