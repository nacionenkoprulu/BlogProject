using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Records.Bases;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Repo<TEntity> : RepoBase<TEntity> where TEntity : RecordBase, new()
    {
        public Repo(Db dbContext) : base(dbContext)
        {

        }
    }
}
