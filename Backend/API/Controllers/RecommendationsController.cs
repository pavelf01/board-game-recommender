using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using DAL.Entity;

namespace API.Controllers
{
    [RoutePrefix("api/recommendations")]
    public class RecommendationsController : ApiController
    {
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
        public HttpResponseMessage GetContent(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _boardGames);
        }

        [Route("content/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetCollaborative(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _boardGames);
        }

        [Route("random/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetRandom(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _boardGames);
        }
    }
}