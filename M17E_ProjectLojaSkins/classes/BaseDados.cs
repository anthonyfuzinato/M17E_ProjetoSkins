using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17E_INTRO_12H.Classes
{
    public class BaseDados
    {
        private string strLigacao;
        private SqlConnection ligacaoBD;
        public BaseDados()
        {
            //ligação à bd
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }
        ~BaseDados()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region Transações
        public SqlTransaction iniciarTransacao()
        {
            return ligacaoBD.BeginTransaction();
        }
        public SqlTransaction iniciarTransacao(IsolationLevel isolamento)
        {
            return ligacaoBD.BeginTransaction(isolamento);
        }
        #endregion
        #region SQL de ação
        
        public void executaSQL(string sql, List<SqlParameter> parametros=null, SqlTransaction transacao=null)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            if (parametros!=null)
                comando.Parameters.AddRange(parametros.ToArray());
            if (transacao!=null)
                comando.Transaction = transacao;
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        
        public int executaEDevolveSQL(string sql, List<SqlParameter> parametros=null, SqlTransaction transacao=null)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            if (parametros!=null)
                comando.Parameters.AddRange(parametros.ToArray());
            if (transacao!=null)
                comando.Transaction = transacao;
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        #endregion
        #region SQL Consultas
        
        public DataTable devolveSQL(string sql, List<SqlParameter> parametros=null, SqlTransaction transacao=null)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            if (transacao!=null)
                comando.Transaction = transacao;
            DataTable registos = new DataTable();
            if (parametros!=null)
                comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        #endregion
    }
}