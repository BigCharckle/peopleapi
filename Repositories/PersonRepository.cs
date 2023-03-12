using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeopleApi.BusinessObjects;
using PeopleApi.DTO;
namespace PeopleApi.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        private const string _fileName = @".\MOCK_DATA.json";
        /// <summary>
        /// get the list of DTO from "database"(the json file), then convert to the list of BO and return it.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetAll()
        {
            var lstDTO = await GetAllDTO();
            var targetList = lstDTO
              .Select(x => x.ToBO())
              .ToList();
            return targetList;
        }
        /// <summary>
        /// convert the person BO to DTO, then persistent it to "database"
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<Person> AddPerson(Person person)
        {
            var list = await GetAllDTO();
            var maxId = list.Any() ? list.Max(x => (int)x.Id) : 0;
            var newId = maxId + 1;
            person.Id = newId;

            var dto = person.ToDTO();
            list.Add(dto);
            await System.IO.File.WriteAllTextAsync(_fileName, JsonConvert.SerializeObject(list));
            return person;
        }
        public async Task ToggleStatus(int id)
        {
            var list = await GetAllDTO();
            var personDTO = list.Where(p => p.Id == id).FirstOrDefault();
            if (personDTO != null)
                personDTO.Status = !personDTO.Status;

            await System.IO.File.WriteAllTextAsync(_fileName, JsonConvert.SerializeObject(list));
        }

        public async Task DeletePerson(int id)
        {
            var list = await GetAllDTO();
            var dto = list.Where(p => p.Id == id).FirstOrDefault();
            if(dto !=null)
                list.Remove(dto);
            await System.IO.File.WriteAllTextAsync(_fileName, JsonConvert.SerializeObject(list));
        }

        private async Task<List<PersonDTO>> GetAllDTO()
        {
            var json = await System.IO.File.ReadAllTextAsync(_fileName);
            return JsonConvert.DeserializeObject<List<PersonDTO>>(json);
        }


    }
}
