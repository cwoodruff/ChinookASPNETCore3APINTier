using System.Linq;
using System.Threading.Tasks;
using Chinook.Data;
using JetBrains.dotMemoryUnit;
using Xunit;

namespace Chinook.UnitTest.DBContext
{
    public class AlbumDBContextTests
    {
        private readonly  ChinookContext _context;

        public AlbumDBContextTests()
        {
            //_context = new ChinookContext();
        }
        
        [Fact]
        public async Task AlbumGetAll()
        {
            // Arrange

            // Act
            var albums= await _context.GetAllAlbums();
            
            // Assert
            Assert.Empty(null);
        }
    }
}