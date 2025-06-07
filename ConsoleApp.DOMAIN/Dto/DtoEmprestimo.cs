namespace ConsoleApp.DOMAIN.Dto
{
    public class DtoEmprestimo
    {
        public int Id { get; set; }

        public int idCliente { get; set; }

        public int idLivro { get; set; }

        public DateTime DataRetirada { get; set; } = DateTime.Now;

        public DateTime DataDevolucao { get; set; } = DateTime.Now.AddDays(7);

        public bool Devolvido { get; set; }
    }
}
