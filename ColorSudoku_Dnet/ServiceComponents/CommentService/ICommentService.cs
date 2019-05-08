using System;
using System.Collections.Generic;
using System.Text;

namespace ColorSudoku_Dnet.ServiceComponents.CommentService
{
    public interface ICommentService
    {
        void AddComment(Comment comment);

        List<Comment> GetLastComments();

        void RemoveComment(String name);

        void ClearComments();
    }
}
