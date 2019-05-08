using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorSudoku_Dnet.ServiceComponents.CommentService;
using System;

namespace ColorSudoku_TEST
{
    [TestClass]
    class CommentServiceTEST
    {
        private CommentServiceList service = new CommentServiceList();


        [TestMethod]
        public void Init()
        {
            service.ClearComments();
        }

        [TestMethod]
        public void TestClearComments()
        {
            service.AddComment(new Comment { Name = "lettv", Message = "ok" });
            service.AddComment(new Comment { Name = "Katka", Message = "i thought it will be better" });

            service.ClearComments();

            Assert.AreEqual<int>(0, service.GetLastComments().Count);
        }

        [TestMethod]
        public void TestAddComment()
        {
            service.AddComment(new Comment { Name = "Janko", Message = "not bad" });

            var comments = service.GetLastComments();

            Assert.AreEqual<int>(1, service.GetLastComments().Count);

            Assert.AreEqual<string>("Janko", comments[0].Name);
            Assert.AreEqual<string>("not bad", comments[0].Message);
        }

        [TestMethod]
        public void TestGetLastComments()
        {
            service.AddComment(new Comment { Name = "letv", Message = "qwqr", TimeOFComment = DateTime.Now });
            service.AddComment(new Comment { Name = "letv1", Message = "qwqr1", TimeOFComment = DateTime.Now });
            service.AddComment(new Comment { Name = "letv2", Message = "qwqr2", TimeOFComment = DateTime.Now });
            service.AddComment(new Comment { Name = "letv3", Message = "qwqr3", TimeOFComment = DateTime.Now });

            var comments = service.GetLastComments();

            Assert.AreEqual<int>(3, service.GetLastComments().Count);

            Assert.AreEqual<string>("letv3", comments[0].Name);
            Assert.AreEqual<string>("qwqr3", comments[0].Message);

            Assert.AreEqual<string>("letv2", comments[1].Name);
            Assert.AreEqual<string>("qwqr2", comments[1].Message);

            Assert.AreEqual<string>("letv1", comments[2].Name);
            Assert.AreEqual<string>("qwqr1", comments[2].Message);


        }

        public void TestRemoveComment()
        {
            service.AddComment(new Comment { Name = "letv", Message = "qwqr", TimeOFComment = DateTime.Now });
            service.AddComment(new Comment { Name = "letv1", Message = "qwqr1", TimeOFComment = DateTime.Now });
            service.AddComment(new Comment { Name = "letv2", Message = "qwqr2", TimeOFComment = DateTime.Now });
            service.AddComment(new Comment { Name = "letv3", Message = "qwqr3", TimeOFComment = DateTime.Now });

            service.RemoveComment("letv3");


            Assert.AreEqual<Comment>(null, service.GetComment(3));

        }

    }
}
