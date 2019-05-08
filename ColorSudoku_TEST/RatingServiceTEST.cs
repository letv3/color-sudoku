using System;
using System.Collections.Generic;
using System.Text;
using ColorSudoku_Dnet.ServiceComponents.RatingService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorSudoku_TEST
{
    [TestClass]
    public class RatingServiceTEST
    {
        private IRatingService service = new RatingServiceList();

        public void Init()
        {
            service.ClearRatings();
        }

        [TestMethod]
        public void TestClearRatings()
        {
            service.AddRating(new Rating { Name = "letv", Mark = 5, TimeOfRating = DateTime.Now });

            service.ClearRatings();

            Assert.AreEqual<int>(0, service.GetLastRatings().Count);
        }

        [TestMethod]
        public void TestAddRating()
        {
            service.AddRating(new Rating { Name = "letv", Mark = 5, TimeOfRating = DateTime.Now });

            var rates = service.GetLastRatings();

            Assert.AreEqual<int>(1, service.GetLastRatings().Count);

            Assert.AreEqual<string>("letv", rates[0].Name);
            Assert.AreEqual<int>(5, rates[0].Mark);
        }


        [TestMethod]
        public void TestGetLastRatings()
        {
            service.AddRating(new Rating { Name = "letv", Mark = 5, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "letv1", Mark = 1, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "letv2", Mark = 2, TimeOfRating = DateTime.Now });
            service.AddRating(new Rating { Name = "letv3", Mark = 3, TimeOfRating = DateTime.Now });

            var rates = service.GetLastRatings();

            Assert.AreEqual<int>(3, rates.Count);

            Assert.AreEqual<string>("letv", rates[0].Name);
            Assert.AreEqual<int>(5, rates[0].Mark);

            Assert.AreEqual<string>("letv3", rates[1].Name);
            Assert.AreEqual<int>(3, rates[1].Mark);

            Assert.AreEqual<string>("letv2", rates[2].Name);
            Assert.AreEqual<int>(2, rates[2].Mark);

        }
    }
}
