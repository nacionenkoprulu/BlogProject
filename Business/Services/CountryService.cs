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
    public interface ICountryService : IService<CountryModel>
    {
    }

    public class CountryService : ICountryService
    {

        private readonly RepoBase<Country> _countryRepo;

        public CountryService(RepoBase<Country> countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public Result Add(CountryModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _countryRepo.Dispose();
        }

        public IQueryable<CountryModel> Query()
        {
            return _countryRepo.Query().Select(c => new CountryModel()
            {
                Id = c.Id,
                Name = c.Name,
                Guid = c.Guid

            });
        }

        public Result Update(CountryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
