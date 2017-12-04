using DAL.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGameCategoryValuesRepository : BaseRepository<BoardGameCategoryValue>
    {
        public BoardGameCategoryValuesRepository(DbContext Context) : base(Context)
        {
        }

        public IEnumerable<BoardGameCategoryValue> GetAll()
        {
            return Context.Set<BoardGameCategoryValue>();
        }

        public void InsertMany(IEnumerable<BoardGameCategoryValue> Entities)
        {
            Context.Set<BoardGameCategoryValue>().AddRange(Entities);
            Context.SaveChanges();
        }
    }
}
