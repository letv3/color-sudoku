using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.RatingService
{
    public class Rating
    {
        //public int Id { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        public int Mark { get; set; }

        public  DateTime TimeOfRating { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Mark)}: {Mark}, {nameof(TimeOfRating)}: {TimeOfRating}";
        }
    }
}
