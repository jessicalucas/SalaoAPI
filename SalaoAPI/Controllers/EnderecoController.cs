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
    public class EnderecoController : Controller
    {
      public string connectionString = "Server=tcp:salaoapi.database.windows.net;Database=SalaoAPI;User ID =api@guiaalimentar.database.windows.net;Password=Salao123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Endereco>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from END_Endereco", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Endereco> result = new List<Endereco>();

                while (dr.Read())
                {
                    Endereco endereco = new Endereco()
                    {
                        Id = (int)dr["ENDIDENTIFICADOR"],
                        CEP = (string)dr["ENDCEP"],
                        Estado = (string)dr["ENDESTADO"],
                        Cidade = (string)dr["ENDCIDADE"],
                        Bairro = (string)dr["ENDBAIRRO"],
                        Rua = (string)dr["ENDRUA"],
                        Numero = (string)dr["ENDNUMERO"],
                        Pais = (string)dr["ENDPAIS"]

                    };
                    result.Add(endereco);

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
        public ActionResult<Endereco> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from END_Endereco where ENDIDENTIFICADOR = " + id,sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Endereco endereco = new Endereco()
                    {
                        Id = (int)dr["ENDIDENTIFICADOR"],
                        CEP = (string)dr["ENDCEP"],
                        Estado = (string)dr["ENDESTADO"],
                        Cidade = (string)dr["ENDCIDADE"],
                        Bairro = (string)dr["ENDBAIRRO"],
                        Rua = (string)dr["ENDRUA"],
                        Numero = (string)dr["ENDNUMERO"],
                        Pais = (string)dr["ENDPAIS"]
                    };
                    sqlConn.Close();
                    return endereco;
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
        public string Post(Endereco endereco)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[END_Endereco]([ENDIDENTIFICADOR],[ENDPAIS],[ENDESTADO],[ENDCIDADE],[ENDBAIRRO],[ENDCEP],[ENDRUA],[ENDNUMERO]) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}') "
                    , endereco.Id, endereco.Pais, endereco.Estado, endereco.Cidade, endereco.Bairro, endereco.CEP, endereco.Rua, endereco.Numero),sqlConn);

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
        public string Put(int id, Endereco endereco)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE [dbo].[END_Endereco]SET [ENDPAIS] = '{0}',[ENDESTADO] = '{1}',[ENDCIDADE] = '{2}',[ENDBAIRRO] = '{3}',[ENDCEP] = '{4}',[ENDRUA] = '{5}',[ENDNUMERO] = '{6}' WHERE ENDIDENTIFICADOR = {7}"
                    , endereco.Pais, endereco.Estado, endereco.Cidade, endereco.Bairro, endereco.CEP, endereco.Rua, endereco.Numero, id),sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from END_Endereco WHERE ENDIDENTIFICADOR = {0}", id),sqlConn);


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

