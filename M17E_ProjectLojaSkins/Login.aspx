<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M17E_ProjectLojaSkins.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl_emaillogin" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="tb_emaillogin" runat="server"></asp:TextBox>
        </div>
        <br />
        <asp:Label ID="lbl_senhalogin" runat="server" Text="Senha"></asp:Label>
        <asp:TextBox ID="tb_password" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        <br />
        <asp:Button ID="btn_login" runat="server" OnClick="btn_login_Click" Text="login" Width="126px" />
        <asp:Button ID="btn_recuperarpassword" runat="server" Text="Recuperar password" Width="131px" />
        <p>
            <asp:Label ID="lb_erro" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
