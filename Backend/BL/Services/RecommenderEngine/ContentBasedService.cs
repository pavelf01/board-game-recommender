using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.RecommenderEngine
{
    public class ContentBasedService
    {
        private int boardGamesCount;

        private readonly UserRatingsService _userRatingsService;
        private readonly BoardGamesService _gamesService;
        private readonly BoardGameCategoriesService _categoriesService;

        public ContentBasedService(UserRatingsService userRatingsService, BoardGamesService gamesService, BoardGameCategoriesService categoriesService)
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
            // TODO save into db
            var boardGames = _gamesService.GetAll();
            boardGamesCount = boardGames.Count();
            return boardGames.Select((boardGame) => boardGame.Id)
                .Select((gameId) => _gamesService.GetWithCategories(gameId))
                .SelectMany((game) => ComputeValuesForOneBoardGame(game))
                .ToList();
        }
        
        public IEnumerable<CategoryIDF> ComputeIDF(IEnumerable<BoardGameCategoryValue> categoryValues)
        {
            var categories = _categoriesService.GetAll().ToList();

            foreach (BoardGameCategory category in categories)
            {
                var value = categoryValues.Where((cv) => cv.CategoryId == category.Id).Count();
                var idfValue = value == 0 ? 0.0 : Math.Log((double)boardGamesCount / (double)value, 10);

                yield return new CategoryIDF(category.Id, idfValue); ;
            }
        }

        public List<BoardGameUserValue> CreateUserProfile(int userId, IEnumerable<int> ignoredGameIds, IEnumerable<BoardGameCategoryValue> categoriesValues)
        {
            var userRatings = _userRatingsService.GetAllUserRatings(userId)
                // remove ignored game ids
                .Where((userRating) => !ignoredGameIds.Contains(userRating.BoardGame.Id)) ;
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

               userProfile.Add(new BoardGameUserValue(userId, category.Id, value));
            }

            return userProfile;
        }

        public IEnumerable<BoardGame> GetRecommendedBoardGames(int numOfGames, int userId, IEnumerable<BoardGameCategoryValue> categoryValues, IEnumerable<CategoryIDF> idfs, IEnumerable<BoardGameUserValue> userProfiles)
        {
            var predictionValue = 0.0;
            var boardGames = _gamesService.GetAll();
            Dictionary<int, double> boardGamesPredValue = new Dictionary<int, double>();
            
            var userProfile = userProfiles.Where((bguv) => bguv.UserId == userId);
            foreach (var boardGame in boardGames)
            {
                predictionValue = ComputePredictionValue(categoryValues, idfs, userProfile, boardGame.Id);
                boardGamesPredValue.Add(boardGame.Id, predictionValue);
            }

            var keys = boardGamesPredValue.OrderByDescending((value) => value.Value).Take(numOfGames).Select((value) => value.Key);

            return boardGames.Where(boardGame => keys.Contains(boardGame.Id));
        }

        public double ComputePredictionValue(IEnumerable<BoardGameCategoryValue> categoryValues, IEnumerable<CategoryIDF> idfs, IEnumerable<BoardGameUserValue> userProfile, int boardGameId)
        {
            var predictionValue = 0.0;

            var gameCVs = categoryValues.Where((cv) => cv.GameId == boardGameId);

            foreach (CategoryIDF idf in idfs)
            {
                var categoryId = idf.CategoryId;

                var categoryValue = gameCVs.Where((cv) => cv.CategoryId == categoryId).FirstOrDefault()?.Value ?? 0.0;
                var userProfileValue = userProfile.Where((uv) => uv.CategoryId == categoryId).FirstOrDefault()?.Value ?? 0.0;

                predictionValue += idf.Value * categoryValue * userProfileValue;
            }

            return predictionValue;
        }
    }
}
