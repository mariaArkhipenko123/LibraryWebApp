using Identity.Application.Interfaces;
using Identity.Application.Services;
using Identity.Web.Models;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<LoginResult> LoginAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return new LoginResult { Succeeded = false, ErrorMessage = "Введите корректные данные" };
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return new LoginResult { Succeeded = false, ErrorMessage = "Пользователь не найден" };
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        return new LoginResult { Succeeded = result.Succeeded, ErrorMessage = result.Succeeded ? null : "Ошибка входа" };
    }

    public async Task<RegistrationResult> RegisterAsync(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return new RegistrationResult { Succeeded = false, ErrorMessage = "Введите корректные данные" };
        }

        var user = new AppUser { UserName = username };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return new RegistrationResult { Succeeded = true };
        }
        else
        {
            return new RegistrationResult { Succeeded = false, ErrorMessage = "Ошибка регистрации" };
        }
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}

