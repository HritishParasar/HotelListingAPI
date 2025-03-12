using HotelListingAPI.Data;
using HotelListingAPI.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Context
{
    public class HotelDbContext : IdentityDbContext<ApiUser>
    {
        public HotelDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<Country>().HasData(
                new Country { 
                  Id = 1,
                  Name = "India",
                  ShortName = "IND"
                },
                new Country
                {
                    Id = 2,
                    Name = "Nepal",
                    ShortName = "NPL"
                },
                new Country
                {
                    Id = 3,
                    Name = "Vietnam",
                    ShortName = "VTNM"
                }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id= 1,
                    Name ="Mango Hotel",
                    Address = "Bhubaneswar,India",
                    Rating = 4.1,
                    CountryId= 1
                },
                new Hotel
                {
                    Id= 2,
                    Name = "ITC Kohinoor",
                    Address ="Hitech City, Hyderabad",
                    Rating = 4.6,
                    CountryId= 1
                },
                new Hotel
                {
                    Id= 3,
                    Name= "The Vivanta",
                    Address = "Kathmandu, Nepal",
                    Rating= 4.2,
                    CountryId= 2
                },
                new Hotel
                {
                    Id= 4,
                    Name= "Welcome Hotel",
                    Address = "Hanoi, Vietnam",
                    Rating = 4.4,
                    CountryId= 3
                }
                );
        }
    }
}
