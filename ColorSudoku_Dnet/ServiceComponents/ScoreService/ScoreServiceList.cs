using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.ScoreService
{
    public class ScoreServiceList : IScoreService
    {
        

        private List<Score> scores = new List<Score>();


        public void AddScore(Score score)
        {
            if (score == null)
                throw new ServiceException("Score must be not null!");
            if (score.Name == null)
                throw new ServiceException("Score contains null Name!");
            scores.Add(score);
            
        }

        public List<Score> GetTopScores()
        { 
            //return (from s in scores orderby s.Points descending select s).Take(5).ToList();
            return scores.OrderByDescending(s => s.Points).Take(3).ToList();
        }
        

        public void ClearScores()
        {
            scores.Clear();
           
        }

    }
}
