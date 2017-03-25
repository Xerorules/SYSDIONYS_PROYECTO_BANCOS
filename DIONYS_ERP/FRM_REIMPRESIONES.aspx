<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_REIMPRESIONES.aspx.cs" Inherits="DIONYS_ERP.FRM_REIMPRESIONES" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 7%;
            height: 32px;
        }
        .auto-style3 {            height: 32px;
        }
        .auto-style4 {
            width: 150px;
            height: 34px;
        }
        .auto-style5 {
            width: 307px;
            height: 34px;
        }
        .auto-style6 {
            width: 34px;
            color: white;
            font-weight:bold;
            height: 34px;
            font-family:Tahoma;
        }
        .auto-style7 {
            width: 44%;
        }
        .auto-style9 {
            width: 2%;
        }
        .auto-style10 {
            width: 4px;
        }
        .auto-style11 {
            width: 45%;
        }
        .auto-style12 {
            width: 100%;
            text-align: right;
            color:white;
            font-weight:bold;
            height: 32px;
            font-family:Tahoma;
        }
        .auto-style13 {
            width: 2%;
            height: 32px;
        }
        .tipoLetra{
            font-family:Tahoma;
        }
        .auto-style14 {
            width: 82px;
        }
        .auto-style15 {
            width: 82px;
            height: 34px;
        }
        .auto-style16 {
            height: 34px;
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

    <div> <!-- INICIO TABLA 1 -->

        <fieldset>
            <legend style="font-family:Tahoma;color:orange;font-weight:bold " >.:. VER DOCUMENTO</legend>
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    </td>
                <td class="auto-style3" colspan="2">
                    <asp:RadioButtonList ID="rdbLISTAOPCIONES" runat="server" AutoPostBack="True" Font-Bold="True" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" OnSelectedIndexChanged="rdbLISTAOPCIONES_SelectedIndexChanged" RepeatDirection="Horizontal" Width="436px" style="margin-top: 0px">
                        <asp:ListItem Selected="True">Activos</asp:ListItem>
                        <asp:ListItem>Anulados</asp:ListItem>
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style12">CLAVE DE AUTORIZACIÓN PARA ANULACIÓN:</td>
                <td class="auto-style13">
                    <asp:TextBox ID="txtCLAVE_AUTO" runat="server" Width="171px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
        </table>

        </fieldset>
     </div> <!-- FIN TABLA 1 -->

    <div> <!-- INICIO TABLA 2 -->
        
        <fieldset>
            <legend style="font-family: Tahoma;color:orange;font-weight:bold">.:. BUSQUEDA</legend>
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style14">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">POR:</td>
                <td class="auto-style4"><asp:DropDownList ID="cboV_OPCIONBUSCAR" runat="server" Height="22px" Width="150px" Font-Bold="True" Font-Italic="False" Font-Names="Tahoma" Font-Size="Medium">
                    <asp:ListItem>-- TODOS --</asp:ListItem>
                    <asp:ListItem Value="ID_VENTA">CODIGO VENTA</asp:ListItem>
                    <asp:ListItem Value="NUMERO">NUMERO DOCUMENTO</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="auto-style5"><asp:TextBox ID="txtBUSCARVENTA" runat="server" Width="299px"></asp:TextBox></td>
                <td class="auto-style15">&nbsp;<asp:Button ID="btnBUSCARVENTA" runat="server" Text="FILTRAR" OnClick="btnBUSCARVENTA_Click" /></td>
                <td class="auto-style16"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style14">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
      </fieldset>

    </div> <!-- FIN TABLA 2 -->
    
    

    <fieldset>
        <legend style="font-family:Tahoma ;color:orange;font-weight:bold">.:. RESULTADOS</legend>
    <div> <!-- INICIO TABLA 3 - GRIDVIEW -->
        <table class="auto-style1">
            <tr>
                <asp:GridView ID="dgvLISTADOVENTAS" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" DataKeyNames="ID_VENTA,NUMERO,FECHA,TOTAL,FECHA_ANULADO" OnRowCommand="dgvLISTADOVENTAS_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID_VENTA" HeaderText="CODIGO" />
                        <asp:BoundField DataField="NUMERO" HeaderText="NUMERO DOC" />
                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
                        <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" />
                        <asp:BoundField DataField="FECHA_ANULADO" HeaderText="ANULADO" />
                        <asp:TemplateField HeaderText="IMPRIMIR">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgIMPRIMIR" runat="server" CommandName="cmdIMPRIMIR" Height="31px" ImageUrl="~/ICONOS/IMPRIMIR.png" Width="30px" />
                            </ItemTemplate>
                            <ControlStyle Font-Strikeout="False" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ANULAR">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgANULAR" runat="server" CommandName="cmdANULAR" Height="33px" ImageUrl="~/ICONOS/ANULAR.png" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>
            </tr>
        </table>
    </div><!-- FIN TABLA 3 - GRIDVIEW -->
    </fieldset>
</asp:Content>
