<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="RentaVideojuegos.pages.IniciarSesion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login PlayStation</title>
    <style>
        body { background-color: #0f172a; font-family: Arial; display: flex; justify-content: center; align-items: center; height: 100vh; }
        .login-container { background-color: #1e293b; padding: 30px; border-radius: 10px; width: 350px; box-shadow: 0px 0px 10px #2563eb; }
        h2 { color: white; text-align: center; margin-bottom: 20px; }
        label { color: white; }
        .txt { width: 100%; padding: 10px; margin-top: 5px; margin-bottom: 15px; border-radius: 5px; border: none; box-sizing: border-box; }
        .btn { width: 100%; background-color: #2563eb; color: white; padding: 10px; border: none; border-radius: 5px; cursor: pointer; }
        .btn:hover { background-color: #1d4ed8; }
        .error { color: red; display: block; margin-top: 10px; text-align: center; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Iniciar Sesión</h2>
            <label>Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
            <label>Clave</label>
            <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="txt"></asp:TextBox>
            
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn" OnClick="btnIngresar_Click" />
            
            <asp:Label ID="lblError" runat="server" CssClass="error" />
            <br />
            <a href="Registrarse.aspx" style="color: #3b82f6; text-align: center; display: block;">¿No tienes cuenta? Registrarse</a>
        </div>
    </form>
</body>
</html>