using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ColorSudoku_Dnet.ServiceComponents.ScoreService
{
    public class Score
    {
        private int a;
        public int Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public DateTime TimeOfScore { get; set; }
       
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Points)}: {Points}, {nameof(TimeOfScore)}: {TimeOfScore}";
        }
    }
}
