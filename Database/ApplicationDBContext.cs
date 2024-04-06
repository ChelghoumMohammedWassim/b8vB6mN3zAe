using b8vB6mN3zAe.Models;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relation Lab to Sector
            modelBuilder.Entity<Lab>()
                .HasMany(lab => lab.Sectors)
                .WithOne(Sector => Sector.Lab)
                .HasForeignKey(Sector => Sector.LabID);

            //relation User to sector
            modelBuilder.Entity<Sector>()
                .HasMany(sector => sector.Users)
                .WithMany(user => user.Sectors)
                .UsingEntity<UserSector>(
                    l => l.HasOne<User>(e => e.User).WithMany(e => e.UsersSectors),
                    r => r.HasOne<Sector>(e => e.Sector).WithMany(e => e.UsersSectors)
                    );

            //relation city to Zip code
            modelBuilder.Entity<City>()
                .HasMany(city => city.ZipCodes)
                .WithOne(ZipCode => ZipCode.City)
                .HasForeignKey(ZipCode => ZipCode.CityID)
                .IsRequired();

            //relation sector to city
            modelBuilder.Entity<Sector>()
                .HasMany(sector => sector.Cities)
                .WithOne(city => city.Sector)
                .HasForeignKey(city => city.SectorID);

            //relation user to city
            modelBuilder.Entity<City>()
                    .HasMany(sector => sector.Users)
                    .WithOne(user => user.City)
                    .HasForeignKey(user => user.CityID);

            //relation lab to city
            modelBuilder.Entity<City>()
                    .HasMany(sector => sector.Labs)
                    .WithOne(lab => lab.City)
                    .HasForeignKey(lab => lab.CityID);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
    }
}