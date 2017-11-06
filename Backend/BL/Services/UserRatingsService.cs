using BL.Repositories;
using DAL.Entity;
using System;

namespace BL.Services
{
    public class UserRatingsService : BaseService<UserRating>
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

        public void Update(UserRating UserRating)
        {
            _repository.Update(UserRating);
        }

        public override UserRating Get(int UserRatingId)
        {
            return _repository.GetById(UserRatingId);
        }
    }
}
