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
        public int TestUserId { get; set; } = 21919;
        private int boardGamesCount;

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
            var boardGames = _gamesService.GetAll();
            boardGamesCount = boardGames.Count();
            return boardGames.Select((boardGame) => boardGame.Id)
                .Select((gameId) => _gamesService.GetWithRelated(gameId))
                .SelectMany((game) => ComputeValuesForOneBoardGame(game))
                .ToList();
        }
        
        public IEnumerable<CategoryIDF> ComputeIDF(IEnumerable<BoardGameCategoryValue> categoryValues)
        {
            var categories = _categoriesService.GetAll().ToList();
            // for know we count only test users board games

            foreach (BoardGameCategory category in categories)
            {
                var value = categoryValues.Where((cv) => cv.CategoryId == category.Id).Count();
                var idfValue = value == 0 ? 0.0 : Math.Log((double)boardGamesCount / (double)value, 10);

                yield return new CategoryIDF(category.Id, idfValue); ;
            }
        }

        public List<BoardGameUserValue> CreateUserProfile(IEnumerable<BoardGameCategoryValue> categoriesValues)
        {
            var userRatings = _userRatingsService.GetAllUserRatings(TestUserId);
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

               userProfile.Add(new BoardGameUserValue(TestUserId, category.Id, value));
            }

            return userProfile;
        }

        public IEnumerable<BoardGame> ComputePredictionValue(IEnumerable<BoardGameCategoryValue> categoryValues, IEnumerable<CategoryIDF> idfs, IEnumerable<BoardGameUserValue> userProfile)
        {
            var predictionValue = 0.0;
            var boardGames = _gamesService.GetAll();
            Dictionary<int, double> boardGamesPredValue = new Dictionary<int, double>();
            
            var up = userProfile.Where((bguv) => bguv.UserId == TestUserId);
            foreach (var boardGame in boardGames)
            {
                var gameCVs = categoryValues.Where((cv) => cv.GameId == boardGame.Id);
                
                foreach (CategoryIDF idf in idfs)
                {
                    var categoryId = idf.CategoryId;

                    var categoryValue = gameCVs.Where((cv) => cv.CategoryId == categoryId).FirstOrDefault()?.Value ?? 0.0;
                    var userProfileValue = up.Where((uv) => uv.CategoryId == categoryId).FirstOrDefault()?.Value ?? 0.0;

                    predictionValue += idf.Value * categoryValue * userProfileValue;
                }
                boardGamesPredValue.Add(boardGame.Id, predictionValue);
                predictionValue = 0.0;
            }

            var keys = boardGamesPredValue.OrderByDescending((value) => value.Value).Take(10).Select((value) => value.Key);

            return boardGames.Where(boardGame => keys.Contains(boardGame.Id));
        }
    }
}
