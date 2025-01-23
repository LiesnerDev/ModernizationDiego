using System.Threading.Tasks;
using Employee.Domain.Entities;

namespace Employee.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(EmployeeRecord employee);
        // Other repository methods can be defined here
    }
}