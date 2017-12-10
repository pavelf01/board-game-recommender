using DAL.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(DbContext Context) : base(Context) { }

        public User ByUserName(string UserName)
        {
            return Context.Set<User>().Where(u => u.UserName == UserName).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return Context.Set<User>();
        }
    }
}
