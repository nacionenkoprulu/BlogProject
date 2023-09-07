#nullable disable

using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{

	public interface IUserService : IService<UserModel>
    {

        //Bu class'a özel metod
        List<UserModel> GetList();

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
            throw new NotImplementedException();
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
                UserName = u.UserName


            });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
