using Fitness.Application.DTOs;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var result = await _userService.RegisterAsync(request);
        return Ok(result);
    }

    // ✅ LOGIN ENDPOINT
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _userService.LoginAsync(request);
        return Ok(result);
    }
}
