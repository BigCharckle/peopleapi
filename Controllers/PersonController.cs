using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public PersonController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        [HttpGet("getall")]
        public IEnumerable<Person> GetAll()
        {
            return _personRepo.GetAll();
        }

        [HttpPost("add")]
        public async Task AddAsync(Person person)
        {

                await _personRepo.AddPerson(person);

        }
    }
}
