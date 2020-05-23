using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCoreCmpldQry.Repositories
{
    public class MediaTypeRepository : EfRepository<Album>
    {
        public MediaTypeRepository(ChinookContext context) : base(context)
        {
        }

        public List<MediaType> GetAll() 
            => _dbContext.GetAllMediaTypes();

        public MediaType GetById(int id)
        {
            var mediaType = _dbContext.GetMediaType(id);
            return mediaType;
        }

        public MediaType Add(MediaType newMediaType)
        {
            _dbContext.MediaType.Add(newMediaType);
            _dbContext.SaveChanges();
            return newMediaType;
        }

        public bool Update(MediaType mediaType)
        {
            if (!Exists(mediaType.MediaTypeId))
                return false;
            _dbContext.MediaType.Update(mediaType);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!Exists(id))
                return false;
            var toRemove = _dbContext.MediaType.Find(id);
            _dbContext.MediaType.Remove(toRemove);
            _dbContext.SaveChanges();
            return true;
        }
    }
}