using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Services
{
  public interface ITouristRouteRepository
  {
    IEnumerable<TouristRoute> GetRouristRoutes();
    TouristRoute GetTouristRoute(Guid touristRouteId);
  }
}
