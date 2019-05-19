using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SalaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SalaoAPI.Projecao;

namespace SalaoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        //public string connectionString = "Server=tcp:salaoapi.database.windows.net;Database=SalaoAPI;User ID =api@salaoapi.database.windows.net;Password=Salao123@;Trusted_Connection=False;Encrypt=True;";
        public string connectionString = "Server=JESSICA\\SQLEXPRESS;Database=Salao;Integrated Security=yes;Uid=auth_windows;";

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<AgendaProjecao>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                //SqlCommand cmd = new SqlCommand("Select * from CAR_CarrinhoDeCompras", sqlConn);

                SqlCommand cmd = new SqlCommand(@"select 
                                                  car.CARIDENTIFICADOR id,
                                                  car.CARDATA dia,
                                                  car.CARHORAINICIO + ' - ' + car.CARHORAFIM horaio,
                                                  usu.USUNOME nome,
                                                  sos.SOSDESCRICAO servico,
                                                  forn.FORNOMEFANTASIA Salao
                                                  FROM CAR_CarrinhoDeCompras car
                                                  inner join USU_Usuario usu on usu.USUIDENTIFICADOR = car.USUIDENTIFICADOR
                                                  inner join LSA_LigServicoSAgenda lsa on lsa.LSAIDENTIFICADOR = car.LSAIDENTIFICADOR
                                                  inner join SOS_ServicosOferecidosSalao sos on sos.SOSIDENTIFICADOR = lsa.SOSIDENTIFICADOR
                                                  inner join FOR_Fornecedor forn on forn.FORIDENTIFICADOR = lsa.FORIDENTIFICADOR ", sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();

                List<AgendaProjecao> result = new List<AgendaProjecao>();

                //while (dr.Read())
                //{
                //    Carrinho carrinho = new Carrinho()
                //    {
                //        Id = (int)dr["CARIDENTIFICADOR"],
                //        IdLigacao = (int)dr["LSAIDENTIFICADOR"],
                //        IdUsuario = (int)dr["USUIDENTIFICADOR"],
                //        Valor = (decimal)dr["CARVALOR"],
                //        DataAgendamento = ((DateTime)dr["CARDATA"]).ToShortDateString(),
                //        HoraInicio = (string)dr["CARHORAINICIO"],
                //        HoraFim = (string)dr["CARHORAFIM"]

                //    };
                //    result.Add(carrinho);
                //}

                while (dr.Read())
                {
                    AgendaProjecao agenda = new AgendaProjecao()
                    {
                        dia = ((DateTime)dr["dia"]).ToShortDateString(),
                        horario = (string)dr["horaio"],
                        nome = (string)dr["nome"],
                        servico = (string)dr["servico"],
                        salao = (string)dr["Salao"],
                        id = ((int)dr["id"]).ToString(),

                    };
                    result.Add(agenda);

                }
                sqlConn.Close();
                return result;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Carrinho> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from CAR_CarrinhoDeCompras where CARIDENTIFICADOR = " + id, sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Carrinho carrinho = new Carrinho()
                    {

                        Id = (int)dr["CARIDENTIFICADOR"],
                        IdLigacao = (int)dr["LSAIDENTIFICADOR"],
                        IdUsuario = (int)dr["USUIDENTIFICADOR"],
                        Valor = (decimal)dr["CARVALOR"],
                        DataAgendamento = ((DateTime)dr["CARDATA"]).ToShortDateString(),
                        HoraInicio = (string)dr["CARHORAINICIO"],
                        HoraFim = (string)dr["CARHORAFIM"]
                    };
                    sqlConn.Close();
                    return carrinho;
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<controller>
        [HttpPost]
        public string Post(Carrinho carrinho)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[CAR_CarrinhoDeCompras] ([CARIDENTIFICADOR],[LSAIDENTIFICADOR] ,[USUIDENTIFICADOR] ,[CARVALOR] ,[CARDATA] ,[CARHORAINICIO] ,[CARHORAFIM]) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') "
                    , carrinho.Id, carrinho.IdLigacao, carrinho.IdUsuario, carrinho.Valor, carrinho.DataAgendamento, carrinho.HoraInicio, carrinho.HoraFim), sqlConn);

                int result = cmd.ExecuteNonQuery();

                sqlConn.Close();

                if (result == 0)
                    return "{\"respServidor\" : \"Houve um erro ao inserir os dados.\"}";

                return "{\"respServidor\" : \"Os dados foram incluídos com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"respServidor\" : \"Houve um erro interno.\"}";
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public string Put(int id, Carrinho carrinho)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();
                                                                                                                                          
        SqlCommand cmd = new SqlCommand(string.Format("UPDATE [dbo].[CAR_CarrinhoDeCompras]SET [LSAIDENTIFICADOR] = '{0}',[USUIDENTIFICADOR] = '{1}',[CARVALOR] = '{2}',[CARDATA] = '{3}',[CARHORAINICIO] = '{4}', [CARHORAFIM] = '{5}', WHERE CARIDENTIFICADOR = {6}"
                    ,carrinho.IdLigacao, carrinho.IdUsuario, carrinho.Valor, carrinho.DataAgendamento, carrinho.HoraInicio, carrinho.HoraFim, id), sqlConn);


                int result = cmd.ExecuteNonQuery();

                sqlConn.Close();

                if (result == 0)
                    return "{\"respServidor\" : \"Houve um erro ao atualizar os dados.\"}";

                return "{\"respServidor\" : \"Os dados foram atualizados com sucesso\"}";
            }
            catch (Exception ex)
            {
                return "{\"respServidor\" : \"Houve um erro interno.\"}";
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("Delete from CAR_CarrinhoDeCompras WHERE CARIDENTIFICADOR = {0}", id), sqlConn);


                int result = cmd.ExecuteNonQuery();


                if (result == 0)
                    return "{\"respServidor\" : \"Houve um erro ao excluir o carrinho.\"}";

                sqlConn.Close();

                return "{\"respServidor\" : \"Usuário excluído com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"respServidor\" : \"Ocorreu um erro.\"}";
            }

        }
    }
}
