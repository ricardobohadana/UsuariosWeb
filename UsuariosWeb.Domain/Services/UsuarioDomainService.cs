using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;
using UsuariosWeb.Domain.Interfaces.Services;

namespace UsuariosWeb.Domain.Services
{
    /// <summary>
    /// Classe para implementação das regras de negócio de usuário
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //declarar atributos para utilizarmos os repositorios
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        //construtor para fazer a injeção de dependência (inicialização) dos repositorios
        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            //verificar se o email já está cadastrado no banco de dados
            //REGRA: Não permitir o cadastro de usuários com o mesmo email
            if (_usuarioRepository.Obter(usuario.Email) != null)
                //retornar um erro
                throw new Exception($"O email '{usuario.Email}' já está cadastrado na aplicação.");

            //REGRA: Todo usuário cadastrado na aplicação deverá
            //ter o perfil 'default' como padrão
            var perfil = _perfilRepository.Obter("default");
            usuario.IdPerfil = perfil.IdPerfil;

            //cadastrando o usuário
            _usuarioRepository.Inserir(usuario);
        }

        public Usuario AutenticarUsuario(string email, string senha)
        {
            Usuario usuario = _usuarioRepository.Obter(email, senha);
            if (usuario != null)
                return usuario;

            throw new Exception("Acesso negado. Usuário inválido");
        }

        public Usuario ObterUsuario(string email)
        {
            var usuario = _usuarioRepository.Obter(email);
            
            if(usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Perfil = _perfilRepository.ObterPorId(usuario.IdPerfil);

            return usuario;

        }

        public Usuario ObterUsuario(Guid idUsuario)
        {
            var usuario = _usuarioRepository.ObterPorId(idUsuario);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Perfil = _perfilRepository.ObterPorId(usuario.IdPerfil);

            return usuario;
        }

        public List<Usuario> ConsultarUsuarios()
        {
            return _usuarioRepository.Consultar();
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            //consultar usuário através do email
            Usuario u = _usuarioRepository.Obter(usuario.Email);

            //verificar se o email já está cadastrado para outro usuário
            if (u != null && u.IdUsuario != u.IdUsuario)
                throw new Exception($"O email '{usuario.Email}' já está cadastrado para outro usuário do sistema.");


            _usuarioRepository.Alterar(usuario);


        }

        public void ExcluirUsuario(Usuario usuario)
        {
            //excluindo o usuário
            _usuarioRepository.Excluir(usuario);

        }

        public List<Perfil> ConsultarPerfis()
        {
            return _perfilRepository.Consultar();
        }

        public Perfil ObterPerfil(Guid idPerfil)
        {
            return _perfilRepository.ObterPorId(idPerfil);
        }
    }
}



