using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Web.Api.Controllers;
using Reolin.Data.Services.Core;
using Reolin.Data.Services;
using Reolin.Data;
using Reolin.Web.Api.Infra.IO;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Reolin.Web.Api.ViewModels;

namespace ControllerUnitTest
{
    [TestClass]
    public class ProfileControllerTests2
    {
        [TestMethod]
        public void ProfilerController_AddImageAction()
        {
            //Arrange:
            IProfileService service = new ProfileService(new DataContext());
            string basePath = @"E:\data";
            IFileService fileService = new FileService(basePath, new TwoCharDirectoryProvider());
            ProfileController controller = new ProfileController(service, null, fileService);
            int id = new DataContext().Profiles.First().Id;
            Mock<IFormFile> fileMock = new Mock<IFormFile>();

            string filePath = @"E:\Sample.txt";
            FileStream stream = new FileStream(filePath, FileMode.Open);
            fileMock.Setup(m => m.OpenReadStream()).Returns(stream);
            fileMock.Setup(m => m.FileName).Returns(Path.GetFileName(filePath));
            IFormFile file = fileMock.Object;

            //Act
            var result = controller.AddImage(
                new AddImageToProfileViewModel() { ProfileId = id },
                new[] { file }).Result;


            // assert:
            Assert.IsTrue(result is OkResult);
        }
    }
}
