using ConsoleApp.DOMAIN.Interface;
using ConsoleApp.DOMAIN.ValuesObject;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.DOMAIN.Entites
{
    public class Livro : IEntidade
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public SituacaoLivro Status { get; set; }
    }
}
