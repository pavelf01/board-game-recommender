using DAL.Entity;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGameDesignersRepository : BaseRepository<BoardGameDesigner>
    {
        public BoardGameDesignersRepository(DbContext Context) : base(Context) { }
        public BoardGameDesigner ByName(string Name)
        {
            return Context.Set<BoardGameDesigner>().Where(i => i.Name == Name).FirstOrDefault();
        }
    }
}
