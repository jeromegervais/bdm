using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BDM.Data.Client.Net.WebServices;
using BDM.Data.Client.Mock;
using System.Threading.Tasks;

namespace BDM.Data.Client.Tests
{
    [TestClass]
    public class WsTest
    {
        private static BlaguesService _blaguesService;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _blaguesService = new BlaguesService(new MockAccessIsolatedStorage());
        }

        [TestMethod]
        public void TestWeCanGetBlagues()
        {
            var popoy = _blaguesService.GetBlagues().Result;
        }

        [TestMethod]
        public void TestWeCanGetCategories()
        {
            var popoy = _blaguesService.GetCategories().Result;
        }

        [TestMethod]
        public void TestWeCanGetBlaguesForCategories()
        {
            var popoy = _blaguesService.GetBlaguesForCategory(19).Result;
        }

        //429351
        [TestMethod]
        public void TestWeCanVoteForABlague()
        {
            var popoy = _blaguesService.Vote(429351, true).Result;
        }
    }
}
