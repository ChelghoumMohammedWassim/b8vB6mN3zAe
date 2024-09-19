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
                            .WithOne(sector => sector.Lab)
                            .HasForeignKey(sector => sector.LabID);

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
                            .WithOne(zipCode => zipCode.City)
                            .HasForeignKey(zipCode => zipCode.CityID)
                            .OnDelete(DeleteBehavior.SetNull);

                        //relation sector to city
                        modelBuilder.Entity<Sector>()
                            .HasMany(sector => sector.Cities)
                            .WithOne(city => city.Sector)
                            .HasForeignKey(city => city.SectorID)
                            .OnDelete(DeleteBehavior.SetNull);

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

                        //relation framer to zip code
                        modelBuilder.Entity<ZipCode>()
                                .HasMany(zipCode => zipCode.Farmers)
                                .WithOne(farmer => farmer.ZipCode)
                                .HasForeignKey(farmer => farmer.ZipCodeID);

                        //relation farmer to Land
                        modelBuilder.Entity<Farmer>()
                                .HasMany(farmer => farmer.Lands)
                                .WithOne(land => land.Farmer)
                                .HasForeignKey(land => land.FarmerID);



                        //relation Land to Exploitation
                        modelBuilder.Entity<Land>()
                                .HasMany(land => land.Exploitations)
                                .WithOne(exploitation => exploitation.Land)
                                .HasForeignKey(exploitation => exploitation.LandID)
                                .IsRequired();

                        //relation Exploitation to Plot
                        modelBuilder.Entity<Exploitation>()
                                .HasMany(exploitation => exploitation.Plots)
                                .WithOne(plot => plot.Exploitation)
                                .HasForeignKey(plot => plot.ExploitationID)
                                .IsRequired();

                        //relation Plot to Sample
                        modelBuilder.Entity<Plot>()
                                .HasMany(plot => plot.Samples)
                                .WithOne(sample => sample.Plot)
                                .HasForeignKey(sample => sample.PlotID)
                                .IsRequired();

                        //relation Plot to Position
                        modelBuilder.Entity<Plot>()
                                .HasMany(plot => plot.Positions)
                                .WithOne(position => position.Plot)
                                .HasForeignKey(position => position.PlotId)
                                .IsRequired();
                       
                       
                        //relation Sample to Analysis
                        modelBuilder.Entity<Sample>()
                                .HasMany(sample => sample.Analyses)
                                .WithOne(analyses => analyses.Sample)
                                .HasForeignKey(analyses => analyses.SampleID)
                                .IsRequired();

                        //relation Recommendation to RecommendedFertilizers
                        modelBuilder.Entity<Recommendation>()
                                .HasMany(recommendation => recommendation.RecommendedFertilizers)
                                .WithOne(recommendedFertilizers => recommendedFertilizers.Recommendation)
                                .HasForeignKey(recommendedFertilizers => recommendedFertilizers.RecommendationID)
                                .IsRequired();
                        

                        //relation Fertilizer to RecommendedFertilizers
                        modelBuilder.Entity<Fertilizer>()
                                .HasMany(fertilizer => fertilizer.RecommendedFertilizers)
                                .WithOne(recommendedFertilizers => recommendedFertilizers.Fertilizer)
                                .HasForeignKey(recommendedFertilizers => recommendedFertilizers.FertilizerID)
                                .IsRequired();

                        //relation Analysis to Recommendation
                        modelBuilder.Entity<Analysis>()
                                .HasMany(analysis => analysis.Recommendations)
                                .WithOne(recommendation => recommendation.Analysis)
                                .HasForeignKey(recommendation => recommendation.AnalysisID)
                                .IsRequired();

                        
                        //relation Sample to Lab
                        modelBuilder.Entity<Lab>()
                                .HasMany(lab => lab.Samples)
                                .WithOne(sample => sample.Lab)
                                .HasForeignKey(sample => sample.LabID)
                                .IsRequired();

                        //relation Sample to Lab
                        modelBuilder.Entity<User>()
                                .HasMany(user => user.Recommendations)
                                .WithOne(recommendation => recommendation.User)
                                .HasForeignKey(recommendation => recommendation.UserID)
                                .IsRequired();



                }

                public DbSet<User> Users { get; set; }
                public DbSet<Sector> Sectors { get; set; }
                public DbSet<City> Cities { get; set; }
                public DbSet<Lab> Labs { get; set; }
                public DbSet<ZipCode> ZipCodes { get; set; }
                public DbSet<UserSector> UserSector { get; set; }
                public DbSet<Farmer> Farmers { get; set; }
                public DbSet<Land> Lands { get; set; }
                public DbSet<Position> Positions { get; set; }
                public DbSet<Exploitation> Exploitations { get; set; }
                public DbSet<Plot> Plots { get; set; }
                public DbSet<Sample> Samples { get; set; }
                public DbSet<Analysis> Analysis { get; set; }
                public DbSet<Recommendation> Recommendations { get; set; }
                public DbSet<RecommendedFertilizer> RecommendedFertilizers { get; set; }
                public DbSet<Fertilizer> Fertilizers { get; set; }
        }
}