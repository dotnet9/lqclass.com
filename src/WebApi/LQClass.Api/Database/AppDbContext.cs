using LQClass.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Database
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		  : base(options)
		{

		}

		public DbSet<TouristRoute> TouristRoutes { get; set; }
		public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var touristRouteID = Guid.NewGuid();
			modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
			{
				Id = touristRouteID,
				Title = "测试标题",
				Description = "说明",
				OriginalPrice = 0,
				CreateTime = DateTime.UtcNow,
				Rating = 3.8,
				DepartureCity = DepartureCity.ChengDu,
				TravelDays = TravelDays.Three,
				TripType = TripType.Person
			});
			modelBuilder.Entity<TouristRoutePicture>().HasData(new TouristRoutePicture
			{
				Id = 1,
				Url = "test",
				TouristRouteId = touristRouteID
			});
			base.OnModelCreating(modelBuilder);
		}
	}
}
