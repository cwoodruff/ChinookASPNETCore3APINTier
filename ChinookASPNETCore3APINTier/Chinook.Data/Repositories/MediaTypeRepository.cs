using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.Data.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly ChinookContext _context;

        public MediaTypeRepository(ChinookContext context)
        {
            _context = context;
        }

        public bool MediaTypeExists(int id) => _context.MediaType.Any(i => i.MediaTypeId == id);

        public void Dispose() => _context.Dispose();

        public async Task<IAsyncEnumerable<MediaType>> GetAll() => await _context.GetAllMediaTypes();

        public async Task<MediaType> GetById(int id) => await _context.GetMediaType(id);

        public async Task<MediaType> Add(MediaType newMediaType)
        {
            if (newMediaType == null)
                return null;
            var mediaType = await _context.MediaType.AddAsync(newMediaType);
            await _context.SaveChangesAsync();
            return mediaType.Entity;
        }

        public async Task<bool> Update(MediaType mediaType)
        {
            if (!MediaTypeExists(mediaType.MediaTypeId))
                return false;
            _context.MediaType.Update(mediaType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (!MediaTypeExists(id))
                return false;
            var toRemove = _context.MediaType.Find(id);
            _context.MediaType.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}