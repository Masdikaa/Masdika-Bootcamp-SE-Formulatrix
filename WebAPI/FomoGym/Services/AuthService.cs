using System.Security.Claims;
using FomoGym.DTOs.Staff;
using FomoGym.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FomoGym.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            if (registerDto == null)
                return new BadRequestObjectResult("Invalid registration data.");

            var appUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return new OkObjectResult(new NewUserDto
                    {
                        Username = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    return new ObjectResult(roleResult.Errors) { StatusCode = 500 };
                }
            }
            else
            {
                return new ObjectResult(createdUser.Errors) { StatusCode = 500 };
            }
        }
        catch (Exception e)
        {
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return new UnauthorizedObjectResult("Invalid Username or Password!");
        }

        return new OkObjectResult(new NewUserDto
        {
            Username = user.UserName!,
            Email = user.Email!,
            Token = _tokenService.CreateToken(user)
        });
    }

    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return new OkObjectResult("User logged out successfully.");
    }

    public async Task<IActionResult> GetUserProfileAsync(ClaimsPrincipal userPrincipal)
    {
        var username = userPrincipal.FindFirstValue(ClaimTypes.GivenName);
        if (string.IsNullOrEmpty(username))
        {
            return new UnauthorizedResult();
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return new NotFoundObjectResult("User not found.");
        }

        var userProfile = new
        {
            user.UserName,
            user.Email,
            user.PhoneNumber
        };

        return new OkObjectResult(userProfile);
    }
}
