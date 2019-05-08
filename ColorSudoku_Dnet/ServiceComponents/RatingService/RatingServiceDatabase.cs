using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.RatingService
{
    public class RatingServiceDatabase : IRatingService
    {
        public void AddRating(Rating rate)
        {
            if (rate == null)
                throw new ServiceException("Rating must be not null!");

            if (rate.Name == null)
                throw new ServiceException("Rating contains null Name!");

            if(rate.Mark>5 || rate.Mark<0)
                throw new ServiceException("Rating must be from 0 to 5");

            using (var db = new SudokuContext())
            {
                //db.Add(rate);
                //db.SaveChanges();


                var name = rate.Name;
                if (db.Ratings.Any(e => e.Name == name))
                {

                    //var currentRating = db.Ratings.First(e => e.Name == name);
                    //currentRating.Name = name;

                    db.Ratings.Attach(rate);
                    db.Entry(rate).State = EntityState.Modified;
                }
                else
                {
                    db.Ratings.Add(rate);
                    db.Entry(rate).State = EntityState.Added;

                }

                db.SaveChanges();
            }
        }

        public void ClearRatings()
        {
            using (var db = new SudokuContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Ratings");
            }
        }

        public List<Rating> GetLastRatings()
        {
            using (var db = new SudokuContext())
            {
                return (from s in db.Ratings orderby s.TimeOfRating descending select s)
                    .Take(3).ToList();

            }
        }

        public Rating GetRating(string name)
        {
            using (var db = new SudokuContext())
            {
               return db.Ratings.SingleOrDefault(r => r.Name == name);
              // return db.Ratings.First(r => r.Name == name);
               //return (from s in db.Ratings where s.Name == name select s).Take(1);
            }
        }

        public int GetAverageRating()
        {
            using (var db = new SudokuContext())
            {
                List<int> ratings = (from s in db.Ratings select s.Mark).ToList();
                double avg = ratings.Count > 0 ? ratings.Average() : 0.0;
                return (int)Math.Round(avg);
            }
        }


        public void RemoveRating(string name)
        {
            using (var db = new SudokuContext())
            {
                db.Remove(db.Ratings.Single(c => c.Name == name));
                db.SaveChanges();
            }
        }
    }
}
