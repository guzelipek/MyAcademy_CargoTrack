using CargoTrack.DTO.DTOs.CityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.Business.Services.Cities
{
    public interface ICityService
    {
        Task<List<ResultCityDto>> GetAllAsync();
        Task<UpdateCityDto> GetByIdAsync(Guid id);
        Task CreateAsync(CreateCityDto createCityDto);
        Task UpdateAsync(UpdateCityDto updateCityDto);
        Task DeleteAsync(Guid id);
    }
}
