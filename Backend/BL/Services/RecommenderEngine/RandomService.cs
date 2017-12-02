using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.RecommenderEngine
{
    public class RandomService
    {
        private readonly UserRatingsService _userRatingsService;
        private readonly BoardGamesService _gamesService;

        public RandomService(UserRatingsService userRatingsService, BoardGamesService gamesService)
        {
            _userRatingsService = userRatingsService;
            _gamesService = gamesService;
        }

        public IEnumerable<BoardGame> GetRecommendedBoardGames(int numOfGames, int userId)
        {
            var games = _gamesService.GetAll();
            var userRatedGamesIds = _userRatingsService.GetAllUserRatings(userId)
                .Select((userRating) => userRating.BoardGame.Id);

            var recommenderCandidates = games.Where((game) => !userRatedGamesIds.Contains(game.Id)).ToList();
            var candidatesCount = recommenderCandidates.Count();

            var random = new Random();
            var recommendedGames = new HashSet<BoardGame>();

            while (recommendedGames.Count() < 10)
            {
                var index = random.Next() % candidatesCount;

                var randomGame = recommenderCandidates.ElementAt(index);
                recommendedGames.Add(randomGame);
            }

            return recommendedGames;
        }
    }
}
