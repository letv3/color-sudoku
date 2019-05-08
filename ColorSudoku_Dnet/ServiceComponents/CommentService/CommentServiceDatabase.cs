using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.CommentService
{
    public class CommentServiceDatabase : ICommentService
    {


        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ServiceException("Comment must be not null!");
            if (comment.Name == null)
                throw new ServiceException("Comment contains null Name!");
            if (comment.Message == null)
                throw new ServiceException("Comment contains null Message!");

            using (var db = new SudokuContext())
            {
                db.Add(comment);
                db.SaveChanges();
            }
        }

        public void ClearComments()
        {
            using (var db = new SudokuContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Comments");
            }
        }



        public List<Comment> GetLastComments()
        {
            using (var db = new SudokuContext())
            {
                return (from s in db.Comments orderby s.TimeOFComment descending select s)
                    .Take(5).ToList();

            }

        }
           

        public void RemoveComment(string name)
        {
            //var sql="DELETE FROM Comment WHERE Name= {}"

            using (var db = new SudokuContext())
            {
                db.Remove(db.Comments.First(c => c.Name == name));
                db.SaveChanges();
            }
        }
    }
}
