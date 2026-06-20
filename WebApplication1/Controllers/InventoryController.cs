using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DatabaseProvider;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryService _service;
        private readonly PlacementService _placementService;
        private readonly UserService _userService;

        public InventoryController(
            InventoryService service,
            PlacementService placementService,
            UserService userService)
        {
            _service = service;
            _placementService = placementService;
            _userService = userService;
        }

        //public IActionResult Index()
        //{
        //    var data = _service.GetAll();

        //    ViewBag.Placements = new SelectList(
        //        _placementService.GetAll(), "Id", "Name");

        //    return View(data);
        //}
        //14.06.2026
        public IActionResult Index(int? placementId)
        {

            var placement = _placementService.GetAll();

            return View(placement);
        }

        [HttpPost]
        public IActionResult GetInventoryData()
        {
            var data = _service.GetAll();
            return Json(new { data });
        }
        [HttpPost]
        public IActionResult Scan(string uid, int placementId)
        {
            if (string.IsNullOrEmpty(uid))
            {
                ModelState.AddModelError("", "UID is required");
                return RedirectToAction("Index");
            }

            _service.UpdateScan(uid, placementId);
            return RedirectToAction("Index");
        }
        //nou 31.05.2026
        [HttpPost]
        public IActionResult PerformInventory(string uid, int placementId)
        {
            var username = User.Identity?.Name;
            var dbUser = _userService.GetByUserName(username);
            _service.PerformInventory(uid, placementId, dbUser?.Id);
            return Ok();
        }
    }
}