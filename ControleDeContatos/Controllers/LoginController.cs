using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            if(_sessao.BuscarSessaoDoUsuario() !=null)return RedirectToAction("Index","Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index","Login");
        }
        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = "Senha invalida.";
                    }
                    TempData["MensagemErro"] = "Usuário e/ou Senha invalido(s).";
                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar o login Erro:{e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["MensagemErro"] = "Não conseguimos redefinir sua senha. Por favor, verifique os dados informado.";
                    RedirectToAction("Index", "Login");

                }
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);
                if (usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();
                    string mensagem = $"Sua nova senha é: {novaSenha}";
                    bool emailEnviado = _email.Enviar(usuario.Email,"Sistema de Contatos - Nova Senha", mensagem);
                    if (emailEnviado)
                    {
                        _usuarioRepositorio.Atualizar(usuario);
                        TempData["MensagemSucesso"] = "Verifique seu email. Nova Senha enviada com sucesso.";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Não conseguimos enviar e-mail. Por favor, tente novamente mais tarde.";
                    }

                    RedirectToAction("Index", "Login");
                }
                else
                {
                    TempData["MensagemErro"] = "Não conseguimos redefinir sua senha. Por favor, verifique os dados informado.";
                    
                }
                return View("Index");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir a senha detalhe do erro:{e.Message}";
                return RedirectToAction("Index");
            }
        }
    }

}
