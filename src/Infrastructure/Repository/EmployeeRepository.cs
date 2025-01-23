using System.Threading.Tasks;
using Employee.Application.Interfaces;
using Employee.Domain.Entities;
using Employee.Infrastructure.Persistence;

namespace Employee.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmployeeRecord employee)
        {
            _context.EmployeeRecords.Add(employee);
            await _context.SaveChangesAsync();
        }
    }
}