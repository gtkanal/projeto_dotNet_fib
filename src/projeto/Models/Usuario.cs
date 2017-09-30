using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projeto.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório mané")]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "Digite o email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Digite a senha")]
        public string senha { get; set; }

        public virtual ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
