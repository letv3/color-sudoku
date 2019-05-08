using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorSudoku_Dnet;
using ColorSudoku_Dnet.ServiceComponents.CommentService;
using ColorSudoku_Dnet.ServiceComponents.RatingService;
using ColorSudoku_Dnet.ServiceComponents.ScoreService;

namespace ColorSudoku_WEB.Models
{
    public class SudokuModel
    {

        public string LoggedUser { get; set; }
        public Field Field { get; set; }

        public string Message { get; set; }

        public IList<Score> Scores { get; set; }

        public IList<Comment> Comments { get; set; }

        public int Rating { get; set; }

    }
}
