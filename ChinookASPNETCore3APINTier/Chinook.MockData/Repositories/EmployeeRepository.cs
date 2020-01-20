using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.MockData.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void Dispose()
        {
        }

        public bool EmployeeExists(int id) => true;

        public async Task<IAsyncEnumerable<Employee>> GetAll()
            => (new List<Employee>
            {new Employee
            {
                EmployeeId = 1
            }}).ToAsyncEnumerable();

        public async Task<Employee> GetById(int id)
            => new Employee
            {
                EmployeeId = id
            };

        public async Task<Employee> Add(Employee newEmployee) => newEmployee;

        public async Task<bool> Update(Employee employee) => true;

        public async Task<bool> Delete(int id) => true;

        public async Task<Employee> GetReportsTo(int id)
            => new Employee
            {
                EmployeeId = id
            };

        public async Task<IAsyncEnumerable<Employee>> GetDirectReports(int id)
            => (new List<Employee>
            {new Employee
            {
                EmployeeId = id
            }}).ToAsyncEnumerable();
    }
}