using DAL.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGameCategoriesRepository : BaseRepository<BoardGameCategory>
    {
        public BoardGameCategoriesRepository(DbContext Context) : base(Context)
        {
        }

        public IEnumerable<BoardGameCategory> GetAll()
        {
            return Context.Set<BoardGameCategory>();
        }

        public BoardGameCategory ByName(string Name)
        {
            return Context.Set<BoardGameCategory>().Where(i => i.Name == Name).FirstOrDefault();
        }
    }
}
