using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IAutorRepository
    {
        IEnumerable<AutorModel> GetAll(
            bool orderAscendant,
            string search = null);
        AutorModel GetById(int id);
        AutorModel Create(AutorModel autorModel);
        AutorModel Edit(AutorModel autorModel);
        void Delete(int id);
    }
}
