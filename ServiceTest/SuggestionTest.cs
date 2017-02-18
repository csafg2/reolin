using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.DTO;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using System;

namespace ServiceTest
{
    [TestClass]
    public class SuggestionTests
    {
        private DataContext _context = new DataContext();
        private ISuggestionService _service;

        public SuggestionTests()
        {
            _service = new SuggestionService(_context);
        }

        [TestMethod]
        public void Suggestion_TestCreate()
        {
            var result = _service.AddSuggestion(new SuggestionCreateModel()
            {
                Description = "some Description for #bmw",
                From = DateTime.Now,
                ProfileId = 21,
                Title = "prices are low now!!",
                To = DateTime.Now.AddDays(5)
            }).Result;

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Suggestion_AddTag()
        {
            int r = _service.AddTag(1, "bmw!").Result;
            Assert.IsTrue(r > 0);
        }
    }
}
