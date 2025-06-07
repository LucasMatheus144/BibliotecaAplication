using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Repository;

namespace ConsoleApp.DOMAIN.Services
{
    public class ClienteService
    {
        private readonly IRepository db;
        private readonly ValidatorService validator;

        public ClienteService(IRepository repository, 
                              ValidatorService validator)
        {
            db = repository;
            this.validator = validator;
        }

        public List<Cliente> ListarAllClientes()
        {
            return db.Consulta<Cliente>().ToList();
        }

        public Cliente ListarPorId(int id)
        {
            return db.ConsultaPorId<Cliente>(id);
        }

        public bool CadastraCliente(Cliente cliente, out List<MensagemErro> erro)
        {
            erro = new List<MensagemErro>();

            if (!validator.ValitatorModels(cliente, out erro)) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();
                db.Incluir(cliente);
                db.Commit();
                return true;
            }
            catch (Exception)
            {
                db.Rollback();
                return false;
            }
        }

        public bool ExcluiCliente(int id)
        {
            var procura = ListarPorId(id);
            if (procura == null) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();
                db.Excluir(procura);
                db.Commit();
                return true; 
            }
            catch(Exception)
            {
                db.Rollback();
                return false;
            }            
        }

        public bool EditarCliente(Cliente cliente, out List<MensagemErro> erro)
        {
            var validaExistencia = ListarPorId(cliente.Id);

            if (!validator.ValitatorModels(cliente, out erro)) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();
                db.Alterar(cliente);
                db.Commit();
                return true;
            }
            catch(Exception)
            {
                db.Rollback();
                return false;
            }
        }
    }
}
