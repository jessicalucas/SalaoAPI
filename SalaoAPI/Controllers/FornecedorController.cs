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
    public class FornecedorController : Controller
    {

        public string connectionString = "Server=tcp:salaoapi.database.windows.net;Database=SalaoAPI;User ID =api@guiaalimentar.database.windows.net;Password=Salao123@;Trusted_Connection=False;Encrypt=True;";


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Fornecedor>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from FOR_Fornecedor", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Fornecedor> result = new List<Fornecedor>();

                while (dr.Read())
                {
                    Fornecedor fo = new Fornecedor()
                    {
                        Id = (int)dr["FORIDENTIFICADOR"],
                        Endereco = (int)dr["ENDIDENTIFICADOR"],
                        NomeFantasia = (string)dr["FORNOMEFANTASIA"],
                        Email = (string)dr["FOREMAIL"],
                        CNPJ = (string)dr["FORCNPJ"],
                        Senha = (string)dr["FORSENHA"]
                    };
                    result.Add(fo);
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
        public ActionResult<Fornecedor> Get(int id)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from FOR_Fornecedor where FORIDENTIFICADOR = " + id, sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Fornecedor en = new Fornecedor()
                    {
                        Id = (int)dr["FORIDENTIFICADOR"],
                        Endereco = (int)dr["ENDIDENTIFICADOR"],
                        NomeFantasia = (string)dr["FORNOMEFANTASIA"],
                        Email = (string)dr["FOREMAIL"],
                        CNPJ = (string)dr["FORCNPJ"],
                        Senha = (string)dr["FORSENHA"]
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
        public string Post(Fornecedor Fornecedor)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[FOR_Fornecedor]([FORIDENTIFICADOR],[ENDIDENTIFICADOR],[FORNOMEFANTASIA],[FOREMAIL],[FORCNPJ],[FORSENHA]) values ('{0}','{1}','{2}','{3}','{4}','{5}') "
                    , Fornecedor.Id, Fornecedor.Endereco, Fornecedor.NomeFantasia, Fornecedor.Email, Fornecedor.CNPJ, Fornecedor.Senha), sqlConn);

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
        public string Put(int id, Fornecedor Fornecedor)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE [dbo].[FOR_Fornecedor]SET [FORNOMEFANTASIA] = '{0}',[FOREMAIL] = '{1}',[FORCNPJ] = '{2}',[ENDIDENTIFICADOR] = '{3}',[FORSENHA] = '{4}' WHERE FORIDENTIFICADOR = {5}"
                    , Fornecedor.NomeFantasia, Fornecedor.Email, Fornecedor.CNPJ, Fornecedor.Endereco, Fornecedor.Senha, id), sqlConn);

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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from FOR_Fornecedor WHERE FORIDENTIFICADOR = {0}", id), sqlConn);


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


