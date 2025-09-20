using CarRental.Models;

namespace CarRental.Repositories.Interfaces


{
    public interface IUserRespository
    {
        User GetUserById(int id);
        Task<bool> CheckUserNameAsync(string userName);
        Task<bool> CheckEmailAsync(string Email);
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        User GetByID(int id);
        void UpdateByID(User user);
        void DeletebyID(int userId);
        User? CheckPassword(string userName);
        string UpdateUser(User user);
    }
}
