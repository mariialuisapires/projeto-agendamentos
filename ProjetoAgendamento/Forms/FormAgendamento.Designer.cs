namespace ProjetoAgendamento.Forms
{
    partial class FormAgendamento
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
            lblSala = new Label();
            cmbSala = new ComboBox();
            lblInicio = new Label();
            dtpInicio = new DateTimePicker();
            lblFim = new Label();
            dtpFim = new DateTimePicker();
            btnSalvar = new Button();
            dgvAgendamentos = new DataGridView();
            btnEditar = new Button();
            btnExcluir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).BeginInit();
            SuspendLayout();
             
            // lblSala
             
            lblSala.Location = new Point(12, 15);
            lblSala.Name = "lblSala";
            lblSala.Size = new Size(60, 23);
            lblSala.TabIndex = 0;
            lblSala.Text = "Sala:";
             
            // cmbSala
             
            cmbSala.Location = new Point(120, 12);
            cmbSala.Name = "cmbSala";
            cmbSala.Size = new Size(200, 28);
            cmbSala.TabIndex = 0;
             
            // lblInicio
             
            lblInicio.Location = new Point(12, 55);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(60, 23);
            lblInicio.TabIndex = 1;
            lblInicio.Text = "Início:";
             
            // dtpInicio
             
            dtpInicio.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.Location = new Point(120, 52);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(200, 27);
            dtpInicio.TabIndex = 1;
             
            // lblFim
             
            lblFim.Location = new Point(12, 95);
            lblFim.Name = "lblFim";
            lblFim.Size = new Size(60, 23);
            lblFim.TabIndex = 2;
            lblFim.Text = "Fim:";
             
            // dtpFim
             
            dtpFim.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFim.Format = DateTimePickerFormat.Custom;
            dtpFim.Location = new Point(120, 92);
            dtpFim.Name = "dtpFim";
            dtpFim.Size = new Size(200, 27);
            dtpFim.TabIndex = 2;
             
            // btnSalvar
             
            btnSalvar.Location = new Point(120, 130);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(94, 29);
            btnSalvar.TabIndex = 3;
            btnSalvar.Text = "Salvar";
            btnSalvar.Click += btnSalvar_Click;
             
            // dgvAgendamentos
             
            dgvAgendamentos.ColumnHeadersHeight = 29;
            dgvAgendamentos.Location = new Point(12, 175);
            dgvAgendamentos.Name = "dgvAgendamentos";
            dgvAgendamentos.RowHeadersWidth = 51;
            dgvAgendamentos.Size = new Size(560, 200);
            dgvAgendamentos.TabIndex = 4;
             
            // btnEditar
             
            btnEditar.Location = new Point(300, 390);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            
            // btnExcluir
             
            btnExcluir.Location = new Point(420, 390);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(94, 29);
            btnExcluir.TabIndex = 6;
            btnExcluir.Text = "Excluir";
            btnExcluir.Click += btnExcluir_Click;
             
            // FormAgendamento
             
            ClientSize = new Size(674, 524);
            Controls.Add(lblSala);
            Controls.Add(cmbSala);
            Controls.Add(lblInicio);
            Controls.Add(dtpInicio);
            Controls.Add(lblFim);
            Controls.Add(dtpFim);
            Controls.Add(btnSalvar);
            Controls.Add(dgvAgendamentos);
            Controls.Add(btnEditar);
            Controls.Add(btnExcluir);
            Name = "FormAgendamento";
            Text = "FormAgendamento";
            ((System.ComponentModel.ISupportInitialize)dgvAgendamentos).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblSala;
        private System.Windows.Forms.ComboBox cmbSala;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblFim;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.DataGridView dgvAgendamentos;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
    }
}