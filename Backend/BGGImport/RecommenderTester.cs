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
        private ContentBasedService contentBasedService;
        private RandomService randomService;
        private RecommendationEvaluationService evaluationService;

        private IWindsorContainer Container;

        // 24, 568 - 10 star rating 
        // 43 - 8 star rating
        // 13 - 5 star rating
        // 54 - 3 star rating
        // 38 - 1 star rating
        public List<int> testGameIds = new List<int>() { 24, 568, 43, 13, 54, 38 };
        public const int TEST_USER_ID = 12248;

        public RecommenderTester(IWindsorContainer Container)
        {
            this.gamesService = Container.Resolve<BoardGamesService>();
            this.userRatingsService = Container.Resolve<UserRatingsService>();
            this.categoriesService = Container.Resolve<BoardGameCategoriesService>();
            this.contentBasedService = Container.Resolve<ContentBasedService>();
            this.randomService = Container.Resolve<RandomService>();
            this.evaluationService = Container.Resolve<RecommendationEvaluationService>();

            this.Container = Container;

            TryContentBasedRecommendations();
            //TryRandomRecommendations();
        }

        public void TryContentBasedRecommendations()
        {
            Console.WriteLine("Content based recommender test running");

            var categoriesValues = contentBasedService.ComputeBoardGameCategoriesValues();
            var idfs = contentBasedService.ComputeIDF(categoriesValues).ToList();
            var userProfile = contentBasedService.CreateUserProfile(TEST_USER_ID, testGameIds, categoriesValues);

            foreach (int gameId in testGameIds)
            {
                var value = contentBasedService.ComputePredictionValue(categoriesValues, idfs, userProfile, gameId);
                Console.WriteLine(value);
            }

            Console.WriteLine("Content based recommender test finished");
        }

        public void EvaluateContentBasedRecommendations()
        {
            Console.WriteLine("Content based recommender evaluation running");

            evaluationService.EvaluateContentBased();

            Console.WriteLine("Content based recommender evaluation finished");
        }

        public void TryRandomRecommendations()
        {
            Console.WriteLine("Random recommender test running");

            var games = randomService.GetRecommendedBoardGames(10, TEST_USER_ID);
            var gameNames = games.Select((game) => game.Name);
            
            foreach(String name in gameNames)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("Random recommender test finished");
        }
    }
}
