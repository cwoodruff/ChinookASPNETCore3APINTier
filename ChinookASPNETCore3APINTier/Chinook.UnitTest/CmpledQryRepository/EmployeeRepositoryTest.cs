using System;
using Chinook.DataEFCoreCmpldQry.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.CmpledQryRepository
{
    public class EmployeeRepositoryTest
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IEmployeeRepository>();
        }

        [Fact]
        public void EmployeeGetAll()
        {
            // Act
            var employees = _repo.GetAll();

            // Assert
            Assert.True(employees.Count > 1, "The number of employees was not greater than 1");
        }
    }
}