using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.DTO;
using Reolin.Data.Services;
using System.Linq;
using System.Threading.Tasks;

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
        public void Profile_AddTag()
        {
            var id = _context.Profiles.First().Id;
            _service.AddTagAsync(id, new[] { "restaurent", "Hellow" }).Wait();
        }

        [TestMethod]
        public void Profile_Create()
        {
            var dto = new CreateProfileDTO()
            {
                Description = "i am a #software #engineer",
                Name = "Hassan hashemi",
                Latitude = 50,
                Longitude = 50
            };
            var userId = _context.Users.First().Id;
            var p = this._service.CreateAsync(userId, dto).Result;
            Assert.IsTrue(p.Id > 0);
        }

        [TestMethod]
        public void Profile_AddLike()
        {
            var profile = this._context.Profiles.First();
            var user = this._context.Users.First();
            
            var result = this._service.AddLikeAsync (user.Id, profile.Id).Result;
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Profile_AddImage()
        {
            var profileId = this._context.Profiles.First().Id;
            int result = _service.AddProfileImageAsync(profileId, @"\99\100\2.jpg").Result;
            
            Assert.IsTrue(result > 0);
        }
    }
}
