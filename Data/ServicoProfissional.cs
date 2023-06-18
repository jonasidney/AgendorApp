using CRUDBlazorSQLServer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data
{
    public class ServicoProfissional
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Informe o Serviço.")]
        public Servico Servico { get; set; }

        public Profissional Profissional { get; set; }
        public List<Horario> Horarios { get; set; } = new List<Horario>();

        public bool Ativo { get; set; } = true;
    }
}
