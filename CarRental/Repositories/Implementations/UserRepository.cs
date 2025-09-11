using CarRental.Data;
using CarRental.DTOs;
using CarRental.Models;
using CarRental.repo.Interfaces;
using CarRental.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.repo.Implementations
{
    public class UserRepository : IUserRespository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckUserNameAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
        }

        public async Task AddAsync(User user)
        {
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
        }
        async Task<IEnumerable<User>> IUserRespository.GetAllAsync()
        {
            var Users = await _context.Users
                         .Where(b => !b.IsDeleted)
                         .Select(b => new User
                         {
                             UserID = b.UserID,
                             Name = b.Name,
                             Email = b.Email,
                             UserName = b.UserName,
                             Role = b.Role
                         })
                         .ToListAsync();

            return Users;
        }
        public User GetByID(int id)
        {
            return _context.Users.Find(id);
        }
        public void UpdateByID(User user)
        {
            var updateUser = _context.Users.FirstOrDefault(u => u.UserID == user.UserID);
            updateUser.Name = user.Name;
            updateUser.Email = user.Email;
            updateUser.Role = user.Role;
            _context.Users.Update(updateUser);
            _context.SaveChanges();
        }
        public void DeletebyID(int userId)
        {
            var updateUser = _context.Users.FirstOrDefault(u => u.UserID == userId);
            updateUser.IsDeleted = true;
            _context.Users.Update(updateUser);
            _context.SaveChanges();
        }

        public User? CheckPassword(string userName)
        {
            return _context.Users
                .Where(b => b.UserName == userName && !b.IsDeleted)
                .Select(b => new User
                {
                    Password = b.Password,
                    Role = b.Role,
                })
                .FirstOrDefault();
        }


    }
}
