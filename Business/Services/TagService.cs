using AppCore.Business.Services.Bases;
using AppCore.DataAccess.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
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

    public interface ITagService : IService<TagModel>
    {

        List<TagModel> GetList();

    }



    public class TagService : ITagService
    {
        private readonly RepoBase<Tag> _tagRepo;

        public TagService(RepoBase<Tag> tagRepo)
        {
            _tagRepo = tagRepo;
        }



		public IQueryable<TagModel> Query()
		{
			return _tagRepo.Query().OrderBy(t => t.Name).Select(t => new TagModel()
			{
				Id = t.Id,
				Name = t.Name,
				Guid = t.Guid,
				IsPopular = t.IsPopular,

				IsPopularDisplay = t.IsPopular ? "Yes" : "No"
			});
		}

        public List<TagModel> GetList()
        {
            return Query().OrderBy(t => t.Name).Select(t => new TagModel()
            {
                Id = t.Id,
                Name = t.Name + (t.IsPopular ? " *" : ""),
                Guid = t.Guid,
                IsPopular = t.IsPopular,

                IsPopularDisplay = t.IsPopular ? "Yes" : "No"

            }).ToList();
        }


        public Result Add(TagModel model)
        {
            if(_tagRepo.Exists(t=>t.Name.ToLower()==model.Name.ToLower().Trim()))
            {
                return new ErrorResult("Tag with the same name exists!");
            }

            Tag entity = new Tag()
            {
                Name = model.Name.Trim(),
                IsPopular = model.IsPopular
            };
            _tagRepo.Add(entity);

            return new SuccessResult("Tag added successfully");

        }

        public Result Delete(int id)
        {
            _tagRepo.Delete(id);
            return new SuccessResult("Tag deleted successfully");
        }

        public void Dispose()
        {
            _tagRepo.Dispose();
        }

        

        public Result Update(TagModel model)
        {
            if(_tagRepo.Exists(t => t.Name.ToLower() == model.Name.ToLower().Trim() && t.Id != model.Id))
            {
				return new ErrorResult("Tag with the same name exists!");
			}

			//Bir diğer yöntem id üzerinden entity çekilmesi ve onun güncellenmesi işlemidir
			Tag entity = new Tag() 
            {
                Id = model.Id,
                Guid = model.Guid,
                Name = model.Name.Trim(),
                IsPopular = model.IsPopular
            };

            _tagRepo.Update(entity);
			return new SuccessResult("Tag updated successfully");
		}

        
    }
}
