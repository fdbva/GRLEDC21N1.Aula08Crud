using System.Collections.Generic;
using Infrastructure.Data;

namespace Aula08Crud.ViewModels
{
    public class AutorIndexViewModel
    {
        public string Search { get; set; }
        public IEnumerable<AutorModel> Autores { get; set; }
    }
}
