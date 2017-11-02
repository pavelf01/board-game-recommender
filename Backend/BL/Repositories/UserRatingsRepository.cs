using DAL.Entity;
using System.Data.Entity;

namespace BL.Repositories
{
    public class UserRatingsRepository : BaseRepository<UserRating, int>
    {
        public UserRatingsRepository(DbContext Context) : base(Context) { }
    }
}
