<%@ Page Title="Modificar Alquiler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarAlquiler.aspx.cs" Inherits="RentaVideojuegos.Pages.ModificarAlquiler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Modificar Alquiler</h2>
    
    <div>
        <label>Fecha de Inicio:</label>
        <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date"></asp:TextBox>
    </div>
    
    <div>
        <label>Fecha de Devolución:</label>
        <asp:TextBox ID="txtFechaDevolucion" runat="server" TextMode="Date"></asp:TextBox>
    </div>
    
    <div style="margin-top: 15px;">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" OnClick="btnGuardar_Click" />
    </div>
</asp:Content>