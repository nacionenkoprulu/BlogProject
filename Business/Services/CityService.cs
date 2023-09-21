
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
    public interface ICityService : IService<CityModel>
    {

        List<CityModel> GetList(int countyId);

    }

    public class CityService : ICityService
    {

        private readonly RepoBase<City> _cityRepo;

        public CityService(RepoBase<City> cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public Result Add(CityModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _cityRepo.Dispose();
        }

        public List<CityModel> GetList(int countyId)
        {
            return Query().Where(c => c.CountryId == countyId).ToList();
        }

        public IQueryable<CityModel> Query()
        {
            return _cityRepo.Query().Select(c => new CityModel()
            {
                Id = c.Id,
                Name = c.Name,
                CountryId = c.CountryId,
                Guid = c.Guid
            });
        }

        public Result Update(CityModel model)
        {
            throw new NotImplementedException();
        }
    }
}
