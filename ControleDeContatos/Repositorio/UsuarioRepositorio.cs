using ControleDeContatos.Data;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.UtcNow;
            usuario.SetPasswordHash();
          _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = _bancoContext.Usuarios.FirstOrDefault(e => e.Id == alterarSenhaModel.Id);
            if(usuarioDB == null) throw new Exception("Erro ao atualizar senha, Usuario não encontrado.");
            if(!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");
            if(usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova Senha deve ser diferente da atual");
            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.UtcNow;
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel result = _bancoContext.Usuarios.FirstOrDefault(e => e.Id == id);
            if (result == null)
            {
                throw new Exception("Id não existe.");
            }
            _bancoContext.Usuarios.Remove(result);
            _bancoContext.SaveChanges();
            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel result = _bancoContext.Usuarios.FirstOrDefault(e => e.Id == usuario.Id);
            if (result == null)
            {
                throw new Exception("Id não existe.");
            }
            result.Nome = usuario.Nome;
            result.Email = usuario.Email;
            result.Login = usuario.Login;
            result.Perfil = usuario.Perfil;
            result.DataAtualizacao = DateTime.UtcNow;
            _bancoContext.Update(result);
            _bancoContext.SaveChanges();
            return result;
        }   

        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(e => e.Email.ToUpper() == email.ToUpper() && e.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(e => e.Login.ToUpper() == login.ToUpper());
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x => x.Contatos)
                .ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(e => e.Id == id);
        }
    }
}
