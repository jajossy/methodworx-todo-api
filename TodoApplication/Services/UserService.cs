using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApplication.IServices;
using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;
using TodoApplication.Repository;

namespace TodoApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly string Issuer;
        private readonly string Audience;
        private readonly string Key;

        public UserService(IRepository repository, IOptionsMonitor<JwtConfig> optionUrl) 
        {
            _repository = repository;
            Issuer = optionUrl.CurrentValue.Issuer;
            Audience = optionUrl.CurrentValue.Audience;
            Key = optionUrl.CurrentValue.Key;
        }

        public async Task<JwtResponse> AuthorizeUser(UserInput userInfo)
        {
            var resp = await _repository.Query<User>()
                .FirstOrDefaultAsync(x => x.Email == userInfo.Email && x.Email == userInfo.Email);
            if (resp != null)
            {
                return new JwtResponse
                {
                    jwt = GenerateToken(resp),
                    userId = resp.UserId,
                    username = resp.Username
                };
                    
            }
            else throw new Exception("user not authorized");
        }

        public async Task<User> GetUser(string email)
        {            
            var resp = await _repository.Query<User>().FirstOrDefaultAsync(x => x.Email == email);
            if (resp != null) return resp;
            else throw new Exception("No user found!");
        }

        public async Task<User> CreateUser(User user)
        {
            user.CreatedDate = DateTimeOffset.UtcNow;
            return await _repository.CreateAsync<User>(user);
        }

        private string GenerateToken(User user)
        {
            var issuer = Issuer;
            var audience = Audience;
            var getkey = Encoding.ASCII.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(getkey),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
