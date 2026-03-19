using System;
using System.Windows.Forms;
using Npgsql;
using ProjetoAgendamento.Database;

namespace ProjetoAgendamento.Forms
{
    public partial class FormSala : Form
    {
        public FormSala()
        {
            InitializeComponent();
            CarregarSalas();
        }

        private void CarregarSalas()
        {
            using var conn = Conexao.ObterConexao();
            using var cmd = new NpgsqlCommand("SELECT id, nome FROM sala ORDER BY nome", conn);
            using var reader = cmd.ExecuteReader();

            var tabela = new System.Data.DataTable();
            tabela.Load(reader);
            dgvSalas.DataSource = tabela;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite o nome da sala.");
                return;
            }

            using var conn = Conexao.ObterConexao();
            using var cmd = new NpgsqlCommand("INSERT INTO sala (nome) VALUES (@nome)", conn);
            cmd.Parameters.AddWithValue("nome", txtNome.Text.Trim());
            cmd.ExecuteNonQuery();

            txtNome.Clear();
            CarregarSalas();
            MessageBox.Show("Sala salva com sucesso!");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvSalas.CurrentRow == null) return;

            var id = (int)dgvSalas.CurrentRow.Cells["id"].Value;

            if (MessageBox.Show("Deseja excluir esta sala?", "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = Conexao.ObterConexao();
                using var cmd = new NpgsqlCommand("DELETE FROM sala WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                CarregarSalas();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvSalas.CurrentRow == null) return;

            var id = (int)dgvSalas.CurrentRow.Cells["id"].Value;
            var novoNome = txtNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(novoNome))
            {
                MessageBox.Show("Digite o novo nome da sala.");
                return;
            }

            using var conn = Conexao.ObterConexao();
            using var cmd = new NpgsqlCommand("UPDATE sala SET nome = @nome WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("nome", novoNome);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            txtNome.Clear();
            CarregarSalas();
            MessageBox.Show("Sala atualizada com sucesso!");
        }

        private void lblNome_Click(object sender, EventArgs e)
        {

        }

        private void FormSala_Load(object sender, EventArgs e)
        {

        }
    }
}