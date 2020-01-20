using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chinook.Data.Configurations;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Data
{
    public class ChinookContext : DbContext
    {
        private static long _instanceCount;

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Album>>> _queryGetAllAlbums =
            EF.CompileAsyncQuery((ChinookContext db) => db.Album.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Album>> _queryGetAlbum =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Album.Find(id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Album>>> _queryGetAlbumsByArtistId =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Album.AsAsyncEnumerable().Where(a => a.ArtistId == id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Artist>>> _queryGetAllArtists =
            EF.CompileAsyncQuery((ChinookContext db) => db.Artist.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Artist>> _queryGetArtist =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Artist.Find(id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Customer>>> _queryGetAllCustomers =
            EF.CompileAsyncQuery((ChinookContext db) => db.Customer.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Customer>> _queryGetCustomer =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Customer.Find(id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Customer>>> _queryGetCustomerBySupportRepId = 
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Customer.AsAsyncEnumerable().Where(a => a.SupportRepId == id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Employee>>> _queryGetAllEmployees =
            EF.CompileAsyncQuery((ChinookContext db) => db.Employee.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Employee>> _queryGetEmployee =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Employee.Find(id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Employee>>> _queryGetDirectReports =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Employee.AsAsyncEnumerable().Where(e => e.ReportsTo == id));

        private static readonly Func<ChinookContext, int, Task<Employee>> _queryGetReportsTo =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Employee.FirstOrDefault(e => e.ReportsTo == id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Genre>>> _queryGetAllGenres =
            EF.CompileAsyncQuery((ChinookContext db) => db.Genre.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Genre>> _queryGetGenre =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Genre.Find(id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<InvoiceLine>>> _queryGetAllInvoiceLines =
            EF.CompileAsyncQuery((ChinookContext db) => db.InvoiceLine.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<InvoiceLine>> _queryGetInvoiceLine =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.InvoiceLine.Find(id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<InvoiceLine>>> _queryGetInvoiceLinesByInvoiceId = 
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.InvoiceLine.AsAsyncEnumerable().Where(a => a.InvoiceId == id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<InvoiceLine>>> _queryGetInvoiceLinesByTrackId = 
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.InvoiceLine.AsAsyncEnumerable().Where(a => a.TrackId == id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Invoice>>> _queryGetAllInvoices =
            EF.CompileAsyncQuery((ChinookContext db) => db.Invoice.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Invoice>> _queryGetInvoice =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Invoice.Find(id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Invoice>>> _queryGetInvoicesByCustomerId
            = EF.CompileAsyncQuery((ChinookContext db, int id) => db.Invoice.AsAsyncEnumerable().Where(a => a.CustomerId == id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<MediaType>>> _queryGetAllMediaTypes =
            EF.CompileAsyncQuery((ChinookContext db) => db.MediaType.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<MediaType>> _queryGetMediaType =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.MediaType.Find(id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Playlist>>> _queryGetAllPlaylists =
            EF.CompileAsyncQuery((ChinookContext db) => db.Playlist.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Playlist>> _queryGetPlaylist =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Playlist.Find(id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<PlaylistTrack>>> _queryGetAllPlaylistTracks =
            EF.CompileAsyncQuery((ChinookContext db) => db.PlaylistTrack.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<PlaylistTrack>>> _queryGetPlaylistTrackByPlaylistId = 
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.PlaylistTrack.AsAsyncEnumerable().Where(a => a.PlaylistId == id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<PlaylistTrack>>> _queryGetPlaylistTracksByTrackId = 
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.PlaylistTrack.AsAsyncEnumerable().Where(a => a.TrackId == id));

        private static readonly Func<ChinookContext, Task<IAsyncEnumerable<Track>>> _queryGetAllTracks =
            EF.CompileAsyncQuery((ChinookContext db) => db.Track.AsAsyncEnumerable());

        private static readonly Func<ChinookContext, int, Task<Track>> _queryGetTrack =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Track.Find(id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Track>>> _queryGetTracksByAlbumId =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Track.AsAsyncEnumerable().Where(a => a.AlbumId == id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Track>>> _queryGetTracksByGenreId =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Track.AsAsyncEnumerable().Where(a => a.GenreId == id));

        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Track>>> _queryGetTracksByMediaTypeId =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Track.AsAsyncEnumerable().Where(a => a.MediaTypeId == id));
        
        private static readonly Func<ChinookContext, int, Task<IAsyncEnumerable<Track>>> _queryGetTracksByPlayListId =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Track.AsAsyncEnumerable().Where(a => a.MediaTypeId == id));

        public ChinookContext(DbContextOptions options) : base(options)
        {
            Interlocked.Increment(ref _instanceCount);
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTrack { get; set; }
        public virtual DbSet<Track> Track { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AlbumConfiguration(modelBuilder.Entity<Album>());
            new ArtistConfiguration(modelBuilder.Entity<Artist>());
            new CustomerConfiguration(modelBuilder.Entity<Customer>());
            new EmployeeConfiguration(modelBuilder.Entity<Employee>());
            new GenreConfiguration(modelBuilder.Entity<Genre>());
            new InvoiceConfiguration(modelBuilder.Entity<Invoice>());
            new InvoiceLineConfiguration(modelBuilder.Entity<InvoiceLine>());
            new MediaTypeConfiguration(modelBuilder.Entity<MediaType>());
            new PlaylistConfiguration(modelBuilder.Entity<Playlist>());
            new PlaylistTrackConfiguration(modelBuilder.Entity<PlaylistTrack>());
            new TrackConfiguration(modelBuilder.Entity<Track>());
        }

        public Task<IAsyncEnumerable<Album>> GetAllAlbums() => _queryGetAllAlbums(this);

        public Task<Album> GetAlbum(int id) => _queryGetAlbum(this, id);

        public Task<IAsyncEnumerable<Album>> GetAlbumsByArtistId(int id) => _queryGetAlbumsByArtistId(this, id);

        public Task<IAsyncEnumerable<Artist>> GetAllArtists() => _queryGetAllArtists(this);

        public Task<Artist> GetArtist(int id) => _queryGetArtist(this, id);

        public Task<IAsyncEnumerable<Customer>> GetAllCustomers() => _queryGetAllCustomers(this);

        public Task<Customer> GetCustomer(int id) => _queryGetCustomer(this, id);

        public Task<IAsyncEnumerable<Customer>> GetCustomerBySupportRepId(int id) => _queryGetCustomerBySupportRepId(this, id);

        public Task<IAsyncEnumerable<Employee>> GetAllEmployees() => _queryGetAllEmployees(this);

        public Task<Employee> GetEmployee(int id) => _queryGetEmployee(this, id);

        public Task<IAsyncEnumerable<Employee>> GetEmployeeDirectReports(int id) => _queryGetDirectReports(this, id);

        public Task<Employee> GetEmployeeGetReportsTo(int id) => _queryGetReportsTo(this, id);

        public Task<IAsyncEnumerable<Genre>> GetAllGenres() => _queryGetAllGenres(this);

        public Task<Genre> GetGenre(int id) => _queryGetGenre(this, id);

        public Task<IAsyncEnumerable<InvoiceLine>> GetAllInvoiceLines() => _queryGetAllInvoiceLines(this);

        public Task<InvoiceLine> GetInvoiceLine(int id) => _queryGetInvoiceLine(this, id);

        public Task<IAsyncEnumerable<InvoiceLine>> GetInvoiceLinesByInvoiceId(int id) => _queryGetInvoiceLinesByInvoiceId(this, id);

        public Task<IAsyncEnumerable<InvoiceLine>> GetInvoiceLinesByTrackId(int id) => _queryGetInvoiceLinesByTrackId(this, id);

        public Task<IAsyncEnumerable<Invoice>> GetAllInvoices() => _queryGetAllInvoices(this);

        public Task<Invoice> GetInvoice(int id) => _queryGetInvoice(this, id);

        public Task<IAsyncEnumerable<Invoice>> GetInvoicesByCustomerId(int id) => _queryGetInvoicesByCustomerId(this, id);

        public Task<IAsyncEnumerable<MediaType>> GetAllMediaTypes() => _queryGetAllMediaTypes(this);

        public Task<MediaType> GetMediaType(int id) => _queryGetMediaType(this, id);

        public Task<IAsyncEnumerable<Playlist>> GetAllPlaylists() => _queryGetAllPlaylists(this);

        public Task<Playlist> GetPlaylist(int id) => _queryGetPlaylist(this, id);

        public Task<IAsyncEnumerable<PlaylistTrack>> GetAllPlaylistTracks() => _queryGetAllPlaylistTracks(this);

        public Task<IAsyncEnumerable<PlaylistTrack>> GetPlaylistTrackByPlaylistId(int id) => _queryGetPlaylistTrackByPlaylistId(this, id);

        public Task<IAsyncEnumerable<PlaylistTrack>> GetPlaylistTracksByTrackId(int id) => _queryGetPlaylistTracksByTrackId(this, id);

        public Task<IAsyncEnumerable<Track>> GetAllTracks() => _queryGetAllTracks(this);

        public Task<Track> GetTrack(int id) => _queryGetTrack(this, id);

        public Task<IAsyncEnumerable<Track>> GetTracksByAlbumId(int id) => _queryGetTracksByAlbumId(this, id);

        public Task<IAsyncEnumerable<Track>> GetTracksByGenreId(int id) => _queryGetTracksByGenreId(this, id);

        public Task<IAsyncEnumerable<Track>> GetTracksByMediaTypeId(int id) => _queryGetTracksByMediaTypeId(this, id);
    }
}