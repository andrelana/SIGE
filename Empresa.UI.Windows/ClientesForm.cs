using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Empresa.Db;
using Empresa.Models;

namespace Empresa.UI.Windows
{
    public partial class ClientesForm : Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }

        private void ExibirGrid()
        {
            listaDataGridView.Visible = true;
            listaDataGridView.Dock = DockStyle.Fill;
            fichaPainel.Visible = false;
            novoButton.Visible = true;
            alterarButton.Visible = true;
            excluirButton.Visible = true;
            sairButton.Visible = true;
            gravarButton.Visible = false;
            alterarConfButton.Visible = false;
            confExcButton.Visible = false;
            voltarButton.Visible = false;

            var db = new ClienteDb();
            listaDataGridView.DataSource = db.Listar();
            listaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listaDataGridView.ReadOnly = false;
            listaDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listaDataGridView.RowHeadersVisible = false;
            listaDataGridView.EnableHeadersVisualStyles = false;
            listaDataGridView.Columns["Id"].Width = 50;
        }

        private void ExibirFicha()
        {
            fichaPainel.Visible = true;
            fichaPainel.Dock = DockStyle.Fill;
            listaDataGridView.Visible = false;

            novoButton.Visible = false;
            alterarButton.Visible = false;
            excluirButton.Visible = false;
            sairButton.Visible = false;
            gravarButton.Visible = true;
            alterarConfButton.Visible = false;
            confExcButton.Visible = false;
            voltarButton.Visible = true;
        }

        public void LimparFicha()
        {
            IdTextBox.Clear();
            nomeTextBox.Clear();
            emailTextBox.Clear();
            telefoneTextBox.Clear();
            IdTextBox.Focus();
        }

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            ExibirGrid();
        }

        private void novoButton_Click(object sender, EventArgs e)
        {
            LimparFicha();
            ExibirFicha();
        }

        private void voltarButton_Click(object sender, EventArgs e)
        {
            ExibirGrid();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente();
            cliente.Id = Convert.ToInt32(IdTextBox.Text);
            cliente.Nome = nomeTextBox.Text;
            cliente.Email = emailTextBox.Text;
            cliente.Telefone = telefoneTextBox.Text;

            var db = new ClienteDb();
            db.incluir(cliente);

            LimparFicha();
            ExibirGrid();
        }

        private void alterarButton_Click(object sender, EventArgs e)
        {
            Cliente cliente = (Cliente) listaDataGridView.CurrentRow.DataBoundItem;
            IdTextBox.Text = cliente.Id.ToString();
            nomeTextBox.Text = cliente.Nome;
            emailTextBox.Text = cliente.Email;
            telefoneTextBox.Text = cliente.Telefone;

            ExibirFicha();

            alterarConfButton.Visible = true;
            gravarButton.Visible = false;
        }

        private void alterarConfButton_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente();
            cliente.Id = Convert.ToInt32(IdTextBox.Text);
            cliente.Nome = nomeTextBox.Text;
            cliente.Email = emailTextBox.Text;
            cliente.Telefone = telefoneTextBox.Text;

            var db = new ClienteDb();
            db.alterar(cliente);

            ExibirGrid();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            Cliente cliente = (Cliente)listaDataGridView.CurrentRow.DataBoundItem;
            IdTextBox.Text = cliente.Id.ToString();
            nomeTextBox.Text = cliente.Nome;
            emailTextBox.Text = cliente.Email;
            telefoneTextBox.Text = cliente.Telefone;

            ExibirFicha();

            confExcButton.Visible = true;
            gravarButton.Visible = false;
        }

        private void confExcButton_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente();
            cliente.Id = Convert.ToInt32(IdTextBox.Text);
            cliente.Nome = nomeTextBox.Text;
            cliente.Email = emailTextBox.Text;
            cliente.Telefone = telefoneTextBox.Text;

            var db = new ClienteDb();
            db.excluir(cliente);

            ExibirGrid();
        }

        private void sairButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
