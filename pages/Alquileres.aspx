<%@ Page Title="Alquileres" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alquileres.aspx.cs" Inherits="RentaVideojuegos.Pages.Alquileres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row mb-4">
        <div class="col-12 border-bottom border-secondary pb-2">
            <h2>Mis Alquileres</h2>
            <p class="text-muted">Consulta el historial y estado de tus juegos rentados.</p>
        </div>
    </div>

    <div class="row">
        <div class="col-12">
            <!-- Aquí agregaremos la tabla HTML / GridView de los alquileres -->
            <div class="alert alert-info bg-dark text-light border-primary" role="alert">
                Aún no tienes alquileres registrados. ¡Explora las sucursales!
            </div>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-12">
            <asp:Button ID="btnProbarConexion" runat="server" Text="Probar Conexión a BD" CssClass="btn btn-success" OnClick="btnProbarConexion_Click" />
            <br /><br />
            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>