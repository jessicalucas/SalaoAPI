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
    public class UsuarioController : Controller
    {
        //public string connectionString = "Server=tcp:salaoapi.database.windows.net;Database=SalaoAPI;User ID =api@salaoapi.database.windows.net;Password=Salao123@;Trusted_Connection=False;Encrypt=True;";
        public string connectionString = "Server=JESSICA\\SQLEXPRESS;Database=Salao;Integrated Security=yes;Uid=auth_windows;";

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from USU_Usuario", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();

                List<Usuario> result = new List<Usuario>();

                while (dr.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        Id = (int)dr["USUIDENTIFICADOR"],
                        Nome = (string)dr["USUNOME"],
                        CPF = (string)dr["USUCPF"],
                        DataNascimento = (DateTime)dr["USUDATANASCIMENTO"],
                        Sexo = (string)dr["USUSEXO"],
                        Email = (string)dr["USUEMAIL"],
                        Senha = (string)dr["USUSENHA"]

                    };
                    result.Add(usuario);

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
        public ActionResult<Usuario> Get(string id,string email, string senha)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("Select * from USU_Usuario where USUEMAIL = '" + email + "' and USUSENHA = '" + senha + "'", sqlConn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        Id = (int)dr["USUIDENTIFICADOR"],
                        Nome = (string)dr["USUNOME"],
                        CPF = (string)dr["USUCPF"],
                        DataNascimento = (DateTime)dr["USUDATANASCIMENTO"],
                        Sexo = (string)dr["USUSEXO"],
                        Email = (string)dr["USUEMAIL"],
                        Senha = (string)dr["USUSENHA"]
                    };
                    sqlConn.Close();
                    return usuario;
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
        [Route("usuario: Usuario")]
        public string Post(Usuario usuario)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[USU_Usuario]([USUIDENTIFICADOR],[USUNOME],[USUCPF],[USUDATANASCIMENTO],[USUSEXO],[USUEMAIL],[USUSENHA]) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}') "
                    , usuario.Id, usuario.Nome, usuario.CPF, usuario.DataNascimento, usuario.Sexo, usuario.Email, usuario.Senha), sqlConn);

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
        public string Put(int id, Usuario usuario)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);

                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(string.Format("UPDATE [dbo].[USU_Usuario]SET [USUNOME] = '{0}',[USUCPF] = '{1}',[USUDATANASCIMENTO] = '{2}',[USUSEXO] = '{3}',[USUEMAIL] = '{4}',[USUSENHA] = '{5}' WHERE ENDIDENTIFICADOR = {6}"
                    ,usuario.Nome, usuario.CPF, usuario.DataNascimento, usuario.Sexo, usuario.Email, usuario.Senha, id), sqlConn);


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

                SqlCommand cmd = new SqlCommand(string.Format("Delete from USU_Usuario WHERE USUIDENTIFICADOR = {0}", id), sqlConn);


                int result = cmd.ExecuteNonQuery();


                if (result == 0)
                    return "{\"respServidor\" : \"Houve um erro ao excluir o usuario.\"}";

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
