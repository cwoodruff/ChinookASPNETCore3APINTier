using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class EmployeeRepository : EfRepository<Album>
    {
        public EmployeeRepository(ChinookContext context) : base(context)
        {
        }

        public List<Employee> GetAll() 
            => _dbContext.GetAllEmployees();

        public Employee GetById(int id)
        {
            var employee = _dbContext.GetEmployee(id);
            return employee;
        }

        public Employee Add(Employee newEmployee)
        {
            _dbContext.Employee.Add(newEmployee);
            _dbContext.SaveChanges();
            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            if (!Exists(employee.EmployeeId))
                return false;
            _dbContext.Employee.Update(employee);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.Employee.Find(id);
            _dbContext.Employee.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }

        public Employee GetReportsTo(int id)
        {
            var employee = _dbContext.GetEmployeeGetReportsTo(id);
            return employee.FirstOrDefault();
        }

        public List<Employee> GetDirectReports(int id) => _dbContext.GetEmployeeDirectReports(id);
    }
}