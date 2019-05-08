using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorSudoku_Dnet.ServiceComponents.RatingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColorSudoku_WEB.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService = new RatingServiceDatabase();


        //POST: api/Rating
        [HttpPost]
        public void Post([FromBody] Rating rating)
        {
            _ratingService.AddRating(rating);
        }

        //GET: api/Rating
        [HttpGet]
        public IEnumerable<Rating> GetLastRatings()
        {
            return _ratingService.GetLastRatings();
        }

        //GET: api/Rating/letv
        [HttpGet("{name}")]
        public Rating GetRating([FromRoute]string name)
        {
            return _ratingService.GetRating(name);
        }

        //Get:api/Rating/avg
        [HttpGet("avg")]
        public int GetAvgRating()
        {
            return _ratingService.GetAverageRating();
        }


        //DELET: api/Rating
        [HttpDelete]
        public void ClearRatings()
        {
            _ratingService.ClearRatings();
        }

        //DELET: api/Rating/letv
        [HttpDelete("{name}")]
        public void RemoveRating([FromRoute] string name)
        {
            _ratingService.RemoveRating(name);
        }


    }
}