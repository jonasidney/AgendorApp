using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data.Database
{

    public class ConexaoSqlServer
    {
        
        private static string connetionString = "Data Source=192.168.1.15;Initial Catalog=agendador;User ID=jonasidney;Password=s1st3m@s;Persist Security Info=True;";
        private static SqlConnection conexao;

        public static SqlConnection getConexao()
        {
            if (conexao == null)
            {
                conexao = new SqlConnection(connetionString);
                conexao.Open();
                Debug.WriteLine("CONECTOU AO BANCO DE DADOS LOCAL!");
            }
            return conexao;
        }


    }
}
