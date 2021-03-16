using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LQClass.Api.Controllers
{
  [Route("api/touristRoutes/{touristRouteId}/pictures")]
  [ApiController]
  public class TouristRoutePicturesController : ControllerBase
  {
    private ITouristRouteRepository _touristRouteRepository;
    private readonly IMapper _mapper;

    public TouristRoutePicturesController(
      ITouristRouteRepository touristRouteRepository
      , IMapper mapper)
    {
      this._touristRouteRepository = touristRouteRepository ??
        throw new ArgumentNullException(nameof(touristRouteRepository));
      this._mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public IActionResult GetPictureListForTouristRoute(Guid touristRouteId)
    {
      if(!_touristRouteRepository.TouristRouteExists(touristRouteId))
      {
        return NotFound("旅游线路不存在");
      }

      var picturesFromRepo = _touristRouteRepository.GetPicturesByTouristRouteId(touristRouteId);
      if(picturesFromRepo==null
        ||picturesFromRepo.Count()<=0)
      {
        return NotFound("照片不存在");
      }

      return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
    }


    [HttpGet("{pictureId}")]
    public IActionResult GetPicture(Guid touristRouteId, int pictureId)
    {
      if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
      {
        return NotFound("旅游线路不存在");
      }

      var pictureFromRepo = _touristRouteRepository.GetPicture(pictureId);
      if (pictureFromRepo == null)
      {
        return NotFound("照片不存在");
      }

      return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
    }
  }
}
