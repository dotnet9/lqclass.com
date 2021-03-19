using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.ResourceParameters;
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
  public class OrdersController : ControllerBase
  {
    private readonly IHttpContextAccessor httpContextAccessor;
    private IMapper mapper;
    private ITouristRouteRepository touristRouteRepository;

    public OrdersController(
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] PaginationResourceParameters parameters
      )
    {
      // 1 获得当前用户
      var userId = httpContextAccessor
        .HttpContext.User.FindFirst(ClaimTypes.Name).Value;

      // 2 使用用户id来获取订单历史记录
      var orders = await touristRouteRepository.GetOrdersByUserIdAsync(userId, parameters.PageSize, parameters.PageNumber);

      return Ok(mapper.Map<IEnumerable<OrderDto>>(orders));
    }

    [HttpGet("{orderId}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
    {
      // 1 获得当前用户
      var userId = httpContextAccessor
        .HttpContext.User.FindFirst(ClaimTypes.Name).Value;

      var order = await touristRouteRepository.GetOrderByIdAsync(orderId);

      return Ok(mapper.Map<OrderDto>(order));
    }
  }
}
