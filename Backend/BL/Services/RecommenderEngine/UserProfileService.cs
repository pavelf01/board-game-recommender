using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.RecommenderEngine
{
    public class UserProfileService
    {
        private const int TEST_USER_ID = 21919;

        private readonly UserRatingsService _userRatingsService;
        private readonly BoardGamesService _gamesService;
        private readonly BoardGameCategoriesService _categoriesService;

        public UserProfileService(UserRatingsService userRatingsService, BoardGamesService gamesService, BoardGameCategoriesService categoriesService)
        {
            _userRatingsService = userRatingsService;
            _gamesService = gamesService;
            _categoriesService = categoriesService;
        }

        public IEnumerable<BoardGameCategoryValue> ComputeValuesForOneBoardGame(BoardGame game)
        {
            var gameCategoriesCount = game.Categories.Count;
            // value is normalized
            var value = 1 / Math.Sqrt(gameCategoriesCount);

            // no board game category value record means that value is zero
            return game.Categories.Select((category) => new BoardGameCategoryValue(game.Id, category.Id, value));
        }

        public List<BoardGameCategoryValue> ComputeBoardGameCategoriesValues()
        {
            // TODO this will do all board games at once and save into db
            // now this is done only for test user
            var userRatings = _userRatingsService.GetAllUserRatings(TEST_USER_ID);
            var userBoardGameIds = userRatings.Select((userRating) => userRating.BoardGame.Id);

            return userBoardGameIds
                .Select((gameId) => _gamesService.GetWithRelated(gameId))
                .SelectMany((game) => ComputeValuesForOneBoardGame(game))
                .ToList();
        }

        public IEnumerable<CategoryIDF> ComputeIDF(IEnumerable<BoardGameCategoryValue> categoryValues)
        {
            var categories = _categoriesService.GetAll().ToList();
            // for know we count only test users board games
            var boardGamesCount = _userRatingsService.GetAllUserRatings(TEST_USER_ID)
                .Select((userRating) => userRating.BoardGame.Id)
                .Count();

            foreach (BoardGameCategory category in categories)
            {
                var value = categoryValues.Where((cv) => cv.CategoryId == category.Id).Count();
                var idfValue = value == 0 ? 0.0 : Math.Log((double)boardGamesCount / (double)value, 10);

                yield return new CategoryIDF(category.Id, idfValue); ;
            }
        }

        public List<BoardGameUserValue> CreateUserProfile(IEnumerable<BoardGameCategoryValue> categoriesValues)
        {
            var userRatings = _userRatingsService.GetAllUserRatings(TEST_USER_ID);
            var userMeanRating = userRatings.Average((userRating) => userRating.Rating);
            var categories = _categoriesService.GetAll().ToList();
            
            // Normalize ratings by subtracting user mean rating
            foreach (UserRating userRating in userRatings)
            {
                userRating.Rating -= userMeanRating;
            }

            var userProfile = new List<BoardGameUserValue>();

            // Count the user profile value for each board game category
            foreach (BoardGameCategory category in categories)
            {
                var cv = categoriesValues.Where((categoryValue) => categoryValue.CategoryId == category.Id);

                var value = 0.0;

                foreach (BoardGameCategoryValue categoryValue in cv)
                {
                    var rating = userRatings.FirstOrDefault((userRating) => userRating.BoardGame.Id == categoryValue.GameId)?.Rating ?? 0;
                    value += rating * categoryValue.Value;
                }

               userProfile.Add(new BoardGameUserValue(TEST_USER_ID, category.Id, value));
            }

            return userProfile;
        }

        public double ComputePredictionValue(IEnumerable<BoardGameCategoryValue> categoryValues, IEnumerable<CategoryIDF> idfs, IEnumerable<BoardGameUserValue> userProfile, int boardGameId)
        {
            var predictionValue = 0.0;

            var gameCVs = categoryValues.Where((cv) => cv.GameId == boardGameId);
            var up = userProfile.Where((bguv) => bguv.UserId == TEST_USER_ID);

            foreach (CategoryIDF idf in idfs)
            {
                var categoryId = idf.CategoryId;

                var categoryValue = gameCVs.Where((cv) => cv.CategoryId == categoryId).FirstOrDefault()?.Value ?? 0.0;
                var userProfileValue = up.Where((uv) => uv.CategoryId == categoryId).FirstOrDefault()?.Value ?? 0.0;

                predictionValue += idf.Value * categoryValue * userProfileValue;
            }

            return predictionValue;
        }
    }
}
