using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaoAPI.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public int Fornecedor { get; set; }

        public string Descricao { get; set; }
    }
}

