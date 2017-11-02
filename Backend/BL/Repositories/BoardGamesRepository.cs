using DAL.Entity;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGamesRepository : BaseRepository<BoardGame, int>
    {
        public BoardGamesRepository(DbContext Context) : base(Context) { }

        public BoardGame ByBGGId(int Id)
        {
            return Context.Set<BoardGame>().Where(i => i.BGGId == Id).FirstOrDefault();
        }
    }
}
