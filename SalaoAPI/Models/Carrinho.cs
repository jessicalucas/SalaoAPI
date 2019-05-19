using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaoAPI.Models
{
    public class Carrinho
    {
        public int Id { get; set; }

        public int IdLigacao { get; set; }

        public int IdUsuario { get; set; }

        public decimal Valor { get; set; }

        public string DataAgendamento { get; set; }

        public string HoraInicio { get; set; }

        public string HoraFim { get; set; }
                      
    }
}
