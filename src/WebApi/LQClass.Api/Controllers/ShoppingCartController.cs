using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.Helper;
using LQClass.Api.Models;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LQClass.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ShoppingCartController : ControllerBase
  {
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ITouristRouteRepository touristRouteRepository;
    private readonly IMapper mapper;

    public ShoppingCartController(
      IHttpContextAccessor httpContextAccessor,
      ITouristRouteRepository touristRouteRepository,
      IMapper mapper
      )
    {
      this.httpContextAccessor = httpContextAccessor;
      this.touristRouteRepository = touristRouteRepository;
      this.mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetShoppingCartAsync()
    {
      // 1 获得当前用户
      var userId = httpContextAccessor
        .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

      // 2 使用userid获得购物车
      var shoppingCart = await touristRouteRepository.GetShoppingCartByUserIdAsync(userId);

      return Ok(mapper.Map<ShoppingCartDto>(shoppingCart));
    }

    [HttpPost("items")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> AddShoppingCartItem(
      [FromBody] ShoppingCartForCreationDto shoppingCartForCreation
      )
    {
      // 1 获得当前用户
      var userId = httpContextAccessor
        .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

      // 2 使用userid获得购物车
      var shoppingCart = await touristRouteRepository
        .GetShoppingCartByUserIdAsync(userId);

      // 3 合建lineItem
      var touristRoute = await touristRouteRepository
        .GetTouristRouteAsync(shoppingCartForCreation.TouristRouteId);
      if (touristRoute == null)
      {
        return NotFound("旅游路线不存在");
      }

      var lineItem = new LineItem()
      {
        TouristRouteId = shoppingCartForCreation.TouristRouteId,
        ShoppingCardId = shoppingCart.Id,
        OriginalPrice = touristRoute.OriginalPrice,
        DiscountPresent = touristRoute.DiscountPresent
      };

      // 4 添加LineItem，并保存数据
      await touristRouteRepository.AddShoppingCartItemAsync(lineItem);
      await touristRouteRepository.SaveAsync();

      return Ok(mapper.Map<ShoppingCartDto>(shoppingCart));
    }

    [HttpDelete("items/{itemId}")]
    public async Task<IActionResult> DeleteShoppingCartItem([FromRoute] int itemId)
    {
      var lineItem = await touristRouteRepository
        .GetShoppingCartItemByItemIdAsync(itemId);
      if (lineItem == null)
      {
        return NotFound("购物车商品找不到");
      }

      touristRouteRepository.DeleteShoppingCartItem(lineItem);
      await touristRouteRepository.SaveAsync();

      return NoContent();
    }

    [HttpDelete("items/({itemIDs})")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> RemoveShoppingCartItems(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))]
        [FromRoute] IEnumerable<int> itemIDs
      )
    {
      var lineItems = await touristRouteRepository
        .GetShoppingCartsByIdListAsync(itemIDs);

      touristRouteRepository.DeleteShoppingCartItems(lineItems);
      await touristRouteRepository.SaveAsync();

      return NoContent();
    }

    [HttpPost("checkout")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CheckoutAsync()
    {
      // 1 获得当前用户
      var userId = httpContextAccessor
        .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

      // 2 使用userid获得购物车
      var shoppingCart = await touristRouteRepository.GetShoppingCartByUserIdAsync(userId);

      // 3 创建订单
      var order = new Order
      {
        Id = Guid.NewGuid(),
        UserId = userId,
        State = OrderStateEnum.Pending,
        OrderItems = shoppingCart.ShoppingCartItems,
        CreateDateUTC = DateTime.UtcNow
      };

      //清空购物车
      shoppingCart.ShoppingCartItems = null;

      // 4 保存数据   
      await touristRouteRepository.AddOrderAsync(order);
      await touristRouteRepository.SaveAsync();

      // return
      return Ok(mapper.Map<OrderDto>(order));
    }
  }
}
