using Assignment.Controllers;
using Assignment.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Assignment.UnitTests
{
    [TestClass]
    public class ServiceTests
    {
        private ILogger<PetsController> logger;
        private PetsController SUT;
        private Mock<IHttpClient> AGLHttpClient;

        [TestInitialize]
        public void Setup()
        {
            
        }
      
        [TestMethod]
        public void PetsControllerTest()
        {
            //Arrange
            var mock = new Mock<ILogger<PetsController>>();            
            var jsonResult = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'Garfield','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Jennifer','gender':'Female','age':18,'pets':[{'name':'Garfield','type':'Cat'}]},{'name':'Steve','gender':'Male','age':45,'pets':null},{'name':'Fred','gender':'Male','age':40,'pets':[{'name':'Tom','type':'Cat'},{'name':'Max','type':'Cat'},{'name':'Sam','type':'Dog'},{'name':'Jim','type':'Cat'}]},{'name':'Samantha','gender':'Female','age':40,'pets':[{'name':'Tabby','type':'Cat'}]},{'name':'Alice','gender':'Female','age':64,'pets':[{'name':'Simba','type':'Cat'},{'name':'Nemo','type':'Fish'}]}]";

            AGLHttpClient = new Mock<IHttpClient>(MockBehavior.Strict);            
            AGLHttpClient.Setup(x => x.GetAsync(new Uri("http://agl-developer-test.azurewebsites.net/people.json"))).ReturnsAsync(jsonResult);            

            logger = mock.Object;
            SUT = new PetsController(logger,AGLHttpClient.Object);
                        
            //Act
            var response = SUT.GetAsync("male", "cat");

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);            
            Assert.AreEqual(((List<string>)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value).Count, 4);
        }                
    }
}
