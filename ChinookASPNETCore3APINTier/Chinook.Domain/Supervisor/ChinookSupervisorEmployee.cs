using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool EmployeeExists(int id) => _employeeRepository.EmployeeExists(id);
        
        public async Task<IAsyncEnumerable<EmployeeApiModel>> GetAllEmployee()
        {
            var employees = (await _employeeRepository.GetAll()).ConvertAll();
            await foreach (var employee in employees)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Employee-", employee.EmployeeId), employee, cacheEntryOptions);
            }
            return employees;
        }

        public async Task<EmployeeApiModel> GetEmployeeById(int id)
        {
            var employeeApiModelCached = _cache.Get<EmployeeApiModel>(string.Concat("Employee-", id));

            if (employeeApiModelCached != null)
            {
                return employeeApiModelCached;
            }
            else
            {
                var employeeApiModel = (await _employeeRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Employee-", employeeApiModel.EmployeeId), employeeApiModel, cacheEntryOptions);

                return employeeApiModel;
            }
        }

        public async Task<EmployeeApiModel> GetEmployeeReportsTo(int id)
        {
            var employee = await _employeeRepository.GetReportsTo(id);
            return employee.Convert();
        }

        public async Task<EmployeeApiModel> AddEmployee(EmployeeApiModel newEmployeeApiModel)
        {
            var employee = newEmployeeApiModel.Convert();

            employee = await _employeeRepository.Add(employee);
            return employee.Convert();
        }

        public async Task<bool> UpdateEmployee(EmployeeApiModel employeeApiModel) => await _employeeRepository.Update(employeeApiModel.Convert());

        public async Task<bool> DeleteEmployee(int id) => await _employeeRepository.Delete(id);

        public async Task<IAsyncEnumerable<EmployeeApiModel>> GetEmployeeDirectReports(int id) => (await _employeeRepository.GetDirectReports(id)).ConvertAll();

        public async Task<IAsyncEnumerable<EmployeeApiModel>> GetDirectReports(int id) => (await _employeeRepository.GetDirectReports(id)).ConvertAll();
        }
}