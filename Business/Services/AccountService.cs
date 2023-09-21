using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{

    public interface IAccountService
    {
        Result Login(AccountLoginModel accountLoginModel, UserModel UserResultModel);

        Result Register(AccountRegisterModel model);
    }



    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public Result Login(AccountLoginModel accountLoginModel, UserModel UserResultModel)
        {
            var user = _userService.Query().SingleOrDefault(u => u.UserName == accountLoginModel.UserName && u.Password == accountLoginModel.Password && u.IsActive);

            if(user is null)
                return new ErrorResult("Invalid user name or password");

            UserResultModel.UserName = user.UserName;
            UserResultModel.Role.Name = user.Role.Name;
            UserResultModel.Id = user.Id;

            return new SuccessResult();
        }

        public Result Register(AccountRegisterModel model)
        {
            UserModel userModel = new UserModel()
            {
                UserName = model.UserName,
                RoleId = Convert.ToInt16(Roles.User),
                IsActive = true,
                Password = model.Password,
                UserDetail = new UserDetailModel()
                {
                    Address = model.UserDetail.Address,
                    CityId = model.UserDetail.CityId,
                    CountryId = model.UserDetail.CountryId,
                    Email = model.UserDetail.Email,
                    Phone = model.UserDetail.Phone,
                    Sex = model.UserDetail.Sex,  
                }

            };

            return _userService.Add(userModel);
        }
    }
}
