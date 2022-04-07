using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        /// <summary>
        /// Método para realização do cadastro do usuário
        /// </summary>
        /// <param name="usuario"></param>
        void CadastrarUsuario(Usuario usuario);


        /// <summary>
        /// Método para autenticação do usuário
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        Usuario AutenticarUsuario(string email, string senha);

        /// <summary>
        /// Método para consultar um único usuário através do email informado
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Usuario ObterUsuario(string email);

        Usuario ObterUsuario(Guid idUsuario);

        /// <summary>
        /// Método para consultar todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        List<Usuario> ConsultarUsuarios();


        public void AtualizarUsuario(Usuario usuario);


        public void ExcluirUsuario(Usuario usuario);

        /// <summary>
        /// Método para consultar todos os perfis cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        List<Perfil> ConsultarPerfis();

        /// <summary>
        /// Método para consultar 1 perfil através do id informado
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        Perfil ObterPerfil(Guid idPerfil);
    }
}
