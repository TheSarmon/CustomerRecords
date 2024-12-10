using Moq;
using NUnit.Framework;
using CustomerRecords.Application.Models;
using CustomerRecords.Application.Repositories;

namespace CustomerRecords.Tests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        [Test]
        public void Create_ShouldAddCustomer()
        {
            // Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var newCustomer = new Customer
            {
                Id = "12345",
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "0931234567",
                Address = "Lviv"
            };

            mockRepository.Setup(repo => repo.Create(newCustomer)).Returns(newCustomer);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Create(newCustomer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.FirstName);
            Assert.AreEqual("Doe", result.LastName);
            Assert.AreEqual("johndoe@example.com", result.Email);
            Assert.AreEqual("0931234567", result.PhoneNumber);
            Assert.AreEqual("Lviv", result.Address);

            mockRepository.Verify(repo => repo.Create(It.Is<Customer>(c =>
                c.Id == "12345" &&
                c.FirstName == "John" &&
                c.LastName == "Doe" &&
                c.Email == "johndoe@example.com" &&
                c.PhoneNumber == "0931234567" &&
                c.Address == "Lviv"
            )), Times.Once);
        }
    }
}