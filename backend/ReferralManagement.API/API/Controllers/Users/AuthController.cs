using FleetManagement.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FleetManagement.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        #region Fields

        private readonly IConfiguration _cfg;

        #endregion

        #region Ctor

        public AuthController(IConfiguration cfg) => _cfg = cfg;

        #endregion

        #region Private Methods

        private (string token, DateTime expiresUtc) IssueJwt(UserLogin user)
        {
            var keyBytes = Convert.FromBase64String(_cfg["Jwt:Key"]!);
            var signingKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(3);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }

        #endregion

        #region API

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.EmailOrUsername) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest("Missing credentials.");

            var userLogin = new UserLogin().FindByUsernameOrEmail(req.EmailOrUsername);

            if (userLogin is null) return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(req.Password, userLogin.PasswordHash))
                return Unauthorized();

            var (token, exp) = IssueJwt(userLogin);
            return Ok(new LoginResponse { Token = token, ExpiresAtUtc = exp,UserId = userLogin.UserId,UserName = userLogin.Username });
        }

        #endregion
    }
}
