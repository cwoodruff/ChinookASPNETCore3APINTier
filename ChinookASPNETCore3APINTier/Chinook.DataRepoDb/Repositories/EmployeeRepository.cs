using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using RepoDb;

namespace Chinook.DataRepoDb.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbInfo _dbInfo;

        public EmployeeRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
            RepoDb.SqlServerBootstrap.Initialize();
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool EmployeeExists(int id) =>
            Connection.Exists("select count(1) from Employee where EmployeeId = @id", new {id});

        public List<Employee> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var employees = cn.QueryAll<Employee>();
            return employees.ToList();
        }

        public Employee GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<Employee>(id).FirstOrDefault();
        }

        public Employee GetReportsTo(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.Query<Employee>(e => e.ReportsTo == id).FirstOrDefault();
        }

        public Employee Add(Employee newEmployee)
        {
            using var cn = Connection;
            cn.Open();
            cn.Insert(newEmployee);
            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeId))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Update(employee) > 0);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using var cn = Connection;
                cn.Open();
                return (cn.Delete(new Employee {EmployeeId = id}) > 0);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Employee> GetDirectReports(int id)
        {
            using var cn = Connection;
            cn.Open();
            var employees = cn.Query<Employee>(e => e.ReportsTo == id);
            return employees.ToList();
        }
    }
}