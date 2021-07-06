using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Empresa.Models;

namespace Empresa.Db
{
    public class ClienteDb
    {
        public void incluir(Cliente cliente)
        {
            var con = new SqlConnection(Db.Conexao);

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Cliente (Id, Nome, Email, Telefone) Values (@Id, @Nome, @Email, @Telefone)";
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
            //cmd.CommandText = "UPDATE Cliente set Nome = @Nome,Email = @Email, Telefone = @Telefone WHERE Id = @Id";
            cmd.CommandText = "exec stp_ed_altera_cliente @Id, @Nome, @Email, @Telefone";
            cmd.Parameters.AddWithValue("@Id", Convert.ToString(cliente.Id));
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void excluir(Cliente cliente)
        {
            var con = new SqlConnection(Db.Conexao);

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE Cliente WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", Convert.ToString(cliente.Id));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Cliente> Listar()
        {
            string sql = "SELECT Id, Nome, Telefone, Email FROM Cliente";
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
        
    }
}
