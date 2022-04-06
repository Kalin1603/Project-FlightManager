namespace FlightManager.Data
{
    using FlightManager.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flight> Flights { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Passanger> Passangers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(option =>
            {
                option
                .HasIndex(x => x.NationalId)
                .IsUnique();
            });

            base.OnModelCreating(builder);
        }

       
    }
}
