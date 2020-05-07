using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Chinook.DataDapper.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbInfo _dbInfo;

        public EmployeeRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool EmployeeExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Employee where EmployeeId = @id", new {id});

        public List<Employee> GetAll()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var employees = Connection.Query<Employee>("Select * From Employee");
                return employees.ToList();
            }
        }

        public Employee GetById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.QueryFirstOrDefault<Employee>("Select * From Employee WHERE Id = @Id", new {id});
            }
        }

        public Employee GetReportsTo(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.QueryFirstOrDefault<Employee>("Select * From Employee WHERE Id = @Id", new {id});
            }
        }

        public Employee Add(Employee newEmployee)
        {
            using (var cn = Connection)
            {
                cn.Open();

                newEmployee.EmployeeId = (int) cn.Insert(
                    new Employee
                    {
                        LastName = newEmployee.LastName,
                        FirstName = newEmployee.FirstName,
                        Title = newEmployee.Title,
                        ReportsTo = newEmployee.ReportsTo,
                        BirthDate = newEmployee.BirthDate,
                        HireDate = newEmployee.HireDate,
                        Address = newEmployee.Address,
                        City = newEmployee.City,
                        State = newEmployee.State,
                        Country = newEmployee.Country,
                        PostalCode = newEmployee.PostalCode,
                        Phone = newEmployee.Phone,
                        Fax = newEmployee.Fax,
                        Email = newEmployee.Email
                    });
            }

            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeId))
                return false;

            try
            {
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Update(employee);
                }
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
                using (var cn = Connection)
                {
                    cn.Open();
                    return cn.Delete(new Employee {EmployeeId = id});
                }  
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Employee> GetDirectReports(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var employees = cn.Query<Employee>("Select * From Employee WHERE ArtistId = @Id", new { id });
                return employees.ToList();
            }
        }
    }
}