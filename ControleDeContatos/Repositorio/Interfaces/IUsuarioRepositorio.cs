using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel BuscarPorEmailLogin(string email, string login);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel ListarPorId(int id);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        bool Apagar(int id);
    }
}
