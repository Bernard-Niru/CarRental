using CarRental.Models;

namespace CarRental.Repositories.Interfaces


{
    public interface IUserRespository
    {
        Task<bool> CheckUserNameAsync(string userName);
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        User GetByID(int id);
        void UpdateByID(User user);
        void DeletebyID(int userId);
        User? CheckPassword(string userName);
    }
}
