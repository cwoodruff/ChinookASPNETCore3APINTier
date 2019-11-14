using System;
using System.Collections.Generic;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<AlbumApiModel> GetAllAlbum()
        {
            var albums = _albumRepository.GetAll().ConvertAll();
            foreach (var album in albums)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Album-", album.AlbumId), album, cacheEntryOptions);
            }

            return albums;
        }

        public AlbumApiModel GetAlbumById(int id)
        {
            var albumApiModelCached = _cache.Get<AlbumApiModel>(string.Concat("Album-", id));

            if (albumApiModelCached != null)
            {
                return albumApiModelCached;
            }
            else
            {
                var albumApiModel = (_albumRepository.GetById(id)).Convert();
                albumApiModel.ArtistName = (_artistRepository.GetById(albumApiModel.ArtistId)).Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Album-", albumApiModel.AlbumId), albumApiModel, cacheEntryOptions);

                return albumApiModel;
            }
        }

        public IEnumerable<AlbumApiModel> GetAlbumByArtistId(int id)
        {
            var albums = _albumRepository.GetByArtistId(id);
            return albums.ConvertAll();
        }

        public AlbumApiModel AddAlbum(AlbumApiModel newAlbumApiModel)
        {
            var album = newAlbumApiModel.Convert();

            album = _albumRepository.Add(album);
            newAlbumApiModel.AlbumId = album.AlbumId;
            return newAlbumApiModel;
        }

        public bool UpdateAlbum(AlbumApiModel albumApiModel)
        {
            var album = _albumRepository.GetById(albumApiModel.AlbumId);

            if (album is null) return false;
            album.AlbumId = albumApiModel.AlbumId;
            album.Title = albumApiModel.Title;
            album.ArtistId = albumApiModel.ArtistId;

            return _albumRepository.Update(album);
        }

        public bool DeleteAlbum(int id)
            => _albumRepository.Delete(id);
    }
}