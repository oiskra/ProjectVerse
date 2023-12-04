using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using projectverseAPI.Constants;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDTO?> LoginUser(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                var accessToken = await _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                await _userManager.UpdateAsync(user);
                return new TokenResponseDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            
            return null;
        }

        public async Task<TokenResponseDTO> RefreshToken(RefreshRequestDTO refreshRequestDTO)
        {
            var accessToken = refreshRequestDTO.AccessToken;
            var refreshToken = refreshRequestDTO.RefreshToken;

            var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(accessToken);
            var userId = principal.FindFirst(c => c.Type == "id")?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new InvalidOperationException("Invalid client request. Sign in again.");

            var newAccessToken = await _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task RevokeToken()
        {
            var currentUser = await GetCurrentUser();
            currentUser.RefreshToken = null;

            await _userManager.UpdateAsync(currentUser);
        }

        public async Task<Guid> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            var userExists = await _userManager.FindByNameAsync(userRegisterDTO.UserName);
            if (userExists is not null)
                throw new ArgumentException("User with that username already exists.", "email");

            var emailExits = await _userManager.FindByEmailAsync(userRegisterDTO.Email);
            if (emailExits is not null)
                throw new ArgumentException("User with that email already exists.", "username");

            var user = _mapper.Map<User>(userRegisterDTO);

            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);
            
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            
            return Guid.Parse(user.Id);
        }

        public async Task<User> GetCurrentUser()
        {
            var id = _contextAccessor.HttpContext?.User.FindFirst(ClaimNameConstants.Identifier)?.Value;

            if (id is null)
                throw new InvalidOperationException("Cannot get current user.");

            var currentUser = await _userManager.FindByIdAsync(id);
            return currentUser;
        }

    }
}
