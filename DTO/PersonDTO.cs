using PeopleApi.BusinessObjects;
namespace PeopleApi.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string Email { get; set; }
        public Genders Gender { get; set; }
        public bool? Status { get; set; }

        
    }
    /// <summary>
    /// using method extension to do BO conversion, this can be done through mapper tool
    /// </summary>
    public static class PersonDTOExtension
    {
        public static Person ToBO(this PersonDTO personDTO)
        {
            return new Person
            {
                Id = personDTO.Id,
                First_Name = personDTO.First_Name,
                Last_Name = personDTO.Last_Name,
                Email = personDTO.Email,
                Gender = personDTO.Gender.ToString(),
                Status = personDTO.Status?.ToString() 
            };
        }
    }
    public enum Genders
    {
        Male,
        Female
    }
}
