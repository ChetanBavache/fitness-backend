using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        var email = User.FindFirstValue(ClaimTypes.Email);

        return Ok(new
        {
            Message = "JWT authentication successful",
            UserId = userId,
            Email = email
        });
    }
}
