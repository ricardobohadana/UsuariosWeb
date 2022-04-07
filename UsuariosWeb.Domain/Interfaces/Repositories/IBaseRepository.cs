using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosWeb.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface de repositório base
    /// </summary>
    /// <typeparam name="TEntity">Classe implementade pelo repositório</typeparam>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity entity);
        void Alterar(TEntity entity);
        void Excluir(TEntity entity);

        List <TEntity> Consultar();
        TEntity ObterPorId(Guid id);
    }
}
