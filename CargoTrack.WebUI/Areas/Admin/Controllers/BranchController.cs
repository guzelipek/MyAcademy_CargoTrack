using CargoTrack.Business.Services.Branches;
using CargoTrack.Business.Services.Cities;
using CargoTrack.DataAccess.Repositories.Branches;
using CargoTrack.DTO.DTOs.BranchDtos;
using CargoTrack.WebUI.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CargoTrack.WebUI.Areas.Admin.Controllers
{
    [Area(Area.Admin)]
    [Authorize(Roles = Roles.Admin)]
    public class BranchController(IBranchService _branchService,
                                    ICityService _cityService) : Controller
    {
        private async Task GetCitiesAsync()
        {
            var cities= await _cityService.GetAllAsync();
            var sortedCities=cities.OrderBy(c => c.Name).ToList();
            ViewBag.cities = (from city in sortedCities 
                              select new SelectListItem
                              { 
                                  Value = city.Id.ToString(),
                                  Text = city.Name
                              }).ToList();
        }
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllAsync();
            return View(branches);
        }
        public async Task < IActionResult > Create()
        {
            await GetCitiesAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDto branch)
        {
            if (ModelState.IsValid)
            {
                await _branchService.CreateAsync(branch);
                return RedirectToAction(nameof(Index));
            }
            await GetCitiesAsync();
            return View(branch);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch is null)
            {
                return NotFound();
            }
            await GetCitiesAsync();
            return View(branch);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBranchDto branch)
        {
            if (ModelState.IsValid)
            {
                await _branchService.UpdateAsync(branch);
                return RedirectToAction(nameof(Index));
            }
            await GetCitiesAsync();
            return View(branch);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch is null)
            {
                return NotFound();
            }
            await _branchService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
