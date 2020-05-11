using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
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

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void EmployeeGetAll()
        {
            // Act
            var employees = _repo.GetAll();

            // Assert
            Assert.True(employees.Count > 1, "The number of employees was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 1408, Types = new[] {typeof(Employee)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new EmployeeRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(8, memory.GetObjects(where => where.Type.Is<Employee>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}