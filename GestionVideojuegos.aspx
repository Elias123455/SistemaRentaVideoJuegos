<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GestionVideojuegos.aspx.cs" Inherits="RentaVideojuegos.GestionVideojuegos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Videojuegos</h2>
    <hr />

    <div style="margin-bottom: 30px;">
        <h3>Registrar Nuevo Videojuego</h3>
        
        <table>
            <tr>
                <td>ID Sucursal:</td>
                <td><asp:DropDownList ID="ddlSucursal" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Título del Juego:</td>
                <td><asp:TextBox ID="txtTitulo" runat="server" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Descripción:</td>
                <td><asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>ID Categoría:</td>
                <td><asp:DropDownList ID="ddlCategoria" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Fecha de Lanzamiento:</td>
                <td><asp:TextBox ID="txtFechaLanzamiento" runat="server" TextMode="Date"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Desarrolladora:</td>
                <td><asp:TextBox ID="txtDesarrolladora" runat="server" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Distribuidora:</td>
                <td><asp:TextBox ID="txtDistribuidora" runat="server" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Portada (Imagen):</td>
                <td><asp:FileUpload ID="fuImagen" runat="server" /></td>
            </tr>
            <tr>
                <td>URL del Tráiler:</td>
                <td><asp:TextBox ID="txtTrailer" runat="server" Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right; padding-top: 10px;">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Videojuego" OnClick="btnGuardar_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div style="margin-top: 40px;">
        <h3>Catálogo de Videojuegos</h3>
        
        <asp:GridView ID="gvVideojuegos" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay videojuegos registrados." CellPadding="4">
            <Columns>
                <asp:BoundField DataField="idVideojuego" HeaderText="ID" />
                <asp:BoundField DataField="titulo" HeaderText="Título" />
                <asp:BoundField DataField="desarrolladora" HeaderText="Desarrolladora" />
                
                <asp:TemplateField HeaderText="Portada">
                    <ItemTemplate>
                        <asp:Image ID="imgPortada" runat="server" Width="80px" 
                            ImageUrl='<%# Eval("imagen", "~/images/portadas/{0}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
