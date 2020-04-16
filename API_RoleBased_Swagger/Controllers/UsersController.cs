using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_RoleBased_Swagger.Services;
using API_RoleBased_Swagger.Models;
using API_RoleBased_Swagger.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using AutoMapper;

namespace API_RoleBased_Swagger.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        readonly IUserService _userService;
        readonly IMapper _mapper;
        readonly ILogger _logger;

        public UsersController(IUserService userService, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<UsersController>();
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
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            _logger.LogInformation("Usuário {user.Username} autenticado com sucesso", user.Username);
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin + ", " + Role.MyCustomRole)]
        // [Authorize(Roles = Role.MyCustomRole)]
        [AppProfiles(EnumAppProfiles.Users | EnumAppProfiles.Forecast)]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();
            _logger.LogInformation("Total de usuários encontrado na lista: {usercount}", users.Count());
            return Ok(users);
        }

        [HttpGet("{id}")]
        [AppProfiles(EnumAppProfiles.Users)]
        [Authorize(Roles = Role.User)]
        public ActionResult<User> GetById(int id)
        {
            // only allow admins to access other user records
            // var currentUserId = int.Parse(User.Identity.Name);
            // if (id != currentUserId && !User.IsInRole(Role.Admin))
            //     return Forbid();
            _logger.LogInformation("Usuário {login} fez a pesquisa pelo ID {id}", User.Identity.Name, id);

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [AllowAnonymous]
        [AppProfiles(EnumAppProfiles.Public)]
        [HttpGet("GetByName/{userName}")]
        public ActionResult<User> GetByName(string userName)
        {
            var user = _userService.GetByName(userName);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        /// <summary>
        /// Retorna um usuário do tipo APIUser
        /// </summary>
        /// <param name="userName">Nome do usuário a ser pesquisado</param>
        /// <returns>Informações do usuário no formado API</returns>
        /// <response code="200">Returns users details</response>
        /// <response code="203">Se não houver autorização para acesso à informação</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /users/getapiuser/{userName}
        ///
        /// </remarks>
        [AllowAnonymous]
        [AppProfiles(EnumAppProfiles.Public)]
        [HttpGet("getapiuser/{userName}")]
        public ActionResult<APIUser> GetAPIUser(string userName)
        {
            var user = _userService.GetByName(userName);

            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<APIUser>(user));
        }
    }
}