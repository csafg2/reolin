using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.DTO;
using Reolin.Data.Services;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;

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
        public void Profile_GetByTag()
        {
            var all = this._service.GetByTagAsync("C++").ToList();
            Assert.IsTrue(all.Count > 0);
        }

        [TestMethod]
        public void Profile_GetRelated()
        {
            var data = this._service.GetRelatedProfiles(1).Result;
            var items = data.ToList();
            Assert.IsTrue(items.Count() > 0);
        }

        [TestMethod]
        public void Profile_GetInDistance()
        {
            string[] tags = new[] { "C++",
               "Programming",
               "restaurent",
               "Hellow",
               "J++",
               "shop",
               "web development" };
            double distance = 1000;
            var service = new ProfileLocationService(this._context);
            foreach (var item in tags)
            {
                var r = service.GetByDistance(item, 50, 50, distance).Result.ToList();
                if (r.Count > 0)
                {
                    Assert.IsTrue(true);
                }
            }

        }

        [TestMethod]
        public void Profile_AddTag()
        {
            var id = _context.Profiles.First().Id;
            _service.AddTagAsync(id, new[] { "Soldier" }).Wait();
        }

        [TestMethod]
        public void Profile_Create()
        {
            var dto = new CreateProfileDTO()
            {
                Description = "the #Musician",
                Name = "Yanni",
                Latitude = 87,
                Longitude = 87
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

            var result = this._service.AddLikeAsync(user.Id, profile.Id).Result;
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
