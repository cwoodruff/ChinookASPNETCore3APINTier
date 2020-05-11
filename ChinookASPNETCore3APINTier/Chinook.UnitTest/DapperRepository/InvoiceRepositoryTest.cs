using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.DapperRepository
{
    public class InvoiceRepositoryTest
    {
        private readonly IInvoiceRepository _repo;

        public InvoiceRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IInvoiceRepository>();
        }

        [Fact]
        public void InvoiceGetAll()
        {
            // Act
            var invoices = _repo.GetAll();

            // Assert
            Assert.True(invoices.Count > 1, "The number of invoices was not greater than 1");
        }
    }
}