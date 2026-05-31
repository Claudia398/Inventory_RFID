
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class NrInventoryController : Controller
    {
        private readonly NrInventoryService _service;
        private readonly CostCenterService _costCenterService;

        public NrInventoryController(
            NrInventoryService service,
            CostCenterService costCenterService)
        {
            _service = service;
            _costCenterService = costCenterService;
        }

        // =========================
        // INDEX
        // =========================
        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        // =========================
        // CREATE - GET
        // =========================
        public IActionResult Create()
        {
            ViewBag.CostCenters = new SelectList(
                _costCenterService.GetAll(), "Id", "Center");

            return View();
        }

        // =========================
        // CREATE - POST
        // =========================
        [HttpPost]
        public IActionResult Create(NrInventoryDTO model)
        {
            _service.Add(model);
            return RedirectToAction("Index");
        }

        // =========================
        // EDIT - GET
        // =========================
        public IActionResult Edit(int id)
        {
            var item = _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            ViewBag.CostCenters = new SelectList(
                _costCenterService.GetAll(), "Id", "Center", item.CostCenterId);

            return View(item);
        }

        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        public IActionResult Edit(NrInventoryDTO model)
        {
            _service.Update(model);
            return RedirectToAction("Index");
        }

        // =========================
        // DELETE (opțional)
        // =========================
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AssignRFIDToInventory(int Id, string RFID)
        {
            _service.AssignRFIDToInventory(Id, RFID);
            return Ok();
        }
    }
}