using System.Data;
using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Repository;
using ConsoleApp.DOMAIN.ValuesObject;

namespace ConsoleApp.DOMAIN.Services
{
    public class LivroService
    {
        private readonly IRepository db;
        private readonly ValidatorService validator;

        public LivroService(IRepository db, ValidatorService validator)
        {
            this.db = db;
            this.validator = validator;
        }

        public List<Livro> ConsultarAll()
        {
            return db.Consulta<Livro>().ToList();
        }

        public Livro ConsultaPorId(int id)
        {
            return db.ConsultaPorId<Livro>(id);
        }
        public bool NewLivro(Livro livro, out List<MensagemErro> msg)
        {
            if (!validator.ValitatorModels(livro, out msg)) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();
                db.Incluir(livro);
                db.Commit();
                return true;
            }
            catch (Exception)
            {
                db.Rollback();
                return false;
            }
        }

        public bool PutLivro (Livro livro, out List<MensagemErro> msg)
        {

            msg = new List<MensagemErro>();
            var procura = ConsultaPorId(livro.Id);

            if (procura == null) return false;

            if (!validator.ValitatorModels(livro, out msg)) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();
                db.Alterar(livro);
                db.Commit();
                return true;
            }
            catch(Exception)
            {
                db.Rollback();
                return false;
            }
        }

        public bool DeleteLicro(int id)
        {
            var procura = ConsultaPorId(id);

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
    }
}
