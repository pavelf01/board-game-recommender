using DAL.Entity;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGamePublishersRepository : BaseRepository<BoardGamePublisher>
    {
        public BoardGamePublishersRepository(DbContext Context) : base(Context) { }

        public BoardGamePublisher ByName(string Name)
        {
            return Context.Set<BoardGamePublisher>().Where(i => i.Name == Name).FirstOrDefault();
        }
    }
}
