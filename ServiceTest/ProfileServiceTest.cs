using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class ProfileServiceTest
    {
        [TestMethod]
        public void TestAddImage()
        {
            var context = new DataContext();
            var profileId = context.Profiles.First().Id;
            IPorofileService service = new ProfileService(context);
            service.AddProfileImageAsync(profileId, @"\99\100\2.jpg").Wait();
            
            
        }
    }
}
