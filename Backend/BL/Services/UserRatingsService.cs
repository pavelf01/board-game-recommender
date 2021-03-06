﻿using BL.Repositories;
using DAL.Entity;
using System;
using System.Collections.Generic;

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

        public IEnumerable<UserRating> GetAllUserRatings(int userId)
        {
            return _repository.GetAllUserRatings(userId);
        }

        public int GetUserRatingsCount(int userId)
        {
            return _repository.GetUserRatingsCount(userId);
        }
    }
}
