using Application.Common.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Microsoft.Extensions.Configuration;

namespace Persistence.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
        }

        public async Task RegisterAsync(string email, string userName, string password)
        {
            if (await _userManager.FindByEmailAsync(email) != null || await _userManager.FindByNameAsync(userName) != null)
                throw new AlreadyExistsException("User with this e-mail address or username already exists!");

            var newUser = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };

            var createUser = await _userManager.CreateAsync(newUser, password);

            if (!createUser.Succeeded)
                throw new IdentityException(createUser.Errors);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? await _userManager.FindByNameAsync(email);
            if (user == null)
                throw new NotFoundException("User does not exist!");

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
                throw new InvalidCredentialsException("E-mail/password combination is wrong!");

            return GenerateToken(user);
        }

        private string GenerateToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            
            var tokenDescriptor = new JwtSecurityToken(
                _configuration["Issuer"],
                _configuration["Issuer"],
                expires: DateTime.Now.AddHours(24),
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }
    }
}
