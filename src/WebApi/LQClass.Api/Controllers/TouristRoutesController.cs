using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LQClass.Api.Dtos;
using LQClass.Api.Helper;
using LQClass.Api.Models;
using LQClass.Api.ResourceParameters;
using LQClass.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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
		private ITouristRouteRepository touristRouteRepository;
		private readonly IMapper mapper;
		private readonly IUrlHelper urlHelper;
		private readonly IPropertyMappingService propertyMappingService;

		public TouristRoutesController(
		  ITouristRouteRepository touristRouteRepository,
		  IMapper mapper,
		  IUrlHelperFactory urlHelperFactory,
		  IActionContextAccessor actionContextAccessor,
		  IPropertyMappingService propertyMappingService
		  )
		{
			this.touristRouteRepository = touristRouteRepository;
			this.mapper = mapper;
			this.urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
			this.propertyMappingService = propertyMappingService;
		}

		private string GenerateTouristRouteResourceURL(
			TouristRouteResourceParameters parameters,
			PaginationResourceParameters parameters2,
			ResourceUriType type
		  )
		{
			return type switch
			{
				ResourceUriType.PreviousPage => urlHelper.Link("GetTouristRoutesAsync",
				  new
				  {
					  fields = parameters.Fields,
					  orderBy = parameters.OrderBy,
					  keyword = parameters.Key,
					  rating = parameters.Rating,
					  pageNumber = parameters2.PageNumber - 1,
					  pageSize = parameters2.PageSize
				  }),
				ResourceUriType.NextPage => urlHelper.Link("GetTouristRoutesAsync",
				  new
				  {
					  fields = parameters.Fields,
					  orderBy = parameters.OrderBy,
					  keyword = parameters.Key,
					  rating = parameters.Rating,
					  pageNumber = parameters2.PageNumber + 1,
					  pageSize = parameters2.PageSize
				  }),
				_ => urlHelper.Link("GetTouristRoutesAsync",
				  new
				  {
					  fields = parameters.Fields,
					  orderBy = parameters.OrderBy,
					  keyword = parameters.Key,
					  rating = parameters.Rating,
					  pageNumber = parameters2.PageNumber,
					  pageSize = parameters2.PageSize
				  })
			};
		}

		// fields=id,title为数据塑形
		// api/touristRoutes?key=传入的参数?fields=id,title
		[HttpGet(Name = "GetTouristRoutesAsync")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetTouristRoutesAsync(
		  [FromQuery] TouristRouteResourceParameters parameters,
		  [FromQuery] PaginationResourceParameters parameters2
		  )
		{
			if (!propertyMappingService
				.IsMappingExists<TouristRouteDto, TouristRoute>(
				parameters.OrderBy))
			{
				return BadRequest("请输入正确的排序参数");
			}

			if(!propertyMappingService
				.IsPropertiesExists<TouristRouteDto>(parameters.Fields))
			{
				return BadRequest("请输入正确的塑形参数");
			}

			var touristRoutesFromRepo = await touristRouteRepository
			  .GetRouristRoutesAsync(
				parameters.Key,
				parameters.RatingOperator,
				parameters.RatingValue,
				parameters2.PageSize,
				parameters2.PageNumber,
				parameters.OrderBy
			  );
			if (touristRoutesFromRepo == null
			  || touristRoutesFromRepo.Count() <= 0)
			{
				return NotFound("没有旅游路线！");
			}
			var touristRoutesFromRepoDto = mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);

			var previousPageLink = touristRoutesFromRepo.HasPrevious
			  ? GenerateTouristRouteResourceURL(
				  parameters, parameters2, ResourceUriType.PreviousPage)
			  : null;
			var nextPageLink = touristRoutesFromRepo.HasNext
			  ? GenerateTouristRouteResourceURL(
				  parameters, parameters2, ResourceUriType.NextPage)
			  : null;

			// x-pagination
			var pagenationMetadata = new
			{
				previousPageLink,
				nextPageLink,
				totalCount = touristRoutesFromRepo.TotalCount,
				pageSize = touristRoutesFromRepo.PageSize,
				currentPage = touristRoutesFromRepo.CurrentPage,
				totalPages = touristRoutesFromRepo.TotalPages
			};

			Response.Headers.Add("x-pagination",
			  Newtonsoft.Json.JsonConvert.SerializeObject(pagenationMetadata));

			// ShapeData数据塑形
			return Ok(touristRoutesFromRepoDto.ShapeData(parameters.Fields));
		}

		// fields=id,title塑形字段
		// api/touristroutes/{touristRouteId}?fields=id,title
		[HttpGet("{touristRouteId}", Name = "GetRouristRouteById")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetRouristRouteByIdAsync(
			Guid touristRouteId,
			string fields)		// 塑形字段
		{
			var touristRouteFromRepo = await touristRouteRepository.GetTouristRouteAsync(touristRouteId);
			if (touristRouteFromRepo == null)
			{
				return NotFound($"旅游路线{touristRouteId}找不到");
			}


			if (!propertyMappingService
				.IsPropertiesExists<TouristRouteDto>(fields))
			{
				return BadRequest("请输入正确的塑形参数");
			}

			var touristRouteFromRepoDto = mapper.Map<TouristRouteDto>(touristRouteFromRepo);
			return Ok(touristRouteFromRepoDto.ShapeData(fields));
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize()]
		public async Task<IActionResult> CreateTouristRouteAsync([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
		{
			var touristRouteModel = mapper.Map<TouristRoute>(touristRouteForCreationDto);
			touristRouteRepository.AddTouristRoute(touristRouteModel);
			await touristRouteRepository.SaveAsync();
			var touristRouteToReturn = mapper.Map<TouristRouteDto>(touristRouteModel);

			return CreatedAtRoute(
			  "GetRouristRouteById"
			  , new { touristRouteId = touristRouteToReturn.Id },
			  touristRouteToReturn
			  );
		}

		[HttpPut("{touristRouteId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateTouristRouteAsync(
		  [FromRoute] Guid touristRouteId,
		  [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto
		  )
		{
			if (!(await touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游路线找不到");
			}

			var touristRouteFromRepo = await touristRouteRepository.GetTouristRouteAsync(touristRouteId);
			mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);

			await touristRouteRepository.SaveAsync();

			return NoContent();
		}

		[HttpPatch("{touristRouteId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> PartiallyUpdateTouristRouteAsync(
		  [FromRoute] Guid touristRouteId,
		  [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> patchDocument
		  )
		{
			if (!(await touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游路线找不到");
			}

			var touristRouteFromRepo = await touristRouteRepository.GetTouristRouteAsync(touristRouteId);
			var touristRouteToPatch = mapper.Map<TouristRouteForUpdateDto>(touristRouteFromRepo);
			patchDocument.ApplyTo(touristRouteToPatch, ModelState); // 传入ModelState是用于验证JsonPatch传入的对象
			if (!TryValidateModel(touristRouteToPatch))
			{
				return ValidationProblem(ModelState);
			}

			mapper.Map(touristRouteToPatch, touristRouteFromRepo);
			await touristRouteRepository.SaveAsync();

			return NoContent();
		}

		[HttpDelete("{touristRouteId}")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteTouristRouteAsync([FromRoute] Guid touristRouteId)
		{
			if (!(await touristRouteRepository.TouristRouteExistsAsync(touristRouteId)))
			{
				return NotFound("旅游路线找不到");
			}

			var touristRoute = await touristRouteRepository.GetTouristRouteAsync(touristRouteId);
			touristRouteRepository.DeleteTouristRoute(touristRoute);
			await touristRouteRepository.SaveAsync();

			return NoContent();
		}

		[HttpDelete("({touristIDs})")]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteByIDsAsync(
		  [ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> touristIDs
		  )
		{
			if (touristIDs == null || touristIDs.Count() <= 0)
			{
				return BadRequest();
			}

			var touristRoutesFromRepo = await touristRouteRepository.GetTouristRoutesByIDListAsync(touristIDs);
			touristRouteRepository.DeleteTouristRoutes(touristRoutesFromRepo);
			await touristRouteRepository.SaveAsync();

			return NoContent();
		}
	}
}
