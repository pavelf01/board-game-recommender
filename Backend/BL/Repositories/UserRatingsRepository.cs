using DAL.Entity;
using System.Linq;
using System.Collections.Generic;
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

        public IEnumerable<UserRating> GetAllUserRatings(int userId)
        {
            var ratings = Context.Set<UserRating>().Where(i => i.User.Id == userId).ToList();

            foreach (var rating in ratings)
            {
                Context.Entry(rating).Reference(g => g.BoardGame).Load();
            }

            return ratings;
        }

        public int GetUserRatingsCount(int userId)
        {
            return Context.Set<UserRating>().ToList().Count();
        }
    }
}
