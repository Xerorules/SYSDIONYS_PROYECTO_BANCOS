<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CONSULTAR_BUSCAR_COMPRA.aspx.cs" Inherits="DIONYS_ERP.FRM_CONSULTAR_BUSCAR_COMPRA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
        width: 241px;
        }
        </style>
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div>
        <fieldset>
            <legend style="font-family:Tahoma;font-weight:bold;color:orange;font-size:14PX">FILTRO POR FECHA</legend>
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="FECHA INICIAL:" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFECHA_INI" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="160px"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="FECHA FINAL:" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFECHA_FIN" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="160px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>
    
     <div>
        <fieldset>
            <legend style="font-family:Tahoma;font-weight:bold;color:orange;font-size:14PX">FILTROS OPCIONALES</legend>

            <table class="auto-style1">
                
                <tr>
                    <td>
                        <asp:Label ID="OBSERVACIONES" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="OBSERVACIONES" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="ESTADO" ForeColor="White"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="MONEDA" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="TIPO DOC" ForeColor="White"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:Button ID="btnBUSCAR" runat="server" Height="50px" Text="BUSCAR" Width="117px" OnClick="btnBUSCAR_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtOBSERVACIONES" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="160px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboESTADO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>ACTIVOS</asp:ListItem>
                            <asp:ListItem>ANULADOS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="cboMONEDA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>SOLES</asp:ListItem>
                            <asp:ListItem>DOLARES</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTIPODOC" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem Value="AA">TODOS</asp:ListItem>
                            <asp:ListItem Value="FT">FACTURA</asp:ListItem>
                            <asp:ListItem Value="BV">BOLETA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:GridView ID="dgvDATOS_COMPRAS" runat="server" CellPadding="4" Font-Names="Tahoma" Width="100%" Font-Size="10px" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="dgvDATOS_COMPRAS_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="C_ID_COMPRA" HeaderText="ID_COMPRA" />
                                <asp:BoundField DataField="C_SERIE" HeaderText="SERIE" />
                                <asp:BoundField DataField="C_NUMERO" HeaderText="NUMERO" />
                                <asp:BoundField DataField="C_TIPO_DOC" HeaderText="TIPO DOC" />
                                <asp:BoundField DataField="C_FECHA" HeaderText="FECHA" />
                                <asp:BoundField DataField="C_MONEDA" HeaderText="MONEDA" />
                                <asp:BoundField DataField="C_VALOR_VENTA" HeaderText="VALOR VENTA" />
                                <asp:BoundField DataField="C_IGV" HeaderText="IGV" />
                                <asp:BoundField DataField="C_TOTAL" HeaderText="TOTAL" />
                                <asp:BoundField DataField="C_SALDO" HeaderText="SALDO" />
                                <asp:BoundField DataField="C_FECHA_ANULADO" HeaderText="FECHA ANULADO" />
                                <asp:BoundField HeaderText="OBSERVACIONES" DataField="C_OBSERVACIONES" />
                                <asp:BoundField DataField="C_ID_PROVEEDOR" HeaderText="ID PROVEEDOR" />
                                <asp:BoundField DataField="P_DESCRIPCION" HeaderText="NOMBRE PROVEEDOR" />
                                <asp:BoundField DataField="P_RUC_DNI" HeaderText="RUC/DNI" />
                                <asp:BoundField DataField="P_DIRECCION" HeaderText="DIRECCION PROV" />
                                <asp:BoundField DataField="P_TELEFONO_1" HeaderText="TELEFONO" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </fieldset>
    </div>

</asp:Content>
