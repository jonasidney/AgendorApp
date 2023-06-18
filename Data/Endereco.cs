using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data
{
    public class Endereco
    {

        public int Codigo { get; set; }

        public String Logradouro { get; set; }

        public String Bairro { get; set; }
        public int Numero { get; set; }
        public String CEP { get; set; }
    }
}
