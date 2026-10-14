using CargoTrack.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoTrack.WebUI.Controllers
{
    public class DefaultController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            if(TempData["error"] != null)
            {
                ViewBag.ErrorMessage = TempData["error"].ToString();
            }
            return View();
        }

        public async Task<IActionResult> CargoDetails(string trackCode)
        {
            if (string.IsNullOrEmpty(trackCode))
            {
                return BadRequest("Tracking code is required.");
            }
            var cargo = await _context.Cargos
                .FirstOrDefaultAsync(c => c.TrackCode == trackCode);
            if (cargo == null)
            {
                TempData["error"] = "Bu takip numarasına ait bir kargo bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(cargo);
        }
    }
}
