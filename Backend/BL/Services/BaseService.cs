namespace BL.Services
{
    public abstract class BaseService<TEntity, TKey>
    {
        public abstract TEntity Get(TKey ID);
        public abstract void Create(TEntity Entity);
    }
}
