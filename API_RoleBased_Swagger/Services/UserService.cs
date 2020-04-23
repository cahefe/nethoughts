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

    //  Creating And Validating JWT Tokens In ASP.NET Core
    //  https://dotnetcoretutorials.com/2020/01/15/creating-and-validating-jwt-tokens-in-asp-net-core/
    public class UserService : IUserService
    {
        public const string ClaimPartyID = "PartyID";
        public const string ClaimInvestorID = "InvestorID";
        public const string ClaimInBehalfOfInvestorID = "BehalfOfInvestorID";


        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = $"{Role.Admin}, {Role.MyCustomRole}"},
            // new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = $"{Role.Admin}"},
            new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User, PartyID = 114 }
        };

        private readonly AppSettings _appSettings;
        readonly ILogger _logger;
        // private readonly IPrivilegeFactory _privilegeFactory;
        private readonly Func<PrivilegeTypeEnum, IPrivilegeService> _privilegeFactory;

        public UserService(IOptions<AppSettings> appSettings, Func<PrivilegeTypeEnum, IPrivilegeService> privilegeFactory, ILoggerFactory loggerFactory)
        {
            _appSettings = appSettings.Value;
            _privilegeFactory = privilegeFactory;
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
                new Claim(ClaimPartyID, user.PartyID.ToString()),
                new Claim(ClaimInvestorID, user.FirstName),
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

            //  Custom Factory (Privilege)
            if (user.Username == "admin")
                user.Privilege = _privilegeFactory(PrivilegeTypeEnum.Admin).ShowMyInfo();
            else
                user.Privilege = _privilegeFactory(PrivilegeTypeEnum.User).ShowMyInfo();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            _logger.LogInformation("Token {user.Token} encontrado", user.Token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll() => _users.WithoutPasswords();

        public User GetById(int id) => _users.FirstOrDefault(x => x.Id == id).WithoutPassword();

        public User GetByName(string user) => _users.FirstOrDefault(u => u.Username.Equals(user ?? ""));

        //  Obter o Claim (se possível)....
        public string GetClaim(string token, string claimType) => (new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken).Claims.FirstOrDefault(c => c.Type.Equals(claimType))?.Value;
        // {
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            // var stringClaimValue = securityToken.Claims.FirstOrDefault(c => c.Type.Equals(claimType))?.Value;
            // return stringClaimValue;
        // }
    }
}