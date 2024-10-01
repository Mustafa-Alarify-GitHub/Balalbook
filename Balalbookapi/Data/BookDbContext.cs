using Microsoft.EntityFrameworkCore;
using Balalbookapi.Models;
namespace Balalbookapi.Data
{
    public class BookDbContext: DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }

        public DbSet<Book> books { get; set; }
        public DbSet<Reservation> reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Book_Name).HasColumnName("Book_Name");
                entity.Property(e => e.Blender_Name).HasColumnName("Blender_Name");
                entity.Property(e => e.Version).HasColumnName("Version");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Book_Id).HasColumnName("Book_Id");
                entity.Property(e => e.date_from_star).HasColumnName("date_from_star");
                entity.Property(e => e.date_to_end).HasColumnName("date_to_end");

            });

        }

    }
}
