using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class JobCategoryTests
    {
        DataContext _context = new DataContext();
        IJobCategoryService _service;

        public JobCategoryTests()
        {
            this._service = new JobCategoryService(_context);
        }

        [TestMethod]
        public void JobCategoryService_GetAll()
        {
            var item = _service.GetAllAsync().Result;

            Assert.IsTrue(item.Count > 0);
        }
    }
}
