using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class Livro
    {
        [Key]
        public int LivroID { get; set; }

        [Required(ErrorMessage = "Digite o Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Digite a quantidade")]
        public int Quantidade { get; set; }

        public string Foto { get; set; }

        public ICollection<AutorLivro> AutorLivros { get; set; }


        public ICollection<LivroEmprestimo> LivroEmprestimos { get; set; }
    }
}
