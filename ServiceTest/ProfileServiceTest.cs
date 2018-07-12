using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Data.DTO;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using System;
using System.Data.Entity;
using System.Linq;

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
        public void CreateEducaiton_Test()
        {
            //int profileId = 15;
            //var profile = _context.Profiles.First(p => p.Id == profileId);
            //var edu = new Education()
            //{
            //    Field = "math",
            //    GraduationYear = 1970,
            //    Level = "pro",
            //    Major = "some stuff",
            //    University = "University of fuck"
            //};
            //profile.Education = edu;
            //int result = _context.SaveChanges();
            //Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Profile_GetAddress()
        {
            var location = this._service.GetLocation(_context.Profiles.First().Id);
            Assert.IsTrue(location.Result.Location != null);
        }

        [TestMethod]
        public void Profile_AddCertificate()
        {
            //int id = 21;
            //int r = 0;
            //r += _service.AddCertificateAsync(id, 2014, "MCSDN").Result;
            //r += _service.AddCertificateAsync(id, 2014, "C++ Couch").Result;

            //Assert.IsTrue(r > 0);
        }

        [TestMethod]
        public void Profile_GetImages()
        {
            var id = 21;
            var all = this._service.GetImages(id).Result;
            Assert.IsTrue(all.Count > 0);
        }

        [TestMethod]
        public void Profile_GetBasicInfo()
        {
            var result = _service.GetBasicInfo(21).Result;
            Assert.IsTrue(!String.IsNullOrEmpty(result.Country));
        }


        [TestMethod]
        public void Profile_AddRelatedType()
        {
            int profielId = 21;
            int r = this._service.AddRelatedType(profielId, "Employee").Result;
            Assert.IsTrue(r > 0);
        }



        [TestMethod]
        public void Profile_AddRelated()
        {
            //var targetId = _context.Profiles.OrderBy(p => p.Name).First().Id;
            var targetId = 21;
            var sourceIds = _context.Profiles.OrderBy(p => p.Name).Skip(1).Select(p => p.Id).ToArray();
            foreach (var source in sourceIds)
            {
                _service.AddRelate(source, targetId, DateTime.Now, "some description", 2).Wait();
            }

            var profile = _context.Profiles.Include(p => p.Relatedes).First(p => p.Id == targetId);

            Assert.IsNotNull(profile.Relatedes.Count > 2);
        }

        [TestMethod]
        public void Profile_Search()
        {
            //DbGeography sourceLocation = GeoHelpers.FromLongitudeLatitude(88, 88);
            //return this._context.
            //    Profiles
            //    .Where(p => p.JobCategories.Any(jc => jc.Id == 43) && p.JobCategories.Any(j => j.Id == 1))
            //    .Where(p => p.Name.Contains("B"))
            //    .Where(p => p.Tags.Any(t => t.Name.Contains("B")))
            //    .Where(p => p.Address.Location.Distance(sourceLocation) < distance)
            //    .Select(p => new ProfileInfoDTO()
            //    {
            //        City = p.Address.City,
            //        Country = p.Address.Country,
            //        Description = p.Description,
            //        Latitude = p.Address.Location.Latitude,
            //        Longitude = p.Address.Location.Longitude,
            //        Name = p.Name
            //    })


            int jobCategory = 43, subJobCategory = 64, userId;
            userId = _context.Users.First().Id;
            //var dto = new CreateProfileDTO()
            //{
            //    Description = "BMW is a car manufacturer",
            //    Name = "BMW",
            //    Latitude = 87,
            //    Longitude = 87,
            //    City = "Berlin",
            //    Country = "Germany",
            //    PhoneNumber = "230489324",
            //    JobCategoryId = jobCategory,
            //    SubJobCategoryId = subJobCategory
            //};

            //int profileId = this._service.CreateWorkAsync(userId, dto).Result.Id;
            int profileId = 33;
            _service.AddTagAsync(profileId, new[] { "BMW" }).Wait();

            //var profiles = _service
            //    .SearchByCategoriesTagsAndDistance(jobCategory, subJobCategory, "B", 87, 87, 1).Result;


            //Assert.IsTrue(profiles.Count > 0);
        }

        [TestMethod]
        public void Profile_AddNetwork()
        {
            var networkId = this._context.SocialNetworks.First().Id;
            var profileId = this._context.Profiles.First().Id;

            int r = _service.AddSocialNetwork(profileId, networkId, "Http://t.me/hassanHashemi", "this is my acc").Result;

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
            //int profileId = _context.Profiles.First().Id;
            //int r = _service.EditEducation(profileId, new EducationEditDTO()
            //{
            //    Field = "Microbiology",
            //    GraduationYear = 2000,
            //    Level = "Bachelore",
            //    University = "MIT"
            //}).Result;
            //Assert.IsTrue(r > 0);
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
        public void Profile_GetTags()
        {
            var id = _context.Profiles.First().Id;

            var tags = this._service.GetTags(id).Result;
            Assert.IsTrue(tags.Count > 0);
        }

        [TestMethod]
        public void Profile_AddTag()
        {
            var id = 21;
            for (int i = 0; i < 10; i++)
            {
                string tag = Guid.NewGuid().ToString();
                _service.AddTagAsync(id, new[] { tag }).Wait();

            }

            Assert.IsTrue(
                _context.Profiles.Include("Tags")
                    .First(p => p.Id == id)
                        .Tags
                     .Count > 0
                );
        }

        [TestMethod]
        public void Profile_GetlatestComments()
        {
            var profileId = 21;
            var comments = _service.GetLatestComments(profileId).Result;
            Assert.IsTrue(comments.Count > 0);
        }

        [TestMethod]
        public void Profile_Edit()
        {
            //var profileId = this._context.Profiles.First(p => p.Id == 5).Id;
            //int r = this._service.EditProfile(profileId, "Manhatan", "US", "baseball couch").Result;
            //Assert.IsTrue(r > 0);
        }

        [TestMethod]
        public void Profile_CreateWork()
        {
            var categories = _service.QueryJobCategories().Result.OrderBy(j => Guid.NewGuid().ToString());
            int jobCategoryId = categories.Where(j => !j.IsSubCategory).First().Id;
            int subCategoryId = categories.First(j => j.IsSubCategory).Id;

            var dto = new CreateProfileDTO()
            {
                Description = "We build cars",
                Name = "Physics Inst",
                Latitude = 34.5,
                Longitude = 50,
                City = "Qom",
                Country = "Iran",
                PhoneNumber = "230489324",
                JobCategoryId = jobCategoryId,
                SubJobCategoryId = subCategoryId
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
                Longitude = 87
            };
            var userId = _context.Users.First().Id;
            var p = this._service.CreatePersonalAsync(userId, dto).Result;
            Assert.IsTrue(p.Id > 0);
        }

        [TestMethod]
        public void Profile_AddComment()
        {

        }

        [TestMethod]
        public void Profile_AddLike()
        {
            //this._service.AddLikeAsync(1, 3).Wait();
            //this._service.AddLikeAsync(2, 3).Wait();

            //Profile receiveTest =
            //    _context.Profiles
            //    .Include(p => p.Likes)
            //    .Include(p => p.ReceivedLikes).First(p => p.Id == 3);

            //Profile sentTest =
            //    _context.Profiles.Include(p => p.Likes).First(p => p.Id == 1);

            //Assert.IsTrue(receiveTest.ReceivedLikes.Count == 2);
            //Assert.IsTrue(sentTest.Likes.Count == 1);

            ////reload object from db
            //receiveTest =
            //    _context.Profiles
            //    .Include(p => p.Likes)
            //    .Include(p => p.ReceivedLikes).First(p => p.Id == 3);

            ////check senderId
            //Assert.IsTrue(receiveTest.ReceivedLikes.Any(l => l.SenderId == 1));
        }

        [TestMethod]
        public void Profile_AddImageCategory()
        {
            var profieID = 21;
            int r = this._service.AddImageCategory(profieID, "Offic").Result;
            Assert.IsTrue(r > 0);
        }

        [TestMethod]
        public void Profile_AddImage()
        {
            var profile = this._context
                .Profiles
                   .Include(p => p.ImageCategories)
                     .First();

            var catId = _context.ImageCategories.First(imc => imc.ProfileId == profile.Id).Id;

            int result = _service
                .AddProfileImageAsync(profile.Id, catId, "wedding image", "this is a wedding", "/ea/a2/a.jpg", new[] { 5, 6 }).Result;

            Assert.IsTrue(result > 0);

            var prof = _context.Profiles.Include("ImageCategories.Images").First(p => p.Id == profile.Id);
            Assert.IsTrue(prof.ImageCategories.Any(imc => imc.Images.Count > 0));
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
