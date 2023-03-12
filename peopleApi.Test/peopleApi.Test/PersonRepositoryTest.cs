using PeopleApi.Repositories;
using PeopleApi.BusinessObjects;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PeopleApi.Test
{

    public class PersonRepositoryTest
    {
        private IPersonRepository _repo;
        public PersonRepositoryTest()
        {
            _repo = new PersonRepository();
        }
        [Test]
        public async Task AddPersonTest()
        {
            var randomPerson = TestData.createTestPerson();
            var resultFromAdd = await _repo.AddPerson(randomPerson);
            if (resultFromAdd == null || resultFromAdd.Id == 0)
                Assert.Fail();
            var resultFromSearch = await _repo.GetPerson(resultFromAdd.Id);
            if (resultFromSearch == null)
                Assert.Fail();
            Assert.AreEqual(resultFromSearch.Id, resultFromAdd.Id);
        }
        /// <summary>
        /// Find a random person, delete it from file, then try to search it, verify search result is null.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeletePersonTest()
        {
            var lst =(await _repo.GetAll()).ToList();
            var random = new System.Random();
            var randomPersonToDelete = lst[random.Next(0, lst.Count - 1)];

            await _repo.DeletePerson(randomPersonToDelete.Id);
            var searchResult = await _repo.GetPerson(randomPersonToDelete.Id);
            Assert.IsNull(searchResult);
        }

        [Test]
        public async Task TogglePersonTest()
        {
            var lst = (await _repo.GetAll()).ToList();
            var random = new System.Random();
            var randomPersonToToggle = lst[random.Next(0, lst.Count - 1)];
            await _repo.ToggleStatus(randomPersonToToggle.Id);

            var searchResult = await _repo.GetPerson(randomPersonToToggle.Id);

            Assert.IsNotNull(searchResult);

            Assert.AreEqual(randomPersonToToggle.ToDTO().Status, !searchResult.ToDTO().Status);
        }

     }
}
