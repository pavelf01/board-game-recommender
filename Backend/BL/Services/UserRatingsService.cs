using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class UserRatingsService : BaseService<UserRating,int>
    {
        private readonly UserRatingsRepository _repository;

        public UserRatingsService(UserRatingsRepository UserRatingsRepository)
        {
            this._repository = UserRatingsRepository;
        }
        public override void Create(UserRating UserRating)
        {
            _repository.Insert(UserRating);
        }

        public override UserRating Get(int UserRatingId)
        {
            return _repository.GetById(UserRatingId);
        }
    }
}
