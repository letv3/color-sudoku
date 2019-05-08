using ColorSudoku_Dnet.ServiceComponents.CommentService;
using ColorSudoku_Dnet.ServiceComponents.RatingService;
using ColorSudoku_Dnet.ServiceComponents.ScoreService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSudoku_Dnet
{
   
        public class SudokuContext : DbContext
        {
            public DbSet<Score> Scores { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Rating> Ratings { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Database_Sudoku;Trusted_Connection=True;");
            }
        }
    

}
