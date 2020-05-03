using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ChinookContext _context;

        public EmployeeRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool EmployeeExists(int id) =>
            _context.Employee.Any(e => e.EmployeeId == id);

        public void Dispose() => _context.Dispose();

        public List<Employee> GetAll() =>
            _context.Employee.AsNoTracking().ToList();

        public Employee GetById(int id) =>
            _context.Employee.Find(id);

        public Employee Add(Employee newEmployee)
        {
            _context.Employee.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeId))
                return false;
            _context.Employee.Update(employee);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!EmployeeExists(id))
                return false;
            var toRemove = _context.Employee.Find(id);
            _context.Employee.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public Employee GetReportsTo(int id) =>
            _context.Employee.Find(id);

        public List<Employee> GetDirectReports(int id) => _context.Employee.Where(e => e.ReportsTo == id).ToList();
        
        public Employee GetToReports(int id) => 
            _context.Employee
                .Find(_context.Employee.
                    Where(e => e.EmployeeId == id)
                    .Select(p => new {p.ReportsTo})
                    .First());
    }
}