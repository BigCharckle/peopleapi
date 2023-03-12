using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.BusinessObjects;
using PeopleApi.Repositories; 
namespace PeopleApi.Controllers
{

    [ApiController]
    [Route("peopleapi/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonRepository _personRepo;
        private IMemoryCache _cache;
        public PersonController(IPersonRepository personRepo,  IMemoryCache cache)
        {
            _personRepo = personRepo;
            _cache = cache;
        }

        /// <summary>
        /// If cache is empty, then get list from db and set cache, and return the list
        /// otherwise directly return the list from cache
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IEnumerable<Person>> GetAll()
        {

            if(!_cache.TryGetValue(CacheKeys.PeopleList, out IEnumerable<Person> peopleList))
            {
                peopleList =  await updateCache();
            }
            return peopleList;
        }

        [HttpPost("add")]
        public async Task<Person> Add(Person person)
        {
            var personBo = await _personRepo.AddPerson(person);
            await updateCache();
            return personBo;
        }

        [HttpPost("togglestatus")]
        public async Task ToggleStatus(Person person)
        {

            await _personRepo.ToggleStatus(person.Id);
            await updateCache();

        }
        [HttpPost("delete")]
        public async Task Delete(Person person)
        {

            await _personRepo.DeletePerson(person.Id);
            await updateCache();

        }

        private async Task<IEnumerable<Person>> updateCache()
        {
            var peopleList = await _personRepo.GetAll(); // Get the data from database
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };
            _cache.Set(CacheKeys.PeopleList, peopleList, cacheEntryOptions);
            return peopleList;
        }
    }
}
