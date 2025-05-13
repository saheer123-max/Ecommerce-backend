using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeekFive.DTO;
using WeekFive.Interfaces;
using WeekFive.Models;

namespace WeekFive.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<UserDto> Register(UserRegistrationDto request)
        {
            if (await _context.User.AnyAsync(u => u.Email == request.Email || u.Username == request.Username))
                throw new InvalidOperationException("Email/Username already exists");

            var user = new Users
            {
                Email = request.Email,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsActive = true,
                Role = "User"
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return UserDto.FromUser(user);
        }

        public async Task<AuthResponse> Login(UserLoginDto request)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account is inactive");

            var token = GenerateJwtToken(user);
            return new AuthResponse { User = UserDto.FromUser(user), Token = token };
        }

        public async Task<AuthResponse> AdminLogin(UserLoginDto request)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!user.IsActive || user.Role != "Admin")
                throw new UnauthorizedAccessException("Admin access denied");

            var token = GenerateJwtToken(user);
            return new AuthResponse { User = UserDto.FromUser(user), Token = token };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _context.User.ToListAsync();
            return users.Select(UserDto.FromUser);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return UserDto.FromUser(user);
        }

        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
