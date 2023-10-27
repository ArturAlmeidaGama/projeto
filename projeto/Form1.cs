using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace projeto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarDadosBanco();
        }

        private void CarregarDadosBanco()
        {
            string conexao = "server=localhost;database=projeto2;uid=root;pwd=";
            MySqlConnection conexaoMYSQL = new MySqlConnection(conexao);
            conexaoMYSQL.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from aluno", conexaoMYSQL);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvAluno.DataSource = dt;
        }

        private void dgvAluno_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtId.Text = dgvAluno.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNome.Text = dgvAluno.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDs.Text = dgvAluno.Rows[e.RowIndex].Cells[2].Value.ToString();
            pnlEspaco.Visible = true;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtDs.Text == "")
            {
                MessageBox.Show("Todos os campos devem ser preenchidos");
            }
            else if (int.TryParse(txtNome.Text, out _) == true || float.TryParse(txtNome.Text, out _) == true || float.TryParse(txtDs.Text, out _) == true)
            {
                MessageBox.Show("Um ou mais campo estão incorretos!");
            }
            else
            {
                string conexao = "server=localhost;database=projeto2;uid=root;pwd=";
                MySqlConnection conexaoMYSQL = new MySqlConnection(conexao);
                conexaoMYSQL.Open();
                MySqlCommand comando = new MySqlCommand("update aluno set nome='" + txtNome.Text + "', ds='" + txtDs.Text + "' where id=" + txtId.Text, conexaoMYSQL);
                comando.ExecuteNonQuery();
                MessageBox.Show("Dados alterados!!!");
                txtNome.Text = "";
                txtDs.Text = "";
                txtId.Text = "";
                pnlEspaco.Visible = false;
                CarregarDadosBanco();
            }
        }
    }
}