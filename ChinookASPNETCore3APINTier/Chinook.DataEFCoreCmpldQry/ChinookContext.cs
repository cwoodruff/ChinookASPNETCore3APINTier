using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chinook.DataEFCoreCmpldQry.Configurations;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry
{
    public class ChinookContext : DbContext
    {
        public static long InstanceCount;

        private static readonly Func<ChinookContext, Task<List<Album>>> _queryGetAllAlbums =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Album.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Album>>> _queryGetAlbum =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Album.Where(a => a.AlbumId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Album>>> _queryGetAlbumsByArtistId =
            EF.CompileAsyncQuery((ChinookContext db, int id) => db.Album.Where(a => a.ArtistId == id).AsNoTracking()
                .ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<Artist>>> _queryGetAllArtists =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Artist.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Artist>>> _queryGetArtist =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Artist.Where(a => a.ArtistId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<Customer>>> _queryGetAllCustomers =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Customer.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Customer>>> _queryGetCustomer =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Customer.Where(c => c.CustomerId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Customer>>> _queryGetCustomerBySupportRepId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Customer.Where(a => a.SupportRepId == id).AsNoTracking().ToListAsync(new CancellationToken())
                    .Result);

        private static readonly Func<ChinookContext, Task<List<Employee>>> _queryGetAllEmployees =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Employee.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Employee>>> _queryGetEmployee =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.EmployeeId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Employee>>> _queryGetDirectReports =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.ReportsTo == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Employee>>> _queryGetReportsTo =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.ReportsTo == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<Genre>>> _queryGetAllGenres =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Genre.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Genre>>> _queryGetGenre =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Genre.Where(g => g.GenreId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<InvoiceLine>>> _queryGetAllInvoiceLines =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.InvoiceLine.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<InvoiceLine>>> _queryGetInvoiceLine =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(i => i.InvoiceLineId == id).AsNoTracking().ToListAsync(new CancellationToken())
                    .Result);

        private static readonly Func<ChinookContext, int, Task<List<InvoiceLine>>> _queryGetInvoiceLinesByInvoiceId
            = EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(a => a.InvoiceId == id).AsNoTracking().ToListAsync(new CancellationToken())
                    .Result);

        private static readonly Func<ChinookContext, int, Task<List<InvoiceLine>>> _queryGetInvoiceLinesByTrackId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(a => a.TrackId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<Invoice>>> _queryGetAllInvoices =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Invoice.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Invoice>>> _queryGetInvoice =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Invoice.Where(i => i.InvoiceId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Invoice>>> _queryGetInvoicesByCustomerId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Invoice.Where(a => a.CustomerId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<MediaType>>> _queryGetAllMediaTypes =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.MediaType.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<MediaType>>> _queryGetMediaType =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.MediaType.Where(m => m.MediaTypeId == id).AsNoTracking().ToListAsync(new CancellationToken())
                    .Result);

        private static readonly Func<ChinookContext, Task<List<Playlist>>> _queryGetAllPlaylists =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Playlist.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Playlist>>> _queryGetPlaylist =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Playlist.Where(p => p.PlaylistId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, Task<List<PlaylistTrack>>> _queryGetAllPlaylistTracks =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.PlaylistTrack.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<PlaylistTrack>>> _queryGetPlaylistTrackByPlaylistId
            = EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.PlaylistTrack.Where(a => a.PlaylistId == id).AsNoTracking().ToListAsync(new CancellationToken())
                    .Result);

        private static readonly Func<ChinookContext, int, Task<List<PlaylistTrack>>> _queryGetPlaylistTracksByTrackId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.PlaylistTrack.Where(a => a.TrackId == id).AsNoTracking().ToListAsync(new CancellationToken())
                    .Result);

        private static readonly Func<ChinookContext, Task<List<Track>>> _queryGetAllTracks =
            EF.CompileAsyncQuery((ChinookContext db) =>
                db.Track.AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Track>>> _queryGetTrack =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Track.Where(t => t.TrackId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Track>>> _queryGetTracksByAlbumId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.AlbumId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Track>>> _queryGetTracksByGenreId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.GenreId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        private static readonly Func<ChinookContext, int, Task<List<Track>>> _queryGetTracksByMediaTypeId =
            EF.CompileAsyncQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.MediaTypeId == id).AsNoTracking().ToListAsync(new CancellationToken()).Result);

        public ChinookContext(DbContextOptions options) : base(options)
        {
            Interlocked.Increment(ref InstanceCount);
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

        public Task<List<Album>> GetAllAlbumsAsync() => _queryGetAllAlbums(this);

        public async Task<List<Album>> GetAlbumAsync(int id) => await _queryGetAlbum(this, id);

        public async Task<List<Album>> GetAlbumsByArtistIdAsync(int id) =>
            await _queryGetAlbumsByArtistId(this, id);

        public async Task<List<Artist>> GetAllArtistsAsync() => await _queryGetAllArtists(this);

        public async Task<List<Artist>> GetArtistAsync(int id) => await _queryGetArtist(this, id);

        public async Task<List<Customer>> GetAllCustomersAsync() => await _queryGetAllCustomers(this);

        public async Task<List<Customer>> GetCustomerAsync(int id) => await _queryGetCustomer(this, id);

        public async Task<List<Customer>> GetCustomerBySupportRepIdAsync(int id) =>
            await _queryGetCustomerBySupportRepId(this, id);

        public async Task<List<Employee>> GetAllEmployeesAsync() => await _queryGetAllEmployees(this);

        public async Task<List<Employee>> GetEmployeeAsync(int id) => await _queryGetEmployee(this, id);

        public async Task<List<Employee>> GetEmployeeDirectReportsAsync(int id) =>
            await _queryGetDirectReports(this, id);

        public async Task<List<Employee>> GetEmployeeGetReportsToAsync(int id) =>
            await _queryGetReportsTo(this, id);

        public async Task<List<Genre>> GetAllGenresAsync() => await _queryGetAllGenres(this);

        public async Task<List<Genre>> GetGenreAsync(int id) => await _queryGetGenre(this, id);

        public async Task<List<InvoiceLine>> GetAllInvoiceLinesAsync() =>
            await _queryGetAllInvoiceLines(this);

        public async Task<List<InvoiceLine>> GetInvoiceLineAsync(int id) =>
            await _queryGetInvoiceLine(this, id);

        public async Task<List<InvoiceLine>> GetInvoiceLinesByInvoiceIdAsync(int id) =>
            await _queryGetInvoiceLinesByInvoiceId(this, id);

        public async Task<List<InvoiceLine>> GetInvoiceLinesByTrackIdAsync(int id) =>
            await _queryGetInvoiceLinesByTrackId(this, id);

        public async Task<List<Invoice>> GetAllInvoicesAsync() => await _queryGetAllInvoices(this);

        public async Task<List<Invoice>> GetInvoiceAsync(int id) => await _queryGetInvoice(this, id);

        public async Task<List<Invoice>> GetInvoicesByCustomerIdAsync(int id) =>
            await _queryGetInvoicesByCustomerId(this, id);

        public async Task<List<MediaType>> GetAllMediaTypesAsync() => await _queryGetAllMediaTypes(this);

        public async Task<List<MediaType>> GetMediaTypeAsync(int id) =>
            await _queryGetMediaType(this, id);

        public async Task<List<Playlist>> GetAllPlaylistsAsync() => await _queryGetAllPlaylists(this);

        public async Task<List<Playlist>> GetPlaylistAsync(int id) => await _queryGetPlaylist(this, id);

        public async Task<List<PlaylistTrack>> GetAllPlaylistTracksAsync() =>
            await _queryGetAllPlaylistTracks(this);

        public async Task<List<PlaylistTrack>> GetPlaylistTrackByPlaylistId(int id) =>
            await _queryGetPlaylistTrackByPlaylistId(this, id);

        public async Task<List<PlaylistTrack>> GetPlaylistTracksByTrackIdAsync(int id) =>
            await _queryGetPlaylistTracksByTrackId(this, id);

        public async Task<List<Track>> GetAllTracksAsync() => await _queryGetAllTracks(this);

        public async Task<List<Track>> GetTrackAsync(int id) => await _queryGetTrack(this, id);

        public async Task<List<Track>> GetTracksByAlbumIdAsync(int id) =>
            await _queryGetTracksByAlbumId(this, id);

        public async Task<List<Track>> GetTracksByGenreIdAsync(int id) =>
            await _queryGetTracksByGenreId(this, id);

        public async Task<List<Track>> GetTracksByMediaTypeIdAsync(int id) =>
            await _queryGetTracksByMediaTypeId(this, id);
    }
}