using CarRental.Data;
using CarRental.DTOs;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;

namespace CarRental.repo.Implementations
{
    public class UserRepository : IUserRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CheckUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName.ToLower() == userName.ToLower());
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        IEnumerable<User> IUserRespository.GetAll()
        {
            var Users = _context.Users
                         .Where(b => !b.IsDeleted)
                         .Select(b => new User
                         {
                             UserID = b.UserID,
                             Name = b.Name,
                             Email = b.Email,
                             UserName = b.UserName,
                             Role = b.Role
                         })
                         .ToList();

            return Users;
        }
        public User GetByID(int id)
        {
            return _context.Users.Find(id);
        }

    }
}
