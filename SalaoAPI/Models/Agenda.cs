using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaoAPI.Models
{
    public class Agenda
    {
        public int Id { get; set; }

        public int ServicoId { get; set; }

        public int TipoAgenda { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public int AbreSegunda { get; set; }

        public int AbreTerca { get; set; }

        public int AbreQuarta { get; set; }

        public int AbreQuinta { get; set; }

        public int AbreSexta { get; set; }

        public int AbreSabado { get; set; }

        public int AbreDomingo { get; set; }


        public string v1 { get; set; }
        public string v2 { get; set; }
        public string v3 { get; set; }
        public string v4 { get; set; }
        public string v5 { get; set; }

    }
}
