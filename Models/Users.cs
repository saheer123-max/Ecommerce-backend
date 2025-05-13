    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace WeekFive.Models
    {
        public class Users
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Username { get; set; } 

            [Required]
            public string Password { get; set; }

            public bool IsActive { get; set; } = true; 

            public string Role { get; set; } = "User"; 
        }
    }