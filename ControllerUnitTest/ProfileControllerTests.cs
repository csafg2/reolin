using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Controllers;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.ViewModels;
using System.IO;
using System.Linq;

namespace ControllerUnitTest
{
    [TestClass]
    public class ProfileControllerTests2
    {
        [TestMethod]
        public void ProfilerController_AddImageAction()
        {
            ////Arrange:
            //IProfileService service = new ProfileService(new DataContext());
            //string basePath = @"E:\data";
            //IFileService fileService = null;// new FileService(new TwoCharDirectoryProvider(), HostingEnvironment);
            //ProfileController controller = new ProfileController(service, null, fileService, null);
            //int id = new DataContext().Profiles.First().Id;
            //Mock<IFormFile> fileMock = new Mock<IFormFile>();

            //string filePath = @"E:\Sample.txt";
            //FileStream stream = new FileStream(filePath, FileMode.Open);
            //fileMock.Setup(m => m.OpenReadStream()).Returns(stream);
            //fileMock.Setup(m => m.FileName).Returns(Path.GetFileName(filePath));
            //IFormFile file = fileMock.Object;

            ////Act
            //var result = controller.AddImage(
            //    new AddImageToProfileViewModel() { ProfileId = id },
            //     file ).Result;


            //// assert:
            //Assert.IsTrue(result is OkResult);
        }
    }
}
