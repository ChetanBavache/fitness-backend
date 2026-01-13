namespace Fitness.Application.DTOs;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
