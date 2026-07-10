<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="RentaVideojuegos.pages.Registrarse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Jugador</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }

        body {
            font-family: 'Segoe UI', sans-serif;
            background: #0f172a;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .card {
            background: #0d1b2a;
            border: 1px solid #1e3a5f;
            border-radius: 14px;
            padding: 40px 36px;
            width: 100%;
            max-width: 420px;
            box-shadow: 0 0 30px rgba(30, 90, 200, 0.25);
        }

        .card h2 {
            font-size: 1.6rem;
            font-weight: 700;
            color: #ffffff;
            text-align: center;
            margin-bottom: 28px;
        }

        .form-group {
            margin-bottom: 16px;
        }

        .form-group label {
            display: block;
            font-size: 0.9rem;
            color: #c5d0e0;
            margin-bottom: 6px;
        }

        .form-group input[type="text"],
        .form-group input[type="password"] {
            width: 100%;
            padding: 10px 12px;
            background: #ffffff;
            border: 1px solid #ccc;
            border-radius: 6px;
            color: #111;
            font-size: 0.95rem;
        }

        .form-group input:focus {
            outline: none;
            border-color: #3b6fd4;
        }

        .check-group {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 22px;
        }

        .check-group input[type="checkbox"] {
            width: 17px;
            height: 17px;
            accent-color: #3b6fd4;
            cursor: pointer;
        }

        .check-group label {
            font-size: 0.9rem;
            color: #c5d0e0;
            cursor: pointer;
        }

        .btn-registrar {
            width: 100%;
            padding: 11px;
            background: #3b6fd4;
            color: #fff;
            border: none;
            border-radius: 6px;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            transition: background 0.2s;
        }

        .btn-registrar:hover {
            background: #2f5bbf;
        }

        .error-msg {
            display: block;
            margin-top: 12px;
            font-size: 0.88rem;
            color: #ff6b6b;
            text-align: center;
        }

        .login-link {
            text-align: center;
            margin-top: 16px;
            font-size: 0.88rem;
        }

        .login-link a {
            color: #8b6fd4;
            text-decoration: none;
        }

        .login-link a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card">
            <h2>Crear Cuenta</h2>

            <div class="form-group">
                <label for="txtNomCompleto">Nombre completo</label>
                <asp:TextBox ID="txtNomCompleto" runat="server" placeholder="Ej. Juan Pérez" />
            </div>

            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="correo@ejemplo.com" />
            </div>

            <div class="form-group">
                <label for="txtClave">Clave</label>
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" placeholder="••••••••" />
            </div>

            <asp:Button ID="btnRegistrar" runat="server"
                Text="Registrarse"
                CssClass="btn-registrar"
                OnClick="btnRegistrar_Click" />

            <asp:Label ID="lblError" runat="server" CssClass="error-msg" />

            <div class="login-link">
                <a href="IniciarSesion.aspx">¿Ya tienes cuenta? Iniciar Sesión</a>
            </div>
        </div>
    </form>
</body>
</html>