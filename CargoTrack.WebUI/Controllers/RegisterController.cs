using CargoTrack.DTO.DTOs.UserDtos;
using CargoTrack.Entity.Entities;
using CargoTrack.WebUI.Const;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrack.WebUI.Controllers
{
    public class RegisterController(UserManager<AppUser> _userManager) : Controller
    {
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = model.Adapt<AppUser>();

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
                return RedirectToAction("Index", "Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
