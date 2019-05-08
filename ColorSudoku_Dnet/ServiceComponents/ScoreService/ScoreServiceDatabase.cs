using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.ScoreService
{
    public class ScoreServiceDatabase : IScoreService
    {
        public void AddScore(Score score)
        {
            if (score == null)
                throw new ServiceException("Score must be not null!");
            if (score.Name == null)
                throw new ServiceException("Score contains null Name!");

            using (var db = new SudokuContext())
            {
                db.Add(score);
                db.SaveChanges();
            }
        }

        public List<Score> GetTopScores()
        {

            using (var db = new SudokuContext())
            {
                return (from s in db.Scores orderby s.Points descending select s)
                    .Take(5).ToList();
              
            }
        }



        public void ClearScores()
        {
            using (var db = new SudokuContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Scores");
            }
        }
    }
}
