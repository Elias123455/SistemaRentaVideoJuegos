<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GestionVideojuegos.aspx.cs" Inherits="RentaVideojuegos.GestionVideojuegos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .fondo-general { background-color: #f0f2f5; padding: 30px; border-radius: 8px; }
        .tarjeta-blanca { background-color: #ffffff; border: 1px solid #d1d5db; box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1); padding: 30px; border-radius: 8px; margin-bottom: 30px; }
        .titulo-seccion { color: #111827; border-bottom: 3px solid #1f2937; padding-bottom: 10px; margin-bottom: 25px; font-weight: bold; }
        .tabla-layout { width: 100%; border-collapse: separate; border-spacing: 0 15px; }
        .etiqueta-texto { font-weight: 600; color: #374151; font-size: 15px; }
        .input-minimalista { width: 100%; max-width: 450px; padding: 10px 12px; border: 1px solid #d1d5db; border-radius: 6px; background-color: #f9fafb; transition: all 0.3s ease; }
        .input-minimalista:focus { border-color: #111827; background-color: #ffffff; outline: none; box-shadow: 0 0 0 2px rgba(17, 24, 39, 0.2); }
        .btn-destacado { background-color: #111827; color: #ffffff; border: none; padding: 12px 30px; font-size: 16px; font-weight: bold; border-radius: 6px; cursor: pointer; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.2); }
        .btn-destacado:hover { background-color: #374151; transform: translateY(-2px); }
        .grid-minimalista { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .grid-minimalista th { background-color: #111827; color: #ffffff; padding: 15px; text-align: left; }
        
     
        .grid-minimalista td { padding: 15px; border-bottom: 1px solid #e5e7eb; vertical-align: middle; color: #111827 !important; } 
        
        .error-texto { color: #dc2626; font-size: 12px; font-weight: bold; display: block; margin-top: 5px; }
    </style>

    <div class="fondo-general">
        <h2 style="color: #111827; font-weight: 800; margin-bottom: 20px;">Gestión de Videojuegos</h2>
        
        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Registrar Nuevo Videojuego</h3>
            
            <table class="tabla-layout">
                <tr>
                    <td style="width: 200px;"><span class="etiqueta-texto">ID Sucursal:</span></td>
                    <td>
                        <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="input-minimalista"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSucursal" runat="server" ControlToValidate="ddlSucursal" InitialValue="0" ErrorMessage="* Seleccione una sucursal" CssClass="error-texto" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Título del Juego:</span></td>
                    <td>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="input-minimalista"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="* El título es obligatorio" CssClass="error-texto" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Descripción:</span></td>
                    <td><asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="input-minimalista"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">ID Categoría:</span></td>
                    <td>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="input-minimalista"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="ddlCategoria" InitialValue="0" ErrorMessage="* Seleccione una categoría" CssClass="error-texto" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Fecha de Lanzamiento:</span></td>
                    <td>
                        <asp:TextBox ID="txtFechaLanzamiento" runat="server" TextMode="Date" CssClass="input-minimalista"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFechaLanzamiento" ErrorMessage="* La fecha es obligatoria" CssClass="error-texto" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Desarrolladora:</span></td>
                    <td><asp:TextBox ID="txtDesarrolladora" runat="server" CssClass="input-minimalista"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Distribuidora:</span></td>
                    <td><asp:TextBox ID="txtDistribuidora" runat="server" CssClass="input-minimalista"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">Portada (Imagen):</span></td>
                    <td><asp:FileUpload ID="fuImagen" runat="server" CssClass="input-minimalista" style="padding: 7px; background-color: #fff;" /></td>
                </tr>
                <tr>
                    <td><span class="etiqueta-texto">URL del Tráiler:</span></td>
                    <td><asp:TextBox ID="txtTrailer" runat="server" CssClass="input-minimalista"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right; padding-top: 25px;">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Videojuego" OnClick="btnGuardar_Click" CssClass="btn-destacado" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Catálogo de Videojuegos</h3>
            
            <asp:GridView ID="gvVideojuegos" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay videojuegos registrados." CssClass="grid-minimalista" GridLines="None" 
                DataKeyNames="idVideojuego" OnRowCommand="gvVideojuegos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="idVideojuego" HeaderText="ID" />
                    <asp:BoundField DataField="titulo" HeaderText="Título" />
                    <asp:BoundField DataField="desarrolladora" HeaderText="Desarrolladora" />
                    
                    <asp:TemplateField HeaderText="Portada">
                        <ItemTemplate>
                            <asp:Image ID="imgPortada" runat="server" Width="80px" ImageUrl='<%# Eval("imagen", "~/images/portadas/{0}") %>' style="border-radius: 4px; box-shadow: 0 2px 5px rgba(0,0,0,0.15);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" CommandName="EditarJuego" CommandArgument='<%# Eval("idVideojuego") %>' style="color: #0284c7; text-decoration: none; font-weight: bold; margin-right: 15px;"></asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" Text="Eliminar" CommandName="EliminarJuego" CommandArgument='<%# Eval("idVideojuego") %>' style="color: #dc2626; text-decoration: none; font-weight: bold;" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este juego de forma permanente?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>