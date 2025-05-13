using WeekFive.DTO;

namespace WeekFive.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Register(UserRegistrationDto request);
        Task<AuthResponse> Login(UserLoginDto request);
        Task<UserDto> GetUserById(int id);
        Task<IEnumerable<UserDto>> GetAllUsers();
    }

    public class AuthResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
