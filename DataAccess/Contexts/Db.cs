using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogTag> BlogTags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>().HasKey(bt => new { bt.BlogId, bt.TagId });
        }


    }
}
