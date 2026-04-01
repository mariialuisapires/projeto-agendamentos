namespace ProjetoAgendamento.Forms
{
    partial class FormMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            btnSalas = new Button();
            btnAgendamentos = new Button();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = false;
            lblTitulo.Location = new Point(0, 60);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(420, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema de Agendamento";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // lblSubtitulo
            lblSubtitulo.AutoSize = false;
            lblSubtitulo.Location = new Point(0, 105);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(420, 30);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Selecione uma opção para continuar";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;

            // btnSalas
            btnSalas.Location = new Point(75, 170);
            btnSalas.Name = "btnSalas";
            btnSalas.Size = new Size(270, 55);
            btnSalas.TabIndex = 2;
            btnSalas.Text = "Gerenciar Salas";
            btnSalas.Click += btnSalas_Click;

            // btnAgendamentos
            btnAgendamentos.Location = new Point(75, 245);
            btnAgendamentos.Name = "btnAgendamentos";
            btnAgendamentos.Size = new Size(270, 55);
            btnAgendamentos.TabIndex = 3;
            btnAgendamentos.Text = "Gerenciar Agendamentos";
            btnAgendamentos.Click += btnAgendamentos_Click;

            // FormMenu
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 360);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(btnSalas);
            Controls.Add(btnAgendamentos);
            Name = "FormMenu";
            Text = "Menu Principal";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Button btnSalas;
        private System.Windows.Forms.Button btnAgendamentos;
    }
}
