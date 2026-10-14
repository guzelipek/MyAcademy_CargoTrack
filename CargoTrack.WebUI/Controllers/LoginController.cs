using CargoTrack.DTO.DTOs.UserDtos;
using CargoTrack.Entity.Entities;
using CargoTrack.WebUI.Const;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrack.WebUI.Controllers
{
    public class LoginController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if(userRoles.Contains(Roles.Admin))
                {
                    return Redirect("/Admin/Branch/Index");
                }
                if(userRoles.Contains(Roles.Manager))
                {
                    return Redirect("/Manager/Dashboard/Index");
                }
                if (userRoles.Contains(Roles.User))
                {
                    return Redirect("/User/Dashboard/Index");
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }
    }
}
