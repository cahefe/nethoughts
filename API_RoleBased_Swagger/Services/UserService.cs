using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using API_RoleBased_Swagger.Entities;
using API_RoleBased_Swagger.Helpers;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace API_RoleBased_Swagger.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByName(string user);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = $"{Role.Admin}, {Role.MyCustomRole}"},
            // new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = $"{Role.Admin}"},
            new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User, PartyID = 114 }
        };

        private readonly AppSettings _appSettings;
        readonly ILogger _logger;

        public UserService(IOptions<AppSettings> appSettings, ILoggerFactory loggerFactory)
        {
            _appSettings = appSettings.Value;
            _logger = loggerFactory.CreateLogger<UserService>();
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            _logger.LogInformation("Usuário {user.Id} encontrado", user.Id);

            //  Prepara um objeto que armazenará informações customizadas do usuário autorizado...
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Spn, user.PartyID.ToString())
            };
            //  ... além dos papéis específicos atribuídos ao usuário...
            claims.AddRange((user.Role ?? "")
                                .Split(',')
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(r => new Claim(ClaimTypes.Role, r.Trim())));

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            _logger.LogInformation("Token {user.Token} encontrado", user.Token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll() => _users.WithoutPasswords();

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }

        public User GetByName(string user) => _users.FirstOrDefault(u => u.Username.Equals(user ?? ""));
    }
}