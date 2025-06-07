using ConsoleApp.DOMAIN.Repository;
using NHibernate;


namespace ConsoleApp.INFRA.Implementation
{
    public class RepositoryContext : IRepository
    {
        private readonly ISessionFactory sessionFactory;
        private readonly ISession session;

        public RepositoryContext(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            this.session = sessionFactory.OpenSession();
        }

        public IQueryable<T> Consulta<T>()
        {
            return session.Query<T>();
        }

        public T ConsultaPorId<T>(int id)
        {
            return session.Get<T>(id);
        }

        public void Excluir(object entity)
        {
            session.Delete(entity);
        }

        public void Incluir(object entity)
        {
            session.Save(entity);
        }

        public void Alterar(object entity)
        {
            session.Update(entity);
        }

        public IDisposable IniciarTransaction()
        {
            var transaction = session.BeginTransaction();
            return transaction;
        }

        public void Rollback()
        {
            session.GetCurrentTransaction()?.Rollback();
        }

        public void Commit()
        {
            session.GetCurrentTransaction().Commit();
        }
    }
}
