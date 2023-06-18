using CRUDBlazorSQLServer.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorSQLServer.Data.Database
{
    internal class ServicoDAO
    {
        public bool gravar(Servico servico)
        {
            if (servico.Codigo == 0)
            {
                return inserir(servico);
            }
            else
            {
                return alterar(servico);
            }
        }

        private bool inserir(Servico servico)
        {
            SqlTransaction transaction = ConexaoSqlServer.getConexao().BeginTransaction();
            try
            {
                //Insere os dados na tabela profissional
                SqlCommand command = new SqlCommand("insert into servico (codigo_emp, descricao, valor, registro_ativo) output INSERTED.codigo_srv values " +
                    "(@empresa, @descricao, @valor, @ativo)", ConexaoSqlServer.getConexao());
                command.Transaction = transaction;
                command.Parameters.AddWithValue("empresa", DadosSessao.EmpresaUsuarioLogado.Codigo);
                command.Parameters.AddWithValue("descricao", servico.Descricao);
                command.Parameters.AddWithValue("valor", servico.Valor);
                command.Parameters.AddWithValue("ativo", servico.Ativo);
                servico.Codigo = int.Parse(command.ExecuteScalar().ToString());
                command.Dispose();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Debug.WriteLine(ex);
            }
            return false;
        }

        private bool alterar(Servico servico)
        {
            SqlTransaction transaction = ConexaoSqlServer.getConexao().BeginTransaction();
            try
            {
                //atualiza os dados na tabela profissional
                SqlCommand command = new SqlCommand("update servico set " +
                    "codigo_emp = @empresa, descricao = @descricao, valor = @valor, registro_ativo = @ativo  " +
                    "where codigo_srv = @id", ConexaoSqlServer.getConexao());
                command.Transaction = transaction;
                command.Parameters.AddWithValue("id", servico.Codigo);
                command.Parameters.AddWithValue("empresa", DadosSessao.EmpresaUsuarioLogado.Codigo);
                command.Parameters.AddWithValue("descricao", servico.Descricao);
                command.Parameters.AddWithValue("valor", servico.Valor);
                command.Parameters.AddWithValue("ativo", servico.Ativo);
                command.ExecuteNonQuery();
                command.Dispose();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Debug.WriteLine(ex);
            }
            return false;
        }

        public List<Servico> listarServicosEmpresa()
        {
            List<Servico> servicos = new List<Servico>();

            SqlCommand command = new SqlCommand("Select codigo_srv, descricao from servico where codigo_emp = @empresa and registro_ativo = 1", ConexaoSqlServer.getConexao());
            try
            {
                command.Parameters.AddWithValue("empresa", DadosSessao.EmpresaUsuarioLogado.Codigo);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Servico servico = new();
                        servico.Codigo = int.Parse(reader["codigo_srv"].ToString());
                        servico.Descricao = reader["descricao"].ToString();
                        servicos.Add(servico);
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return servicos;
        }

        public Servico carregarTodosOsDados(int idServico)
        {
            Servico serv = new();
            serv.Codigo = idServico;
            SqlCommand command = new SqlCommand("select * from servico " +
                "where codigo_srv = @id", ConexaoSqlServer.getConexao());
            try
            {
                command.Parameters.AddWithValue("id", idServico);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Empresa empresa = new();
                        empresa.Codigo = int.Parse(reader["codigo_emp"].ToString());
                        serv.Empresa = empresa;

                        serv.Descricao = reader["descricao"].ToString();
                        serv.Valor = float.Parse(reader["valor"].ToString());
                        serv.Ativo = reader["registro_ativo"].ToString() == "True";

                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return serv;
        }

        public bool excluir(int idServico)
        {
            SqlTransaction transaction = ConexaoSqlServer.getConexao().BeginTransaction();
            try
            {
                //Insere os dados na tabela profissional
                SqlCommand command = new SqlCommand("update servico set registro_ativo = 0 where codigo_srv = @id", ConexaoSqlServer.getConexao());
                command.Transaction = transaction;
                command.Parameters.AddWithValue("id", idServico);
                command.ExecuteNonQuery();
                command.Dispose();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return false;
        }


    }
}
