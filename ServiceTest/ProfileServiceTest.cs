using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.DTO;
using Reolin.Data.Services;
using System.Linq;
using System.Data.Entity;
using Reolin.Data.Services.Core;
using System;

namespace ServiceTest
{
    [TestClass]
    public class ProfileServiceTest
    {
        private DataContext _context;
        private IProfileService _service;

        public ProfileServiceTest()
        {
            this._context = new DataContext();
            this._service = new ProfileService(_context);
        }


        [TestMethod]
        public void Profile_AddNetwork()
        {
            var networkId = this._context.SocialNetworks.First().Id;
            var profileId = this._context.Profiles.First().Id;

            int r = _service.AddSocialNetwork(profileId, networkId, "Http://t.me/hassanHashemi").Result;

            Assert.IsTrue(r > 0);
            var profile = _context
                .Profiles
                .Include(p => p.Networks)
                .First(p => p.Id == profileId);
            Assert.IsTrue(profile.Networks.Count > 0);
        }

        [TestMethod]
        public void Profile_EditEducation()
        {
            int profileId = _context.Profiles.First().Id;
            int r = _service.EditEducation(profileId, new EducationEditDTO()
            {
                Field = "Microbilogy",
                GraduationYear = 2000,
                Level = "Bachelore",
                University = "MIT"
            }).Result;
            Assert.IsTrue(r > 0);
        }


        [TestMethod]
        public void Profile_UpdateLocation()
        {
            var profileId = _context.Profiles.FirstAsync(p => p.Id % 2 == 0).Result.Id;

            var result = _service.UpdateLocaiton(profileId, 35, 35).Result;

            Assert.IsTrue(result > 0);
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
                var r = service.GetInRangeAsync(item, 50, 50, distance).Result.ToList();
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
            _service.AddTagAsync(id, new[] { "C#" }).Wait();
        }

        [TestMethod]
        public void Profile_GetlatestComments()
        {
            var profileId = 6;
            var comments = _service.GetLatestComments(profileId).Result;
            Assert.IsTrue(comments.Count > 0);
        }

        
        [TestMethod]
        public void Profile_Edit()
        {
            var profileId = this._context.Profiles.First(p => p.Id == 5).Id;
            int r = this._service.EditProfile(profileId, "Manhatan", "US", "baseball couch").Result;
            Assert.IsTrue(r > 0);
        }

        [TestMethod]
        public void Profile_CreateWork()
        {
            var dto = new CreateProfileDTO()
            {
                Description = "Mortazavi Restaurent is a nice thing",
                Name = "Mortazavi Restaurent has every food you want",
                Latitude = 83,
                Longitude = 35,
                City = "Qom",
                Country = "Iran",
                PhoneNumber = "230489324",
                JobCategoryId = 1
            };
            var userId = _context.Users.First().Id;
            var p = this._service.CreateWorkAsync(userId, dto).Result;
            Assert.IsTrue(p.Id > 0);
        }

        [TestMethod]
        public void Profile_CreatePersonal()
        {
            var dto = new CreateProfileDTO()
            {
                Description = "software architect",
                Name = "Hassan",
                Latitude = 87,
                Longitude = 87,
                JobCategoryId = null
            };
            var userId = _context.Users.First().Id;
            var p = this._service.CreatePersonalAsync(userId, dto).Result;
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

        [TestMethod]
        public void Profile_AddSkill()
        {
            var profileId = _context.Profiles.FirstOrDefault().Id;

            //random skill
            int r = this._service.AddSkill(profileId, Guid.NewGuid().ToString()).Result;


            Assert.IsTrue(r > 0);
        }
    }
}
