using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SalaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SalaoAPI.Projecao;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalaoAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : Controller
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

                SqlCommand cmd = new SqlCommand(@"select 
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

                while (dr.Read())
                {
                    AgendaProjecao agenda = new AgendaProjecao()
                    {
                        dia = ((DateTime)dr["dia"]).ToShortDateString(),
                        horario = (string)dr["horaio"],
                        nome = (string)dr["nome"],
                        servico = (string)dr["servico"],
                        salao = (string)dr["Salao"],

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
        public ActionResult<Agenda> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from AGE_AGENDA where AGEIDENTIFICADOR = " + id, sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Agenda agenda = new Agenda()
                    {
                        Id = (int)dr["AGEIDENTIFICADOR"],
                        ServicoId = (int)dr["SOSIDENTIFICADOR"],
                        TipoAgenda = (int)dr["AGETIPOAGENDA"],
                        DataInicio = (DateTime)dr["AGEDATAINICIO"],
                        DataFim = (DateTime)dr["AGEDATAFIM"],
                        AbreSegunda = (int)dr["AGEABRESEG"],
                        AbreTerca = (int)dr["AGEABRETER"],
                        AbreQuarta = (int)dr["AGEABREQUA"],
                        AbreQuinta = (int)dr["AGEABREQUI"],
                        AbreSexta = (int)dr["AGEABRESEX"],
                        AbreSabado = (int)dr["AGEABRESAB"],
                        AbreDomingo = (int)dr["AGEABREDOM"]
                    };
                    sqlConn.Close();
                    return agenda;
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
        public string Post(Agenda agenda)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[AGE_AGENDA]([AGEIDENTIFICADOR],[SOSIDENTIFICADOR],[AGETIPOAGENDA],[AGEDATAINICIO],[AGEDATAFIM],[AGEABRESEG],[AGEABRETER],[AGEABREQUA],[AGEABREQUI],[AGEABRESEX],[AGEABRESAB],[AGEABREDOM]) " +
                    "values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}') "
                    , agenda.Id, agenda.ServicoId, agenda.TipoAgenda, agenda.DataInicio, agenda.DataFim, agenda.AbreSegunda, agenda.AbreTerca, agenda.AbreQuarta, agenda.AbreQuinta, agenda.AbreSexta, agenda.AbreSabado, agenda.AbreDomingo), sqlConn);

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
        public string Put(int id, Agenda agenda)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE [dbo].[AGE_AGENDA],[SOSIDENTIFICADOR] = '{0}',[AGETIPOAGENDA] = '{1}',[AGEDATAINICIO] = '{2}',[AGEDATAFIM] = '{3}',[AGEABRESEG] = '{4}',[AGEABRETER] = '{5}',[AGEABREQUA] = '{6}' ,[AGEABREQUI] = '{7}'" +
                    ",[AGEABRESEX] = '{8}',[AGEABRESAB] = '{9}',[AGEABREDOM] = '{10}' WHERE AGEIDENTIFICADOR = {11}"
                    , agenda.ServicoId, agenda.TipoAgenda, agenda.DataInicio, agenda.DataFim, agenda.AbreSegunda, agenda.AbreTerca, agenda.AbreQuarta, agenda.AbreQuinta, agenda.AbreSexta, agenda.AbreSabado, agenda.AbreDomingo, id), sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from [AGE_AGENDA] WHERE AGEIDENTIFICADOR = {0}", id), sqlConn);


                int result = cmd.ExecuteNonQuery();


                if (result == 0)
                    return "{\"respServidor\" : \"Houve um erro ao excluir a agenda.\"}";

                sqlConn.Close();

                return "{\"respServidor\" : \"Agenda excluída com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"respServidor\" : \"Ocorreu um erro.\"}";
            }

        }
    }
}

