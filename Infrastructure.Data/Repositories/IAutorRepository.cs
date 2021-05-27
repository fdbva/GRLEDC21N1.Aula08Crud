using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public interface IAutorRepository
    {
        IEnumerable<AutorModel> GetAll(
            string search = null);
        AutorModel GetById(int id);
        AutorModel Create(AutorModel autorModel);
        AutorModel Edit(AutorModel autorModel);
        void Delete(int id);
    }
}
