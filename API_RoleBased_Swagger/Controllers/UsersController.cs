using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_RoleBased_Swagger.Services;
using API_RoleBased_Swagger.Models;
using API_RoleBased_Swagger.Entities;
using Microsoft.AspNetCore.Http;

namespace API_RoleBased_Swagger.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates an user and generates JWT ĩnformation
        /// </summary>
        /// <param model="id">User and password information</param>
        /// <returns>A JWT token (if success</returns>
        /// <response code="200">Returns users details</response>
        /// <response code="203">If user informatation has not been filled</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /authenticate
        ///     {
        ///        "user": "username",
        ///        "password": "passwd"
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AppProfiles(EnumAppProfiles.Users | EnumAppProfiles.Forecast)]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet("GetByName/{userName}")]
        public IActionResult GetByName(string userName)
        {
            var user = _userService.GetByName(userName);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}