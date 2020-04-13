using System;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class EmployeeRepositoryTest
    {
        private readonly EmployeeRepository _repo;

        public EmployeeRepositoryTest()
        {
            _repo = new EmployeeRepository();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public void EmployeeGetAll()
        {
            // Act
            var employees = _repo.GetAll();

            // Assert
            Assert.Single(employees);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Employee)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new EmployeeRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Employee>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}