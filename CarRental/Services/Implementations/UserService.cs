
﻿using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.repo.Implementations;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _repo;

        public UserService(IUserRespository repo)
        {
            _repo = repo;
        }
        public async Task<bool> CheckAsync(string UserName)
        {
            return await _repo.CheckUserNameAsync(UserName);
        }
        public async Task<bool> CheckEmailAsync(string Email)
        {
            return await _repo.CheckEmailAsync(Email);
        }
        public async Task AddAsync(UserViewModel model)
        {
            string hashedPassword = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(model.Password.Trim()))
            );
            var User = UserMapper.ToModel(model, hashedPassword);
            await _repo.AddAsync(User);

        }
        public async Task<IEnumerable<UserDTO>> GetallAsync()
        {
            var user = await _repo.GetAllAsync();
            var userDTO = new List<UserDTO>();
            foreach(var u in user)
            {
                var Userdto = new UserDTO
                {
                    Id = u.UserID,
                    Name = u.Name,
                    UserName = u.UserName,
                    Email = u.Email,
                    Role = u.Role,
                };
                userDTO.Add(Userdto);
            }
            return userDTO;
        }
        
        public UserDTO GetbyId(int id)
        {
            var user = UserMapper.ToDTO(_repo.GetByID(id));
            return user;

        }
        public void Edit(UserDTO userDTO)
        {
            var User = new User
            {
                UserID = userDTO.Id,
                Name = userDTO.Name,
                UserName = userDTO.UserName,
                Password = "No need",
                Email = userDTO.Email,
                Role= userDTO.Role,
            };
            _repo.UpdateByID(User);
        }
        public void Delete(int id)
        {
            _repo.DeletebyID(id);
        }
        public string CheckPassword(LoginViewModels login)
        {
            var User = _repo.CheckPassword(login.Usernamelogin.Trim());
            if (User == null)
            {
                return "Account Blocked";
            }
            string hashedPassword = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(login.Passwordlogin.Trim()))
            );
            if (User.Password != hashedPassword)
            {
                return "Incorrect Password";
            }
            var role = User.Role.ToString() + "," + User.UserID.ToString();
            return role;
        }



        private string HashPassword(string password)
        {
            return Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(password.Trim()))
            );
        }

        public string ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _repo.GetUserById(userId);
            if (user == null)
            {
                return "User not found";
            }

            // Hash the old password input
            string hashedOldPassword = HashPassword(oldPassword);

            // Compare with DB
            if (user.Password != hashedOldPassword)
            {
                return "Old password is incorrect";
            }

            // Hash and update new password
            user.Password = HashPassword(newPassword);
            _repo.UpdateUser(user);

            return "Password updated successfully";
        }

        public UserDTO GetUserById(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null) return null;

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
        public string UpdatePassword(ProfileViewModel vm,int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null) return "User not found";

            if (!string.IsNullOrWhiteSpace(vm.NewPassword))
                {
                    string hashedOldPassword = HashPassword(vm.OldPassword);
                    if (user.Password != hashedOldPassword)
                        return "Old password is incorrect";

                    if (vm.NewPassword != vm.ConfirmPassword)
                        return "New password and confirm password do not match.";

                    user.Password = HashPassword(vm.NewPassword);
                }

            return _repo.UpdateUser(user);
           
        }
        public string ViewUser(ProfileViewModel VM)
        {
            var user = _repo.GetUserById(VM.Id);
            if (user == null) return "User not found";

            // update profile fields
            user.Name = VM.Name;
            user.UserName = VM.UserName;
            user.Email = VM.Email;

            return ViewUser (VM);

        }

        public async Task<int> GetNewUsersThisMonthAsync()
        {
            var users = await _repo.GetAllAsync();
            var now = DateTime.Now;
            return users.Count(u => u.CreatedDate.Month == now.Month && u.CreatedDate.Year == now.Year);
        }

        public async Task<int> GetActiveCustomersAsync()
        {
            // Active = users who have at least one booking
            // Assuming you have Bookings navigation in User
            var users = await _repo.GetAllAsync();
            return users.Count(u => u.Bookings != null && u.Bookings.Any());
        }

        public async Task<string> UpdateProfileImageAsync(int userId, byte[] imageBytes)
        {
            var user = _repo.GetUserById(userId);
            if (user == null) return "User not found";

            user.ProfileImage = imageBytes;
            await _repo.UpdateUserAsync(user);
            return "update successful";
        }



    }
}
//now
