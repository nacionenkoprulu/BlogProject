using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=Blog;user id=sa;password=sa;multipleactiveresultsets=true;trustservercertificate=true;");
            // önce veritabanımızın (development veritabanı kullanılması daha uygundur) connection string'ini içeren bir obje oluşturuyoruz

            return new Db(optionsBuilder.Options); // daha sonra yukarıda oluşturduğumuz obje üzerinden Db tipinde bir obje dönüyoruz
        }
    }
}
