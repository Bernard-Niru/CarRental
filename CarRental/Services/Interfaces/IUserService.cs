using CarRental.DTOs;
using CarRental.ViewModels;

namespace CarRental.Services.Interfaces
{
    public interface IUserService
    {

        Task<bool> CheckAsync(string UserName);
        Task<bool> CheckEmailAsync(string Email);
        Task AddAsync(UserViewModel model);
        Task<IEnumerable<UserDTO>> GetallAsync();
        UserDTO GetbyId(int id);
        void Edit(UserDTO userDTO);
        void Delete(int id);
        string CheckPassword(UserViewModel login);

    }
}
