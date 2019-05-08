using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ColorSudoku_Dnet;
using ColorSudoku_Dnet.ServiceComponents.ScoreService;
using ColorSudoku_Dnet.ServiceComponents.CommentService;
using ColorSudoku_Dnet.ServiceComponents.RatingService;

namespace Color_sudoku_CUI
{
    class ConsoleUI
    {
        private Field field;

        static readonly Regex STANDARD_INPUT_PATTERN = new Regex("^([1-9])([a-iA-I])([1-9]$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static readonly Regex HELP_INPUT_PATTERN = new Regex("^([hH])([a-iA-I])([1 - 9]$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //private readonly IScoreService scoreService = new ScoreServiceList();
        private readonly IScoreService scoreService = new ScoreServiceDatabase();

        //private readonly ICommentService commentService = new CommentServiceList();
        private readonly ICommentService commentService = new CommentServiceDatabase();


        //private readonly IRatingService ratingService = new RatingServiceList();
        private readonly IRatingService ratingService = new RatingServiceDatabase();


        public void Run()
        {
            var name = ReadName();
            field = new Field();
            do
            {
                PrintField();
                ProcessInput();
            } while (field.State != GameState.SOLVED);

            Console.WriteLine("CONGRATS, U SOLVED THIS PUZZLE");
            scoreService.AddScore(new Score { Name =name, Points = field.GetScore(),TimeOfScore = DateTime.Now});


            CommentAdding(name);
            RatingAdding(name);

            PrintTopScores();
            PrintLastComments();
            PrintLastRatings();
            PrintAverageRating();

            

            Console.WriteLine("Want to play one more time?(y/n)");
            String line = ReadInput().ToUpper();
            if (line.Equals("Y"))
            {
                Run();
            }
            else Environment.Exit(0);

        }


        public string ReadName()
        {
            Console.WriteLine("Enter you name: ");
            return ReadInput();
        }

        private void RatingAdding(string name)
        {
            Console.WriteLine("Leave you Rate here(from 0 to 5)(if u dont want rate type n): ");
            string ratingInput = ReadInput();
            if (ratingInput.Equals("n")) return;
            int rate = int.Parse(ratingInput);
            ratingService.AddRating(new Rating {Name = name, Mark = rate, TimeOfRating = DateTime.Now});
        }

        private void CommentAdding(string name)
        {
            Console.WriteLine("Leave you comment here(if u dont want comment type n ) ");
            string comment = ReadInput();
            if (comment.Equals("n")) return;
            commentService.AddComment(
                new Comment { Name = name, Message = comment,TimeOFComment = DateTime.Now });
        }

        private void PrintAverageRating()
        {

            Console.WriteLine();
            Console.WriteLine("Average Rating");  
            string rating = "";

            for (int i = 0; i < ratingService.GetAverageRating(); i++)
            {
                rating = String.Concat("*", rating);
               
            }

            Console.Write(rating);
            Console.WriteLine();
        }
        private void PrintLastRatings()
        {
            Console.WriteLine();
            Console.WriteLine("Last Rating");
            foreach (var rate in ratingService.GetLastRatings())
            {
                Console.WriteLine(rate.ToString());
            }

            Console.WriteLine();
        }
        private void PrintLastComments()
        {
            Console.WriteLine();
            Console.WriteLine("Recent Comments");
            foreach (var comment in commentService.GetLastComments())
            {
                Console.WriteLine(comment.ToString());
            }
        }

        private void PrintTopScores()
        {
            Console.WriteLine();
            Console.WriteLine("Recent Ratings");
            Console.WriteLine("Top Scores");
            foreach (Score s in scoreService.GetTopScores())
            {
                Console.WriteLine(s.ToString());
            }
        }

        private void PrintField()
        {
            PrintHeader();
            PrintFieldBody();
        }

        private void PrintHeader()
        {
            Console.Write("   ");
            for (int column = 0; column < field.RowCount; column++)
            {
                Console.Write(" "+(column + 1)+" ");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int column = 0; column < field.RowCount; column++)
            {
                Console.Write("---");
            }
            Console.WriteLine();
        }

        private void PrintFieldBody()
        {
            for (int row = 0; row < field.RowCount; row++)
            {
                Console.Write(" "+((char)(row + 'A')) + "|");
                for (int column = 0; column < field.RowCount; column++)
                {
                    Tile tile = field.GetTile(row, column);
                    //System.out.print(' ');
                    Console.Write(" "+tile.Value+" ");

                }
                Console.WriteLine();
            }
        }

        private void ProcessInput()
        {
            Console.WriteLine("Type input (ex. 9A1, 4A1,HA1- hint on A1, X-exit):");
            String line = ReadInput().ToUpper();


            if (line.Equals("X")) { Environment.Exit(0);  }

            Match match = STANDARD_INPUT_PATTERN.Match(line);
            Match helpmatch = HELP_INPUT_PATTERN.Match(line);



            if (helpmatch.Success)
            {
                int row = helpmatch.Groups[2].Value[0] - 65;
                int col = int.Parse(helpmatch.Groups[3].Value) - 1;
                field.AskForHelp(row, col);
            }
            else if (match.Success)
            {

                int row = match.Groups[2].Value[0] - 65;
                int col = int.Parse(match.Groups[3].Value) - 1;
                int value = int.Parse(match.Groups[1].Value);
                field.UpdateTile(value, row, col);
            }
            else
            {
                Console.WriteLine("Incorrect input, try again.");
            }
        }

        private  String ReadInput()
        {
            try
            {
                return Console.ReadLine().ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cant read the input, try again");
                return "";
            }
        }

    }
}
