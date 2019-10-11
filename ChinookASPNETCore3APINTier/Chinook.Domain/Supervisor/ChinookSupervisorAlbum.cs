using System;
using System.Collections.Generic;
using Chinook.Domain.Extensions;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<AlbumApiModel> GetAllAlbum()
        {
            var albums = _albumRepository.GetAll();
            foreach (var album in albums)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(album.AlbumId, album, cacheEntryOptions);
            }

            return albums.ConvertAll();
        }

        public AlbumApiModel GetAlbumById(int id)
        {
            var album = _cache.Get<Album>(id);

            if (album != null)
            {
                var albumApiModel = album.Convert;
                albumApiModel.ArtistName = (_artistRepository.GetById(albumApiModel.ArtistId)).Name;
                return albumApiModel;
            }
            else
            {
                var albumApiModel = (_albumRepository.GetById(id)).Convert;
                albumApiModel.ArtistName = (_artistRepository.GetById(albumApiModel.ArtistId)).Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(albumApiModel.AlbumId, albumApiModel, cacheEntryOptions);

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
            var album = newAlbumApiModel.Convert;

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