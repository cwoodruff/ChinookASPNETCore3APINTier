using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Chinook.DataEFCoreCmpldQry.Configurations;
using Chinook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataEFCoreCmpldQry
{
    public class ChinookContext : DbContext
    {
        public static long InstanceCount;

        private static readonly Func<ChinookContext, List<Album>> _queryGetAllAlbums =
            EF.CompileQuery((ChinookContext db) =>
                db.Album.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Album>> _queryGetAlbum =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Album.Where(a => a.AlbumId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Album>> _queryGetAlbumsByArtistId =
            EF.CompileQuery((ChinookContext db, int id) => db.Album.Where(a => a.ArtistId == id).AsNoTracking()
                .ToList());

        private static readonly Func<ChinookContext, List<Artist>> _queryGetAllArtists =
            EF.CompileQuery((ChinookContext db) =>
                db.Artist.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Artist>> _queryGetArtist =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Artist.Where(a => a.ArtistId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<Customer>> _queryGetAllCustomers =
            EF.CompileQuery((ChinookContext db) =>
                db.Customer.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Customer>> _queryGetCustomer =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Customer.Where(c => c.CustomerId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Customer>> _queryGetCustomerBySupportRepId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Customer.Where(a => a.SupportRepId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<Employee>> _queryGetAllEmployees =
            EF.CompileQuery((ChinookContext db) =>
                db.Employee.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Employee>> _queryGetEmployee =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.EmployeeId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Employee>> _queryGetDirectReports =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.ReportsTo == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Employee>> _queryGetReportsTo =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Employee.Where(e => e.ReportsTo == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<Genre>> _queryGetAllGenres =
            EF.CompileQuery((ChinookContext db) =>
                db.Genre.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Genre>> _queryGetGenre =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Genre.Where(g => g.GenreId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<InvoiceLine>> _queryGetAllInvoiceLines =
            EF.CompileQuery((ChinookContext db) =>
                db.InvoiceLine.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<InvoiceLine>> _queryGetInvoiceLine =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(i => i.InvoiceLineId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<InvoiceLine>> _queryGetInvoiceLinesByInvoiceId
            = EF.CompileQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(a => a.InvoiceId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<InvoiceLine>> _queryGetInvoiceLinesByTrackId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.InvoiceLine.Where(a => a.TrackId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<Invoice>> _queryGetAllInvoices =
            EF.CompileQuery((ChinookContext db) =>
                db.Invoice.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Invoice>> _queryGetInvoice =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Invoice.Where(i => i.InvoiceId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Invoice>> _queryGetInvoicesByCustomerId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Invoice.Where(a => a.CustomerId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<MediaType>> _queryGetAllMediaTypes =
            EF.CompileQuery((ChinookContext db) =>
                db.MediaType.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<MediaType>> _queryGetMediaType =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.MediaType.Where(m => m.MediaTypeId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<Playlist>> _queryGetAllPlaylists =
            EF.CompileQuery((ChinookContext db) =>
                db.Playlist.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Playlist>> _queryGetPlaylist =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Playlist.Where(p => p.PlaylistId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<PlaylistTrack>> _queryGetAllPlaylistTracks =
            EF.CompileQuery((ChinookContext db) =>
                db.PlaylistTrack.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<PlaylistTrack>> _queryGetPlaylistTrackByPlaylistId
            = EF.CompileQuery((ChinookContext db, int id) =>
                db.PlaylistTrack.Where(a => a.PlaylistId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<PlaylistTrack>> _queryGetPlaylistTracksByTrackId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.PlaylistTrack.Where(a => a.TrackId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, List<Track>> _queryGetAllTracks =
            EF.CompileQuery((ChinookContext db) =>
                db.Track.AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Track>> _queryGetTrack =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(t => t.TrackId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Track>> _queryGetTracksByAlbumId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.AlbumId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Track>> _queryGetTracksByGenreId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.GenreId == id).AsNoTracking().ToList());

        private static readonly Func<ChinookContext, int, List<Track>> _queryGetTracksByMediaTypeId =
            EF.CompileQuery((ChinookContext db, int id) =>
                db.Track.Where(a => a.MediaTypeId == id).AsNoTracking().ToList());

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

        public List<Album> GetAllAlbums() => _queryGetAllAlbums(this);

        public List<Album> GetAlbum(int id) => _queryGetAlbum(this, id);

        public List<Album> GetAlbumsByArtistId(int id) =>
            _queryGetAlbumsByArtistId(this, id);

        public List<Artist> GetAllArtists() => _queryGetAllArtists(this);

        public List<Artist> GetArtist(int id) => _queryGetArtist(this, id);

        public List<Customer> GetAllCustomers() => _queryGetAllCustomers(this);

        public List<Customer> GetCustomer(int id) => _queryGetCustomer(this, id);

        public List<Customer> GetCustomerBySupportRepId(int id) =>
            _queryGetCustomerBySupportRepId(this, id);

        public List<Employee> GetAllEmployees() => _queryGetAllEmployees(this);

        public List<Employee> GetEmployee(int id) => _queryGetEmployee(this, id);

        public List<Employee> GetEmployeeDirectReports(int id) =>
            _queryGetDirectReports(this, id);

        public List<Employee> GetEmployeeGetReportsTo(int id) =>
            _queryGetReportsTo(this, id);

        public List<Genre> GetAllGenres() => _queryGetAllGenres(this);

        public List<Genre> GetGenre(int id) => _queryGetGenre(this, id);

        public List<InvoiceLine> GetAllInvoiceLines() =>
            _queryGetAllInvoiceLines(this);

        public List<InvoiceLine> GetInvoiceLine(int id) =>
            _queryGetInvoiceLine(this, id);

        public List<InvoiceLine> GetInvoiceLinesByInvoiceId(int id) =>
            _queryGetInvoiceLinesByInvoiceId(this, id);

        public List<InvoiceLine> GetInvoiceLinesByTrackId(int id) =>
            _queryGetInvoiceLinesByTrackId(this, id);

        public List<Invoice> GetAllInvoices() => _queryGetAllInvoices(this);

        public List<Invoice> GetInvoice(int id) => _queryGetInvoice(this, id);

        public List<Invoice> GetInvoicesByCustomerId(int id) =>
            _queryGetInvoicesByCustomerId(this, id);

        public List<MediaType> GetAllMediaTypes() => _queryGetAllMediaTypes(this);

        public List<MediaType> GetMediaType(int id) =>
            _queryGetMediaType(this, id);

        public List<Playlist> GetAllPlaylists() => _queryGetAllPlaylists(this);

        public List<Playlist> GetPlaylist(int id) => _queryGetPlaylist(this, id);

        public List<PlaylistTrack> GetAllPlaylistTracks() =>
            _queryGetAllPlaylistTracks(this);

        public List<PlaylistTrack> GetPlaylistTrackByPlaylistId(int id) =>
            _queryGetPlaylistTrackByPlaylistId(this, id);

        public List<PlaylistTrack> GetPlaylistTracksByTrackId(int id) =>
            _queryGetPlaylistTracksByTrackId(this, id);

        public List<Track> GetAllTracks() => _queryGetAllTracks(this);

        public List<Track> GetTrack(int id) => _queryGetTrack(this, id);

        public List<Track> GetTracksByAlbumId(int id) =>
            _queryGetTracksByAlbumId(this, id);

        public List<Track> GetTracksByGenreId(int id) =>
            _queryGetTracksByGenreId(this, id);

        public List<Track> GetTracksByMediaTypeId(int id) =>
            _queryGetTracksByMediaTypeId(this, id);
    }
}