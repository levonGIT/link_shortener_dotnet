using api.Data;
using api.Dtos.User;
using api.Handlers;
using api.Models;
using api.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    public class JwtService
    {
        private readonly UserRepository _userRepo;
        private readonly IConfiguration _configuration;
        public JwtService(UserRepository userRepo, IConfiguration configuration) 
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> Authenticate(LoginUserDto loginUserDto)
        {
            var userModel = await _userRepo.GetByLoginAsync(loginUserDto.Login);
            if (userModel is null || !PasswordHashHandler.VerifyPasswordHash(loginUserDto.Password, userModel.Password))
                return null;

            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var key = _configuration["JWT:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JWT:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, userModel.Login),
                ]),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                Login = loginUserDto.Login,
                ExiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
