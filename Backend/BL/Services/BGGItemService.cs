using System;

namespace BL.Services
{
    public abstract class BGGItemService<TEntity, TKey,TBGGKey> : BaseService<TEntity, TKey>
    {
        public virtual TEntity GetByBGGIdentifier(TBGGKey Key)
        {
            throw new NotImplementedException("This item does not support getting by bgg id.");
        }
    }
}
