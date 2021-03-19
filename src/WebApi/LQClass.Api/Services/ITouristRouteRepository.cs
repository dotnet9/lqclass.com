using LQClass.Api.Helper;
using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Services
{
	public interface ITouristRouteRepository
	{
		Task<PaginationList<TouristRoute>> GetRouristRoutesAsync(
		  string key, string operatorType, int? ratingValue
		  , int pageSize, int pageNumber
		  , string orderBy);
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
		Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId);
		void DeleteShoppingCartItem(LineItem lineItem);
		Task<IEnumerable<LineItem>> GetShoppingCartsByIdListAsync(IEnumerable<int> ids);
		void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);
		Task AddOrderAsync(Order order);
		Task<PaginationList<Order>> GetOrdersByUserIdAsync(string userId, int pageSize, int pageNumber);
		Task<Order> GetOrderByIdAsync(Guid orderId);
		Task<bool> SaveAsync();
	}
}
