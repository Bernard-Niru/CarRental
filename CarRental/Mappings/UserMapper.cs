using CarRental.DTOs;
using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public class UserMapper
    {
        public static User ToModel(UserViewModel model,string hashedpassword)
        {
            return new User
            {
                UserID = model.Id,
                Name = model.Name,
                Email = model.EmailAddress.Trim(),
                UserName = model.UserName.Trim(),
                Password = hashedpassword,
                Role = model.Role,
                IsDeleted = false,

                
            };
        }
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.UserID,
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role,
            ProfileImage = user.ProfileImage

            };
        }
        public static User ToModel(ProfileViewModel model, string hashedpassword)
        {
            return new User
            {
                UserID = model.Id,
                Name = model.Name,
                Email = model.Email.Trim(),
                UserName = model.UserName.Trim(),
                Password = hashedpassword,
             
                IsDeleted = false,
                ProfileImage = model.ProfileImage


            };
        }

    }
}
