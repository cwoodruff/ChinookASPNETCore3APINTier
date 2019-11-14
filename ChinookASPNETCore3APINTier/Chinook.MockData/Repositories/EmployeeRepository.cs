using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void Dispose()
        {
        }

        public List<Employee> GetAll()
            => new List<Employee>
            {new Employee
            {
                EmployeeId = 1
            }};

        public Employee GetById(int id)
            => new Employee
            {
                EmployeeId = id
            };

        public Employee Add(Employee newEmployee) => newEmployee;

        public bool Update(Employee employee) => true;

        public bool Delete(int id) => true;

        public Employee GetReportsTo(int id)
            => new Employee
            {
                EmployeeId = id
            };

        public List<Employee> GetDirectReports(int id)
            => new List<Employee>
            {new Employee
            {
                EmployeeId = id
            }};
    }
}