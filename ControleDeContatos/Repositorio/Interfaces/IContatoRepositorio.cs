using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio.Interfaces
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato);
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel ListarPorId(int id);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);
    }
}
