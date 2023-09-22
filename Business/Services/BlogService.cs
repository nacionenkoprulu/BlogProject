using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{

    public interface IBlogService : IService<BlogModel>
    {
        Result DeleteImg(int blogId);

	}


    public class BlogService : IBlogService
    {

        private readonly RepoBase<Blog> _blogRepo;
        public BlogService(RepoBase<Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }


        public IQueryable<BlogModel> Query()
        {
            return _blogRepo.Query().OrderBy(b => b.CreateDate).ThenBy(b => b.Title).Select(b => new BlogModel()
            {
                Id = b.Id,
                Guid = b.Guid,
                Title = b.Title,
                Content = b.Content,
                CreateDate = b.CreateDate,
                UpdateDate = b.UpdateDate,
                UserId = b.UserId,
                Score = b.Score,

                CreateDateDisplay = b.CreateDate.ToString("MM/dd/yyyy HH:mm"),
                UpdateDateDisplay = b.UpdateDate.HasValue ? b.UpdateDate.Value.ToString("MM/dd/yyyy HH:mm") : "",
                UserNameDisplay = b.User.UserName,
                ScoreDisplay = (b.Score ?? 0).ToString("N1"),

                TagsDisplay = b.BlogTags.Select(bt => new TagModel()
                {
                    Id = bt.Tag.Id,
                    Guid = bt.Tag.Guid,
                    IsPopular = bt.Tag.IsPopular,
                    Name = bt.Tag.Name

                }).ToList(),

                TagIds = b.BlogTags.Select(bt => bt.TagId).ToList(),
                ImageSrcDisplay = "/Images/BlogImages/" + b.ImageName

            });
        }
        public Result Add(BlogModel model)
        {

            var location = "";
            var newImageName = "";

            if (model.Image is not null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                newImageName = Guid.NewGuid() + extension;
                location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/BlogImages",newImageName);
                var stream = new FileStream(location, FileMode.Create);

                model.Image.CopyTo(stream);
            }


            Blog entity = new Blog()
            {
                Title = model.Title.Trim(),
                Content = model.Content.Trim(),
                CreateDate = DateTime.Now,
                Score = model.Score,
                UserId = model.UserId.Value,

                BlogTags = model.TagIds.Select(tagId => new BlogTag()
                {
                    TagId = tagId
                }).ToList(),

                ImageURL = location,
                ImageName = newImageName

            };

            _blogRepo.Add(entity);

            return new SuccessResult("Blog added successfully");
        }

        public Result Delete(int id)
        {

            DeleteRelationalBlogTags(id);

            _blogRepo.Delete(id);

            return new SuccessResult("Blog deleted successfully");

        }

        public void Dispose()
        {
            _blogRepo?.Dispose();
            GC.SuppressFinalize(this);
        }


        private void DeleteRelationalBlogTags(int blogId)
        {
            _blogRepo.Delete<BlogTag>(bt => bt.BlogId == blogId);
        }


        public Result Update(BlogModel model)
        {
            
            if(_blogRepo.Exists(b=>b.UserId == model.UserId && b.Title.ToLower() == model.Title.ToLower() && b.Id != model.Id))
            {
                return new ErrorResult("Blogs with the same title exists!");
            }

            DeleteRelationalBlogTags(model.Id);


            var entity = _blogRepo.GetItem(model.Id);


            //entity.Id = model.Id;
            //entity.Guid = model.Guid;
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.UpdateDate = DateTime.Now;
            entity.Score = model.Score;
            entity.UserId = model.UserId.Value;
            entity.BlogTags = model.TagIds.Select(tagIds => new BlogTag()
            {
                TagId = tagIds
            }).ToList();



            var location = entity.ImageURL;
            var newImageName = entity.ImageName;

			if (model.Image is not null)
			{
				
				var extension = Path.GetExtension(model.Image.FileName);
				newImageName = Guid.NewGuid() + extension;
				location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/BlogImages", newImageName);
				var stream = new FileStream(location, FileMode.Create);

				model.Image.CopyTo(stream);
				entity.ImageURL = location;
				entity.ImageName = newImageName;
			}


			_blogRepo.Update(entity);
            return new SuccessResult("Blog updated successfully");
        }

		public Result DeleteImg(int blogId)
		{
            var entity = _blogRepo.GetItem(blogId);
            entity.ImageURL = null;
            entity.ImageName = null;
            _blogRepo.Update(entity);

            return new SuccessResult("Photo deleted is successfully!");

		}
	}
}
