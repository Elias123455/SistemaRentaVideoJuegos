<%@ Page Title="Bitácora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="RentaVideojuegos.Pages.BitacoraPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .fondo-general { background-color: #f0f2f5; padding: 30px; border-radius: 8px; }
        .tarjeta-blanca { background-color: #ffffff; border: 1px solid #d1d5db; box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1); padding: 30px; border-radius: 8px; margin-bottom: 30px; }
        .titulo-seccion { color: #111827; border-bottom: 3px solid #1f2937; padding-bottom: 10px; margin-bottom: 25px; font-weight: bold; }
        .grid-minimalista { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .grid-minimalista th { background-color: #1f2937; color: #ffffff; padding: 15px; text-align: left; }
        .grid-minimalista td { padding: 12px 15px; border-bottom: 1px solid #e5e7eb; vertical-align: middle; color: #111827 !important; font-size: 14px; }
    </style>

    <div class="fondo-general">
        <h2 style="color: #111827; font-weight: 800; margin-bottom: 5px;">Auditoría del Sistema</h2>
        <p style="color: #6b7280; margin-bottom: 25px;">Historial de acciones y eventos registrados en la base de datos.</p>
        
        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Historial de Operaciones</h3>
            
            <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" EmptyDataText="No se registran eventos en la bitácora actualmente." CssClass="grid-minimalista" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="IdBitacora" HeaderText="ID Evento" />
                    <asp:BoundField DataField="IdJugador" HeaderText="Jugador" />
                    <asp:BoundField DataField="AccionRealizada" HeaderText="Acción Realizada" />
                    <asp:BoundField DataField="FechaDeLaAccion" HeaderText="Fecha y Hora" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                    <asp:BoundField DataField="IdAlquiler" HeaderText="ID Alquiler" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>