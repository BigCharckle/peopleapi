using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeopleApi.BusinessObjects;
namespace PeopleApi.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        private const string _fileName = @".\MOCK_DATA.json";
        public IEnumerable<Person> GetAll()
        {
            var json = System.IO.File.ReadAllText(_fileName);

            return JsonConvert.DeserializeObject<List<Person>>(json);
        }
        public async Task<Person> AddPerson(Person person)
        {
            var json = await System.IO.File.ReadAllTextAsync(_fileName);
            var list = JsonConvert.DeserializeObject<List<Person>>(json);
            var maxId = list.Any() ? list.Max(x => (int)x.Id) : 0;
            var newId = maxId + 1;
            person.Id = newId;

            list.Add(person);
            await System.IO.File.WriteAllTextAsync(_fileName, JsonConvert.SerializeObject(list));
            return person;
        }
    }
}
