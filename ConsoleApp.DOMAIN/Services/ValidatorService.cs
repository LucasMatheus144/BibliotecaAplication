using ConsoleApp.DOMAIN.Entites;
using ConsoleApp.DOMAIN.Repository;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.DOMAIN.Services
{
    public class ValidatorService
    {

        private readonly IRepository db;

        public ValidatorService(IRepository repository)
        {
            this.db = repository;
        }   

        public bool ValitatorModels<AllModels>(AllModels obj, out List<MensagemErro> erros)
        {

            var valida = new List<ValidationResult>();

            var IsValid = Validator.TryValidateObject(obj, new ValidationContext(obj), valida, true);

            erros = new List<MensagemErro>();

            if (!IsValid)
            {
                erros = valida
                    .Select(x => new MensagemErro(
                        x.MemberNames.FirstOrDefault(),
                        TratarMensagemErro(x.ErrorMessage) , "Entites Error"
                        ))
                    .ToList();
            }

            //Validação Banco de Dados
            if(obj is Livro entity)
            {
                var x = ValidaLivroNomeUnique(entity.Nome);

                if (!x)
                {
                    IsValid = false;
                    erros.Add(new MensagemErro("Livro", "Nome do livro duplicado", "Banco de Dados"));
                }
            }
            else if (obj is Cliente _cliente)
            {
                if ( !Cliente_Unque(_cliente.Cpf))
                {
                    IsValid = false;
                    erros.Add(new MensagemErro("Cliente", "CPF do cliente duplicado", "Banco de Dados"));
                }
            }
            else if (obj is Entites.RegistroEmprestimo)
            {
                // regra de negocio do registro emprestimo
            }

            return IsValid;
        }

        // Validar Livro unique
        private bool ValidaLivroNomeUnique(string nome)
        {
            var consulta = db.Consulta<Livro>().Count( x => x.Nome == nome);

            if (consulta == 0)
            {
                return true;
            }

            return false;
        }

        // validar Cliente
        private bool Cliente_Unque(string cpf)
        {
            var consulta = db.Consulta<Cliente>().Count(x => x.Cpf == cpf);
            if (consulta == 0)
            {
                return true;
            }
            return false;
        }

        private string TratarMensagemErro(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return "Deu Zebra ai";

            else if (msg.Contains("field is required.")) return "O campo é obrigatorio!";

            return msg;
        }
    }
}
