using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbInfo _dbInfo;

        public EmployeeRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool EmployeeExists(int id)
        {
            return true;
        }

        public List<Employee> GetAll()
        {
            return null;
        }

        public Employee GetById(int id)
        {
            return null;
        }

        public Employee GetReportsTo(int id)
        {
            return null;
        }

        public Employee Add(Employee newEmployee)
        {
            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeId))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Employee> GetDirectReports(int id)
        {
            return null;
        }
    }
}