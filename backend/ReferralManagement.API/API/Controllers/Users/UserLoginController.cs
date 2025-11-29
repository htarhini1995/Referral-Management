using FleetManagement.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserLoginController : ControllerBase
    {
        #region Private Methods
        private static UserLogin CreateUserLogin(UserLoginRequest body)
        {
            var user = new User
            {
                FirstName = body.FirstName,
                LastName = body.LastName,
                PhoneNumber = body.PhoneNumber,
                MobilePhoneNumber = body.MobilePhoneNumber,
                CompanyName = body.CompanyName
            };

            var createdUser = user.Insert();

            var hash = BCrypt.Net.BCrypt.HashPassword(body.Password);

            var userLogin = new UserLogin
            {
                Username = body.Username,
                Email = body.Email,
                PasswordHash = hash,
                UserId = createdUser.Id
            };

            var created = userLogin.Insert();
            return created;
        }

        #endregion

        #region API

        [HttpPost("get-by-id")]
        [Authorize]
        public ActionResult<UserLogin> GetById([FromBody] IdRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new UserLogin().GetById(req.Id);
            if (entity is null) return NotFound();

            return Ok(entity);
        }

        [HttpPost("create")]
        public ActionResult<UserLogin> Create([FromBody] UserLoginRequest body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userByUsername = new UserLogin().GetByUserName(body.Username);

            if (userByUsername is not null)
            {
                return BadRequest("Username is already taken.");
            }

            userByUsername = new UserLogin().GetByUserEmail(body.Email);

            if (userByUsername is not null)
            {
                return BadRequest("Email is already taken.");
            }

            UserLogin created = CreateUserLogin(body);
            return Ok(created);
        }

        [HttpPost("update")]
        [Authorize]
        public ActionResult<UserLogin> Update([FromBody] User body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            body.Save();
            return Ok();
        }

        [HttpPost("delete")]
        [Authorize]
        public IActionResult Delete([FromBody] IdRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ok = new UserLogin().DeleteById(req.Id);
            if (!ok) return NotFound();

            return NoContent();
        }

        #endregion
    }
}
