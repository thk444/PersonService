using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonServiceLibrary
{
    /*Assumptions:
     * 1. Not checking if record values are empty
     * 2. Screen output always in CSV
    */
    public sealed class PersonService
    {
        public List<Person> PersonRepository { get; set; }

        //Constructor
        private PersonService()
        {
            PersonRepository = new List<Person>();
        }

        private static PersonService instance = null;

        public static PersonService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonService();

                }
                return instance;
            }
        }

        //Parse single record
        public void ParsePersonRecord(string InputRecord)
        {
            List<string> PersonDetails_CommaSeparated = InputRecord.Split(',').ToList();
            List<string> PersonDetails_PipeSeparated = InputRecord.Split('|').ToList();
            List<string> PersonDetails_SpaceSeparated = InputRecord.Split(' ').ToList();

            if (PersonDetails_CommaSeparated.Count == 5 && PersonDetails_PipeSeparated.Count == 1 && PersonDetails_SpaceSeparated.Count == 1)
            {
                SavePerson(PersonDetails_CommaSeparated);
            }
            else if (PersonDetails_CommaSeparated.Count == 1 && PersonDetails_PipeSeparated.Count == 5 && PersonDetails_SpaceSeparated.Count == 1)
            {
                SavePerson(PersonDetails_PipeSeparated);
            }
            else if (PersonDetails_CommaSeparated.Count == 1 && PersonDetails_PipeSeparated.Count == 1 && PersonDetails_SpaceSeparated.Count == 5)
            {
                SavePerson(PersonDetails_SpaceSeparated);

            }
            else
            {
                throw new FormatException("Invalid file record format");
            }
        }

        //save to repository
        private void SavePerson(List<string> PersonDetails)
        {
            Person P = new Person();

            try
            {
                P.LastName = PersonDetails[0];
                P.FirstName = PersonDetails[1];
                P.Gender = PersonDetails[2];
                P.FavoriteColor = PersonDetails[3];
                P.DateofBirth = Convert.ToDateTime(PersonDetails[4]);
            }
            catch (FormatException)
            {

                throw;
            }

            PersonRepository.Add(P);

        }
        //Sort by Gender
        public List<Person> GetPersons_orderbyGender()
        {
            return PersonRepository.OrderBy(P => P.Gender).ToList();
        }

        //Sort by DOB
        public List<Person> GetPersons_orderbyBirthDate()
        {
            return PersonRepository.OrderBy(P => P.DateofBirth).ToList();
        }

        //Sort by LastName
        public List<Person> GetPersons_orderbyLastNameDescending()
        {
            return PersonRepository.OrderByDescending(P => P.LastName).ToList();
        }

        //Build Output string
        public string CreateOutput(List<Person> persons)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Person P in persons)
            {
                sb.Append(P.LastName + ",");
                sb.Append(P.FirstName + ",");
                sb.Append(P.Gender + ",");
                sb.Append(P.FavoriteColor + ",");
                sb.Append(P.DateofBirth.ToString("M/d/yyyy") + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
