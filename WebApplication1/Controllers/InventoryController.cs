using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryService _service;
        private readonly PlacementService _placementService;
        
        public InventoryController(
            InventoryService service,
            PlacementService placementService)
        {
            _service = service;
            _placementService = placementService;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();

            ViewBag.Placements = new SelectList(
                _placementService.GetAll(), "Id", "Name");

            return View(data);
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
            _service.PerformInventory(uid, placementId);
            return Ok();
        }
    }
}