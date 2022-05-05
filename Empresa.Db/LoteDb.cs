using Empresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Db
{
    public class LoteDb
    {
        public List<Lote> ListarLote()
        {
            string sql = "SELECT * FROM Lote";
            var con = new SqlConnection(Db.Conexao);
            var cmd = new SqlCommand(sql, con);

            List<Lote> listaLote = new List<Lote>();

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var lote = new Lote();
                lote.cod_lote = Convert.ToInt32(reader["cod_lote"]);
                lote.lote = reader["lote"].ToString();
                lote.cod_produto= Convert.ToInt32(reader["cod_produto"]);
                lote.validade = Convert.ToDateTime(reader["validade"]);
                lote.cod_barra = reader["cod_barra"].ToString();

                listaLote.Add(lote);
            }
            reader.Close();
            con.Close();
            return listaLote;
        }
    }
}
