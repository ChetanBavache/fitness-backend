using Fitness.Application.DTOs;

namespace Fitness.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> RegisterAsync(RegisterUserRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
