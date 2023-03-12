using System;
using System.Collections.Generic;
using System.Text;
using PeopleApi.BusinessObjects;
namespace PeopleApi.Test
{
    public static class TestData
    {

        /// <summary>
        /// generate a person record with random data
        /// </summary>
        /// <returns></returns>
        public static Person createTestPerson()
        {
            var first_Name = Guid.NewGuid().ToString().Substring(0, 8);
            var last_Name = Guid.NewGuid().ToString().Substring(0, 4);
            var random = new System.Random();

            bool randomBool = false;
            if (random.Next(0, 1) == 1)
            {
                randomBool = true;
            }

            return new Person()
            {
                First_Name = first_Name,
                Last_Name = last_Name,
                Email = $"{first_Name}.{last_Name}@hotmail.com",
                Gender = (randomBool == true) ? "Male" : "Female",
                Status = (randomBool == true) ? "True" : "False"
            };
        }

        public static Person[] getMockList()
        {

            return new[]
 { new Person()
{
    Id = 1,
    First_Name = "Chester", Last_Name = "Bruggen",Email ="cbruggen0@google.ru",Gender ="Male",Status = "True"},
new Person
{
    Id = 2,
    First_Name = "Gery", Last_Name = "Sparkes",Email ="gsparkes1@geocities.jp",Gender ="Male",Status = "False"},
new Person
{
    Id = 3,
    First_Name = "Lia", Last_Name = "Cremin",Email ="lcremin2@theglobeandmail.com",Gender ="Female",Status = "False"},
new Person
{
    Id = 4,
    First_Name = "Rosita", Last_Name = "Barkworth",Email ="rbarkworth3@mediafire.com",Gender ="Female",Status = "True"},
new Person
{
    Id = 5,
    First_Name = "Killy", Last_Name = "Ghelarducci",Email ="kghelarducci4@marketwatch.com",Gender ="Male",Status = "True"},
new Person
{
    Id = 6,
    First_Name = "Zachariah", Last_Name = "Keirle",Email ="zkeirle5@people.com.cn",Gender ="Male",Status = "True"},
new Person
{
    Id = 7,
    First_Name = "Link", Last_Name = "Tarbert",Email ="ltarbert6@sciencedaily.com",Gender ="Male",Status = "False"},
new Person
{
    Id = 20,
    First_Name = "Malcolm", Last_Name = "Kenson",Email ="mkensonj@senate.gov",Gender ="Male",Status = "True"
} };

        }
    }
}
