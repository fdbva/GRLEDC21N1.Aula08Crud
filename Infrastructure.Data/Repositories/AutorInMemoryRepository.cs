using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class AutorInMemoryRepository
    {
        private static List<AutorModel> Autores { get; } = new List<AutorModel>
        {
            new AutorModel
            {
                Id = 0,
                Nome = "Felipe",
                UltimoNome = "Andrade",
                Nacionalidade = "Brasileiro",
                Nascimento = new DateTime(1988, 02, 23),
                QuantidadeLivrosPublicados = 0
            },
            new AutorModel
            {
                Id = 1,
                Nome = "Felipe2",
                UltimoNome = "Andrade2",
                Nacionalidade = "Brasileiro2",
                Nascimento = new DateTime(2000, 02, 23),
                QuantidadeLivrosPublicados = 0
            }
        };

        public IEnumerable<AutorModel> GetAll()
        {
            return Autores;
        }

        public AutorModel GetById(int id)
        {
            foreach (var autor in Autores)
            {
                if (autor.Id == id)
                {
                    return autor;
                }
            }

            return null;
        }

        public AutorModel Create(AutorModel autorModel)
        {
            Autores.Add(autorModel);

            //TODO: auto-increment Id e atualizar para o retorno
            return autorModel;
        }

        public AutorModel Edit(AutorModel autorModel)
        {
            foreach (var autor in Autores)
            {
                if (autor.Id == autorModel.Id)
                {
                    autor.Nacionalidade = autorModel.Nacionalidade;
                    autor.Nome = autorModel.Nome;
                    autor.UltimoNome = autorModel.UltimoNome;
                    autor.Nascimento = autorModel.Nascimento;
                    autor.QuantidadeLivrosPublicados = autorModel.QuantidadeLivrosPublicados;

                    return autor;
                }
            }

            return null;
        }

        public void Delete(int id)
        {
            AutorModel autorEncontrado = null;
            foreach (var autor in Autores)
            {
                if (autor.Id == id)
                {
                    autorEncontrado = autor;
                }
            }

            if (autorEncontrado != null)
            {
                Autores.Remove(autorEncontrado);
            }
        }
    }
}
