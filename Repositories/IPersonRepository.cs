using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleApi.BusinessObjects;
namespace PeopleApi.Repositories
{
    public interface IPersonRepository
    {
        public IEnumerable<Person> GetAll();
        public Task AddPerson(Person person);
    }
}
