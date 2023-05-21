using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a senha atual do usuario. ")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuario. ")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha ")]
        [Compare("NovaSenha",ErrorMessage ="Senhas não conferem.")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
