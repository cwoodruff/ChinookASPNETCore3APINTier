using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection _sqlconn;

        public EmployeeRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private bool EmployeeExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckEmployee", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public List<Employee> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployee", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;
            return converted;
        }

        public Employee GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployeeDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;

            return converted.FirstOrDefault();
        }

        public Employee GetReportsTo(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployeeReportTo", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;

            return converted.FirstOrDefault();
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
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployeeDirectReports", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;
            return converted;
        }
    }
}