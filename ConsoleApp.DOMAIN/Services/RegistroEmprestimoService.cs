using ConsoleApp.DOMAIN.Dto;
using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Repository;
using ConsoleApp.DOMAIN.ValuesObject;

namespace ConsoleApp.DOMAIN.Services
{
    public class RegistroEmprestimoService
    {
        private readonly IRepository db;
        private readonly ValidatorService validator;

        public RegistroEmprestimoService(IRepository db
                                        ,ValidatorService validator)
        {
            this.db = db;
            this.validator = validator;
        }

        public RegistroEmprestimo ListarPorId(int id)
        {
            return db.ConsultaPorId<RegistroEmprestimo>(id);
        }

        public List<RegistroEmprestimo> AllEmprestimo()
        {
            return db.Consulta<RegistroEmprestimo>().ToList();
        }

        public bool CadastraEmprestimo(DtoEmprestimo dto, out List<MensagemErro> erro)
        {
            erro = new List<MensagemErro>();

            var livro = ConsultaPorId(dto.idLivro);
            if (livro == null)
            {
                erro.Add(new MensagemErro("Livro", "Livro não encontrado",null));
                return false;
            }

            var cliente = ConsultaCliente(dto.idCliente);
            if (cliente == null)
            {
                erro.Add(new MensagemErro("Cliente", "Cliente não encontrado",null));
                return false;
            }


            var emprestimo = new EmprestimoBuilder()
                .ComCliente(cliente)
                .ComLivro(livro)
                .ComDataRetirada(dto.DataRetirada)
                .ComDataDevolucao(dto.DataDevolucao)
                .ComStatusDevolucao(false)
                .Build();

            var isValid = validator.ValitatorModels(emprestimo, out erro);
            if (!isValid) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();

                db.Incluir(emprestimo);
                AlugarLivro(dto.idLivro);
                db.Commit();
                return true;
            }
            catch (Exception)
            {
                db.Rollback();
                return false;
            }
        }

        public bool DeletaEmprestimo(int id)
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

        public bool EditarEmprestimo(DtoEmprestimo dto, out List<MensagemErro> erro)
        {
            erro = new List<MensagemErro>();

            var emprestimoExistente = ListarPorId(dto.Id);
            if (emprestimoExistente == null)
            {
                erro.Add(new MensagemErro("Empréstimo", "Registro de empréstimo não encontrado", null));
                return false;
            }

            bool houveAlteracao =
                emprestimoExistente.DataRetirada != dto.DataRetirada ||
                emprestimoExistente.DataDevolucao != dto.DataDevolucao ||
                emprestimoExistente.Devolvido != dto.Devolvido;

            if (!houveAlteracao)
            {
                erro.Add(new MensagemErro("Empréstimo", "Nenhuma alteração foi detectada nos campos permitidos", null));
                return false;
            }


            var livro = ConsultaPorId(dto.idLivro);
            if (livro == null)
            {
                erro.Add(new MensagemErro("Livro", "Livro não encontrado", null));
                return false;
            }

            var cliente = ConsultaCliente(dto.idCliente);
            if (cliente == null)
            {
                erro.Add(new MensagemErro("Cliente", "Cliente não encontrado", null));
                return false;
            }

            var emprestimoAtualizado = new EmprestimoBuilder()
                .ComId(dto.Id)
                .ComCliente(cliente)
                .ComLivro(livro)
                .ComDataRetirada(dto.DataRetirada)
                .ComDataDevolucao(dto.DataDevolucao)
                .ComStatusDevolucao(dto.Devolvido)
                .Build();


            var isValid = validator.ValitatorModels(emprestimoAtualizado, out erro);
            if (!isValid) return false;

            try
            {
                using var iniciar = db.IniciarTransaction();

                db.Alterar(emprestimoAtualizado);
                db.Commit();

                if(emprestimoAtualizado.Devolvido == true)
                {
                    LiberarLivro(emprestimoAtualizado.Livro.Id);
                }

                return true;
            }
            catch (Exception)
            {
                db.Rollback();
                return false;
            }

        }

        private Livro ConsultaPorId(int id)
        {
            return db.ConsultaPorId<Livro>(id);
        }

        private Cliente ConsultaCliente(int id)
        {
            return db.ConsultaPorId<Cliente>(id);
        }

        private void AlugarLivro(int id)
        {
            var livro = ConsultaPorId(id);

            livro.Status = SituacaoLivro.Alugado;

            db.Alterar(livro);
        }

        private void LiberarLivro(int id)
        {
            var livro = ConsultaPorId(id);

            livro.Status = SituacaoLivro.Livre;
            db.Alterar(livro);
        }
    }
}
