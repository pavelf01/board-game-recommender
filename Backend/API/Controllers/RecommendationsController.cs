using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using BL.Services.RecommenderEngine;
using DAL.Entity;

namespace API.Controllers
{
    [RoutePrefix("api/recommendations")]
    public class RecommendationsController : ApiController
    {
        private const int RECOMMENDATION_COUNT = 10;

        public ContentBasedService _contentBasedService { get; set; }
        public RandomService _randomService { get; set; }

        private static readonly BoardGame BoardGame1 = new BoardGame()
        {
            Name = "Cyclades",
            Id = 1
        };

        private static readonly BoardGame BoardGame2 = new BoardGame()
        {
            Name = "Catan",
            Id = 2
        };

        private readonly BoardGame[] _boardGames = {BoardGame1, BoardGame2};
        
        [Route("collaborative/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetCollaborative(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _boardGames);
        }

        [Route("content/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetContentBased(int id)
        {
            var categoriesValues = _contentBasedService.ComputeBoardGameCategoriesValues();
            var idfs = _contentBasedService.ComputeIDF(categoriesValues).ToList();
            var userProfile = _contentBasedService.CreateUserProfile(id, new List<int>(), categoriesValues);

            var games = _contentBasedService.GetRecommendedBoardGames(RECOMMENDATION_COUNT, id, categoriesValues, idfs, userProfile); 
            
            return Request.CreateResponse(HttpStatusCode.OK, games);
        }

        [Route("random/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetRandom(int id)
        {
            var games = _randomService.GetRecommendedBoardGames(RECOMMENDATION_COUNT, id);

            return Request.CreateResponse(HttpStatusCode.OK, games);
        }
    }
}