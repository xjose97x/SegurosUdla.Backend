using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SegurosUdla.WebApi.Data;
using SegurosUdla.WebApi.Exceptions.Auth;
using SegurosUdla.WebApi.Interfaces;

namespace SegurosUdla.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<string> Authenticate(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var signInResult = await signInManager.PasswordSignInAsync(userName, password, false, false);
            if (!signInResult.Succeeded)
            {
                throw new InvalidPasswordException();
            }
            
            var x = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")), SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task Register(string userName, string password)
        {
            var registerResult = await userManager.CreateAsync(new User(userName), password);
            if (!registerResult.Succeeded)
            {
                throw new RegisterException(registerResult.Errors);
            }
        }
    }
}

