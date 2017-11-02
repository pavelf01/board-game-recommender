using DAL.Entity;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGameArtistsRepository : BaseRepository<BoardGameArtist,int>
    {
        public BoardGameArtistsRepository(DbContext Context) : base(Context)
        {
        }

        public BoardGameArtist ByName(string Name)
        {
            return Context.Set<BoardGameArtist>().Where(i => i.Name == Name).FirstOrDefault();
        }
    }
}
