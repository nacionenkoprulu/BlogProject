using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IRoleService : IService<RoleModel>
    {
    }

    public class RoleService : IRoleService
    {

        private readonly RepoBase<Role> _roleRepo;

        public RoleService(RepoBase<Role> roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public Result Add(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _roleRepo.Dispose();
        }

        public IQueryable<RoleModel> Query()
        {
            return _roleRepo.Query().OrderBy(r => r.Name).Select(r => new RoleModel()
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public Result Update(RoleModel model)
        {
            throw new NotImplementedException();
        }
    }
}
