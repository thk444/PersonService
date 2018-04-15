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

namespace PersonAPIService.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        [Ignore]
        public void Get()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
            IEnumerable<Person> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [Ignore]
        [TestMethod]
        public void GetById()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
        //    Person result = controller.Get("gender");

            // Assert
         //   Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            PersonsController controller = new PersonsController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
