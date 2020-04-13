using System;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class InvoiceLineRepositoryTest
    {
        private readonly InvoiceLineRepository _repo;

        public InvoiceLineRepositoryTest()
        {
            _repo = new InvoiceLineRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void InvoiceLineGetAll()
        {
            // Act
            var invoiceLines = _repo.GetAll();

            // Assert
            Assert.Single(invoiceLines);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(InvoiceLine)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new InvoiceLineRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<InvoiceLine>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}