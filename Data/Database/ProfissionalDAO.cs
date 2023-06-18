using CRUDBlazorSQLServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MauiBlazorSQLServer.Data.Database
{
    public class ProfissionalDAO
    {
        public bool gravar(Profissional profissional)
        {
            if (profissional.Codigo == 0)
            {
                return inserir(profissional);
            }
            else
            {
                return alterar(profissional);
            }
        }

        private bool inserir(Profissional profissional)
        {
            SqlTransaction transaction = ConexaoSqlServer.getConexao().BeginTransaction();
            try
            {
                //Insere os dados na tabela profissional
                SqlCommand command = new SqlCommand("insert into profissional (codigo_emp, nome, atividade, registro_ativo) output INSERTED.codigo_pro values " +
                    "(@empresa, @nome, @atividade, @ativo)", ConexaoSqlServer.getConexao());
                command.Transaction = transaction;
                command.Parameters.AddWithValue("empresa", DadosSessao.EmpresaUsuarioLogado.Codigo);
                command.Parameters.AddWithValue("nome", profissional.Nome);
                command.Parameters.AddWithValue("atividade", profissional.Atividade);
                command.Parameters.AddWithValue("ativo", profissional.Ativo);
                profissional.Codigo = int.Parse(command.ExecuteScalar().ToString());
                command.Dispose();

                //Insere os dados na tabela servico_profissional
                foreach (var servProf in profissional.ServicosPrestados)
                {
                    SqlCommand commandDetalhe = new SqlCommand("insert into servico_profissional (codigo_pro, codigo_srv, registro_ativo) output INSERTED.codigo_servprof values " +
                                       "(@profissional, @servico, @ativo)", ConexaoSqlServer.getConexao());
                    commandDetalhe.Transaction = transaction;
                    commandDetalhe.Parameters.AddWithValue("profissional", profissional.Codigo);
                    commandDetalhe.Parameters.AddWithValue("servico", servProf.Servico.Codigo);
                    commandDetalhe.Parameters.AddWithValue("ativo", servProf.Ativo);
                    servProf.Codigo = int.Parse(commandDetalhe.ExecuteScalar().ToString());
                    commandDetalhe.Dispose();

                    //Insere os dados na tabela horario
                    foreach (Horario horario in servProf.Horarios)
                    {
                        SqlCommand commandDetalheHorario = new SqlCommand("insert into horario " +
                            "(codigo_servprof, dia_semana, inicio, fim, registro_ativo) " +
                            "output INSERTED.codigo_servprof values " +
                            "(@servProf, @dia, @inicio, @fim, @ativo)", ConexaoSqlServer.getConexao());
                        commandDetalheHorario.Transaction = transaction;
                        commandDetalheHorario.Parameters.AddWithValue("servProf", servProf.Codigo);
                        commandDetalheHorario.Parameters.AddWithValue("dia", horario.Dia.Id);
                        commandDetalheHorario.Parameters.AddWithValue("inicio", horario.HoraInicio.ToString());
                        commandDetalheHorario.Parameters.AddWithValue("fim", horario.HoraFim.ToString());
                        commandDetalheHorario.Parameters.AddWithValue("ativo", horario.Ativo);
                        horario.Codigo = int.Parse(commandDetalheHorario.ExecuteScalar().ToString());
                        commandDetalheHorario.Dispose();
                    }
                }

               

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

        private bool alterar(Profissional profissional)
        {
            SqlTransaction transaction = ConexaoSqlServer.getConexao().BeginTransaction();
            try
            {
                //atualiza os dados na tabela profissional
                SqlCommand command = new SqlCommand("update profissional set " +
                    "codigo_emp = @empresa, nome = @nome, atividade = @atividade, registro_ativo = @ativo  " +
                    "where codigo_pro = @id", ConexaoSqlServer.getConexao());
                command.Transaction = transaction;
                command.Parameters.AddWithValue("id", profissional.Codigo);
                command.Parameters.AddWithValue("empresa", DadosSessao.EmpresaUsuarioLogado.Codigo);
                command.Parameters.AddWithValue("nome", profissional.Nome);
                command.Parameters.AddWithValue("atividade", profissional.Atividade);
                command.Parameters.AddWithValue("ativo", profissional.Ativo);
                command.ExecuteNonQuery();
                command.Dispose();

                //Atualiza os dados na tabela servico_profissional
                foreach (var servProf in profissional.ServicosPrestados)
                {
                    if (servProf.Codigo == 0)
                    {
                        SqlCommand commandDetalhe = new SqlCommand("insert into servico_profissional (codigo_pro, codigo_srv, registro_ativo) output INSERTED.codigo_servprof values " +
                                           "(@profissional, @servico, @ativo)", ConexaoSqlServer.getConexao());
                        commandDetalhe.Transaction = transaction;
                        commandDetalhe.Parameters.AddWithValue("profissional", profissional.Codigo);
                        commandDetalhe.Parameters.AddWithValue("servico", servProf.Servico.Codigo);
                        commandDetalhe.Parameters.AddWithValue("ativo", servProf.Ativo);
                        servProf.Codigo = int.Parse(commandDetalhe.ExecuteScalar().ToString());
                        commandDetalhe.Dispose();
                    }
                    else
                    {
                        SqlCommand commandDetalhe = new SqlCommand("update servico_profissional set " +
                            "codigo_pro = @profissional, codigo_srv = @servico, registro_ativo = @ativo " +
                                          "where codigo_servprof = @id", ConexaoSqlServer.getConexao());
                        commandDetalhe.Transaction = transaction;
                        commandDetalhe.Parameters.AddWithValue("id", servProf.Codigo);
                        commandDetalhe.Parameters.AddWithValue("profissional", servProf.Profissional.Codigo);
                        commandDetalhe.Parameters.AddWithValue("servico", servProf.Servico.Codigo);
                        commandDetalhe.Parameters.AddWithValue("ativo", servProf.Ativo);
                        commandDetalhe.ExecuteNonQuery();
                        commandDetalhe.Dispose();
                    }

                    //atualiza os dados na tabela horario
                    foreach (Horario horario in servProf.Horarios)
                    {
                        if (horario.Codigo == 0)
                        {
                            SqlCommand commandDetalheHorario = new SqlCommand("insert into horario " +
                                "(codigo_servprof, dia_semana, inicio, fim, registro_ativo) " +
                                "output INSERTED.codigo_servprof values " +
                                "(@servProf, @dia, @inicio, @fim, @ativo)", ConexaoSqlServer.getConexao());
                            commandDetalheHorario.Transaction = transaction;
                            commandDetalheHorario.Parameters.AddWithValue("servProf", servProf.Codigo);
                            commandDetalheHorario.Parameters.AddWithValue("dia", horario.Dia.Id);
                            commandDetalheHorario.Parameters.AddWithValue("inicio", horario.HoraInicio.ToString());
                            commandDetalheHorario.Parameters.AddWithValue("fim", horario.HoraFim.ToString());
                            commandDetalheHorario.Parameters.AddWithValue("ativo", horario.Ativo);
                            horario.Codigo = int.Parse(commandDetalheHorario.ExecuteScalar().ToString());
                            commandDetalheHorario.Dispose();
                        }
                        else
                        {
                            SqlCommand commandDetalheHorario = new SqlCommand("update horario set " +
                                                            "codigo_servprof = @servProf, dia_semana = @dia, inicio = @inicio, fim = @fim, registro_ativo = @ativo " +
                                                            "where codigo_hor = @id", ConexaoSqlServer.getConexao());
                            commandDetalheHorario.Transaction = transaction;
                            commandDetalheHorario.Parameters.AddWithValue("id", horario.Codigo);
                            commandDetalheHorario.Parameters.AddWithValue("servProf", servProf.Codigo);
                            commandDetalheHorario.Parameters.AddWithValue("dia", horario.Dia.Id);
                            commandDetalheHorario.Parameters.AddWithValue("inicio", horario.HoraInicio.ToString());
                            commandDetalheHorario.Parameters.AddWithValue("fim", horario.HoraFim.ToString());
                            commandDetalheHorario.Parameters.AddWithValue("ativo", horario.Ativo);
                            commandDetalheHorario.ExecuteNonQuery();
                            commandDetalheHorario.Dispose();
                        }
                        
                    }
                }

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

        public Profissional carregarTodosOsDados(int idProfissional)
        {
            Profissional prof = new();
            prof.Codigo = idProfissional;
            SqlCommand command = new SqlCommand("select " +
                "p.codigo_emp, p.nome as nomeprof, p.atividade, p.registro_ativo,  " +
                "sp.codigo_servprof as idsp, sp.registro_ativo as spativo, " +
                "s.codigo_srv, s.descricao as nomeservico " +
                "from profissional p " +
                "left join servico_profissional sp on sp.codigo_pro = p.codigo_pro " +
                "left join servico s on s.codigo_srv = sp.codigo_srv " +
                "where p.codigo_pro = @id", ConexaoSqlServer.getConexao());
            try
            {
                command.Parameters.AddWithValue("id", idProfissional);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Empresa empresa = new();
                        empresa.Codigo = int.Parse(reader["codigo_emp"].ToString());
                        prof.Empresa = empresa;

                        prof.Nome = reader["nomeprof"].ToString();
                        prof.Atividade = reader["atividade"].ToString();
                        prof.Ativo = reader["registro_ativo"].ToString() == "True";

                        if (reader["idsp"] != null)
                        {
                            ServicoProfissional sp = new();
                            sp.Profissional = prof;

                            sp.Codigo = int.Parse(reader["idsp"].ToString());
                            sp.Ativo = reader["spativo"].ToString() == "True";

                            Servico serv = new();
                            serv.Codigo = int.Parse(reader["codigo_srv"].ToString());
                            serv.Descricao = reader["nomeservico"].ToString();
                            sp.Servico = serv;

                            prof.ServicosPrestados.Add(sp);
                        }
                    }
                }


                //Carrega horários do serviço prestado
                foreach (ServicoProfissional servProf in prof.ServicosPrestados)
                {
                    SqlCommand cmdHorarios = new SqlCommand("select " +
                       " * " +
                       "from horario h " +
                       "where h.codigo_servprof = @id " +
                       "order by dia_semana, inicio, fim", ConexaoSqlServer.getConexao());
                    cmdHorarios.Parameters.AddWithValue("id", servProf.Codigo);
                    using (SqlDataReader readerHorarios = cmdHorarios.ExecuteReader())
                    {
                        while (readerHorarios.Read())
                        {
                            Horario horario = new();
                            horario.ServicoProfissional = servProf;
                            horario.Codigo = int.Parse(readerHorarios["codigo_hor"].ToString());
                            horario.Dia = new DiaSemana(int.Parse(readerHorarios["dia_semana"].ToString()));
                            horario.HoraInicio = TimeOnly.Parse(readerHorarios["inicio"].ToString());
                            horario.HoraFim = TimeOnly.Parse(readerHorarios["fim"].ToString());
                            horario.Ativo = readerHorarios["registro_ativo"].ToString() == "True";

                            servProf.Horarios.Add(horario);
                        }
                    }
                }
                /////////////////////////////////////
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return prof;
        }

        public bool excluir(int idProfissional)
        {
            SqlTransaction transaction = ConexaoSqlServer.getConexao().BeginTransaction();
            try
            {
                //Insere os dados na tabela profissional
                SqlCommand command = new SqlCommand("update profissional set registro_ativo = 0 where codigo_pro = @id", ConexaoSqlServer.getConexao());
                command.Transaction = transaction;
                command.Parameters.AddWithValue("id", idProfissional);
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

        public List<Profissional> pesquisar(String filtro)
        {
            List<Profissional> profissionais = new List<Profissional>();
            SqlCommand command = new SqlCommand("Select codigo_pro, nome, atividade from profissional where codigo_emp = @empresa and registro_ativo = 1 and lower(nome) like @filtro", ConexaoSqlServer.getConexao());
            try
            {
                command.Parameters.AddWithValue("empresa", DadosSessao.EmpresaUsuarioLogado.Codigo);
                command.Parameters.AddWithValue("filtro", "%" + filtro.ToLower() + "%");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Profissional prof = new();
                        prof.Codigo = int.Parse(reader["codigo_pro"].ToString());
                        prof.Nome = reader["nome"].ToString();
                        prof.Atividade = reader["atividade"].ToString();
                        profissionais.Add(prof);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return profissionais;
        }
    }
}
