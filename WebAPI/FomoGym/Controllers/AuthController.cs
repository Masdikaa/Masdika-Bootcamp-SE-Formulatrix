using System.Security.Claims;
using FomoGym.DTOs.Staff;
using FomoGym.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FomoGym.Controllers;

[Route("fomogym/auth")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService, SignInManager<IdentityUser> signInManager) {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
        try {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var appUser = new IdentityUser {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded) {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // Default Role
                if (roleResult.Succeeded) {
                    return Ok(new NewUserDto {
                        Username = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });
                } else {
                    return StatusCode(500, roleResult.Errors);
                }
            } else {
                return StatusCode(500, createdUser.Errors);
            }
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(loginDto.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password)) {
            return Unauthorized("Invalid Username or Password!");
        }

        return Ok(new NewUserDto {
            Username = user.UserName!,
            Email = user.Email!,
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout() {
        await _signInManager.SignOutAsync();
        return Ok("User logged out successfully.");
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile() {
        var username = User.FindFirstValue(ClaimTypes.GivenName);
        if (string.IsNullOrEmpty(username)) {
            return Unauthorized();
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user == null) {
            return NotFound("User not found.");
        }

        var userProfile = new {
            user.UserName,
            user.Email,
            user.PhoneNumber
        };

        return Ok(userProfile);
    }
}