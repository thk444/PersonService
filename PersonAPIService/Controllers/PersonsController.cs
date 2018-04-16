using PersonServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonAPIService.Controllers
{
        /*Assumptions:
         * 1. We don't have unique identifier to identify each record in data. So for simplicity, Put and Delete actions are depending on last name. 
      */
    public class PersonsController : ApiController
    {
        public PersonService PS = PersonService.GetInstance;

        // GET api/values
        public IHttpActionResult Get()
        {

            try
            {
                return Ok(PS.PersonRepository.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET api/values/{sortby}
        public IHttpActionResult Get(string input)
        {
            try
            {
                switch (input.ToLower())
                {
                    case "gender":
                        {
                            return Ok(PS.GetPersons_orderbyGender());

                        }
                    case "dateofbirth":
                    case "dob":
                        {
                            return Ok(PS.GetPersons_orderbyBirthDate());

                        }
                    case "lastname":
                    case "name":
                        {
                            return Ok(PS.GetPersons_orderbyLastNameDescending());

                        }
                    default:
                        {
                            return Content(HttpStatusCode.NotFound, "Invalid sort type");
                        }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]string value)
        {
            try
            {
                if (value == null | value == string.Empty)
                { 
                    return BadRequest("Person record cannot be null or empty");
                }
                else
                { 
                    PS.ParsePersonRecord(value);
                    return Created(new Uri(Request.RequestUri.ToString()), value);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/values/{input}
        public IHttpActionResult Put(string input, [FromBody]string value)
        {
            try
            {
                Person P = PS.PersonRepository.FirstOrDefault(Person => Person.LastName == input);
                if (P == null)
                {
                    return Content(HttpStatusCode.NotFound, "Person Not found");
                }
                else
                {
                    PS.PersonRepository.RemoveAll(Person => Person.LastName == input);
                    PS.ParsePersonRecord(value);
                    return Content(HttpStatusCode.OK, "Person with last name " + input + " updated");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/values/{input}
        public IHttpActionResult Delete([FromUri]string input)
        {
            try
            {
                Person P = PS.PersonRepository.FirstOrDefault(Person => Person.LastName == input);
                if (P == null)
                {
                    return Content(HttpStatusCode.NotFound, "Person Not found");
                }
                else
                {
                    PS.PersonRepository.RemoveAll(Person => Person.LastName == input);
                    return Content(HttpStatusCode.OK, "Person with last name " + input + " deleted");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
