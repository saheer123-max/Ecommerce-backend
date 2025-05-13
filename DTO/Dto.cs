using System.ComponentModel.DataAnnotations;
using WeekFive.Models;

namespace WeekFive.DTO
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; } 

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; } 
        public string Role { get; set; } 
        public bool IsActive { get; set; } 

        public static UserDto FromUser(Users user) => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username, 
            Role = user.Role, 
            IsActive = user.IsActive 
        };
    }

}
