using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Presentation.Models;

namespace UsuariosWeb.Presentation.Controllers
{
    public class AccountController : Controller
    {
        //atributo
        private readonly IUsuarioDomainService _usuarioDomainService;

        public AccountController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Os dados inseridos estão incorretos, por favor verifique.";
                return View(model);
            }
            try
            {
                Usuario usuario = _usuarioDomainService.AutenticarUsuario(model.Email, model.Senha);


                #region Criando a permissão do usuário

                ClaimsIdentity identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                #endregion

                // redirecionando para a página HomeIndex
                return RedirectToAction("Index", "Home"); 
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }



            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Os dados inseridos estão incorretos, por favor verifique.";
                return View(model);
            }
            try
            {
                var usuario = new Usuario()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = model.Senha,
                    DataCadastro = DateTime.Now
                };

                // realizando o cadastro
                _usuarioDomainService.CadastrarUsuario(usuario);
                TempData["MensagemSucesso"] = $"Parabéns {usuario.Nome}, sua conta foi criada com sucesso.";
                ModelState.Clear();

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }

        public IActionResult Logout()
        {
            #region Remover a permissão de acesso do usuário

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            #endregion

            return RedirectToAction("Login", "Account");
        }
    }
}
