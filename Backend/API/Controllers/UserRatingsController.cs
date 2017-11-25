using API.DTO;
using BL.Services;
using DAL.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/userRatings")]
    public class UserRatingsController : ApiController
    {
        public UserRatingsService _service { get; set; }
        public UsersService _userService { get; set; }
        public BoardGamesService _boardGameService { get; set; }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]UserRatings rating)
        {
            var userRating = new UserRating
            {
                Comment = rating.Comment,
                Rating = rating.Value,
                User = _userService.GetApplicationUser(),
                BoardGame = _boardGameService.Get(rating.BoardGameId)
            };
            this._service.Create(userRating);
            return Request.CreateResponse(HttpStatusCode.OK, userRating.Id);
        }
        
        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetRatingsById(int id)
        {
            var userRatings = this._service.GetAllUserRatings(id);
            return Request.CreateResponse(HttpStatusCode.OK, userRatings);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody]UserRatings rating)
        {
            var userRating = _service.Get(rating.Id);
            userRating.Comment = rating.Comment;
            userRating.Rating = rating.Value;
            this._service.Update(userRating);
            return Request.CreateResponse(HttpStatusCode.OK, userRating.Id);
        }
    }
}