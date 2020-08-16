using System;
using System.Collections.Generic;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataLinq2Db.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetReportsTo(int id)
        {
            throw new NotImplementedException();
        }

        public Employee Add(Employee newEmployee)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetDirectReports(int id)
        {
            throw new NotImplementedException();
        }
    }
}