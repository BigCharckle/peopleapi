using PeopleApi.Repositories;
using PeopleApi.BusinessObjects;
using PeopleApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Extensions.Caching.Memory;
using Moq;
namespace PeopleApi.Test
{
    public class PersonControllerTest
    {
        private Mock<IPersonRepository> _personReopMoq;
        private IMemoryCache _cache;
        private PersonController _personController;
        private Person _mockAddedPerson;

        public PersonControllerTest()
        {
            _personReopMoq = new Mock<IPersonRepository>();
            var options = new MemoryCacheOptions()
            {
                CompactionPercentage = 0.5,
                ExpirationScanFrequency = new TimeSpan(1000),
                SizeLimit = 1024

            };
            _cache = new MemoryCache(options);
            _personController = new PersonController(_personReopMoq.Object, _cache);

            var mockList = TestData.getMockList();

            _mockAddedPerson = TestData.createTestPerson();
            _mockAddedPerson.Id = 100;

            _personReopMoq.Setup(r => r.AddPerson(It.IsAny<Person>())).Returns(Task.FromResult(_mockAddedPerson));
            _personReopMoq.Setup(r => r.GetAll()).Returns(Task.FromResult<IEnumerable<Person>>(mockList));
            _personReopMoq.Setup(r => r.ToggleStatus(It.IsAny<int>())).Returns(Task.FromResult(true));
            _personReopMoq.Setup(r => r.DeletePerson(It.IsAny<int>())).Returns(Task.FromResult(true));
            //_cacheMoq.Setup(c => c.Set(It.IsAny<string>(), It.IsAny<IEnumerable<Person>>(), It.IsAny<MemoryCacheEntryOptions>())).Returns(mockList);
        }

        [Test]
        public async Task PersonController_AddPerson_Test()
        {
            var mockNewPerson = TestData.createTestPerson();
            var result = await _personController.Add(mockNewPerson);
            Assert.AreEqual(_mockAddedPerson.Id, result.Id);
        }

        [Test]
        public async Task PersonController_GetAll_Test()
        {
            var expectedList = TestData.getMockList();
            var resultList = (await _personController.GetAll()).ToList();

            Assert.NotNull(resultList);
            Assert.AreEqual(expectedList.Count(), resultList.Count());
            Assert.AreEqual(expectedList[0].Id, resultList[0].Id);
            Assert.AreEqual(expectedList[expectedList.Count()-1].Id, resultList[resultList.Count()-1].Id);
        }

        [Test]
        public async Task PersonController_ToggleStatus_Test()
        {
            var mockPerson = TestData.createTestPerson();
            mockPerson.Id = 8;
            await _personController.ToggleStatus(mockPerson);

            Assert.Pass();
        }

        [Test]
        public async Task PersonController_DeletePerson_Test()
        {
            var mockPerson = TestData.createTestPerson();
            mockPerson.Id = 8;
            await _personController.Delete(mockPerson);

            Assert.Pass();
        }
    }
}
