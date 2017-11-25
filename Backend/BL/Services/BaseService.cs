using System.Collections.Generic;

namespace BL.Services
{
    public abstract class BaseService<TEntity>
    {
        public abstract TEntity Get(int ID);
        public abstract void Create(TEntity Entity);
    }
}
