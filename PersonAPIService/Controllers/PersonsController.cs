using PersonServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonAPIService.Controllers
{
    public class PersonsController : ApiController
    {
        private PersonService PS = PersonService.GetInstance;

        // GET api/values
        public List<Person> Get()
        {

            return PS.GetPersons_orderbyLastNameDescending();
        }

        // GET api/values/5
        public List<Person> Get(string SortType)
        {
            switch (SortType.ToLower())
            {
                case "gender":
                    {
                        return PS.GetPersons_orderbyGender();

                    }
                case "dateofbirth":
                case "dob":
                    {
                        return PS.GetPersons_orderbyBirthDate();

                    }
                case "lastname":
                case "name":
                    {
                        return PS.GetPersons_orderbyLastNameDescending();

                    }
                default:
                    {
                        return PS.GetPersons_orderbyLastNameDescending();
                    }
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            PS.ParsePersonRecord(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
