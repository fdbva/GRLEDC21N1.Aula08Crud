using System.Collections.Generic;
using Domain.Models;

namespace Aula08Crud.ViewModels
{
    public class LivroIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<LivroModel> Livros { get; set; }
    }
}
