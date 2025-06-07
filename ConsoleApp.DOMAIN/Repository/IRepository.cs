using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DOMAIN.Repository
{
    public interface IRepository
    {
        void Incluir(object entity);

        void Alterar(object entity);

        void Excluir(object entity);

        T ConsultaPorId<T>(int id);

        IQueryable<T> Consulta<T>();

        IDisposable IniciarTransaction();

        void Commit();

        void Rollback();
    }
}
