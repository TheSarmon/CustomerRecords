using CustomerRecords.API.Controllers;
using CustomerRecords.Application.Models;
using CustomerRecords.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CustomerRecords.Tests
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private Mock<ICustomerRepository> _mockRepository;
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _controller = new CustomersController(_mockRepository.Object);
        }

        [Test]
        public void GetCustomers_ShouldReturnJsonResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = "1", FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", Address = "123 Main St" },
                new Customer { Id = "2", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "0987654321", Address = "456 Elm St" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).Returns(customers);

            // Act
            var result = _controller.GetCustomers();

            // Assert
            Assert.IsInstanceOf<JsonResult>(result);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(customers, jsonResult.Value);
        }

        [Test]
        public void CreateCustomer_ShouldReturnJsonResult_WithNewCustomer()
        {
            // Arrange
            var newCustomer = new Customer { Id = "3", FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", PhoneNumber = "1234509876", Address = "789 Pine St" };
            _mockRepository.Setup(repo => repo.Create(It.IsAny<Customer>())).Returns(newCustomer);

            // Act
            var result = _controller.CreateCustomer(newCustomer);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(newCustomer, jsonResult.Value);
        }

        [Test]
        public void DeleteCustomer_ShouldReturnJsonResult_WithSuccessMessage()
        {
            // Arrange
            var customerId = "1";
            var successMessage = "Customer deleted successfully.";
            _mockRepository.Setup(repo => repo.Delete(customerId)).Returns(successMessage);

            // Act
            var result = _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsInstanceOf<JsonResult>(result);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(successMessage, jsonResult.Value);
        }
    }
}