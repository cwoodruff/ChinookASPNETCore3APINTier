using System;
using System.Collections.Generic;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataJson.Repositories
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly DbInfo _dbInfo;

        public MediaTypeRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public void Dispose()
        {
        }

        private bool MediaTypeExists(int id)
        {
            return true;
        }

        public List<MediaType> GetAll()
        {
            return null;
        }

        public MediaType GetById(int id)
        {
            return null;
        }

        public MediaType Add(MediaType newMediaType)
        {
            return newMediaType;
        }

        public bool Update(MediaType mediaType)
        {
            if (!MediaTypeExists(mediaType.MediaTypeId))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}