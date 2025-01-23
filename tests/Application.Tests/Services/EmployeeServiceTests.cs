using System.Threading.Tasks;
using Moq;
using Xunit;
using Employee.Application.Interfaces;
using Employee.Application.Services;
using Employee.Application.Users.Models;
using Employee.Domain.Entities;
using Employee.Domain.SeedWork;

namespace Employee.Application.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<INotification> _notificationMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _notificationMock = new Mock<INotification>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _notificationMock.Object);
        }

        [Fact]
        public async Task AddAsync_ValidEmployee_InsertsEmployeeAndReturnsSuccess()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                EmployeeID = 1234,
                EmployeeName = "John Doe",
                EmployeeAge = 30,
                EmployeeAddress = "123 Main St"
            };

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.Is<EmployeeRecord>(e =>
                e.EmployeeID == employeeRequest.EmployeeID &&
                e.EmployeeName == employeeRequest.EmployeeName &&
                e.EmployeeAge == employeeRequest.EmployeeAge &&
                e.EmployeeAddress == employeeRequest.EmployeeAddress
            )), Times.Once);
            _notificationMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.NotNull(result);
            Assert.Empty(result.Notifications);
            Assert.Equal(employeeRequest.EmployeeID, result.EmployeeID);
            Assert.Equal(employeeRequest.EmployeeName, result.EmployeeName);
            Assert.Equal(employeeRequest.EmployeeAge, result.EmployeeAge);
            Assert.Equal(employeeRequest.EmployeeAddress, result.EmployeeAddress);
        }

        [Fact]
        public async Task AddAsync_InvalidEmployeeID_ReturnsError()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                EmployeeID = 123,
                EmployeeName = "John Doe",
                EmployeeAge = 30,
                EmployeeAddress = "123 Main St"
            };

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<EmployeeRecord>()), Times.Never);
            _notificationMock.Verify(n => n.AddNotification("EmployeeID", "EmployeeID must have exactly 4 digits."), Times.Once);
            Assert.NotNull(result);
            Assert.Contains(result.Notifications, n => n.Key == "EmployeeID" && n.Message == "EmployeeID must have exactly 4 digits.");
        }

        [Fact]
        public async Task AddAsync_InvalidEmployeeName_ReturnsError()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                EmployeeID = 1234,
                EmployeeName = "ThisNameIsWayTooLongForTheSpecification",
                EmployeeAge = 30,
                EmployeeAddress = "123 Main St"
            };

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<EmployeeRecord>()), Times.Never);
            _notificationMock.Verify(n => n.AddNotification("EmployeeName", "EmployeeName must have at most 20 characters."), Times.Once);
            Assert.NotNull(result);
            Assert.Contains(result.Notifications, n => n.Key == "EmployeeName" && n.Message == "EmployeeName must have at most 20 characters.");
        }

        [Fact]
        public async Task AddAsync_InvalidEmployeeAge_ReturnsError()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                EmployeeID = 1234,
                EmployeeName = "John Doe",
                EmployeeAge = 9,
                EmployeeAddress = "123 Main St"
            };

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<EmployeeRecord>()), Times.Never);
            _notificationMock.Verify(n => n.AddNotification("EmployeeAge", "EmployeeAge must have exactly 2 digits."), Times.Once);
            Assert.NotNull(result);
            Assert.Contains(result.Notifications, n => n.Key == "EmployeeAge" && n.Message == "EmployeeAge must have exactly 2 digits.");
        }

        [Fact]
        public async Task AddAsync_InvalidEmployeeAddress_ReturnsError()
        {
            // Arrange
            var employeeRequest = new EmployeeRequest
            {
                EmployeeID = 1234,
                EmployeeName = "John Doe",
                EmployeeAge = 30,
                EmployeeAddress = "This address is way too long to be accepted by the system and exceeds 30 characters"
            };

            // Act
            var result = await _employeeService.AddAsync(employeeRequest);

            // Assert
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<EmployeeRecord>()), Times.Never);
            _notificationMock.Verify(n => n.AddNotification("EmployeeAddress", "EmployeeAddress must have at most 30 characters."), Times.Once);
            Assert.NotNull(result);
            Assert.Contains(result.Notifications, n => n.Key == "EmployeeAddress" && n.Message == "EmployeeAddress must have at most 30 characters.");
        }
    }
}