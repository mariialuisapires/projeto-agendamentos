namespace ProjetoAgendamento.Forms
{
    partial class FormSala
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
            lblNome = new Label();
            txtNome = new TextBox();
            btnSalvar = new Button();
            dgvSalas = new DataGridView();
            btnEditar = new Button();
            btnExcluir = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSalas).BeginInit();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.Location = new Point(12, 15);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(100, 23);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome:";

            // 
            // txtNome
            // 
            txtNome.Location = new Point(128, 12);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(200, 27);
            txtNome.TabIndex = 0;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(369, 12);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(94, 29);
            btnSalvar.TabIndex = 1;
            btnSalvar.Text = "Salvar";
            btnSalvar.Click += button1_Click;
            // 
            // dgvSalas
            // 
            dgvSalas.ColumnHeadersHeight = 29;
            dgvSalas.Location = new Point(12, 55);
            dgvSalas.Name = "dgvSalas";
            dgvSalas.RowHeadersWidth = 51;
            dgvSalas.Size = new Size(560, 200);
            dgvSalas.TabIndex = 2;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(12, 288);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Location = new Point(141, 288);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new Size(94, 29);
            btnExcluir.TabIndex = 4;
            btnExcluir.Text = "Excluir";
            btnExcluir.Click += btnExcluir_Click;
            // 
            // FormSala
            // 
            ClientSize = new Size(816, 652);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(btnSalvar);
            Controls.Add(dgvSalas);
            Controls.Add(btnEditar);
            Controls.Add(btnExcluir);
            Name = "FormSala";
            Text = "FormSala";
            ((System.ComponentModel.ISupportInitialize)dgvSalas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.DataGridView dgvSalas;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
    }
}