using System.Linq;
using Aula08Crud.ViewModels;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aula08Crud.Controllers
{
    public class LivroController : Controller
    {
        //https://www.nuget.org/packages/dotnet-aspnet-codegenerator/ 

        //dotnet aspnet-codegenerator view Index List -p "Aula08Crud" --model LivroModel -outDir "Views/Livro" -udl -scripts
        //dotnet aspnet-codegenerator view Create Create -p "Aula08Crud" --model LivroModel -outDir "Views/Livro" -udl -scripts
        //dotnet aspnet-codegenerator view Edit Edit -p "Aula08Crud" --model LivroModel -outDir "Views/Livro" -udl -scripts
        //dotnet aspnet-codegenerator view Delete Delete -p "Aula08Crud" --model LivroModel -outDir "Views/Livro" -udl -scripts
        //dotnet aspnet-codegenerator view Details Details -p "Aula08Crud" --model LivroModel -outDir "Views/Livro" -udl -scripts

        //--useDefaultLayout ou -udl Usa o layout padrão das exibições.
        //--model ou -m Classe de modelo a ser usada.
        //--project ou -p Indica pasta do Projeto
        //--referenceScriptLibraries ou -scripts Faz referência a bibliotecas de script nas exibições geradas.Adiciona _ValidationScriptsPartial para Editar e Criar páginas.
        
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;

        public LivroController(
            ILivroRepository livroRepository,
            IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }

        // GET: LivroController
        public ActionResult Index(LivroIndexViewModel livroIndexRequest)
        {
            var livros = _livroRepository
                .GetAll(livroIndexRequest.OrderAscendant, livroIndexRequest.Search)
                .ToList();

            var livroIndexViewModel = new LivroIndexViewModel
            {
                Search = livroIndexRequest.Search,
                Livros = livros,
                OrderAscendant = livroIndexRequest.OrderAscendant
            };

            return View(livroIndexViewModel);
        }

        // GET: LivroController/Details/5
        public ActionResult Details(int id)
        {
            var livro = _livroRepository.GetById(id);

            if (livro != null)
            {
                return View(livro);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LivroController/Create
        public ActionResult Create()
        {
            var autores = _autorRepository.GetAll(true);

            var autoresSelectList = new SelectList(
                autores,
                nameof(AutorModel.Id),
                nameof(AutorModel.Nome));

            ViewData["Autores"] = autoresSelectList;
            return View();
        }

        // POST: LivroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LivroModel livroModel)
        {
            try
            {
                _livroRepository.Create(livroModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivroController/Edit/5
        public ActionResult Edit(int id)
        {
            var livro = _livroRepository.GetById(id);

            if (livro != null)
            {
                var autores = _autorRepository.GetAll(true);

                var autoresSelectList = new SelectList(
                    autores,
                    nameof(AutorModel.Id),
                    nameof(AutorModel.Nome),
                    livro.AutorId);

                ViewData["Autores"] = autoresSelectList;

                return View(livro);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: LivroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LivroModel livroModel)
        {
            try
            {
                var livroEditado = _livroRepository.Edit(livroModel);

                if (livroEditado != null)
                {
                    return RedirectToAction(nameof(Details), new { id = livroEditado.Id });
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivroController/Delete/5
        public ActionResult Delete(int id)
        {
            var livro = _livroRepository.GetById(id);

            if (livro != null)
            {
                return View(livro);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: LivroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromMemory(int id)
        {
            try
            {
                _livroRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
