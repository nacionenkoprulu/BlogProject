using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
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

            return new SuccessResult();
        }
    }
}
