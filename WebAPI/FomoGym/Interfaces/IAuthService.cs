using System.Security.Claims;
using FomoGym.DTOs.Staff;
using Microsoft.AspNetCore.Mvc;

namespace FomoGym.Interfaces;

public interface IAuthService
{
    Task<IActionResult> RegisterAsync(RegisterDto registerDto);
    Task<IActionResult> LoginAsync(LoginDto loginDto);
    Task<IActionResult> LogoutAsync();
    Task<IActionResult> GetUserProfileAsync(ClaimsPrincipal user);
}
