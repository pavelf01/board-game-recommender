using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.RecommenderEngine
{
    public class RecommendationEvaluationService
    {
        private UserRatingsService _userRatingsService;
        private UsersService _usersService;
        private ContentBasedService _contentBasedService;

        public RecommendationEvaluationService(ContentBasedService contentBasedService, UserRatingsService userRatingsService, UsersService usersService)
        {
            _contentBasedService = contentBasedService;
            _userRatingsService = userRatingsService;
            _usersService = usersService;
        }

        public IEnumerable<int> GetUsersToEvaluate()
        {
            // takes too long to do
            //var users = _usersService.GetAll().ToList();

            //return users
            //    .Where((user) => _userRatingsService.GetUserRatingsCount(user.Id) > 100)
            //    .Select((user) => user.Id);

            return new List<int>() { 6776, 15348, 8257, 935, 41557, 21882, 53011, 49316, 12248, 4337,
                88646, 3948, 49950, 8787, 37469, 28914, 8164, 13448, 7904, 45175 };
        }

        private Dictionary<int, double> ComputeIgnoredGameIds(IEnumerable<UserRating> userRatings)
        {
            var ignoredGameIds = new Dictionary<int, double>();

            // compute ignored games - one highest rated, one lowest rated, each a dictionary of id and rating
            var highestRating = userRatings.Select((userRating) => userRating.Rating).Max();
            var lowestRating = userRatings.Select((userRating) => userRating.Rating).Min();
            ignoredGameIds.Add(userRatings.Where((userRating) => userRating.Rating == highestRating).First().BoardGame.Id, highestRating);
            ignoredGameIds.Add(userRatings.Where((userRating) => userRating.Rating == lowestRating).First().BoardGame.Id, lowestRating);

            return ignoredGameIds;
        }

        public void EvaluateContentBased()
        {
            var userIds = GetUsersToEvaluate();

            var categoriesValues = _contentBasedService.ComputeBoardGameCategoriesValues();
            var idfs = _contentBasedService.ComputeIDF(categoriesValues).ToList();

            var rightRecommendationCount = 0;
            var wrongRecCount = 0;

            foreach (int userId in userIds)
            {
                var ratings = _userRatingsService.GetAllUserRatings(userId);
                var ignoredGameIds = ComputeIgnoredGameIds(ratings);

                var userProfile = _contentBasedService.CreateUserProfile(userId, ignoredGameIds.Keys, categoriesValues);

                Console.WriteLine("UserId: " + userId);

                var firstGame = ignoredGameIds.First();
                var secondGame = ignoredGameIds.Last();

                var firstValue = _contentBasedService.ComputePredictionValue(categoriesValues, idfs, userProfile, firstGame.Key);
                var secondValue = _contentBasedService.ComputePredictionValue(categoriesValues, idfs, userProfile, secondGame.Key);

                if (firstGame.Value < secondGame.Value && firstValue < secondValue ||
                    firstGame.Value > secondGame.Value && firstValue > secondValue)
                {
                    rightRecommendationCount++;
                } else
                {
                    wrongRecCount++;
                }

                Console.WriteLine(firstValue + " rating: " + firstGame.Value);
                Console.WriteLine(secondValue + " rating: " + secondGame.Value);
            }

            Console.WriteLine("Right recommendation count: " + rightRecommendationCount + " Wrong Count: " + wrongRecCount);
        }
    }
}
