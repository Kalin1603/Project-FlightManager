namespace FlightManager.Data
{
    using FlightManager.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

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

            var hasher = new PasswordHasher<User>();
            string adminRoleId = Guid.NewGuid().ToString();
            string adminId = Guid.NewGuid().ToString();

            builder.Entity<User>(option =>
            {
                option
                .HasIndex(x => x.NationalId)
                .IsUnique();
            });
            builder.Entity<IdentityRole<string>>(option =>
            {
                option.HasData(new IdentityRole<string>()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
                option.HasData(new IdentityRole<string>()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER"
                });
            });
            builder.Entity<User>(option =>
            {
                option.HasData(new User()
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    FirstName = "Admin",
                    LastName = "Admin",
                    NationalId = "1234567890",
                    Email = "admin@admin.bg",
                    NormalizedEmail = "ADMIN@ADMIN.BG",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }); ;
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });
            base.OnModelCreating(builder);
        }

    }

       
    
}
