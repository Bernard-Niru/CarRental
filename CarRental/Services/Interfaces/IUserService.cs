using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IUserService
    {

        bool check(string UserName);
        void Add(UserViewModel model);
        IEnumerable<UserDTO> Getall();
        UserDTO GetbyId(int id);

    }
}
