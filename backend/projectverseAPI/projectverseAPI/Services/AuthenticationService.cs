using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectverseAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private User? _user;

        public AuthenticationService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IMapper mapper,
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = config;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<string?> LoginUser(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                _user = user;
                return await CreateTokenAsync();
            }
            
            return null;
        }

        public async Task Logout()
        { }

        public async Task<bool> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            var userExists = await _userManager.FindByNameAsync(userRegisterDTO.UserName);
            if (userExists is not null)
                throw new ArgumentException("User with that username already exists.", "username");

            var emailExits = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            if (emailExits is not null)
                throw new ArgumentException("User with that email already exists.", "email");

            var user = _mapper.Map<User>(userRegisterDTO);

            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);
            
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            
            return result.Succeeded;
        }

        public async Task<User?> GetCurrentUser()
        {
            var name = _contextAccessor.HttpContext?.User.Identity?.Name;

            if (name is null)
                return null;

            var currentUser = await _userManager.FindByNameAsync(name);
            return currentUser;
        }

        private async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("id", _user.Id),
                new Claim("username", _user.UserName),
                new Claim("email", _user.Email),
                /*new Claim("name", _user.Name),
                new Claim("surname", _user.Surname),
                new Claim("country", _user.Country)*/
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                //expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
