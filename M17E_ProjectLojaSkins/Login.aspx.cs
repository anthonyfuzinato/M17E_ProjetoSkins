using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17E_ProjectLojaSkins
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            //validar
            if (tb_emaillogin.Text == "" || tb_password.Text == "")
            {
                lb_erro.Text = "Login falhou. Tente novamente.";
                return;
            }

            // AQUI FAZER O RECAPTCHA -------------

            //consulta à tabela de utilizadores
            Utilizadores novo = new Utilizadores();
            novo.email = tb_email.Text;
            novo.palavra_passe = tb_password.Text;
            if (novo.VerificaLogin() == false)
            {
                lb_erro.Text = "Login falhou. Tente novamente.";
                return;
            }
            //Sessão - perfil, email, nome
            Session["id"] = novo.id;
            Session["email"] = novo.email;
            Session["perfil"] = novo.perfil;
            Session["nome"] = novo.nome;
            Session["ip"] = Request.UserHostAddress;
            Session["useragent"] = Request.UserAgent;
            //redirecionar o utilizador de acordo com perfil
            if (novo.perfil == 0)
            {
                Response.Redirect("admin.aspx");
            }
            if (novo.perfil == 1)
            {
                Response.Redirect("cliente.aspx");
            }
        }
    }
}