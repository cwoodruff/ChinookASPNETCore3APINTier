using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class CustomerRepositoryTest
    {
        private readonly ICustomerRepository _repo;

        public CustomerRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<ICustomerRepository>();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void CustomerGetAll()
        {
            // Act
            var customers = _repo.GetAll();

            // Assert
            Assert.True(customers.Count > 1, "The number of customers was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 8024, Types = new[] {typeof(Customer)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new CustomerRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(59, memory.GetObjects(where => where.Type.Is<Customer>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}