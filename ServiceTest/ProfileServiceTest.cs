using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Services;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class ProfileServiceTest
    {
        private DataContext _context;
        private ProfileService _service;

        public ProfileServiceTest()
        {
            this._context = new DataContext();
            this._service = new ProfileService(_context);
        }

        [TestMethod]
        public void TestAddLike()
        {
            var profile = this._context.Profiles.First();
            var user = this._context.Users.First();

            var result = this._service.AddLikeAsync(user.Id, profile.Id).Result;
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void TestAddImage()
        {
            var profileId = this._context.Profiles.First().Id;
            int result = _service.AddProfileImageAsync(profileId, @"\99\100\2.jpg").Result;
            
            Assert.IsTrue(result > 0);
        }
    }
}
