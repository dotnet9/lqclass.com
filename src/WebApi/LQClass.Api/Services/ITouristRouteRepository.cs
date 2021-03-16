using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Services
{
	public interface ITouristRouteRepository
	{
		IEnumerable<TouristRoute> GetRouristRoutes(string key, string operatorType, int? ratingValue);
		TouristRoute GetTouristRoute(Guid touristRouteId);
		bool TouristRouteExists(Guid touristRouteId);
		IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRouteId);
		TouristRoutePicture GetPicture(int pictureId);
	}
}
