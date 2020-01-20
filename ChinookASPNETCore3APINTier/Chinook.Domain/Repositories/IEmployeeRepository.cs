using Chinook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chinook.Domain.Repositories
{
    public interface IEmployeeRepository : IDisposable
    {
        public bool EmployeeExists(int id);
        Task<IAsyncEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<Employee> GetReportsTo(int id);
        Task<Employee> Add(Employee newEmployee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(int id);
        Task<IAsyncEnumerable<Employee>> GetDirectReports(int id);
    }
}