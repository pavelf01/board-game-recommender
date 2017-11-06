using BL.Services;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace API.Controllers
{
    [RoutePrefix("api/boardGames")]
    public class BoardGamesController : ApiController
    {
        public BoardGamesService _service { get; set; }

        [Route("list/{page:int=1}")]
        [HttpGet]
        public HttpResponseMessage GetList(int page = 1)
        {
            var games = _service.GetList(page);
            return Request.CreateResponse(HttpStatusCode.OK, games);
        }

        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var game = _service.GetWithRelated(id);
            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        [Route("search/{term}")]
        [HttpGet]
        public HttpResponseMessage Search(string term)
        {
            var games = _service.GetByFulltextSearch(term);
            return Request.CreateResponse(HttpStatusCode.OK, games);
        }
    }
}
