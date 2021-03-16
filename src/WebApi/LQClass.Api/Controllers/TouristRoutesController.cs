using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LQClass.Api.Dtos;
using LQClass.Api.Models;
using LQClass.Api.ResourceParameters;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Http;
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
		public IActionResult GetTouristRoutes(
			[FromQuery] TouristRouteResourceParameters parameters
			)
		{
			var touristRoutesFromRepo = _touristRouteRepository.GetRouristRoutes(parameters.Key, parameters.RatingOperator, parameters.RatingValue);
			if (touristRoutesFromRepo == null
			  || touristRoutesFromRepo.Count() <= 0)
			{
				return NotFound("没有旅游路线！");
			}
			var touristRoutesFromRepoDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);

			return Ok(touristRoutesFromRepoDto);
		}

		// api/touristroutes/{touristRouteId}
		[HttpGet("{touristRouteId}")]
		public IActionResult GetRouristRouteById(Guid touristRouteId)
		{
			var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
			if (touristRouteFromRepo == null)
			{
				return NotFound($"旅游路线{touristRouteId}找不到");
			}
			var touristRouteFromRepoDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
			return Ok(touristRouteFromRepoDto);
		}
	}
}
