using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace ProjetoAgendamento.Database
{
    internal class Conexao
    {
        private static string _connectionString =
           "Host=localhost; Database=agendamento_salas; Username=postgres; Password=212227";

        public static NpgsqlConnection ObterConexao()
        {
            var conexao = new NpgsqlConnection(_connectionString);
            conexao.Open();
            return conexao;
        }


    }
}
