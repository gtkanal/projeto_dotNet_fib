using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto.Models
{
    public class AutorLivro
    {
        public int AutorID { get; set; }
        public Autor Autores { get; set; }

        public int LivroID { get; set; }
        public Livro Livros { get; set; }
    }
}
