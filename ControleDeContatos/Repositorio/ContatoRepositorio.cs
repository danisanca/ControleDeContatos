using ControleDeContatos.Data;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio.Interfaces;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
          _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public bool Apagar(int id)
        {
            ContatoModel result = _bancoContext.Contatos.FirstOrDefault(e => e.Id == id);
            if (result == null)
            {
                throw new Exception("Id não existe.");
            }
            _bancoContext.Contatos.Remove(result);
            _bancoContext.SaveChanges();
            return true;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel result = _bancoContext.Contatos.FirstOrDefault(e => e.Id == contato.Id);
            if (result == null)
            {
                throw new Exception("Id não existe.");
            }
            result.Nome = contato.Nome;
            result.Email = contato.Email;
            result.Telefone = contato.Telefone;
            _bancoContext.Update(result);
            _bancoContext.SaveChanges();
            return result;
        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Contatos.Where(x=>x.UsuarioId == usuarioId).ToList();
        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(e => e.Id == id);
        }
    }
}
