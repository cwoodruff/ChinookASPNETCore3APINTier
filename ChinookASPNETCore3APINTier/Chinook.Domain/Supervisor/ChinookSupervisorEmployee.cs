using System;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<EmployeeApiModel> GetAllEmployee()
        {
            var employees = _employeeRepository.GetAll().ConvertAll();
            foreach (var employee in employees)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Employee-", employee.EmployeeId), employee, cacheEntryOptions);
            }
            return employees;
        }

        public EmployeeApiModel GetEmployeeById(int id)
        {
            var employeeApiModelCached = _cache.Get<EmployeeApiModel>(string.Concat("Employee-", id));

            if (employeeApiModelCached != null)
            {
                return employeeApiModelCached;
            }
            else
            {
                var employeeApiModel = (_employeeRepository.GetById(id)).Convert();
                employeeApiModel.Customers = (GetCustomerBySupportRepId(employeeApiModel.EmployeeId)).ToList();
                employeeApiModel.DirectReports = (GetEmployeeDirectReports(employeeApiModel.EmployeeId)).ToList();
                employeeApiModel.Manager = employeeApiModel.ReportsTo.HasValue
                    ? GetEmployeeReportsTo(employeeApiModel.ReportsTo.GetValueOrDefault())
                    : null;
                if (employeeApiModel.Manager != null)
                    employeeApiModel.ReportsToName = employeeApiModel.ReportsTo.HasValue
                        ? $"{employeeApiModel.Manager.LastName}, {employeeApiModel.Manager.FirstName}"
                        : string.Empty;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Employee-", employeeApiModel.EmployeeId), employeeApiModel, cacheEntryOptions);

                return employeeApiModel;
            }
        }

        public EmployeeApiModel GetEmployeeReportsTo(int id)
        {
            var employee = _employeeRepository.GetReportsTo(id);
            return employee.Convert();
        }

        public EmployeeApiModel AddEmployee(EmployeeApiModel newEmployeeApiModel)
        {
            var employee = newEmployeeApiModel.Convert();

            employee = _employeeRepository.Add(employee);
            newEmployeeApiModel.EmployeeId = employee.EmployeeId;
            return newEmployeeApiModel;
        }

        public bool UpdateEmployee(EmployeeApiModel employeeApiModel)
        {
            var employee = _employeeRepository.GetById(employeeApiModel.EmployeeId);

            if (employee == null) return false;
            employee.EmployeeId = employeeApiModel.EmployeeId;
            employee.LastName = employeeApiModel.LastName;
            employee.FirstName = employeeApiModel.FirstName;
            employee.Title = employeeApiModel.Title;
            employee.ReportsTo = employeeApiModel.ReportsTo;
            employee.BirthDate = employeeApiModel.BirthDate;
            employee.HireDate = employeeApiModel.HireDate;
            employee.Address = employeeApiModel.Address;
            employee.City = employeeApiModel.City;
            employee.State = employeeApiModel.State;
            employee.Country = employeeApiModel.Country;
            employee.PostalCode = employeeApiModel.PostalCode;
            employee.Phone = employeeApiModel.Phone;
            employee.Fax = employeeApiModel.Fax;
            employee.Email = employeeApiModel.Email;

            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id) 
            => _employeeRepository.Delete(id);

        public IEnumerable<EmployeeApiModel> GetEmployeeDirectReports(int id)
        {
            var employees = _employeeRepository.GetDirectReports(id);
            return employees.ConvertAll();
        }

        public IEnumerable<EmployeeApiModel> GetDirectReports(int id)
        {
            var employees = _employeeRepository.GetDirectReports(id);
            return employees.ConvertAll();
        }
    }
}