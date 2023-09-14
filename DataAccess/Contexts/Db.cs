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

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }


        public Db(DbContextOptions options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>().HasKey(bt => new { bt.BlogId, bt.TagId });

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.User)
                .WithOne(u => u.UserDetail)
                .HasForeignKey<UserDetail>(ud => ud.UserId)
                .OnDelete(DeleteBehavior.NoAction);
			

			modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.Country)
                .WithMany(c => c.UserDetails)
                .HasForeignKey(ud => ud.CountryId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<UserDetail>()
				.HasOne(ud => ud.City)
				.WithMany(c => c.UserDetails)
				.HasForeignKey(ud => ud.CityId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<City>()
				.HasOne(c=>c.Country)
				.WithMany(co=>co.Cities)
				.HasForeignKey(ci=>ci.CountryId)
				.OnDelete(DeleteBehavior.NoAction);


		}


    }
}
