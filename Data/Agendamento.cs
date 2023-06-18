using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data
{
    public class Agendamento
    {
        public int Codigo { get; set; }

        public Cliente Cliente{ get; set; }

        public Horario Horario { get; set; }

        public DateOnly Data { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
