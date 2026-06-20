using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Services;
using WebApplication1.DatabaseProvider;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                _service.Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        //edit
        public IActionResult Edit(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            //21.06.2026
            ViewBag.Roles = _service.GetRoles();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        // DELETE

        public IActionResult Delete(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

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
   