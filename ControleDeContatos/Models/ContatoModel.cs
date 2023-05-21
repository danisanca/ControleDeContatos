using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage ="Email invalido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [Phone(ErrorMessage ="O Celular informado não é valido.")]
        public string Telefone { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
