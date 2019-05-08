using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorSudoku_Dnet.ServiceComponents.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColorSudoku_WEB.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService = new CommentServiceDatabase();

        //api/Comments/letv
        [HttpPost]
        public void AddComment([FromBody] Comment comment)
        {
            _commentService.AddComment(comment);
        }

        //api/Comments/letv
        [HttpGet]
        public IEnumerable<Comment> GetLastComments()
        {
            return _commentService.GetLastComments();
        }

        //api/Comments/letv
        [HttpDelete("{name}")]
        public void RemoveComment([FromRoute] string name)
        {
            _commentService.RemoveComment(name);
        }

        //api/Comments
        [HttpDelete]
        //[HttpDelete("/clear")]
        public void ClearComments()
        {
            _commentService.ClearComments();
        }

        

    }
}