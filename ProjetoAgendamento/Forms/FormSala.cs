using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using ProjetoAgendamento.Database;

namespace ProjetoAgendamento.Forms
{
    public partial class FormSala : Form
    {
        private static readonly Color CorFundo = Color.FromArgb(18, 18, 18);
        private static readonly Color CorPainel = Color.FromArgb(30, 30, 30);
        private static readonly Color CorTexto = Color.FromArgb(220, 220, 220);
        private static readonly Color CorDestaque = Color.FromArgb(49, 130, 206);
        private static readonly Color CorSucesso = Color.FromArgb(56, 161, 105);
        private static readonly Color CorPerigo = Color.FromArgb(229, 62, 62);
        private static readonly Color CorInput = Color.FromArgb(45, 45, 45);

        public FormSala()
        {
            InitializeComponent();
            AplicarTema();
            CarregarSalas();
        }

        private void AplicarTema()
        {
            this.BackColor = CorFundo;
            this.ForeColor = CorTexto;
            this.Text = "Gerenciar Salas";
            this.Font = new Font("Segoe UI", 10);
            this.Size = new Size(650, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            
            lblNome.ForeColor = CorTexto;
            lblNome.Font = new Font("Segoe UI", 10);

           
            txtNome.BackColor = CorInput;
            txtNome.ForeColor = CorTexto;
            txtNome.BorderStyle = BorderStyle.FixedSingle;
            txtNome.Font = new Font("Segoe UI", 10);

         
            EstilizarBotao(btnSalvar, CorDestaque);
            EstilizarBotao(btnEditar, CorSucesso);
            EstilizarBotao(btnExcluir, CorPerigo);
            EstilizarBotao(btnVoltarMenu, CorDestaque);

           
            EstilizarGrid(dgvSalas);
        }

        private void EstilizarBotao(Button btn, Color cor)
        {
            btn.BackColor = cor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = 35;
        }

        private void EstilizarGrid(DataGridView grid)
        {
            grid.BackgroundColor = CorPainel;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Color.FromArgb(50, 50, 50);
            grid.DefaultCellStyle.BackColor = CorPainel;
            grid.DefaultCellStyle.ForeColor = CorTexto;
            grid.DefaultCellStyle.SelectionBackColor = CorDestaque;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 40);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = CorDestaque;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 40);
            foreach (DataGridViewColumn col in grid.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void CarregarSalas()
        {
            using var conn = Conexao.ObterConexao();
            using var cmd = new NpgsqlCommand("SELECT id, nome FROM sala ORDER BY nome", conn);
            using var reader = cmd.ExecuteReader();

            var tabela = new System.Data.DataTable();
            tabela.Load(reader);
            dgvSalas.DataSource = tabela;
            dgvSalas.Columns["id"].HeaderText = "Identificador";
            dgvSalas.Columns["nome"].HeaderText = "Nome";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MostrarErro("Digite o nome da sala.");
                return;
            }

            try
            {
                using var conn = Conexao.ObterConexao();
                using var cmd = new NpgsqlCommand("INSERT INTO sala (nome) VALUES (@nome)", conn);
                cmd.Parameters.AddWithValue("nome", txtNome.Text.Trim());
                cmd.ExecuteNonQuery();
                txtNome.Clear();
                CarregarSalas();
                MostrarSucesso("Sala salva com sucesso!");
            }
            catch (Exception ex)
            {
                MostrarErro("Erro: " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvSalas.CurrentRow == null) return;

            var id = (int)dgvSalas.CurrentRow.Cells["id"].Value;

            if (MessageBox.Show("Deseja excluir esta sala?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using var conn = Conexao.ObterConexao();
                    using var cmd = new NpgsqlCommand("DELETE FROM sala WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    CarregarSalas();
                    MostrarSucesso("Sala excluída com sucesso!");
                }
                catch (Exception ex)
                {
                    MostrarErro("Erro: " + ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvSalas.CurrentRow == null) return;

            var id = (int)dgvSalas.CurrentRow.Cells["id"].Value;
            var novoNome = txtNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(novoNome))
            {
                MostrarErro("Digite o novo nome da sala.");
                return;
            }

            try
            {
                using var conn = Conexao.ObterConexao();
                using var cmd = new NpgsqlCommand("UPDATE sala SET nome = @nome WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("nome", novoNome);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                txtNome.Clear();
                CarregarSalas();
                MostrarSucesso("Sala atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                MostrarErro("Erro: " + ex.Message);
            }
        }

        private void btnVoltarMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MostrarSucesso(string msg) =>
            MessageBox.Show(msg, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void MostrarErro(string msg) =>
            MessageBox.Show(msg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}