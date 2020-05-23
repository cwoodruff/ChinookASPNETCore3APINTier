using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.JsonRepository
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

        [Fact]
        public void CustomerGetAll()
        {
            // Act
            var customers = _repo.GetAll();

            // Assert
            Assert.True(customers.Count > 1, "The number of customers was not greater than 1");
        }
    }
}