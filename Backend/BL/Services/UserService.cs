using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class UsersService : BGGItemService<User, int, string>
    {
        private readonly UsersRepository _repository;

        public UsersService(UsersRepository UsersRepository)
        {
            this._repository = UsersRepository;
        }
        public override void Create(User User)
        {
            _repository.Insert(User);
        }

        public override User Get(int UserId)
        {
            return _repository.GetById(UserId);
        }

        public User GetUserByUserName(string UserName)
        {
            return _repository.ByUserName(UserName);
        }
        public override User GetByBGGIdentifier(string Key)
        {
            return this.GetUserByUserName(Key);
        }
    }
}
