using LQClass.Api.Database;
using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			return _context.TouristRoutes.ToList();
		}

		public TouristRoute GetTouristRoute(Guid touristRouteId)
		{
			return _context.TouristRoutes.FirstOrDefault(n => n.Id == touristRouteId);
		}
	}
}
