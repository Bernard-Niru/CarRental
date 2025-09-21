using CarRental.Enums.UserEnums;
using System.ComponentModel.DataAnnotations;

namespace CarRental.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }

        public byte[]? ProfileImage { get; set; }
    }
}
