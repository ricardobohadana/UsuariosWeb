using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;

namespace UsuariosWeb.Infra.Data.Repositories
{
    /// <summary>
    /// Classe para implementação da entidade Perfil
    /// </summary>
    public class PerfilRepository : IPerfilRepository
    {
        private string _connectionString;

        public PerfilRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Método para inserir ou criar um Perfil no banco de dados
        /// </summary>
        /// <param name="entity"></param>
        public void Inserir(Perfil entity)
        {
            string query = @"INSERT INTO PERFIL (IDPERFIL, NOME) VALUES (@IdPerfil, @Nome)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        /// <summary>
        /// Método para alterar ou modificar um perfil no banco de dados
        /// </summary>
        /// <param name="entity"></param>
        public void Alterar(Perfil entity)
        {
            string query = @"UPDATE PERFIL SET VALUES NOME=@Nome WHERE IDPERFIL=@IdPerfil";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Perfil> Consultar()
        {
            string query = @"SELECT * FROM PERFIL ORDER BY NOME";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Perfil>(query).ToList();
            }
        }

        public void Excluir(Perfil entity)
        {
            string query = @"DELETE FROM PERFIL WHERE IDPERFIL=@IdPerfil";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Perfil ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM PERFIL WHERE IDPERFIL=@id";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Perfil>(query, new { id }).FirstOrDefault();
            }
        }

        public Perfil Obter(string nome)
        {
            string query = @"SELECT * FROM PERFIL WHERE NOME=@nome";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Perfil>(query, new { nome }).FirstOrDefault();
            }
        }
    }
}
