using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class EmployeeRepository : EfRepository<Employee>
    {
        public EmployeeRepository(ChinookContext context) : base(context)
        {
        }

        public Employee GetReportsTo(int id) =>
            _dbContext.Employee.Find(id);

        public List<Employee> GetDirectReports(int id) => _dbContext.Employee.Where(e => e.ReportsTo == id).ToList();
        
        public Employee GetToReports(int id) => 
            _dbContext.Employee
                .Find(_dbContext.Employee.
                    Where(e => e.EmployeeId == id)
                    .Select(p => new {p.ReportsTo})
                    .First());
    }
}