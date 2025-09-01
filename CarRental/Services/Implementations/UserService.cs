using CarRental.DTOs;
using CarRental.Mappings;
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
        public bool check(string UserName)
        {
            return _repo.CheckUserName(UserName);
        }
        public void Add(UserViewModel model)
        {
            var User = UserMapper.ToModel(model);
            _repo.Add(User);

        }
        public IEnumerable<UserDTO> Getall()
        {
            var user = _repo.GetAll();
            var userDTO = new List<UserDTO>();
            foreach(var u in user)
            {
                var Userdto = new UserDTO
                {
                    Id = u.UserID,
                    Name = u.UserName,
                    UserName = u.UserName,
                    Email = u.Email,
                    Role = u.Role,
                   

                };
                userDTO.Add(Userdto);
            }
            return userDTO;
        }
    }
}
