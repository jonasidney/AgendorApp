using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data
{
    public class Empresa
    {
        public int Codigo { get; set; }

        public Endereco Endereco { get; set; }

        public int CNPJ { get; set; }

        public String Nome { get; set; }

        public String Telefone { get; set; }

        public bool Ativo { get; set; } = true; 

        public Empresa() { }
        public Empresa(int Codigo)
        {
            this.Codigo = Codigo;
        }
    }
}
