<%@ Page Title="Catálogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CatalogoJuegos.aspx.cs" Inherits="RentaVideojuegos.Pages.CatalogoJuegos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .fondo-general { background-color: #f0f2f5; padding: 30px; border-radius: 8px; }
        .contenedor-galeria { display: flex; flex-wrap: wrap; gap: 25px; margin-top: 20px; }
        .tarjeta-juego { 
            background-color: #ffffff; 
            border: 1px solid #d1d5db; 
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08); 
            border-radius: 8px; 
            width: 280px; 
            padding: 20px; 
            display: flex; 
            flex-direction: column; 
            justify-content: space-between;
        }
        .imagen-portada { width: 100%; height: 320px; object-fit: cover; border-radius: 6px; box-shadow: 0 2px 8px rgba(0,0,0,0.15); margin-bottom: 15px; }
        .titulo-juego { font-size: 18px; font-weight: bold; color: #111827; margin: 5px 0; }
        .info-juego { font-size: 14px; color: #4b5563; margin-bottom: 4px; }
        .btn-rentar { 
            background-color: #111827; 
            color: #ffffff; 
            border: none; 
            padding: 10px; 
            width: 100%; 
            font-weight: bold; 
            border-radius: 6px; 
            cursor: pointer; 
            margin-top: 15px;
            text-align: center;
        }
        .btn-rentar:hover { background-color: #374151; }
    </style>

    <div class="fondo-general">
        <h2 style="color: #111827; font-weight: 800; margin-bottom: 5px;">Explorar Catálogo</h2>
        <p style="color: #6b7280; margin-bottom: 25px;">Selecciona tu próximo videojuego y recógelo en tu sucursal favorita.</p>

        <div class="contenedor-galeria">
            <asp:Repeater ID="rptCatalogo" runat="server">
                <ItemTemplate>
                    <div class="tarjeta-juego">
                        <div>
                            <asp:Image ID="imgPortada" runat="server" CssClass="imagen-portada" 
                                ImageUrl='<%# "~/images/portadas/" + (Eval("Imagen") != null && !string.IsNullOrEmpty(Eval("Imagen").ToString()) ? System.IO.Path.GetFileName(Eval("Imagen").ToString()) : "default.png") %>' />
                            
                            <div class="titulo-juego"><%# Eval("Titulo") %></div>
                            <div class="info-juego"><strong>Desarrollador:</strong> <%# Eval("Desarrolladora") %></div>
                        </div>
                        <asp:Button ID="btnAlquilar" runat="server" Text="RENTAR JUEGO" CssClass="btn-rentar" CommandArgument='<%# Eval("IdVideojuego") %>' OnClick="btnAlquilar_Click" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>