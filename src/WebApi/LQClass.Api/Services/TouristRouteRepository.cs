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

    public IEnumerable<TouristRoute> GetRouristRoutes()
		{
      // include vs join
			return _context.TouristRoutes.Include(t=>t.TouristRoutePictures);
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
