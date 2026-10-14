using CargoTrack.DataAccess.Repositories.Abouts;
using CargoTrack.DTO.DTOs.AboutDtos;
using CargoTrack.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CargoTrack.Business.Services.Abouts
{
    public class AboutService(IAboutRepository _aboutRepository) : IAboutService
    {
        public async Task CreateAsync(CreateAboutDto createAboutDto)
        {
            var about = createAboutDto.Adapt<About>();
            await _aboutRepository.CreateAsync(about);
        }

        public async Task DeleteAsync(Guid id)
        {
            var about = await _aboutRepository.GetByIdAsync(id);
            if(about == null)
            {
                throw new ValidationException("About not found");
            }
            await _aboutRepository.DeleteAsync(about);
        }

        public async Task<List<ResultAboutDto>> GetAllAsync()
        {
            var abouts = await _aboutRepository.GetAllAsync();
            return abouts.Adapt<List<ResultAboutDto>>();
        }

        public async Task<UpdateAboutDto> GetByIdAsync(Guid id)
        {
            var about = await _aboutRepository.GetByIdAsync(id);
            if(about is null)
            {
                throw new ValidationException("About not found");
            }
            return about.Adapt<UpdateAboutDto>();
        }

        public async Task UpdateAsync(UpdateAboutDto updateAboutDto)
        { 
            var aboutx = updateAboutDto.Adapt<About>();
            await _aboutRepository.UpdateAsync(aboutx);
        }
    }
}
