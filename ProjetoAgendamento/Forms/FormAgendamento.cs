using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using ProjetoAgendamento.Database;

namespace ProjetoAgendamento.Forms
{
    public partial class FormAgendamento : Form
    {
        private static readonly Color CorFundo = Color.FromArgb(18, 18, 18);
        private static readonly Color CorPainel = Color.FromArgb(30, 30, 30);
        private static readonly Color CorTexto = Color.FromArgb(220, 220, 220);
        private static readonly Color CorDestaque = Color.FromArgb(49, 130, 206);
        private static readonly Color CorSucesso = Color.FromArgb(56, 161, 105);
        private static readonly Color CorPerigo = Color.FromArgb(229, 62, 62);
        private static readonly Color CorInput = Color.FromArgb(45, 45, 45);

        public FormAgendamento()
        {
            InitializeComponent();
            AplicarTema();
            CarregarSalas();
            CarregarAgendamentos();
        }

        private void AplicarTema()
        {
            this.BackColor = CorFundo;
            this.ForeColor = CorTexto;
            this.Text = "Gerenciar Agendamentos";
            this.Font = new Font("Segoe UI", 10);
            this.Size = new Size(650, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblSala.ForeColor = CorTexto;
            lblInicio.ForeColor = CorTexto;
            lblFim.ForeColor = CorTexto;

            cmbSala.BackColor = CorInput;
            cmbSala.ForeColor = CorTexto;
            cmbSala.FlatStyle = FlatStyle.Flat;

            dtpInicio.BackColor = CorInput;
            dtpInicio.ForeColor = CorTexto;
            dtpFim.BackColor = CorInput;
            dtpFim.ForeColor = CorTexto;

            EstilizarBotao(btnSalvar, CorDestaque);
            EstilizarBotao(btnEditar, CorSucesso);
            EstilizarBotao(btnExcluir, CorPerigo);
            EstilizarBotao(btnVoltarMenu, CorDestaque);

            EstilizarGrid(dgvAgendamentos);
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
            grid.ColumnHeadersDefaultCellStyle.ForeColor = CorDestaque;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 40);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
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

            cmbSala.DataSource = tabela;
            cmbSala.DisplayMember = "nome";
            cmbSala.ValueMember = "id";
        }

        private void CarregarAgendamentos()
        {
            using var conn = Conexao.ObterConexao();
            using var cmd = new NpgsqlCommand(@"
                SELECT a.id, s.nome AS sala, a.data_inicio, a.data_fim 
                FROM agendamento a
                JOIN sala s ON s.id = a.id_sala
                ORDER BY a.data_inicio", conn);
            using var reader = cmd.ExecuteReader();

            var tabela = new System.Data.DataTable();
            tabela.Load(reader);
            dgvAgendamentos.DataSource = tabela;

            if (dgvAgendamentos.Columns.Count > 0)
            {
                dgvAgendamentos.Columns["id"].Visible = false;
                dgvAgendamentos.Columns["sala"].HeaderText = "Sala";
                dgvAgendamentos.Columns["data_inicio"].HeaderText = "Início";
                dgvAgendamentos.Columns["data_fim"].HeaderText = "Fim";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbSala.SelectedItem == null)
            {
                MostrarErro("Selecione uma sala.");
                return;
            }

            try
            {
                var idSala = (int)((System.Data.DataRowView)cmbSala.SelectedItem)["id"];
                using var conn = Conexao.ObterConexao();
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO agendamento (id_sala, data_inicio, data_fim) VALUES (@id_sala, @inicio, @fim)", conn);
                cmd.Parameters.AddWithValue("id_sala", idSala);
                cmd.Parameters.AddWithValue("inicio", dtpInicio.Value);
                cmd.Parameters.AddWithValue("fim", dtpFim.Value);
                cmd.ExecuteNonQuery();

                CarregarAgendamentos();
                MostrarSucesso("Agendamento salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MostrarErro("Erro: " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvAgendamentos.CurrentRow == null) return;

            var id = (int)dgvAgendamentos.CurrentRow.Cells["id"].Value;

            if (MessageBox.Show("Deseja excluir este agendamento?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using var conn = Conexao.ObterConexao();
                    using var cmd = new NpgsqlCommand("DELETE FROM agendamento WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    CarregarAgendamentos();
                    MostrarSucesso("Agendamento excluído com sucesso!");
                }
                catch (Exception ex)
                {
                    MostrarErro("Erro: " + ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAgendamentos.CurrentRow == null) return;
            if (cmbSala.SelectedItem == null)
            {
                MostrarErro("Selecione uma sala.");
                return;
            }

            try
            {
                var id = (int)dgvAgendamentos.CurrentRow.Cells["id"].Value;
                var idSala = (int)((System.Data.DataRowView)cmbSala.SelectedItem)["id"];

                using var conn = Conexao.ObterConexao();
                using var cmd = new NpgsqlCommand(
                    "UPDATE agendamento SET id_sala = @id_sala, data_inicio = @inicio, data_fim = @fim WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id_sala", idSala);
                cmd.Parameters.AddWithValue("inicio", dtpInicio.Value);
                cmd.Parameters.AddWithValue("fim", dtpFim.Value);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();

                CarregarAgendamentos();
                MostrarSucesso("Agendamento atualizado com sucesso!");
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