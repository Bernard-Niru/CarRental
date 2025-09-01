using CarRental.Models;
using CarRental.ViewModels;

namespace CarRental.Mappings
{
    public class UserMapper
    {
        public static User ToModel(UserViewModel model)
        {
            return new User
            {
                UserID = model.Id,
                Name = model.Name,
                Email = model.EmailAddress,
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role,
                IsDeleted = false
            };
        }


    }
}
