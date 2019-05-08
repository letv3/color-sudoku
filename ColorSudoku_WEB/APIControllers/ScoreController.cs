using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorSudoku_Dnet.ServiceComponents.ScoreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColorSudoku_WEB.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private IScoreService _scoreService = new ScoreServiceDatabase();

        // GET: api/Score
        [HttpGet]
        public IEnumerable<Score> Get()
        {
            return _scoreService.GetTopScores();
        }

        // POST: api/Score
        [HttpPost]
        public void Post([FromBody] Score score)
        {
            _scoreService.AddScore(score);
        }

        //DELETE: api/Score
        [HttpDelete]
        public void Delete()
        {
            _scoreService.ClearScores();
        }

    }
}