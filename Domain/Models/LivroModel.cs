using System;

namespace Domain.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public DateTime Lancamento { get; set; }

        public int AutorId { get; set; }
    }
}
