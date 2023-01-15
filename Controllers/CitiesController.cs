using System;
using System.Text.Json;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
	[ApiController]
	//[Authorize]
	//[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    //[Route("api/v{version:apiVersion}/cities")]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
	{
		private readonly ICityInfoRepository _cityInfoRepository;
		private readonly IMapper _mapper;
		const int maxCitiesPageSize = 20;

		// Inject the contract
		public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
		{
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

		//Get All Cities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(
			string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
		{
			if (pageSize > maxCitiesPageSize)
			{
				pageSize = maxCitiesPageSize;
			}

			// call the GetCitiesAsync Method to get the enumerables
			var (cityEntities, paginationMetaData) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

			// return the dtos by mapping the cityEntities to dtos
			return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

		// Get Specific City
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest=false)
		{
			var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

			if (city == null)
			{
				return NotFound();
			}

			if (includePointsOfInterest)
			{
				return Ok(_mapper.Map<CityDto>(city));
			}

			return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
		}
	}
}

