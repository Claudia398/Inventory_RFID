//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using WebApplication1.DTO;
//using WebApplication1.Services;

//namespace WebApplication1.Controllers
//{
//    public class NrInventoryController : Controller
//    {
//        private readonly NrInventoryService _service;
//        private readonly NrSubInventoryService _subInventoryService;
//        private readonly CostCenterService _costCenterService;

//        public NrInventoryController(
//            NrInventoryService service,
//            NrSubInventoryService subInventoryService,
//            CostCenterService costCenterService)
//        {
//            _service = service;
//            _subInventoryService = subInventoryService;
//            _costCenterService = costCenterService;
//        }

//        public IActionResult Index()
//        {
//            return View(_service.GetAll());
//        }

//        public IActionResult Create()
//        {
//            ViewBag.CostCenters = new SelectList(
//                _costCenterService.GetAll(), "Id", "Center");

//            ViewBag.SubInventories = new SelectList(
//                _subInventoryService.GetAll(), "Id", "Name");

//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(NrInventoryDTO model, string SubInventoryInput, string NewCostCenterName)
//        {
//            // SUBINVENTORY
//            if (!string.IsNullOrWhiteSpace(SubInventoryInput))
//            {
//                var name = SubInventoryInput.Trim();


//                var idSubInv = _subInventoryService.Add(new NrSubInventoryDTO { Name = name });


//                model.SubInventory.Add(_subInventoryService.GetById(idSubInv));

//            }

//            // COST CENTER
//            if (!string.IsNullOrWhiteSpace(NewCostCenterName))
//            {
//                var name = NewCostCenterName.Trim();

//                var existing = _costCenterService.GetAll()
//                    .FirstOrDefault(x => x.Center.ToLower() == name.ToLower());

//                if (existing != null)
//                {
//                    model.CostCenterId = existing.Id;
//                }
//                else
//                {
//                    _costCenterService.Add(new CostCenterDTO { Center = name });

//                    var last = _costCenterService.GetAll()
//                        .OrderByDescending(x => x.Id)
//                        .FirstOrDefault();

//                    if (last != null)
//                        model.CostCenterId = last.Id;
//                }
//            }

//            _service.Add(model);
//            return RedirectToAction("Index");
//        }
//        //04/07/2026
//        public IActionResult Edit(int id)
//        {
//            var item = _service.GetById(id);

//            if (item == null)
//            {
//                return NotFound();
//            }

//            ViewBag.CostCenters = new SelectList(
//                _costCenterService.GetAll(), "Id", "Center", item.CostCenterId);

//            ViewBag.SubInventories = new SelectList(
//                _subInventoryService.GetAll(), "Id", "Name");

//            return View(item);
//        }
//        //04/07/2026
//        [HttpPost]
//        public IActionResult Edit(int id, NrInventoryDTO model, string SubInventoryInput, string NewCostCenterName)
//        {
//            if (id != model.Id)
//            {
//                return NotFound();
//            }
//            //nou

//            public IActionResult Edit(NrInventoryDTO model)
//            {
//                _service.Update(model);
//                return RedirectToAction("Index");
//            }

//            // SUBINVENTORY
//            if (!string.IsNullOrWhiteSpace(SubInventoryInput))
//            {
//                var name = SubInventoryInput.Trim();

//                var idSubInv = _subInventoryService.Add(new NrSubInventoryDTO { Name = name });

//                model.SubInventory.Add(_subInventoryService.GetById(idSubInv));
//            }

//            // COST CENTER
//            if (!string.IsNullOrWhiteSpace(NewCostCenterName))
//            {
//                var name = NewCostCenterName.Trim();

//                var existing = _costCenterService.GetAll()
//                    .FirstOrDefault(x => x.Center.ToLower() == name.ToLower());

//                if (existing != null)
//                {
//                    model.CostCenterId = existing.Id;
//                }
//                else
//                {
//                    _costCenterService.Add(new CostCenterDTO { Center = name });

//                    var last = _costCenterService.GetAll()
//                        .OrderByDescending(x => x.Id)
//                        .FirstOrDefault();

//                    if (last != null)
//                        model.CostCenterId = last.Id;
//                }
//            }

//            _service.Update(model); // 

//            return RedirectToAction("Index");
//        }
//    }
//}
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