<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="RentaVideojuegos.Inicio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .hero-section {
            background-color: #111827;
            color: #ffffff;
            border-radius: 12px;
            padding: 80px 40px;
            text-align: center;
            margin-top: 20px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
        }
        .hero-title {
            font-size: 3rem;
            font-weight: 800;
            margin-bottom: 20px;
            letter-spacing: -1px;
        }
        .hero-subtitle {
            font-size: 1.2rem;
            color: #9ca3af;
            max-width: 600px;
            margin: 0 auto 40px auto;
            line-height: 1.6;
        }
        .btn-principal {
            background-color: #ffffff;
            color: #111827;
            padding: 15px 35px;
            font-size: 1.1rem;
            font-weight: bold;
            text-decoration: none;
            border-radius: 8px;
            transition: all 0.3s ease;
            display: inline-block;
            margin: 10px;
        }
        .btn-principal:hover {
            background-color: #f3f4f6;
            transform: translateY(-2px);
            text-decoration: none;
            color: #111827;
        }
        .btn-secundario {
            background-color: transparent;
            color: #ffffff;
            border: 2px solid #ffffff;
            padding: 13px 35px;
            font-size: 1.1rem;
            font-weight: bold;
            text-decoration: none;
            border-radius: 8px;
            transition: all 0.3s ease;
            display: inline-block;
            margin: 10px;
        }
        .btn-secundario:hover {
            background-color: #ffffff;
            color: #111827;
            text-decoration: none;
        }
    </style>

    <div class="row">
        <div class="col-12">
            <div class="hero-section">
                <h1 class="hero-title">Bienvenido a PSRenta</h1>
                <p class="hero-subtitle">
                    Tu plataforma definitiva para alquilar los mejores videojuegos. Explora nuestro catálogo, administra tus rentas y disfruta de la mejor experiencia de gaming.
                </p>
                <div>
                    <a href="~/Pages/CatalogoJuegos.aspx" runat="server" class="btn-principal">Explorar Catálogo</a>
                    <a href="#" class="btn-secundario">Iniciar Sesión</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>