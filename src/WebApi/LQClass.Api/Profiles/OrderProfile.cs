using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Profiles
{
  public class OrderProfile : Profile
  {
    public OrderProfile()
    {
      CreateMap<Order, OrderDto>()
       .ForMember(
          dest => dest.State,
          opt =>
          {
            opt.MapFrom(src => src.State.ToString());
          }
        );
    }
  }
}
