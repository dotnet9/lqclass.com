using LQClass.Api.Database;
using LQClass.Api.Dtos;
using LQClass.Api.Helper;
using LQClass.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LQClass.Api.Services
{
	public class TouristRouteRepository : ITouristRouteRepository
	{
		private readonly AppDbContext _context;
		private readonly IPropertyMappingService propertyMappingService;

		public TouristRouteRepository(
			AppDbContext context,
			IPropertyMappingService propertyMappingService
			)
		{
			_context = context;
			this.propertyMappingService = propertyMappingService;
		}

		// /api/touristRoutes?orderby=raing desc, originalPrice desc
		public async Task<PaginationList<TouristRoute>> GetRouristRoutesAsync(
		  string key,
		  string operatorType,
		  int? ratingValue,
		  int pageSize,
		  int pageNumber,
		  string orderBy
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

			if (!string.IsNullOrWhiteSpace(orderBy))
			{
				var touristRouteMappingDictionary = propertyMappingService
					.GetPropertyMapping<TouristRouteDto, TouristRoute>();

				result = result.ApplySort(orderBy, touristRouteMappingDictionary);
			}

			// include vs join
			return await PaginationList<TouristRoute>.CreateAsync(pageNumber, pageSize, result);
		}

		public async Task<TouristRoute> GetTouristRouteAsync(Guid touristRouteId)
		{
			return await _context.TouristRoutes.Include(t => t.TouristRoutePictures).FirstOrDefaultAsync(n => n.Id == touristRouteId);
		}


		public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDListAsync(IEnumerable<Guid> ids)
		{
			return await _context.TouristRoutes.Where(t => ids.Contains(t.Id)).ToListAsync();
		}

		public async Task<bool> TouristRouteExistsAsync(Guid touristRouteId)
		{
			return await _context.TouristRoutes.AnyAsync(t => t.Id == touristRouteId);
		}

		public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIdAsync(Guid touristRouteId)
		{
			return await _context.TouristRoutePictures
			  .Where(p => p.TouristRouteId == touristRouteId).ToListAsync();
		}

		public async Task<TouristRoutePicture> GetPictureAsync(int pictureId)
		{
			return await _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefaultAsync();
		}

		public void AddTouristRoute(TouristRoute touristRoute)
		{
			if (touristRoute == null)
			{
				throw new ArgumentNullException(nameof(touristRoute));
			}
			_context.TouristRoutes.Add(touristRoute);
		}

		public void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture)
		{
			if (touristRouteId == Guid.Empty)
			{
				throw new ArgumentNullException(nameof(touristRouteId));
			}
			if (touristRoutePicture == null)
			{
				throw new ArgumentNullException(nameof(touristRoutePicture));
			}

			touristRoutePicture.TouristRouteId = touristRouteId;
			_context.TouristRoutePictures.Add(touristRoutePicture);
		}

		public void DeleteTouristRoute(TouristRoute touristRoute)
		{
			_context.TouristRoutes.Remove(touristRoute);
		}
		public void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes)
		{
			_context.TouristRoutes.RemoveRange(touristRoutes);
		}
		public void DeleteTouristRoutePicture(TouristRoutePicture picture)
		{
			_context.TouristRoutePictures.Remove(picture);
		}

		public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
		{
			return await _context.ShoppingCarts
			  .Include(s => s.User)
			  .Include(s => s.ShoppingCartItems).ThenInclude(li => li.TouristRoute)
			  .Where(s => s.UserId == userId)
			  .FirstOrDefaultAsync();
		}

		public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
		{
			await _context.ShoppingCarts.AddAsync(shoppingCart);
		}

		public async Task AddShoppingCartItemAsync(LineItem lineItem)
		{
			await _context.LineItems.AddAsync(lineItem);
		}
		public async Task<LineItem> GetShoppingCartItemByItemIdAsync(int lineItemId)
		{
			return await _context.LineItems
			  .Where(li => li.Id == lineItemId)
			  .FirstOrDefaultAsync();
		}
		public void DeleteShoppingCartItem(LineItem lineItem)
		{
			_context.LineItems.Remove(lineItem);
		}

		public async Task<IEnumerable<LineItem>> GetShoppingCartsByIdListAsync(
		  IEnumerable<int> ids)
		{
			return await _context.LineItems
			  .Where(li => ids.Contains(li.Id))
			  .ToListAsync();
		}

		public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems)
		{
			_context.LineItems.RemoveRange(lineItems);
		}

		public async Task AddOrderAsync(Order order)
		{
			await _context.Orders.AddAsync(order);
		}

		public async Task<PaginationList<Order>> GetOrdersByUserIdAsync(
		  string userId, int pageSize, int pageNumber)
		{
			var result = _context.Orders.Where(s => s.UserId == userId);

			return await PaginationList<Order>.CreateAsync(pageNumber, pageSize, result);
		}

		public async Task<Order> GetOrderByIdAsync(Guid orderId)
		{
			return await _context.Orders
			  .Include(o => o.OrderItems).ThenInclude(oi => oi.TouristRoute)
			  .Where(o => o.Id == orderId).FirstOrDefaultAsync();
		}

		public async Task<bool> SaveAsync()
		{
			return (await _context.SaveChangesAsync() >= 0);
		}
	}
}
