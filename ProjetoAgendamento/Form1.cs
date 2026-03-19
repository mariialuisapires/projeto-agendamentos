using System;
using System.Windows.Forms;
using ProjetoAgendamento.Forms;

namespace ProjetoAgendamento
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var formSala = new FormSala();
            formSala.Show();
            var formAgendamento = new FormAgendamento();
            formAgendamento.Show();
        }
    }
}