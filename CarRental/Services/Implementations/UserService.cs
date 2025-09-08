
﻿using CarRental.DTOs;
using CarRental.Mappings;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.ViewModels;

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
        public async Task AddAsync(UserViewModel model)
        {
            var User = UserMapper.ToModel(model);
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
    }
}
//now
