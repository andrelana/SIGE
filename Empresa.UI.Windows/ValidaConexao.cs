using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;

namespace Empresa.UI.Windows
{
    public partial class ValidaConexao : Form
    {
        public ValidaConexao()
        {
            InitializeComponent();
        }

        public void limpaCampos()
        {
            idTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            telefoneTextBox.Text = string.Empty;
            incluirButton.Enabled = true;
            atualizaButton.Enabled = false;
            excluirButton.Enabled = false;
        }


        private void Mensagem(string msg)
        {
            listBox1.Items.Add(msg);
        }

        private void Conecta()
        {
            var con = new SqlConnection();
            //con.ConnectionString = @"Data Source=DESKTOP-T4EL1IC;Initial Catalog=EmpresaDb;Integrated Security=True;";
            con.ConnectionString = @"Data Source=192.168.50.163;Initial Catalog=EmpresaDb;User ID=disbbyweb;Password=disbbyweb;";
            con.Open();
        }

        private void Desconecta()
        {
            var con = new SqlConnection();
            //con.ConnectionString = @"Data Source=DESKTOP-T4EL1IC;Initial Catalog=EmpresaDb;Integrated Security=True;";
            con.ConnectionString = @"Data Source=192.168.50.163;Initial Catalog=EmpresaDb;User ID=disbbyweb;Password=disbbyweb;";
            con.Close();
        }

        private void conectarButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                Conecta();
                Mensagem("Conexão aberta");
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                Desconecta();
                Mensagem("Conexão fechada");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void preencheListBox()
        {
            var con = new SqlConnection();
            con.ConnectionString = @"Data Source=192.168.50.163;Initial Catalog=EmpresaDb;User ID=disbbyweb;Password=disbbyweb;";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Nome FROM Cliente";

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (listBox1.Items.Count > 0)
            {
                listBox1.Items.Clear();
            }

            while (reader.Read())
            {
                string nome = reader["Nome"].ToString();
                listBox1.Items.Add(nome);
            }
            reader.Close();
            con.Close();
            limpaCampos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            preencheListBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = new SqlConnection();
            con.ConnectionString = @"Data Source=192.168.50.163;Initial Catalog=EmpresaDb;User ID=disbbyweb;Password=disbbyweb;";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Cliente (Id, Nome, Email, Telefone, data_inclusao) Values (@Id, @Nome, @Email, @Telefone, @data_inclusao)";
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(idTextBox.Text));
            cmd.Parameters.AddWithValue("@Nome", Convert.ToString(nomeTextBox.Text));
            cmd.Parameters.AddWithValue("@Email", Convert.ToString(emailTextBox.Text));
            cmd.Parameters.AddWithValue("@Telefone", Convert.ToString(telefoneTextBox.Text));
            cmd.Parameters.AddWithValue("@data_inclusao", Convert.ToDateTime(DateTime.Now));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            limpaCampos();
            preencheListBox();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            limpaCampos();

            incluirButton.Enabled = false;
            atualizaButton.Enabled = true;
            excluirButton.Enabled = true;

            int Id = 0;
            string Email = string.Empty;
            string Telefone = string.Empty;


            var con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-T4EL1IC;Initial Catalog=EmpresaDb;Integrated Security=True;";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "exec stp_ed_busca_cliente @Nome";
            cmd.Parameters.AddWithValue("@Nome", Convert.ToString(listBox1.SelectedItem));

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Id = Convert.ToInt32(reader["Id"]);
                Email = reader["Email"].ToString();
                Telefone = reader["Telefone"].ToString();
            }

            
            idTextBox.Text = Id.ToString();
            nomeTextBox.Text = Convert.ToString(listBox1.SelectedItem);
            emailTextBox.Text = Email;
            telefoneTextBox.Text = Telefone;

            reader.Close();
            con.Close();
        }

        private void ValidaConexao_Load(object sender, EventArgs e)
        {
            atualizaButton.Enabled = false;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            var con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-T4EL1IC;Initial Catalog=EmpresaDb;Integrated Security=True;";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete Cliente WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(idTextBox.Text));
            //cmd.Parameters.AddWithValue("@Nome", Convert.ToString(nomeTextBox.Text));
            //cmd.Parameters.AddWithValue("@Email", Convert.ToString(emailTextBox.Text));
            //cmd.Parameters.AddWithValue("@Telefone", Convert.ToString(telefoneTextBox.Text));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            limpaCampos();
            preencheListBox();

        }

        private void atualizaButton_Click(object sender, EventArgs e)
        {
            var con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-T4EL1IC;Initial Catalog=EmpresaDb;Integrated Security=True;";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Cliente set Nome = @Nome, Email = @Email, Telefone = @Telefone WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(idTextBox.Text));
            cmd.Parameters.AddWithValue("@Nome", Convert.ToString(nomeTextBox.Text));
            cmd.Parameters.AddWithValue("@Email", Convert.ToString(emailTextBox.Text));
            cmd.Parameters.AddWithValue("@Telefone", Convert.ToString(telefoneTextBox.Text));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            limpaCampos();
            preencheListBox();

        }
    }
}
