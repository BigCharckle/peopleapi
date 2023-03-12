using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleApi.BusinessObjects;
namespace PeopleApi.Repositories
{
    public interface IPersonRepository
    {
        public Task<IEnumerable<Person>> GetAll();
        public Task<Person> AddPerson(Person person);
        public Task DeletePerson(int id);
        public Task ToggleStatus(int id);

    }
}
