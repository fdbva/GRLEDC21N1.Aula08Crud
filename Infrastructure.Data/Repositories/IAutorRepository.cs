using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public interface IAutorRepository
    {
        IEnumerable<AutorModel> GetAll();
        AutorModel GetById(int id);
        AutorModel Create(AutorModel autorModel);
        AutorModel Edit(AutorModel autorModel);
        void Delete(int id);
    }
}
