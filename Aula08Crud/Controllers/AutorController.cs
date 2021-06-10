using System.Linq;
using Aula08Crud.ViewModels;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aula08Crud.Controllers
{
    public class AutorController : Controller
    {
        private readonly AutorSqlServerRepository _autorRepository;

        public AutorController()
        {
            _autorRepository = new AutorSqlServerRepository();
        }

        // GET: AutorController
        public ActionResult Index(
            string search = null)
        {
            var autores = _autorRepository.GetAll(search);

            var autorIndexViewModel = new AutorIndexViewModel
            {
                Search = search,
                Autores = autores
            };

            return View(autorIndexViewModel);
        }

        // GET: AutorController/Details/5
        public ActionResult Details(int id)
        {
            var autor = _autorRepository.GetById(id);

            if (autor != null)
            {
                return View(autor);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AutorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AutorModel autorModel)
        {
            try
            {
                _autorRepository.Create(autorModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutorController/Edit/5
        public ActionResult Edit(int id)
        {
            var autor = _autorRepository.GetById(id);

            if (autor != null)
            {
                return View(autor);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: AutorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AutorModel autorModel)
        {
            try
            {
                var autorEditado = _autorRepository.Edit(autorModel);

                if (autorEditado != null)
                {
                    return RedirectToAction(nameof(Details), new { id = autorEditado.Id });
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutorController/Delete/5
        public ActionResult Delete(int id)
        {
            var autor = _autorRepository.GetById(id);

            if (autor != null)
            {
                return View(autor);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: AutorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromMemory(int id)
        {
            try
            {
                _autorRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
