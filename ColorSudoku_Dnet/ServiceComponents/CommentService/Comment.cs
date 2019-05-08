using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSudoku_Dnet.ServiceComponents.CommentService
{
    [Serializable]
    public class Comment
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Message { get; set; }

        public  DateTime TimeOFComment { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Message)}: {Message}, {nameof(TimeOFComment)}: {TimeOFComment}";
        }
    }
}
