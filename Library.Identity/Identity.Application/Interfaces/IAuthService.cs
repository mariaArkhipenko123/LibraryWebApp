using Identity.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(string username, string password);
        Task<RegistrationResult> RegisterAsync(string username, string password);
        Task LogoutAsync();
    }

}