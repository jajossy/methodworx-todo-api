using Microsoft.AspNetCore.Mvc;
using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;

namespace TodoApplication.IServices
{
    public interface IUserService
    {
        Task<JwtResponse> AuthorizeUser(UserInput userInfo);
        Task<User> GetUser(string username);
        Task<User> CreateUser(User user);
    }
}
