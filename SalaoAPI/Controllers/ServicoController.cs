using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SalaoAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalaoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : Controller
    {

        public string connectionString = "Server=tcp:salaoapi.database.windows.net;Database=SalaoAPI;User ID =api@guiaalimentar.database.windows.net;Password=Salao123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Servico>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from SOS_ServicosOferecidosSalao", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Servico> result = new List<Servico>();

                while (dr.Read())
                {
                    Servico se = new Servico()
                    {
                        Id = (int)dr["SOSIDENTIFICADOR"],
                        Fornecedor = (int)dr["FORIDENTIFICADOR"],
                        Descricao = (string)dr["SOSDESCRICAO"]
                    };
                    result.Add(se);
                }

                sqlConn.Close();
                return result;

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Servico> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from SOS_ServicosOferecidosSalao where SOSIDENTIFICADOR = " + id, sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Servico en = new Servico()
                    {
                        Id = (int)dr["SOSIDENTIFICADOR"],
                        Fornecedor = (int)dr["FORIDENTIFICADOR"],
                        Descricao = (string)dr["SOSDESCRICAO"]
                    };
                    sqlConn.Close();
                    return en;
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // POST api/<controller>
        [HttpPost]
        public string Post(Servico Servico)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[SOS_ServicosOferecidosSalao]([SOSIDENTIFICADOR],[FORIDENTIFICADOR],[SOSDESCRICAO]) values ('{0}','{1}','{2}') "
                    , Servico.Id, Servico.Fornecedor, Servico.Descricao), sqlConn);

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
        public string Put(int id, Servico Servico)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE [dbo].[SOS_ServicosOferecidosSalao]SET [FORIDENTIFICADOR] = '{0}',[SOSDESCRICAO] = '{1}' WHERE SOSIDENTIFICADOR = {2}"
                    , Servico.Fornecedor, Servico.Descricao, id), sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from SOS_ServicosOferecidosSalao WHERE SOSIDENTIFICADOR = {0}", id), sqlConn);


                int result = cmd.ExecuteNonQuery();


                if (result == 0)
                    return "{\"respServidor\" : \"Houve um erro ao excluir o endereço.\"}";

                sqlConn.Close();

                return "{\"respServidor\" : \"Endereço excluído com sucesso.\"}";
            }
            catch (Exception ex)
            {
                return "{\"respServidor\" : \"Ocorreu um erro.\"}";
            }

        }
    }
}