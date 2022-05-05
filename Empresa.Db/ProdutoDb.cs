using Empresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Db
{
    public class ProdutoDb
    {
        public List<Produto> Listar()
        {
            string sql = "SELECT cod_produto, descricao, cod_situacao FROM Produto";
            var con = new SqlConnection(Db.Conexao);
            var cmd = new SqlCommand(sql, con);

            List<Produto> lista = new List<Produto>();

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var produto = new Produto();
                produto.cod_produto = Convert.ToInt32(reader["cod_produto"]);
                produto.descricao = reader["descricao"].ToString();
                produto.cod_situacao = Convert.ToInt32(reader["cod_situacao"]);

                lista.Add(produto);
            }
            reader.Close();
            con.Close();
            return lista;
        }
        public void IncluirProduto(Produto produto)
        {
            var con = new SqlConnection(Db.Conexao);
            var cmd = new SqlCommand();
            string Execute = "exec stp_ed_inclui_produto ";
            Execute += "'" + produto.descricao + "',";
            Execute += "'" + produto.cod_situacao + "'";
            cmd.Connection = con;
            cmd.CommandText = Execute;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
