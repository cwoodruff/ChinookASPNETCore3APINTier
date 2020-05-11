using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Chinook.DataEFCore.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly ChinookContext _context;

        public MediaTypeRepository(ChinookContext context)
        {
            _context = context;
        }

        public MediaTypeRepository()
        {
            var services = new ServiceCollection();
            
            var connection = String.Empty;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = "Server=.;Database=Chinook;Trusted_Connection=True;Application Name=ChinookASPNETCoreAPINTier";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = "Server=localhost,1433;Database=Chinook;User=sa;Password=P@55w0rd;Trusted_Connection=False;Application Name=ChinookASPNETCoreAPINTier";
            }

            services.AddDbContextPool<ChinookContext>(options => options.UseSqlServer(connection));
            
            var serviceProvider = services.BuildServiceProvider();

            _context = serviceProvider.GetService<ChinookContext>();
        }

        private bool MediaTypeExists(int id) =>
            _context.MediaType.Any(i => i.MediaTypeId == id);

        public void Dispose() => _context.Dispose();

        public List<MediaType> GetAll() =>
            _context.MediaType.AsNoTracking().ToList();

        public MediaType GetById(int id) =>
            _context.MediaType.Find(id);

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