using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto.Models
{
    public class LivroEmprestimo
    {
        public int LivroID{ get; set; }
        public Livro Livros { get; set; }

        public int EmprestimoID { get; set; }
        public Emprestimo Emprestimos { get; set; }
    }
}
