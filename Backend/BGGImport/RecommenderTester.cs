using BL.Services;
using BL.Services.RecommenderEngine;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGGImport
{
    public class RecommenderTester
    {
        private UserRatingsService userRatingsService;
        private BoardGamesService gamesService;
        private BoardGameCategoriesService categoriesService;
        private UserProfileService userProfileService;

        private IWindsorContainer Container;

        public RecommenderTester(IWindsorContainer Container)
        {
            this.gamesService = Container.Resolve<BoardGamesService>();
            this.userRatingsService = Container.Resolve<UserRatingsService>();
            this.categoriesService = Container.Resolve<BoardGameCategoriesService>();
            this.userProfileService = Container.Resolve<UserProfileService>();

            this.Container = Container;

            TryContentBasedRecommendations();
        }

        public void TryContentBasedRecommendations()
        {
            Console.WriteLine("Content based recommender test running");

            var categoriesValues = userProfileService.ComputeBoardGameCategoriesValues();
            var idfs = userProfileService.ComputeIDF(categoriesValues).ToList();
            var userProfile = userProfileService.CreateUserProfile(categoriesValues);

            var value = userProfileService.ComputePredictionValue(categoriesValues, idfs, userProfile, 101); // 2
            var value1 = userProfileService.ComputePredictionValue(categoriesValues, idfs, userProfile, 661); // 10

            Console.WriteLine("Content based recommender test finished");
        }
    }
}
