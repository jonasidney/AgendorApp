using MauiBlazorSQLServer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBlazorSQLServer.Data
{
    public class Profissional
    {
        public int Codigo { get; set; }

        public Empresa Empresa { get; set; }

        [Required(ErrorMessage = "Informe o nome do profissional.")]
        [StringLength(50, ErrorMessage = "O nome do profissional não pode ser maior que 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a atividade exercida pelo profissional.")]
        [StringLength(100, ErrorMessage = "A descrição da atividade não pode ser maior que 100 caracteres.")]
        public string Atividade { get; set; }
        public bool Ativo { get; set; } = true;

        public List<ServicoProfissional> ServicosPrestados { get; set; } = new List<ServicoProfissional>();

        
        public Profissional()
        {
           /* Servico s1 = new();
            s1.Codigo = 1;
            s1.Descricao = "Corte de cabelo Masculino";

            ServicoProfissional sp1 = new();
            sp1.Servico = s1;
            ServicosPrestados.Add(sp1);

            Servico s2 = new();
            s2.Codigo = 2;
            s2.Descricao = "Corte de cabelo Feminino";

            ServicoProfissional sp2 = new();
            sp2.Servico = s2;

            Horario h1 = new();
            h1.Dia = new DiaSemana(2);
            h1.HoraInicio = new TimeOnly(10,50);
            h1.HoraFim= new TimeOnly(11, 57);
            sp2.Horarios.Add(h1);

            ServicosPrestados.Add(sp2);*/
        }

    }
}
