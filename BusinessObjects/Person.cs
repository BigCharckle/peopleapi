
using System;
using PeopleApi.DTO;
namespace PeopleApi.BusinessObjects
{
    public class Person
    {
        public int Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

    }
    /// <summary>
    /// using method extension to do DTO conversion, this can be done through mapper tool
    /// </summary>
    public static class PersonExtension
    {
        public static PersonDTO ToDTO(this Person person)
        {
            bool status;
            var result = bool.TryParse(person.Status.ToString().ToLower(), out status);
            return new PersonDTO
            {
                Id = person.Id,
                First_Name = person.First_Name,
                Last_Name = person.Last_Name,
                Email = person.Email,
                Gender = (Genders)Enum.Parse(typeof(Genders), person.Gender),
                Status = status
            };
        }
    }
}
