using DAL.Entity;
using System.Data.Entity;

namespace BL.Repositories
{
    public class UserRatingsRepository : BaseRepository<UserRating>
    {
        public UserRatingsRepository(DbContext Context) : base(Context) { }

        public void Update(UserRating UserRating)
        {
            Context.SaveChanges();
        }
    }
}
