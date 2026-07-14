<%@ Page Title="Gestión de Alquileres" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionAlquileres.aspx.cs" Inherits="RentaVideojuegos.Pages.GestionAlquileres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .fondo-general { background-color: #f0f2f5; padding: 30px; border-radius: 8px; }
        .tarjeta-blanca { background-color: #ffffff; border: 1px solid #d1d5db; box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1); padding: 30px; border-radius: 8px; margin-bottom: 30px; }
        .titulo-seccion { color: #111827; border-bottom: 3px solid #1f2937; padding-bottom: 10px; margin-bottom: 25px; font-weight: bold; }
        .grid-minimalista { width: 100%; border-collapse: collapse; margin-top: 15px; }
        .grid-minimalista th { background-color: #1f2937; color: #ffffff; padding: 15px; text-align: left; }
        .grid-minimalista td { padding: 12px 15px; border-bottom: 1px solid #e5e7eb; vertical-align: middle; color: #111827 !important; font-size: 14px; }
        .filtro-container { display: flex; gap: 15px; margin-bottom: 20px; align-items: flex-end; }
        .input-filtro { padding: 8px; border: 1px solid #ccc; border-radius: 4px; }
        .btn-buscar { background-color: #2563eb; color: white; padding: 9px 15px; border: none; border-radius: 4px; cursor: pointer; font-weight: bold; }
    </style>

    <div class="fondo-general">
        <h2 style="color: #111827; font-weight: 800; margin-bottom: 5px;">Gestión de Alquileres</h2>
        <p style="color: #6b7280; margin-bottom: 25px;">Administración total, filtros y cancelación de rentas del sistema.</p>
        
        <div class="tarjeta-blanca">
            <h3 class="titulo-seccion">Filtros de Búsqueda</h3>
            
            <div class="filtro-container">
                <div>
                    <label style="display: block; font-size: 14px; margin-bottom: 5px;">Jugador:</label>
                    <asp:TextBox ID="txtFiltroJugador" runat="server" CssClass="input-filtro" placeholder="Nombre del jugador..."></asp:TextBox>
                </div>
                <div>
                    <label style="display: block; font-size: 14px; margin-bottom: 5px;">Desde (Inicio):</label>
                    <asp:TextBox ID="txtFiltroInicio" runat="server" TextMode="Date" CssClass="input-filtro"></asp:TextBox>
                </div>
                <div>
                    <label style="display: block; font-size: 14px; margin-bottom: 5px;">Hasta (Devolución):</label>
                    <asp:TextBox ID="txtFiltroFin" runat="server" TextMode="Date" CssClass="input-filtro"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn-buscar" OnClick="btnBuscar_Click" />
                </div>
            </div>

            <asp:GridView ID="gvAlquileres" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="No se encontraron alquileres con esos filtros." 
                CssClass="grid-minimalista" GridLines="None"
                OnRowCommand="gvAlquileres_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdAlquiler" HeaderText="ID Renta" />
                    <asp:BoundField DataField="Jugador" HeaderText="Jugador" />
                    <asp:BoundField DataField="Videojuego" HeaderText="Videojuego" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaDevolucion" HeaderText="Devolución" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <a href='ModificarAlquiler.aspx?id=<%# Eval("IdAlquiler") %>' style="color: blue; font-weight: bold; text-decoration: underline; margin-right: 15px;">
                                Editar
                            </a>
                            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="CancelarRenta" 
                                CommandArgument='<%# Eval("IdAlquiler") %>' 
                                Text="Cancelar" style="color: red; font-weight: bold; text-decoration: underline;" 
                                OnClientClick="return confirm('¿Estás seguro de que deseas cancelar este alquiler? El estado pasará a Inactivo.');">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>