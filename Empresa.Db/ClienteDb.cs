using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Empresa.Models;
using System.Data;

namespace Empresa.Db
{
    public class ClienteDb
    {
        public void incluir(Cliente cliente)
        {
            var con = new SqlConnection(Db.Conexao);

            var cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = "INSERT INTO Cliente (Id, Nome, Email, Telefone) Values (@Id, @Nome, @Email, @Telefone)";
            cmd.CommandText = "exec stp_ed_inclui_cliente @Id, @Nome, @Email, @Telefone";
            cmd.Parameters.AddWithValue("@Id", Convert.ToString(cliente.Id));
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void alterar(Cliente cliente)
        {
            var con = new SqlConnection(Db.Conexao);

            var cmd = new SqlCommand();
            cmd.Connection = con;
            string proc = "exec stp_ed_altera_cliente ";
            proc += Convert.ToString(cliente.Id) + ", ";
            proc += "'" + cliente.Nome + "', ";
            proc += "'" + cliente.Email + "', ";
            proc += "'" + cliente.Telefone + "'";
            //cmd.CommandText = "UPDATE Cliente set Nome = @Nome,Email = @Email, Telefone = @Telefone WHERE Id = @Id";
            cmd.CommandText = proc;
            //cmd.Parameters.AddWithValue("@Id", Convert.ToString(cliente.Id));
            //cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            //cmd.Parameters.AddWithValue("@Email", cliente.Email);
            //cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void excluir(Cliente cliente)
        {
            var con = new SqlConnection(Db.Conexao);

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE Entidade WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", Convert.ToString(cliente.Id));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Cliente> Listar()
        {
            string sql = "SELECT Id, Nome, Telefone, Email FROM Entidade";
            var con = new SqlConnection(Db.Conexao);
            var cmd = new SqlCommand(sql, con);

            List<Cliente> lista = new List<Cliente>();

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var cliente = new Cliente();
                cliente.Id = Convert.ToInt32(reader["Id"]);
                cliente.Nome = reader["Nome"].ToString();
                cliente.Telefone = reader["Telefone"].ToString();
                cliente.Email = reader["Email"].ToString();

                lista.Add(cliente);
            }
            reader.Close();
            con.Close();
            return lista;
        }

        public Cliente ObterPorId(int Id)
        
        {
            string sql = "exec stp_ed_busca_cliente null, @Id";
            var con = new SqlConnection(Db.Conexao);
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", Id);

            Cliente cliente = null;

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                cliente = new Cliente();
                cliente.Id = Convert.ToInt32(reader["Id"]);
                cliente.Nome = reader["Nome"].ToString();
                cliente.Telefone = reader["Telefone"].ToString();
                cliente.Email = reader["Email"].ToString();
            }
            reader.Close();
            con.Close();
            return cliente;
        }
        public int ValidaClienteUnico(Cliente cliente)
        {
            string sql = "exec stp_ed_valida_cliente_unico @Id, @Nome, @Email, @Telefone";
            var con = new SqlConnection(Db.Conexao);
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            int result = 0;
            if (reader.Read())
            {
                result = Convert.ToInt32(reader["Result"]);
            }
                
            reader.Close();
            con.Close();
           
            return result;
        }
        
    }
}
