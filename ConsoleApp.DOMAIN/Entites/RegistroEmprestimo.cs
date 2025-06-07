using ConsoleApp.DOMAIN.Interface;

namespace ConsoleApp.DOMAIN.Entites
{
    public class RegistroEmprestimo : IEntidade
    {
        public int Id { get; set; }

        public Cliente Cliente { get; set; }

        public Livro Livro { get; set; }

        public DateTime DataRetirada { get; set; }

        public DateTime DataDevolucao { get; set; } 

        public bool Devolvido { get; set; }
    }

    public class EmprestimoBuilder
    {
        private readonly RegistroEmprestimo _registro = new();

        public EmprestimoBuilder ComId(int id)
        {
            _registro.Id = id;
            return this;
        }
        public EmprestimoBuilder ComCliente(Cliente cliente)
        {
            _registro.Cliente = cliente;
            return this;
        }

        public EmprestimoBuilder ComLivro(Livro livro)
        {
            _registro.Livro = livro;
            return this;
        }

        public EmprestimoBuilder ComDataRetirada(DateTime dataRetirada)
        {
            _registro.DataRetirada = dataRetirada;
            return this;
        }

        public EmprestimoBuilder ComDataDevolucao(DateTime dataDevolucao)
        {
            _registro.DataDevolucao = dataDevolucao;
            return this;
        }

        public EmprestimoBuilder ComStatusDevolucao(bool devolvido)
        {
            _registro.Devolvido = devolvido;
            return this;
        }

        public RegistroEmprestimo Build()
        {
            return _registro;
        }
    }
}
