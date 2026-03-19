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
        public FormAgendamento()
        {
            InitializeComponent();
            CarregarSalas();
            CarregarAgendamentos();
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
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cmbSala.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma sala.");
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
                MessageBox.Show("Agendamento salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvAgendamentos.CurrentRow == null) return;

            var id = (int)dgvAgendamentos.CurrentRow.Cells["id"].Value;

            if (MessageBox.Show("Deseja excluir este agendamento?", "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = Conexao.ObterConexao();
                using var cmd = new NpgsqlCommand("DELETE FROM agendamento WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                CarregarAgendamentos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAgendamentos.CurrentRow == null) return;
            if (cmbSala.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma sala.");
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
                MessageBox.Show("Agendamento atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void cmbSala_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormAgendamento_Load(object sender, EventArgs e)
        {

        }
    }
}
