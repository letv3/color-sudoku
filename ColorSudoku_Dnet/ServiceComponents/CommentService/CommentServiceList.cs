using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.CommentService
{
    public class CommentServiceList : ICommentService
    {
        private List<Comment> comments = new List<Comment>();
        public void AddComment(Comment comment)
        {
            if (comment == null) throw new ServiceException("Comment must be not null!");
            if (comment.Name == null || comment.Message == null) throw new ServiceException("Comment contains null Name or Message!");
            comments.Add(comment);

        }

        public List<Comment> GetLastComments()
        {
            //comments.Reverse();
            //List<Comment> LastThreeComments = comments.Take(3).ToList();
            return comments.OrderByDescending(s => s.TimeOFComment).Take(3).ToList();
            //return LastThreeComments;
            //OrderBy(i => i.TimeOFComment)
        }

        public void RemoveComment(string name)
        {
            foreach(Comment c in comments){
                if (c.Name.Equals(name)) comments.Remove(c);
            }
        }

        public void ClearComments()
        {
            comments.Clear();
        }

        public Comment GetComment(int index)
        {
            return comments[index];
        }
    }
}
