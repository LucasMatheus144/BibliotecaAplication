using ConsoleApp.DOMAIN.Interface;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.DOMAIN.Entites
{
    public class Cliente : IEntidade
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage ="O minimo é 3 caracteres!")]
        [MaxLength(20, ErrorMessage ="O maximo é de 20 caracteres!")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage ="O maximo de 50 caracteres!")]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        private string _cpf { get; set; } = string.Empty;

        [Required]
        public string Cpf 
        { 
            get
            {
                if (!string.IsNullOrEmpty(_cpf) && _cpf.Length == 11)
                    return $"{_cpf.Substring(0, 3)}.{_cpf.Substring(3, 3)}.{_cpf.Substring(6, 3)}-{_cpf.Substring(9, 2)}";
                return "";
            }
            set
            {
                _cpf = new string(value.Where(char.IsDigit).ToArray());
            }
        }

        public DateTime DataNascimento { get; set; }

        public DateTime UltimaAlteracao { get; set; } = DateTime.Now;

        public int Idade
        {
            get
            {
                var idade = DateTime.Now.Year - DataNascimento.Year;
                if (DataNascimento > DateTime.Now.AddYears(-idade)) idade--;
                return idade;
            }
        }

    }
}

