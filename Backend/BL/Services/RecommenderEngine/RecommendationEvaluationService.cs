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
        private ContentBasedService _contentBasedService;

        public RecommendationEvaluationService(ContentBasedService contentBasedService, UserRatingsService userRatingsService)
        {
            _contentBasedService = contentBasedService;
            _userRatingsService = userRatingsService;
        }

        public IEnumerable<int> GetUsersToEvaluate()
        {
            return new List<int>() { 12248, 21919 };
        }

        public void EvaluateContentBased()
        {
            var userIds = GetUsersToEvaluate();

            var categoriesValues = _contentBasedService.ComputeBoardGameCategoriesValues();
            var idfs = _contentBasedService.ComputeIDF(categoriesValues).ToList();
            
            foreach(int userId in userIds)
            {
                var ratings = _userRatingsService.GetAllUserRatings(userId);
                var ignoredGameIds = new List<int>();

                // compute ignored games - one 10 rated, one 5 rated, one 1 rated ???

                var userProfile = _contentBasedService.CreateUserProfile(userId, ignoredGameIds, categoriesValues);

                foreach (int gameId in ignoredGameIds)
                {
                    var value = _contentBasedService.ComputePredictionValue(categoriesValues, idfs, userProfile, gameId);
                    Console.WriteLine(value);
                }
            }
        }
    }
}
