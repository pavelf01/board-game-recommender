using DAL.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class, BaseEntity
    {
        protected  DbContext Context { get; set; }
        public BaseRepository(DbContext Context)
        {
            this.Context = Context;
        }

        public void Insert(TEntity Entity)
        {
            Context.Set<TEntity>().Add(Entity);
            Context.SaveChanges();
        }

        public TEntity GetById(int Id)
        {
            return Context.Set<TEntity>().Where(i => i.Id == Id).FirstOrDefault();
        }
    }
}
