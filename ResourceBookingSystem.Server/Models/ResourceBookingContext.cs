using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Server.Models;

public class ResourceBookingContext : DbContext
{
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public string DbPath { get; }

    public ResourceBookingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "resourceBooking.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Resource)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
