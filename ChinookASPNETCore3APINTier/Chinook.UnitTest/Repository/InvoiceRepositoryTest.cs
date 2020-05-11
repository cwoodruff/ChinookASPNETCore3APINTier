using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using JetBrains.dotMemoryUnit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.Repository
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

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void InvoiceGetAll()
        {
            // Act
            var invoices = _repo.GetAll();

            // Assert
            Assert.True(invoices.Count > 1, "The number of invoices was not greater than 1");
        }

        [AssertTraffic(AllocatedSizeInBytes = 47632, Types = new[] {typeof(Invoice)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new InvoiceRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(458, memory.GetObjects(where => where.Type.Is<Invoice>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}