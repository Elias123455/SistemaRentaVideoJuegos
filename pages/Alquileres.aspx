<%@ Page Title="Alquileres" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alquileres.aspx.cs" Inherits="RentaVideojuegos.Pages.Alquileres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- ESTILOS MINIMALISTAS (Unificados con el resto del sistema) -->
    <style>
        .fondo-general {
            background-color: #f0f2f5; 
            padding: 30px;
            border-radius: 8px;
        }
        .tarjeta-blanca {
            background-color: #ffffff; 
            border: 1px solid #d1d5db; 
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1); 
            padding: 30px;
            border-radius: 8px;
            margin-bottom: 30px;
        }
        .titulo-seccion {
            color: #111827;
            border-bottom: 3px solid #1f2937;
            padding-bottom: 10px;
            margin-bottom: 25px;
            font-weight: bold;
        }
        .grid-minimalista {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }
        .grid-minimalista th {
            background-color: #111827;
            color: #ffffff;
            padding: 15px;
            text-align: left;
        }
        .grid-minimalista td { 
    padding: 15px; 
    border-bottom: 1px solid #e5e7eb; 
    vertical-align: middle; 
    color: #111827 !important; 
}
        .texto-ayuda {
            color: #6b7280;
            font-size: 14px;
            margin-bottom: 20px;
            display: block;
        }
    </style>

    <div class="fondo-general">
        <h2 style="color: #111827; font-weight: 800; margin-bottom: 20px;">Mis Alquileres</h2>

        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Historial de Juegos Rentados</h3>
            <span class="texto-ayuda">Consulta el estado, los costos y las fechas de devolución de tus videojuegos.</span>
            
            <!-- GRIDVIEW PREPARADO PARA LOS ALQUILERES -->
            <asp:GridView ID="gvAlquileres" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="Aún no tienes alquileres registrados. ¡Explora el catálogo y renta un juego!" 
                CssClass="grid-minimalista" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="idAlquiler" HeaderText="ID Renta" />
                    <asp:BoundField DataField="idVideojuego" HeaderText="ID Juego" />
                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha Renta" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="fechaDevolucion" HeaderText="Fecha Devolución" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="costoTotal" HeaderText="Costo Total" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>