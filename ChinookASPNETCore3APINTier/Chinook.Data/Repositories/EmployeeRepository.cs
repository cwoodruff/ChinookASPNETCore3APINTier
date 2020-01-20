using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ChinookContext _context;

        public EmployeeRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool EmployeeExists(int id) => _context.Employee.Any(e => e.EmployeeId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<Employee>> GetAll() => await _context.GetAllEmployees();

        public async Task<Employee> GetById(int id) => await _context.GetEmployee(id);

        public async Task<Employee> Add(Employee newEmployee)
        {
            if (newEmployee == null)
                return null;
            var employee = await _context.Employee.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return employee.Entity;
        }

        public async Task<bool> Update(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeId))
                return false;
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!EmployeeExists(id))
                return false;
            var toRemove = _context.Employee.Find(id);
            _context.Employee.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetReportsTo(int id) => await _context.GetEmployeeGetReportsTo(id);
        
        public async Task<IAsyncEnumerable<Employee>> GetDirectReports(int id) => await _context.GetEmployeeDirectReports(id);
    }
}