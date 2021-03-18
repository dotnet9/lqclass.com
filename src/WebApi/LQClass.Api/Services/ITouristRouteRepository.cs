using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Services
{
  public interface ITouristRouteRepository
  {
    Task<IEnumerable<TouristRoute>> GetRouristRoutesAsync(string key, string operatorType, int? ratingValue);
    Task<TouristRoute> GetTouristRouteAsync(Guid touristRouteId);
    Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDListAsync(IEnumerable<Guid> ids);
    Task<bool> TouristRouteExistsAsync(Guid touristRouteId);
    Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIdAsync(Guid touristRouteId);
    Task<TouristRoutePicture> GetPictureAsync(int pictureId);
    void AddTouristRoute(TouristRoute touristRoute);
    void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture);
    void DeleteTouristRoute(TouristRoute touristRoute);
    void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes);
    void DeleteTouristRoutePicture(TouristRoutePicture picture);
    Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);
    Task CreateShoppingCartAsync(ShoppingCart shoppingCart);
    Task AddShoppingCartItemAsync(LineItem lineItem);
    Task<bool> SaveAsync();
  }
}
