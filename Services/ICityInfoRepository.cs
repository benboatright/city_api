using System;
using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
	public interface ICityInfoRepository
	{
		Task<IEnumerable<City>> GetCitiesAsync();

		Task<(IEnumerable<City>, PaginationMetaData)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

		Task<City> GetCityAsync(int cityId, bool includePointsOfInterest);

		Task<bool> CityExistsAsync(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

		Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

		Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

		Task<bool> SaveChangesAsync();

		Task<bool> CityNameMAtchesCityId(string? cityName, int cityId);

		void DeletePointOfInterest(PointOfInterest pointOfInterest);
	}
}

