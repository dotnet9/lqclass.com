using LQClass.Api.Database;
using LQClass.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LQClass.Api.Services
{
	public class TouristRouteRepository : ITouristRouteRepository
	{
		private readonly AppDbContext _context;

		public TouristRouteRepository(AppDbContext context)
		{
			_context = context;
		}

		public IEnumerable<TouristRoute> GetRouristRoutes(
			string key
			, string operatorType
			, int? ratingValue
			)
		{
			IQueryable<TouristRoute> result = _context.TouristRoutes.Include(t => t.TouristRoutePictures);
			if (!string.IsNullOrWhiteSpace(key))
			{
				key = key.Trim();
				result = result.Where(t => t.Title.Contains(key));
			}
			if (ratingValue >= 0)
			{
				result = operatorType switch
				{
					"largerThan" => result.Where(t => t.Rating >= ratingValue),
					"lessThan" => result.Where(t => t.Rating <= ratingValue),
					_ => result.Where(t => t.Rating == ratingValue)
				};
			}
			// include vs join
			return result.ToList();
		}

		public TouristRoute GetTouristRoute(Guid touristRouteId)
		{
			return _context.TouristRoutes.Include(t => t.TouristRoutePictures).FirstOrDefault(n => n.Id == touristRouteId);
		}

		public bool TouristRouteExists(Guid touristRouteId)
		{
			return _context.TouristRoutes.Any(t => t.Id == touristRouteId);
		}

		public IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId)
		{
			return _context.TouristRoutePictures
			  .Where(p => p.TouristRouteId == touristRouteId).ToList();
		}

		public TouristRoutePicture GetPicture(int pictureId)
		{
			return _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefault();
		}
	}
}
