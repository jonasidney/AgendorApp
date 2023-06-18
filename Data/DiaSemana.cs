using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data
{
    public class DiaSemana
    {
        public int Id { get; set; }
        public String Nome { get; set; }


        public DiaSemana(int Id)
        {
            this.Id = Id;
            switch (this.Id)
            {
                case 1: this.Nome = "Domingo"; break;
                case 2: this.Nome = "Segunda-Feira"; break;
                case 3: this.Nome = "Terça-Feira"; break;
                case 4: this.Nome = "Quarta-Feira"; break;
                case 5: this.Nome = "Quinta-Feira"; break;
                case 6: this.Nome = "Sexta-Feira"; break;
                case 7: this.Nome = "Sábado"; break;
            }
        }

        public static List<DiaSemana> getDiasSemana()
        {
            List<DiaSemana> dias = new List<DiaSemana>();
            dias.Add(new DiaSemana(1));
            dias.Add(new DiaSemana(2));
            dias.Add(new DiaSemana(3));
            dias.Add(new DiaSemana(4));
            dias.Add(new DiaSemana(5));
            dias.Add(new DiaSemana(6));
            dias.Add(new DiaSemana(7));
            return dias;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
