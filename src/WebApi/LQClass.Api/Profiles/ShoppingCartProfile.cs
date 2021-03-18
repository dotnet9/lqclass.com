using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Profiles
{
  public class ShoppingCartProfile : Profile
  {
    public ShoppingCartProfile()
    {
      CreateMap<ShoppingCart, ShoppingCartDto>();
      CreateMap<LineItem, LineItemDto>();
    }
  }
}
