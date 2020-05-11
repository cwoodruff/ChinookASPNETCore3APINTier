using System;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Chinook.UnitTest.DapperRepository
{
    public class ArtistRepositoryTest
    {
        private readonly IArtistRepository _repo;

        public ArtistRepositoryTest()
        {
            var services = new ServiceCollection();
            services.AddTransient<IArtistRepository, ArtistRepository>();

            var serviceProvider = services.BuildServiceProvider();

            _repo = serviceProvider.GetService<IArtistRepository>();
        }

        [Fact]
        public void ArtistGetAll()
        {
            // Act
            var artists = _repo.GetAll();

            // Assert
            Assert.True(artists.Count > 1, "The number of artists was not greater than 1");
        }
    }
}