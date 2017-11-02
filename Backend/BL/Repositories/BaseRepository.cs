using DAL.Entity;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BaseRepository<TEntity,TKey> where TEntity : class, BaseEntity<TKey>
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

        public TEntity GetById(TKey Id)
        {
            return Context.Set<TEntity>().Where(i => i.Id.Equals(Id)).FirstOrDefault();
        }
    }
}
