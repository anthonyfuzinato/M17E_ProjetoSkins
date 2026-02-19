using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17E_INTRO_12H.Classes
{
    public class Utilizadores
    {
        public int id;
        public string email;
        public string nome;
        public string palavra_passe;
        public int sal;
        public int perfil;
        BaseDados bd;
        public Utilizadores()
        {
            bd = new BaseDados();
        }

        public bool VerificaLogin()
        {
            DataTable dados;
            //Não FAZER
            string sql = "SELECT * FROM Utilizadores WHERE email='"+email+"' AND palavra_passe=HASHBYTES('SHA2_512',concat(@palavra_passe,sal))";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                //new SqlParameter()
                //{
                //    ParameterName="@email",
                //    SqlDbType=SqlDbType.VarChar,
                //    Value=email
                //},
                new SqlParameter()
                {
                    ParameterName="@palavra_passe",
                    SqlDbType=SqlDbType.VarChar,
                    Value=palavra_passe
                }
            };
            dados = bd.devolveSQL(sql, parametros);
            if (dados == null || dados.Rows.Count == 0)
                return false;
            id = int.Parse(dados.Rows[0]["id"].ToString());
            nome = dados.Rows[0]["nome"].ToString();
            email = dados.Rows[0]["email"].ToString();
            perfil = int.Parse(dados.Rows[0]["perfil"].ToString());
            return true;
        }
        public void Adicionar()
        {
            string sql = @"INSERT INTO utilizadores(email,nome,palavra_passe,perfil,sal)
                    VALUES (@email,@nome,HASHBYTES('SHA2_512',concat(@password,@sal)),@perfil,@sal)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter()
                {
                    ParameterName="@nome",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },

                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.palavra_passe
                },
                new SqlParameter()
                {
                    ParameterName="@perfil",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.perfil
                },
                new SqlParameter()
                {
                    ParameterName="@sal",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.sal
                },
            };
            bd.executaSQL(sql, parametros);
        }

        internal DataTable ListaTodosUtilizadores()
        {
            return bd.devolveSQL("SELECT * FROM Utilizadores");
        }
        public DataTable listaTodosUtilizadoresDisponiveis()
        {
            string sql = $@"SELECT id, email,nome, perfil 
                    FROM utilizadores where estado=1";
            return bd.devolveSQL(sql);
        }

        public void atualizarUtilizador()
        {
            string sql = @"UPDATE utilizadores SET nome=@nome
                    WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=nome },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
            };
            bd.executaSQL(sql, parametros);
        }
        public DataTable devolveDadosUtilizador(int id)
        {
            string sql = "SELECT * FROM utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            if (dados.Rows.Count == 0)
            {
                return null;
            }
            return dados;
        }
        public int estadoUtilizador(int id)
        {
            string sql = "SELECT estado FROM utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            return int.Parse(dados.Rows[0][0].ToString());
        }
        public void ativarDesativarUtilizador(int id)
        {
            int estado = this.estadoUtilizador(id);
            //troca o estado
            if (estado == 0)
                estado = 1;
            else
                estado = 0;
            string sql = "UPDATE utilizadores SET estado = @estado WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=estado },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            bd.executaSQL(sql, parametros);
        }
        public void removerUtilizador(int id)
        {
            string sql = "DELETE FROM Utilizadores WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value= id},
            };
            bd.executaSQL(sql, parametros);
        }
        public void recuperarPassword(string email, string guid)
        {
            string sql = "UPDATE utilizadores set token=@lnk WHERE email=@email";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            bd.executaSQL(sql, parametros);
        }
        public void atualizarPassword(string guid, string password)
        {
            string sql = "UPDATE utilizadores set palavra_passe=HASHBYTES('SHA2_512',concat(@password,sal)),token=null WHERE token=@lnk";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password},
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            bd.executaSQL(sql, parametros);
        }
        public DataTable devolveDadosUtilizador(string email)
        {
            string sql = "SELECT * FROM utilizadores WHERE email=@email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email }
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            return dados;
        }
    }
}