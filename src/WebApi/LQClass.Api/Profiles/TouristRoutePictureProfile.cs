using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.Models;

namespace LQClass.Api.Profiles
{
  public class TouristRoutePictureProfile : Profile
  {
    public TouristRoutePictureProfile()
    {
      CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
    }
  }
}
