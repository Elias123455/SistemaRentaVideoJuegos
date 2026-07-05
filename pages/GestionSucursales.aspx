<%@ Page Title="Sucursales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionSucursales.aspx.cs" Inherits="RentaVideojuegos.Pages.GestionSucursales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .fondo-general { background-color: #f0f2f5; padding: 30px; border-radius: 8px; }
        .tarjeta-blanca { background-color: #ffffff; border: 1px solid #d1d5db; box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1); padding: 30px; border-radius: 8px; margin-bottom: 30px; }
        .titulo-seccion { color: #111827; border-bottom: 3px solid #1f2937; padding-bottom: 10px; margin-bottom: 25px; font-weight: bold; }
        .tabla-layout { width: 100%; border-collapse: separate; border-spacing: 0 15px; }
        .etiqueta-texto { font-weight: 600; color: #374151; font-size: 15px; }
        .input-minimalista { width: 100%; max-width: 450px; padding: 10px 12px; border: 1px solid #d1d5db; border-radius: 6px; background-color: #f9fafb; }
        .btn-destacado { background-color: #111827; color: #ffffff; border: none; padding: 12px 30px; font-size: 16px; font-weight: bold; border-radius: 6px; cursor: pointer; }
        .grid-minimalista { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .grid-minimalista th { background-color: #111827; color: #ffffff; padding: 15px; text-align: left; }
        .grid-minimalista td { padding: 15px; border-bottom: 1px solid #e5e7eb; vertical-align: middle; color: #111827 !important; }
        .error-texto { color: #dc2626; font-size: 12px; font-weight: bold; display: block; margin-top: 5px; }
    </style>

    <div class="fondo-general">
        <h2 style="color: #111827; font-weight: 800; margin-bottom: 20px;">Administración de Sucursales</h2>
        
        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Registrar Nueva Sucursal</h3>
            <table class="tabla-layout">
                <tr>
                    <td style="width: 200px;"><span class="etiqueta-texto">Nombre Sucursal:</span></td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="input-minimalista"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="* Obligatorio" CssClass="error-texto" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Dirección Física:</span></td>
                    <td><asp:TextBox ID="txtDireccion" runat="server" CssClass="input-minimalista" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Teléfono de Contacto:</span></td>
                    <td><asp:TextBox ID="txtTelefono" runat="server" CssClass="input-minimalista"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right; padding-top: 25px;">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Sucursal" OnClick="btnGuardar_Click" CssClass="btn-destacado" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Sucursales Activas</h3>
            <asp:GridView ID="gvSucursales" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay sucursales registradas." CssClass="grid-minimalista" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="idSucursal" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>