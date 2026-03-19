using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoAgendamento.Models
{
    internal class Agendamento
    {
        public int Id { get; set; }
        public int IdSala { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
