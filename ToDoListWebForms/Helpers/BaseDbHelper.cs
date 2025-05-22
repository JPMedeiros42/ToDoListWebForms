using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ToDoListWebForms.Helpers
{
    public class BaseDbHelper : IDisposable
    {
        protected SqlConnection _conexao = null;

        protected SqlCommand _cmmd = null;

        private bool _disposed = false;

        public BaseDbHelper()
        {
            SetConn();
        }

        protected void SetConn()
        {
            if (_conexao == null)
            {
                string connString = ConfigurationManager.ConnectionStrings["ToDoListWFDB"].ConnectionString;
                _conexao = new SqlConnection(connString);
            }
        }

        public void SetCommand(string nomeProcedure)
        {
            if (string.IsNullOrWhiteSpace(nomeProcedure))
            {
                throw new ArgumentException("Nome da procedure não pode ser vazio ou nulo.", nameof(nomeProcedure));
            }

            _cmmd = new SqlCommand(nomeProcedure, _conexao)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

        }

        public void AbreConexao()
        {
            if (_conexao.State == ConnectionState.Closed)
            {
                _conexao.Open();
            }
        }

        public void FechaConexao()
        {
            if (_conexao.State == ConnectionState.Open)
            {
                _conexao.Close();
            }
        }

        public void DescartaComando()
        {
            if (_cmmd != null)
            {
                _cmmd.Dispose();
            }
        }

        public void DescartaConexao()
        {
            if (_conexao != null)
            {
                _conexao.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cmmd?.Dispose();

                    FechaConexao();
                    _conexao?.Dispose();
                }

                _disposed = true;
            }
        }

        ~BaseDbHelper()
        {
            Dispose(false);
        }
    }
}