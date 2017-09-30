using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class Autor
    {
        [Key]
        public int AuthorID { get; set; }
        [Required]
        public string Nome { get; set; }

        public ICollection<AutorLivro> AutorLivros { get; set; }
    }
}
