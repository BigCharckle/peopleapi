
using System.ComponentModel.DataAnnotations;
namespace PeopleApi.BusinessObjects
{
    public class Person
    {
        public int Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string Email { get; set; }
        public string Gender { get; set; }
        public bool? Status { get; set; }

        
    }
    public enum Genders
    {
        Male,
        Female
    }
}
