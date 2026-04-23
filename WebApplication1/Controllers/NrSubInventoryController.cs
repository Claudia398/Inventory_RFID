using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    public class NrSubInventoryController : Controller
    {
        private readonly NrSubInventoryService _service;

        public NrSubInventoryController(NrSubInventoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }

    

             public IActionResult Edit(int id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(NrSubInventoryDTO model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}