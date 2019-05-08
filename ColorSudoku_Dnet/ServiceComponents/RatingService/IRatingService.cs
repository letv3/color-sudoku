using System;
using System.Collections.Generic;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.RatingService
{
    public interface IRatingService
    {
        void AddRating(Rating rate);

        List<Rating> GetLastRatings();

        void RemoveRating(String name);

        void ClearRatings();

        Rating GetRating(String name);

        int GetAverageRating();
    }
}
