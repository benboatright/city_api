using System;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
	public class CityInfoContext : DbContext
	{
		public DbSet<City> Cities { get; set; } = null!;

		public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

		public CityInfoContext(DbContextOptions<CityInfoContext> options)
			:base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>().HasData(
				new City("New York City")
				{
					Id = 1,
					Description = "The one with a big park."
				},
				new City("Antwerp")
				{
					Id = 2,
					Description = "The one with a cathedral."
				},
				new City("Paris")
				{
					Id = 3,
					Description = "The one with a tower."
				});

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
					CityId = 1,
                    Description = "The big park."
                },
                new PointOfInterest("Empire State building")
                {
                    Id = 2,
					CityId = 1,
                    Description = "The building."
                },
                new PointOfInterest("Cathedral")
                {
                    Id = 3,
					CityId = 2,
                    Description = "The cathedral."
                },
                new PointOfInterest("Eiffel Tower")
                {
                    Id = 4,
                    CityId = 3,
                    Description = "The tower."
                });
            base.OnModelCreating(modelBuilder);
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlite("connecitonstring");
		//	base.OnConfiguring(optionsBuilder);
		//}
	}
}

