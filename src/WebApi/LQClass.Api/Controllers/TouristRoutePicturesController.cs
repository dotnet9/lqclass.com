using AutoMapper;
using LQClass.Api.Dtos;
using LQClass.Api.Models;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetPictureListForTouristRouteAsync(Guid touristRouteId)
		{
			if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游线路不存在");
			}

			var picturesFromRepo = await _touristRouteRepository.GetPicturesByTouristRouteIdAsync(touristRouteId);
			if (picturesFromRepo == null
			  || picturesFromRepo.Count() <= 0)
			{
				return NotFound("照片不存在");
			}

			return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
		}


		[HttpGet("{pictureId}", Name = "GetPicture")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetPictureAsync(Guid touristRouteId, int pictureId)
		{
			if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游线路不存在");
			}

			var pictureFromRepo = await _touristRouteRepository.GetPictureAsync(pictureId);
			if (pictureFromRepo == null)
			{
				return NotFound("照片不存在");
			}

			return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateTouristRoutePictureAsync(
			[FromRoute] Guid touristRouteId,
			[FromBody] TouristRoutePictureForCreationDto routePictureForCreationDto
		  )
		{
			if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游线路不存在");
			}

			var pictureModel = _mapper.Map<TouristRoutePicture>(routePictureForCreationDto);
			_touristRouteRepository.AddTouristRoutePicture(touristRouteId, pictureModel);
			await _touristRouteRepository.SaveAsync();
			var pictureToReturn = _mapper.Map<TouristRoutePictureDto>(pictureModel);
			return CreatedAtRoute(
				"GetPicture",
				new
				{
					touristRouteId = pictureModel.TouristRouteId,
					pictureId = pictureModel.Id
				},
				pictureToReturn
			  );
		}

		[HttpDelete("{pictureId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeletePictureAsync(
		  [FromRoute] Guid touristRouteId,
		  [FromRoute] int pictureId
		  )
		{
			if (!(await _touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游线路不存在");
			}

			var picture = await _touristRouteRepository.GetPictureAsync(pictureId);
			_touristRouteRepository.DeleteTouristRoutePicture(picture);
			await _touristRouteRepository.SaveAsync();

			return NoContent();
		}
	}
}
