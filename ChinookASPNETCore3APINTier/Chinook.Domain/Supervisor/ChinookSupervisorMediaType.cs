using System;
using System.Collections.Generic;
using Chinook.Domain.ApiModels;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool MediaTypeExists(int id) => _mediaTypeRepository.MediaTypeExists(id);
        public async Task<IAsyncEnumerable<MediaTypeApiModel>> GetAllMediaType()
        {
            var mediaTypes = (await _mediaTypeRepository.GetAll()).ConvertAll();
            await foreach (var mediaType in mediaTypes)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("MediaType-", mediaType.MediaTypeId), mediaType, cacheEntryOptions);
            }
            return mediaTypes;
        }

        public async Task<MediaTypeApiModel> GetMediaTypeById(int id)
        {
            var mediaTypeApiModelCached = _cache.Get<MediaTypeApiModel>(string.Concat("MediaType-", id));

            if (mediaTypeApiModelCached != null)
            {
                return mediaTypeApiModelCached;
            }
            else
            {
                var mediaTypeApiModel = (await _mediaTypeRepository.GetById(id)).Convert();
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("MediaType-", mediaTypeApiModel.MediaTypeId), mediaTypeApiModel, cacheEntryOptions);

                return mediaTypeApiModel;
            }
        }

        public async Task<MediaTypeApiModel> AddMediaType(MediaTypeApiModel newMediaTypeApiModel)
        {
            var mediaType = newMediaTypeApiModel.Convert();
            mediaType = await _mediaTypeRepository.Add(mediaType);
            return mediaType.Convert();
        }

        public async Task<bool> UpdateMediaType(MediaTypeApiModel mediaTypeApiModel) => 
            await _mediaTypeRepository.Update(mediaTypeApiModel.Convert());

        public async Task<bool> DeleteMediaType(int id) => await _mediaTypeRepository.Delete(id);
    }
}