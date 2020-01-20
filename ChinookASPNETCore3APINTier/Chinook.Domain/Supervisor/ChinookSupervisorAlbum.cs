using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public bool AlbumExists(int id) => _albumRepository.AlbumExists(id);
        
        public async Task<IAsyncEnumerable<AlbumApiModel>> GetAllAlbum()
        {
            var albums = (await _albumRepository.GetAll()).ConvertAll();
            await foreach (var album in albums)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Album-", album.AlbumId), album, cacheEntryOptions);
            }
            return albums;
        }

        public async Task<AlbumApiModel> GetAlbumById(int id)
        {
            var albumApiModelCached = _cache.Get<AlbumApiModel>(string.Concat("Album-", id));
            if (albumApiModelCached != null)
            {
                return albumApiModelCached;
            }
            else
            {
                var albumApiModel = (await _albumRepository.GetById(id)).Convert();
                albumApiModel.ArtistName = (await _artistRepository.GetById(albumApiModel.ArtistId)).Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Album-", albumApiModel.AlbumId), albumApiModel, cacheEntryOptions);

                return albumApiModel;
            }
        }

        public async Task<IAsyncEnumerable<AlbumApiModel>> GetAlbumByArtistId(int id) => 
            (await _albumRepository.GetByArtistId(id)).ConvertAll();

        public async Task<AlbumApiModel> AddAlbum(AlbumApiModel newAlbumApiModel)
        {
            var album = newAlbumApiModel.Convert();
            album = await _albumRepository.Add(album);
            return album.Convert();
        }

        public async Task<bool> UpdateAlbum(AlbumApiModel albumApiModel) => await _albumRepository.Update(albumApiModel.Convert());

        public async Task<bool> DeleteAlbum(int id) => await _albumRepository.Delete(id);
    }
}