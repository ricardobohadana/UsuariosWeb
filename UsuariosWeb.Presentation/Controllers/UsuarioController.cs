using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Services;
using UsuariosWeb.Presentation.Models;
using UsuariosWeb.Presentation.Reports;

namespace UsuariosWeb.Presentation.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioDomainService _usuarioDomainService;

        public UsuarioController(IUsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(UsuariosCadastroModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                Usuario usuario = new Usuario()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    Email = model.Email,
                    Senha = model.Senha,
                };

                _usuarioDomainService.CadastrarUsuario(usuario);

                ModelState.Clear();
                TempData["MensagemSucesso"] = $"Usuário {usuario.Nome}, cadastrado com sucesso!";
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = ("Ocorreu um erro: " + e.Message);
            }

            return View();
        }

        public IActionResult Consulta()
        {
            List<UsuariosConsultaModel> consultaLista = new List<UsuariosConsultaModel>();

            try
            {
                foreach (var item in _usuarioDomainService.ConsultarUsuarios())
                {
                    consultaLista.Add(new UsuariosConsultaModel()
                    {
                        IdUsuario = item.IdUsuario,
                        Nome = item.Nome,
                        Email = item.Email,
                        DataCadastro = item.DataCadastro,
                    });
                }
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = e.Message;
            }

            return View(consultaLista);
        }



        public IActionResult Relatorio()
        {
            try
            {

                var pdf = UsuarioReport.GerarPdf(_usuarioDomainService.ConsultarUsuarios());

                return File(pdf, "application/pdf");
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = e.Message;
            }
            return View();
        }

        public IActionResult MinhaConta()
        {
            UsuariosMinhaContaModel model = new UsuariosMinhaContaModel();
            try
            {
                string email = HttpContext.User.Identity.Name;

                // consultar os dados do usuário através do email
                Usuario usuario = _usuarioDomainService.ObterUsuario(email);

                model.Nome = usuario.Nome;
                model.DataCadastro = usuario.DataCadastro;
                model.Email = usuario.Email;
                model.Perfil = usuario.Perfil.Nome.ToUpper();

            } catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        private List<SelectListItem> ObterListagemPerfis()
        {
            List < SelectListItem > lista = new List<SelectListItem>(); 
            foreach (Perfil perfil in _usuarioDomainService.ConsultarPerfis())
            {
                lista.Add(
                    new SelectListItem()
                    {
                        Value = perfil.IdPerfil.ToString(),
                        Text = perfil.Nome.ToUpper()

                    }
                );
            }

            return lista;
        }

        public IActionResult Edicao(Guid id)
        {
            UsuariosEdicaoModel model = new UsuariosEdicaoModel();
            model.ListagemPerfis = ObterListagemPerfis();

            try
            {
                Usuario usuario = _usuarioDomainService.ObterUsuario(id);

                model.IdUsuario = usuario.IdUsuario;
                model.Nome = usuario.Nome;
                model.Email = usuario.Email;
                model.IdPerfil = usuario.IdPerfil.ToString();

                

            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = e.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(UsuariosEdicaoModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Ocorreu um erro de validação nos dados preenchidos");
                }

                Usuario usuario = new Usuario()
                {
                    IdUsuario = model.IdUsuario,
                    IdPerfil = Guid.Parse(model.IdPerfil),
                    Email = model.Email,
                    Nome = model.Nome
                };

                _usuarioDomainService.AtualizarUsuario(usuario);

                TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";

            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    IdUsuario = id,
                };

                _usuarioDomainService.ExcluirUsuario(usuario);
                TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
            }catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta", "Usuario");
        }
    }
}
