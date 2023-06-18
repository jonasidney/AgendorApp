using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data
{
    public class Horario
    {
        public int Codigo { get; set; }
        public ServicoProfissional ServicoProfissional { get; set; }

        public DiaSemana Dia { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
