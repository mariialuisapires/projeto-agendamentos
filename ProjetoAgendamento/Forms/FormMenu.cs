using System;
using System.Drawing;
using System.Windows.Forms;
using ProjetoAgendamento.Forms;

namespace ProjetoAgendamento.Forms
{
    public partial class FormMenu : Form
    {
        private static readonly Color CorFundo = Color.FromArgb(18, 18, 18);
        private static readonly Color CorTexto = Color.FromArgb(220, 220, 220);
        private static readonly Color CorSubtexto = Color.FromArgb(140, 140, 140);
        private static readonly Color CorDestaque = Color.FromArgb(49, 130, 206);
        private static readonly Color CorSucesso = Color.FromArgb(56, 161, 105);

        public FormMenu()
        {
            InitializeComponent();
            AplicarTema();
        }

        private void AplicarTema()
        {
            this.BackColor = CorFundo;
            this.ForeColor = CorTexto;
            this.Font = new Font("Segoe UI", 10);

            lblTitulo.ForeColor = CorDestaque;
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.BackColor = Color.Transparent;

            lblSubtitulo.ForeColor = CorSubtexto;
            lblSubtitulo.Font = new Font("Segoe UI", 10);
            lblSubtitulo.BackColor = Color.Transparent;

            EstilizarBotao(btnSalas, CorDestaque);
            EstilizarBotao(btnAgendamentos, CorSucesso);
        }

        private void EstilizarBotao(Button btn, Color cor)
        {
            btn.BackColor = cor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void btnSalas_Click(object sender, EventArgs e)
        {
            using var form = new FormSala();
            form.ShowDialog(this);
        }

        private void btnAgendamentos_Click(object sender, EventArgs e)
        {
            using var form = new FormAgendamento();
            form.ShowDialog(this);
        }
    }
}
