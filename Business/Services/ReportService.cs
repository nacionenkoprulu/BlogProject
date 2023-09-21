using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Services
{


    public interface IReportService
    {
        List<ReportItemModel> GetList(bool useInnerJoin = true, FilterItemModel filter = null);
    }

    public class ReportService : IReportService
    {
        private readonly RepoBase<Blog> _blogRepo;

        public ReportService(RepoBase<Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public List<ReportItemModel> GetList(bool useInnerJoin = true, FilterItemModel filter = null)
        {
            #region Query
            var blogQuery = _blogRepo.Query();
            var tagQuery = _blogRepo.Query<Tag>();
            var blogTagQuery = _blogRepo.Query<BlogTag>();
            var userQuery = _blogRepo.Query<User>();
            var roleQuery = _blogRepo.Query<Role>();

            IQueryable<ReportItemModel> query;

            if (useInnerJoin)
            {
                query = from b in blogQuery
                        join bt in blogTagQuery
                        on b.Id equals bt.BlogId
                        join t in tagQuery
                        on bt.TagId equals t.Id
                        join u in userQuery
                        on b.UserId equals u.Id
                        join r in roleQuery
                        on u.RoleId equals r.Id
                        select new ReportItemModel()
                        {
                            Active = u.IsActive ? "Yes" : "No",
                            BlogContent = b.Content,
                            BlogCreateDate = b.CreateDate.ToString("MM/dd/yyyy"),
                            BlogUpdateDate = b.UpdateDate.HasValue ? b.UpdateDate.Value.ToString("MM/dd/yyyy") : "",
                            BlogTitle = b.Title,
                            Popular = t.IsPopular ? "Yes" : "No",
                            RoleName = r.Name,
                            Score = b.Score,
                            Tag = t.Name,
                            UserName = u.UserName,
                            IsPopular = t.IsPopular,
                            BlogCreateDateInput = b.CreateDate,
                            BlogUpdateDateInput = b.UpdateDate,
                            UserId = u.Id
                        };
            }
            else
            {
                query = from b in blogQuery
                        join bt in blogTagQuery
                        on b.Id equals bt.BlogId into blogTagJoin
                        from blogTag in blogTagJoin.DefaultIfEmpty()
                        join t in tagQuery
                        on blogTag.TagId equals t.Id into tagJoin
                        from tag in tagJoin.DefaultIfEmpty()
                        join u in userQuery
                        on b.UserId equals u.Id into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        join r in roleQuery
                        on user.RoleId equals r.Id into roleJoin
                        from role in roleJoin.DefaultIfEmpty()
                        select new ReportItemModel()
                        {
                            Active = user.IsActive==true ? "Yes" :user.IsActive==false ? "No" : "",
                            BlogContent = b.Content,
                            BlogCreateDate = b.CreateDate.ToString("MM/dd/yyyy"),
                            BlogUpdateDate = b.UpdateDate.HasValue ? b.UpdateDate.Value.ToString("MM/dd/yyyy") : "",
                            BlogTitle = b.Title,
                            Popular = tag.IsPopular == true ? "Yes" : tag.IsPopular ? "No" : "",
                            RoleName = role.Name,
                            Score = b.Score,
                            Tag = tag.Name,
                            UserName = user.UserName,
                            IsPopular = tag.IsPopular,
                            BlogCreateDateInput = b.CreateDate,
                            BlogUpdateDateInput = b.UpdateDate,
                            UserId = user.Id
                            
                        };

            }


                            //select b.Title, b.Content,b.CreateDate[Create Date], b.UpdateDate[Update Name], b.Score,
                            //u.UserName[User], u.IsActive,
                            //r.Name[Role],
                            //t.Name Tag, t.IsPopular[Popular]
                            //from Blogs b left
                            //join Users u on b.UserId = u.Id
                            //left
                            //join Roles r on u.RoleId = r.Id
                            //left
                            //join BlogTags bt on bt.BlogId = b.Id
                            //left
                            //join Tags t on bt.TagId = t.Id

            #endregion

            #region Sorting
            query = query.OrderBy(q => q.BlogTitle);
            #endregion

            #region Filters
            if(filter is not null)
            {
                if (!string.IsNullOrWhiteSpace(filter.BlogTitle))
                {
                    query = query.Where(q => q.BlogTitle.ToUpper().Contains(filter.BlogTitle.ToUpper().Trim()));
                }
                if (filter.CreateDateBegin.HasValue)
                {
                    query = query.Where(q => q.BlogCreateDateInput >= filter.CreateDateBegin.Value);
                }
                if(filter.CreateDateEnd.HasValue)
                {
                    query = query.Where(q => q.BlogCreateDateInput <= filter.CreateDateEnd.Value);
                }
                if (filter.ScoreBegin.HasValue)
                {
                    query = query.Where(q => q.Score >= filter.ScoreBegin);
                }
                if (filter.ScoreEnd.HasValue)
                {
                    query = query.Where(q => q.Score <= filter.ScoreEnd);
                }
                if (filter.UserId.HasValue)
                {
                    query = query.Where(q => q.UserId == filter.UserId);
                }

            }
            

            #endregion


            return query.ToList();
        }
    }
}
