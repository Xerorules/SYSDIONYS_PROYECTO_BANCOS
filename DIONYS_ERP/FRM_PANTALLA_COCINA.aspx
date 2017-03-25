<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_PANTALLA_COCINA.aspx.cs" Inherits="DIONYS_ERP.FRM_PANTALLA_COCINA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
        width: 28%;
    }
        .auto-style13 {
        }
        .header{
            font-weight:bold;
            position:absolute;
            background-color:White;
            /*border-top-left-radius:inherit;*/
          }
        .auto-style15 {
            width: 1%;
        }
        .auto-style18 {
            width: 35%;
        }
        .auto-style25 {
            width: 65%;
        }
        .auto-style26 {
            width: 108px;
        }
        .auto-style28 {
            width: 32%;
        }
        .auto-style29 {
            width: 100%;
        }
        .auto-style30 {
            width: 150px;
        }
        .auto-style31 {
            width: 128px;
        }
    </style>
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />

    <!-- ================================================= -->
    <!-- SCRIPT PARA BLOQUEAR LAS FLECHAS DE NAVEGACION -->
    <script type="text/javascript">
        {
            if (history.forward(1))
                location.replace(history.forward(1))
        }
        </script>
    
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- ================================================= -->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
        </asp:Timer>
       
    <table class="auto-style29">
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style30">
                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="ANULAR PEDIDO:"></asp:Label>
            </td>
            <td class="auto-style31">
                <asp:TextBox ID="txtNUM_ANULADO" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnANULAR_PEDIDO" runat="server" Text="ANULAR" OnClick="btnANULAR_PEDIDO_Click" OnClientClick="return confirm(&quot;¿DESEA ANULAR EL PEDIDO SELECCIONADO?&quot;);" />
            </td>
        </tr>
    </table>
       
    <asp:DataList ID="DataList1" runat="server" CellPadding="4" RepeatColumns="3" RepeatDirection="Horizontal" Width="1172px" BackColor="Teal" BorderColor="#336699" BorderStyle="None" BorderWidth="6px" CellSpacing="6" GridLines="Both" Font-Bold="False" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" OnItemCommand="DataList1_ItemCommand">
    <FooterStyle BackColor="#0099FF" ForeColor="#330099" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <ItemStyle BackColor="White" ForeColor="#330099" />
    <ItemTemplate>
        <table class="auto-style1">
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style26">
                    <asp:Label ID="Label3" runat="server" Text="FECHA PEDIDO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                </td>
                <td class="auto-style28">
                    <asp:Label ID="lblFECHA_PEDIDO" runat="server" Text='<%# Eval("FECHA") %>' Font-Names="Tahoma" Font-Size="14px" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style18">
                    &nbsp;
                    </td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style26">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="DEMORA:"></asp:Label>
                </td>
                <td class="auto-style28">
                    <asp:Label ID="lblDEMORA_MINUTO" runat="server" Font-Names="Tahoma" Font-Size="14px" Text="Label" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style26" >
                    <asp:Label ID="Label4" runat="server" Text="Nº PEDIDO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                </td>
                <td class="auto-style25" colspan="2">
                    <asp:Label ID="lblNUM_PEDIDO" runat="server" Text='<%# Eval("ID_PEDIDO") %>' Font-Names="Tahoma" Font-Size="14px" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td style="text-align: left" class="auto-style26">
                    <asp:Label ID="Label1" runat="server" Text="CLIENTE:" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:Label ID="lblCLIENTE" runat="server" Text='<%# Eval("CLIENTE") %>' Font-Names="Tahoma" Font-Size="14px" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td style="text-align: center; text-align:left" class="auto-style26">
                    <asp:Label ID="Label5" runat="server" Text="# DOC VENTA" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                </td>
                <td style="text-align: left" colspan="2">
                    &nbsp;<asp:Label ID="lblSERIE" runat="server" Font-Bold="True" Font-Size="14px" Text='<%# Eval("V_SERIE") %>'></asp:Label>
                    &nbsp;-
                    <asp:Label ID="lblNUM_DOCVENTA" runat="server" Text='<%# Eval("NUMERO") %>' Font-Names="Tahoma" Font-Size="14px" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td colspan="3" style="text-align: center">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td colspan="3">
                    <asp:GridView ID="dgvDETALLE_PEDIDO" runat="server" AutoGenerateColumns="False" Width="262px" OnRowCommand="dgvDETALLE_PEDIDO_RowCommand" OnRowDataBound="dgvDETALLE_PEDIDO_RowDataBound" Font-Bold="True" Font-Names="Tahoma">
                        <Columns>
                            <asp:TemplateField HeaderText="DESP">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgATENDER" runat="server" CommandName="cmdATENDER" Height="28px" ImageUrl="~/ICONOS/DESPACHAR.png" Width="35px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID_BIEN" HeaderText="ID" >
                            <ControlStyle Width="1px" />
                            <FooterStyle Width="1px" />
                            <HeaderStyle Width="1px" />
                            <ItemStyle Font-Size="5px" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CANTIDAD" HeaderText="CANT" >
                            <ItemStyle Width="40px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="BIEN" >
                            <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEM" HeaderText="I">
                            <ItemStyle Font-Size="5px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_PREPARADO" HeaderText="HORA" DataFormatString="{0:t}" >
                            <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_DESPACHADO" HeaderText="FECHA_DESPACHADO" Visible="False" />
                            <asp:TemplateField HeaderText="PREP">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgESTADO" runat="server" CommandName="cmdESTADO" Height="28px" ImageUrl="~/ICONOS/PREPARANDO.png" Width="27px" />
                                </ItemTemplate>
                                <ControlStyle Font-Overline="False" />
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style13" colspan="3">
                    <asp:Button ID="btnLISTO" runat="server" CommandName="cmdDESPACHAR_TODO" Text="DESPACHAR TODO" Width="167px" />
                </td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25" colspan="2">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
            </tr>
        </table>
    </ItemTemplate>
    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
</asp:DataList>

    


</asp:Content>
