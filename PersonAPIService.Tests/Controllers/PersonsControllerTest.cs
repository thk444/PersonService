using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonAPIService;
using PersonAPIService.Controllers;
using PersonServiceLibrary;
using System.Web.Http.Results;
using System.Net;

namespace PersonAPIService.Tests.Controllers
{
    [TestClass]
    public class PersonsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            controller.Post("Talapaneni Hemanth Male Yellow 2017/12/01");
            controller.Post("Halapaneni Temanth Female Green 2018/2/1");
            controller.Post("Balapaneni Remanth Female Blue 2014/5/01");
            controller.Post("Malapaneni Bemanth Male Red 2015/11/1");

            List<Person> Persons = new List<Person>();
            // Act
            IHttpActionResult actionResult = controller.Get();

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult,typeof(OkNegotiatedContentResult<List<Person>>));
            OkNegotiatedContentResult<List<Person>> Result = (OkNegotiatedContentResult<List<Person>>)actionResult;
            Persons = Result.Content;
            Assert.AreEqual(4, Persons.Count);
            Assert.AreEqual("Halapaneni", Persons[1].LastName);
        }

        
        [TestMethod]
        public void GetById()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            controller.PS.PersonRepository.RemoveAll(P=>P.LastName.Length > 0);
            controller.Post("Talapaneni Hemanth Male Yellow 2017/12/01");
            controller.Post("Halapaneni Temanth Female Green 2018/2/1");
            controller.Post("Balapaneni Remanth Female Blue 2014/5/01");
            controller.Post("Malapaneni Bemanth Male Red 2015/11/1");

            List<Person> Persons = new List<Person>();
            // Act
            IHttpActionResult actionResult = controller.Get("dob");

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<Person>>));
            OkNegotiatedContentResult<List<Person>> Result = (OkNegotiatedContentResult<List<Person>>)actionResult;
            Persons = Result.Content;
            Assert.AreEqual(4, Persons.Count);
            Assert.AreEqual("Malapaneni", Persons[1].LastName);
        }


        [TestMethod]
        public void Post__EmptyRecord()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
            IHttpActionResult actionResult = controller.Post("");

            // Assert

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void Post_Recordwithmorecolumns()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
            IHttpActionResult actionResult = controller.Post("Talapaneni, Hemanth, Male, Yellow, Green, 2017 / 12 / 01");

            // Assert

            Assert.IsInstanceOfType(actionResult,typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            PersonsController controller = new PersonsController();
            controller.PS.PersonRepository.RemoveAll(P => P.LastName.Length > 0);
            controller.Post("Talapaneni Hemanth Male Yellow 2017/12/01");
            controller.Post("Halapaneni Temanth Female Green 2018/2/1");
            controller.Post("Balapaneni Remanth Female Blue 2014/5/01");
            controller.Post("Malapaneni Bemanth Male Red 2015/11/1");
            List<Person> Persons = new List<Person>();
            // Act
            IHttpActionResult actionResult = controller.Put("Malapaneni", "Malapaneni Demanth Male Red 2015/11/1");

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NegotiatedContentResult<string>));
            NegotiatedContentResult<string> Result = (NegotiatedContentResult<string>)actionResult;
            
            Assert.AreEqual(HttpStatusCode.OK,Result.StatusCode);

            // Act
             actionResult = controller.Get("dob");

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<Person>>));
            OkNegotiatedContentResult<List<Person>> result = (OkNegotiatedContentResult<List<Person>>)actionResult;
            Persons = result.Content;
            Assert.AreEqual(4, Persons.Count);
            Assert.AreEqual("Demanth", Persons[1].FirstName);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            controller.PS.PersonRepository.RemoveAll(P => P.LastName.Length > 0);
            controller.Post("Talapaneni Hemanth Male Yellow 2017/12/01");
            controller.Post("Halapaneni Temanth Female Green 2018/2/1");
            controller.Post("Balapaneni Remanth Female Blue 2014/5/01");
            controller.Post("Malapaneni Bemanth Male Red 2015/11/1");
            List<Person> Persons = new List<Person>();
            // Act
            IHttpActionResult actionResult = controller.Delete("Malapaneni");
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NegotiatedContentResult<string>));
            NegotiatedContentResult<string> Result = (NegotiatedContentResult<string>)actionResult;

            Assert.AreEqual(HttpStatusCode.OK, Result.StatusCode);

            // Act
            actionResult = controller.Get("dob");

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<Person>>));
            OkNegotiatedContentResult<List<Person>> result = (OkNegotiatedContentResult<List<Person>>)actionResult;
            Persons = result.Content;
            Assert.AreEqual(3, Persons.Count);
            Assert.AreEqual("Hemanth", Persons[1].FirstName);
        }
    }
}
