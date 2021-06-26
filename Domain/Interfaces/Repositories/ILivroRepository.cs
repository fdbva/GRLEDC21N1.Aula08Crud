using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        IEnumerable<LivroModel> GetAll(
            bool orderAscendant,
            string search = null);
        LivroModel GetById(int id);
        LivroModel Create(LivroModel livroModel);
        LivroModel Edit(LivroModel livroModel);
        void Delete(int id);
    }
}
