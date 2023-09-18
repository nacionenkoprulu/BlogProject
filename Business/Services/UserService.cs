#nullable disable

using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{

	public interface IUserService : IService<UserModel>
    {
        List<UserModel> GetList(); //Bu class'a özel metod

    }



    public class UserService : IUserService
    {

        private readonly RepoBase<User> _userRepo;

        public UserService(RepoBase<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public Result Add(UserModel model)
        {
            List<User> users = _userRepo.Query().ToList();

            if (users.Exists(u => u.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase)))
                return new ErrorResult("User with the same name exists!");

            User entity = new User()
            {
                UserName = model.UserName,
                IsActive = model.IsActive,
                Password = model.Password,
                RoleId = model.RoleId,
            };

            _userRepo.Add(entity);

            return new SuccessResult("User login is successfully");
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }

        public List<UserModel> GetList()
        {
            return Query().ToList();
        }

        public IQueryable<UserModel> Query()
        {
            return _userRepo.Query().OrderByDescending(u=>u.IsActive).ThenBy(u=>u.UserName).Select(u => new UserModel()
            {
                Guid = u.Guid,
                Id = u.Id,
                IsActive = u.IsActive,
                Password = u.Password,
                RoleId = u.RoleId,
                UserName = u.UserName,
                Role = new RoleModel()
                {
                    Name = u.Role.Name,
                },
                UserDetail = new UserDetailModel()
                {
                    Address = u.UserDetail.Address,
                    Email = u.UserDetail.Email,
                    CityId = u.UserDetail.CityId,
                    CountryId = u.UserDetail.CountryId,
                    Phone = u.UserDetail.Phone,
                    Sex = u.UserDetail.Sex,
                    City = new CityModel()
                    {
                        Name = u.UserDetail.City.Name,
                        
                    },
                    Country = new CountryModel()
                    {
                        Name = u.UserDetail.Country.Name
                    }
                }

            });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
