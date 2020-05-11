using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.DapperRepository
{
    public class GenreRepositoryTest
    {
        private readonly IGenreRepository _repo;

        public GenreRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IGenreRepository, GenreRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IGenreRepository>();
        }

        [Fact]
        public void GenreGetAll()
        {
            // Act
            var genres = _repo.GetAll();

            // Assert
            Assert.True(genres.Count > 1, "The number of genres was not greater than 1");
        }
    }
}