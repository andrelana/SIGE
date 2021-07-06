using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empresa.UI.Windows
{
    public partial class PrincipalForm : Form
    {
        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void novoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClienteIncluirForm cliinc = new ClienteIncluirForm();
            cliinc.Show();
        }

        private void validarConexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidaConexao valida = new ValidaConexao();
            valida.Show();
        }

        private void manipulaçãoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManipulaBanco mb = new ManipulaBanco();
            mb.Show();
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClientesForm cf = new ClientesForm();
            cf.Show();
        }
    }
}
