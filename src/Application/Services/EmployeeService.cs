using System.Threading.Tasks;
using Employee.Application.Interfaces;
using Employee.Application.Users.Models;
using Employee.Domain.Entities;
using Employee.Domain.SeedWork;

namespace Employee.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotification _notification;

        public EmployeeService(IEmployeeRepository employeeRepository, INotification notification)
        {
            _employeeRepository = employeeRepository;
            _notification = notification;
        }

        public async Task<EmployeeModelResponse> AddAsync(EmployeeRequest employeeRequest)
        {
            // Validate EmployeeID
            if (employeeRequest.EmployeeID < 1000 || employeeRequest.EmployeeID > 9999)
            {
                _notification.AddNotification("EmployeeID", "EmployeeID must have exactly 4 digits.");
            }

            // Validate EmployeeName
            if (string.IsNullOrWhiteSpace(employeeRequest.EmployeeName) || employeeRequest.EmployeeName.Length > 20)
            {
                _notification.AddNotification("EmployeeName", "EmployeeName must have at most 20 characters.");
            }

            // Validate EmployeeAge
            if (employeeRequest.EmployeeAge < 10 || employeeRequest.EmployeeAge > 99)
            {
                _notification.AddNotification("EmployeeAge", "EmployeeAge must have exactly 2 digits.");
            }

            // Validate EmployeeAddress
            if (string.IsNullOrWhiteSpace(employeeRequest.EmployeeAddress) || employeeRequest.EmployeeAddress.Length > 30)
            {
                _notification.AddNotification("EmployeeAddress", "EmployeeAddress must have at most 30 characters.");
            }

            if (_notification.HasNotifications)
            {
                return new EmployeeModelResponse(_notification);
            }

            var employee = new EmployeeRecord
            {
                EmployeeID = employeeRequest.EmployeeID,
                EmployeeName = employeeRequest.EmployeeName,
                EmployeeAge = employeeRequest.EmployeeAge,
                EmployeeAddress = employeeRequest.EmployeeAddress
            };

            await _employeeRepository.AddAsync(employee);

            return new EmployeeModelResponse(employee);
        }
    }
}