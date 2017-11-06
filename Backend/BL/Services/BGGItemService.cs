using System;

namespace BL.Services
{
    public abstract class BGGItemService<TEntity,TBGGKey> : BaseService<TEntity>
    {
        public virtual TEntity GetByBGGIdentifier(TBGGKey Key)
        {
            throw new NotImplementedException("This item does not support getting by bgg id.");
        }
    }
}
