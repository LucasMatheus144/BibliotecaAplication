using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DOMAIN.Entites
{
    public class MensagemErro
    {
        public string Property { get; set; }    

        public string Message { get; set; }

        public string? Tipo { get; set; }

        public MensagemErro(string property, string message, string?Tipo)
        {
            Property = property;
            Message = message;
            this.Tipo = Tipo;
        }
    }
}
