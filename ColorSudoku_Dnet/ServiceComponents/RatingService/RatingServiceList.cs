using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.RatingService
{
    public class RatingServiceList :IRatingService
    {
        private List<Rating> rates = new List<Rating>();


        public void AddRating(Rating rate)
        {
            if (rate == null) throw new ServiceException("Rate must be not null!");
            if (rate.Name == null || rate.Mark == null) throw new ServiceException("Rate contains null Name or Rate!");
            if (rate.Mark >= 0 && rate.Mark <= 5) rates.Add(rate);

        }

        public void ClearRatings()
        {
            rates.Clear();
        }

        public List<Rating> GetLastRatings()
        {
            //return rates.Take(3).ToList();
            return rates.OrderByDescending(s => s.Mark).Take(3).ToList();
        }

        public Rating GetRating(string name)
        {
            foreach (Rating r in rates)
            {
                if (r.Name.Equals(name)) return r;
            }
            return null;
        }

        public void RemoveRating(string name)
        {
            foreach (Rating r in rates)
            {
                if (r.Name.Equals(name)) rates.Remove(r); ;
            }
            
        }

        public int GetAverageRating()
        {
            var ratings = rates.Select(r => r.Mark).Distinct().ToList();
            double avg = ratings.Count > 0 ? ratings.Average() : 0.0;
            return (int)Math.Round(avg);
            
        }
    }
}
