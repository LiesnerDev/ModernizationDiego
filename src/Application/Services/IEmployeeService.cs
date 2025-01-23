using System.Threading.Tasks;
using Employee.Application.Users.Models;

namespace Employee.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeModelResponse> AddAsync(EmployeeRequest employeeRequest);
        // Other service methods can be defined here
    }
}