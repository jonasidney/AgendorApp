using MauiBlazorSQLServer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBlazorSQLServer.Data
{
    public class Servico
    {
        public int Codigo { get; set; }

        public Empresa Empresa { get; set; }

        [Required(ErrorMessage = "Informe a descrição do serviço.")]
        [StringLength(100, ErrorMessage = "A descrição do serviço não pode ser maior que 100 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a atividade exercida pelo profissional.")]
        public float Valor { get; set; }
        public bool Ativo { get; set; } = true;

        public override string ToString()
        {
            return Descricao;
        }

    }
}
