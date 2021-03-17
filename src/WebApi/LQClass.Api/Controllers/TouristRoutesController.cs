using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LQClass.Api.Dtos;
using LQClass.Api.Helper;
using LQClass.Api.Models;
using LQClass.Api.ResourceParameters;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LQClass.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TouristRoutesController : ControllerBase
  {
    private ITouristRouteRepository _touristRouteRepository;
    private readonly IMapper _mapper;

    public TouristRoutesController(
      ITouristRouteRepository touristRouteRepository
      , IMapper mapper)
    {
      this._touristRouteRepository = touristRouteRepository ??
        throw new ArgumentNullException(nameof(touristRouteRepository));
      this._mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    }

    // api/touristRoutes?key=传入的参数
    [HttpGet]
    public async Task<IActionResult> GetTouristRoutesAsync(
      [FromQuery] TouristRouteResourceParameters parameters
      )
    {
      var touristRoutesFromRepo = await _touristRouteRepository.GetRouristRoutesAsync(parameters.Key, parameters.RatingOperator, parameters.RatingValue);
      if (touristRoutesFromRepo == null
        || touristRoutesFromRepo.Count() <= 0)
      {
        return NotFound("没有旅游路线！");
      }
      var touristRoutesFromRepoDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);

      return Ok(touristRoutesFromRepoDto);
    }

    // api/touristroutes/{touristRouteId}
    [HttpGet("{touristRouteId}", Name = "GetRouristRouteById")]
    public async Task<IActionResult> GetRouristRouteByIdAsync(Guid touristRouteId)
    {
      var touristRouteFromRepo = await _touristRouteRepository.GetTouristRouteAsync(touristRouteId);
      if (touristRouteFromRepo == null)
      {
        return NotFound($"旅游路线{touristRouteId}找不到");
      }
      var touristRouteFromRepoDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
      return Ok(touristRouteFromRepoDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTouristRouteAsync([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
    {
      var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteForCreationDto);
      _touristRouteRepository.AddTouristRoute(touristRouteModel);
      await _touristRouteRepository.SaveAsync();
      var touristRouteToReturn = _mapper.Map<TouristRouteDto>(touristRouteModel);

      return CreatedAtRoute(
        "GetRouristRouteById"
        , new { touristRouteId = touristRouteToReturn.Id },
        touristRouteToReturn
        );
    }

    [HttpPut("{touristRouteId}")]
    public async Task<IActionResult> UpdateTouristRouteAsync(
        [FromRoute] Guid touristRouteId,
        [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto
      )
    {
      if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
      {
        return NotFound("旅游路线找不到");
      }

      var touristRouteFromRepo = await _touristRouteRepository.GetTouristRouteAsync(touristRouteId);
      _mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);

      await _touristRouteRepository.SaveAsync();

      return NoContent();
    }

    [HttpPatch("{touristRouteId}")]
    public async Task<IActionResult> PartiallyUpdateTouristRouteAsync(
        [FromRoute] Guid touristRouteId,
        [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> patchDocument
      )
    {
      if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
      {
        return NotFound("旅游路线找不到");
      }

      var touristRouteFromRepo = await _touristRouteRepository.GetTouristRouteAsync(touristRouteId);
      var touristRouteToPatch = _mapper.Map<TouristRouteForUpdateDto>(touristRouteFromRepo);
      patchDocument.ApplyTo(touristRouteToPatch, ModelState); // 传入ModelState是用于验证JsonPatch传入的对象
      if (!TryValidateModel(touristRouteToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(touristRouteToPatch, touristRouteFromRepo);
      await _touristRouteRepository.SaveAsync();

      return NoContent();
    }

    [HttpDelete("{touristRouteId}")]
    public async Task<IActionResult> DeleteTouristRouteAsync([FromRoute] Guid touristRouteId)
    {
      if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
      {
        return NotFound("旅游路线找不到");
      }

      var touristRoute = await _touristRouteRepository.GetTouristRouteAsync(touristRouteId);
      _touristRouteRepository.DeleteTouristRoute(touristRoute);
      await _touristRouteRepository.SaveAsync();

      return NoContent();
    }

    [HttpDelete("({touristIDs})")]
    public async Task<IActionResult> DeleteByIDsAsync(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> touristIDs
      )
    {
      if (touristIDs == null || touristIDs.Count() <= 0)
      {
        return BadRequest();
      }

      var touristRoutesFromRepo = await _touristRouteRepository.GetTouristRoutesByIDListAsync(touristIDs);
      _touristRouteRepository.DeleteTouristRoutes(touristRoutesFromRepo);
      await _touristRouteRepository.SaveAsync();

      return NoContent();
    }
  }
}
