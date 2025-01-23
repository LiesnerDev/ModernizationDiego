using Employee.Domain.SeedWork;
using Employee.Domain.Entities;

namespace Employee.Application.Users.Models
{
    public class EmployeeModelResponse : BaseResponse
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeAge { get; set; }
        public string EmployeeAddress { get; set; }

        public EmployeeModelResponse() : base() { }

        public EmployeeModelResponse(EmployeeRecord employee) : base()
        {
            if (employee != null)
            {
                EmployeeID = employee.EmployeeID;
                EmployeeName = employee.EmployeeName;
                EmployeeAge = employee.EmployeeAge;
                EmployeeAddress = employee.EmployeeAddress;
            }
        }
    }
}