using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly ChinookContext _context;

        public MediaTypeRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool MediaTypeExists(int id) =>
            _context.MediaType.Any(i => i.MediaTypeId == id);

        public void Dispose() => _context.Dispose();

        public List<MediaType> GetAll() 
            => _context.GetAllMediaTypes();

        public MediaType GetById(int id)
        {
            var mediaType = _context.GetMediaType(id);
            return mediaType;
        }

        public MediaType Add(MediaType newMediaType)
        {
            _context.MediaType.Add(newMediaType);
            _context.SaveChanges();
            return newMediaType;
        }

        public bool Update(MediaType mediaType)
        {
            if (!MediaTypeExists(mediaType.MediaTypeId))
                return false;
            _context.MediaType.Update(mediaType);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!MediaTypeExists(id))
                return false;
            var toRemove = _context.MediaType.Find(id);
            _context.MediaType.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}