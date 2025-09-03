using CarRental.Models;

namespace CarRental.Repositories.Interfaces


{
    public interface IUserRespository
    {
        bool CheckUserName(string userName);
        void Add(User user);
        IEnumerable<User> GetAll();
        User GetByID(int id);
    }
}
