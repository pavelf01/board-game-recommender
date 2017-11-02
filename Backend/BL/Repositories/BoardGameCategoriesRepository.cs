using DAL.Entity;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGameCategoriesRepository : BaseRepository<BoardGameCategory, int>
    {
        public BoardGameCategoriesRepository(DbContext Context) : base(Context)
        {
        }

        public BoardGameCategory ByName(string Name)
        {
            return Context.Set<BoardGameCategory>().Where(i => i.Name == Name).FirstOrDefault();
        }
    }
}
