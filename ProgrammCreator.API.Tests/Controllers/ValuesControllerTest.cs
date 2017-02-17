using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammCreator.API;
using ProgrammCreator.API.Controllers;

namespace ProgrammCreator.API.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            PictureController controller = new PictureController();
            
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            PictureController controller = new PictureController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            PictureController controller = new PictureController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            PictureController controller = new PictureController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            PictureController controller = new PictureController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
