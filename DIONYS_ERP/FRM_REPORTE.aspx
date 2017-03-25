<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_REPORTE.aspx.cs" Inherits="DIONYS_ERP.FRM_REPORTE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <legend style="font-family:Tahoma;color: aquamarine;font-weight:bold">.:. VER DOCUMENTO</legend>
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
                    &nbsp;</td>
                <td class="auto-style3" colspan="2">
                    <asp:RadioButtonList ID="rdbLISTAOPCIONES" runat="server" AutoPostBack="True" Font-Bold="True" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" OnSelectedIndexChanged="rdbLISTAOPCIONES_SelectedIndexChanged" RepeatDirection="Horizontal" Width="436px" Font-Size="14px">
                        <asp:ListItem>Activos</asp:ListItem>
                        <asp:ListItem>Anulados</asp:ListItem>
                        <asp:ListItem Selected="True">Todos</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
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
     </div>

    <div> <!-- INICIO TABLA 2 -->
        
        <fieldset>
            <legend style="font-family:Tahoma;color: aquamarine;font-weight:bold">.:. BUSQUEDA</legend>
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style12">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="FECHA INICIAL"></asp:Label>
                </td>
                
                <td class="auto-style6">
                    <asp:TextBox ID="txtFECHAINI" runat="server" Font-Names="Tahoma"></asp:TextBox>
                </td>
                
                <td class="auto-style6">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="FECHA FINAL"></asp:Label>
                </td>
                <td class="auto-style5"><asp:TextBox ID="txtFECHAFINAL" runat="server" Width="140px" Font-Names="Tahoma"></asp:TextBox></td>
                <td>&nbsp;<asp:Button ID="btnFILTRARVENTA" runat="server" Text="FILTRAR" OnClick="btnBUSCARVENTA_Click" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" /></td>
                <td>&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style12">

                    <asp:Button ID="btnIMPRIMIR" runat="server" Text="IMPRIMIR REPORTE" Width="152px" OnClick="btnIMPRIMIR_Click" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" />

                </td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td colspan="2" class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style14"></td>
                <td class="auto-style8"></td>
            </tr>
        </table>
      </fieldset>

    </div> <!-- FIN TABLA 2 -->

    <div>
        <fieldset >
            <legend style="font-family:Tahoma;color: aquamarine;font-weight:bold">.:. RESULTADOS</legend>

            <asp:GridView ID="dgvLISTARVENTAS" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="dgvLISTARVENTAS_PageIndexChanging" OnSelectedIndexChanged="dgvLISTARVENTAS_SelectedIndexChanged" Font-Names="Tahoma" Font-Size="14px" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID_VENTA" HeaderText="CODIGO" />
                    <asp:BoundField DataField="TIPO_DOC" HeaderText="TIPO DOC" />
                    <asp:BoundField DataField="SERIE" HeaderText="SERIE" />
                    <asp:BoundField DataField="NUMERO" HeaderText="NUMERO" />
                    <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
                    <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" />
                    <asp:BoundField DataField="VALOR_VENTA" HeaderText="SUB TOTAL" />
                    <asp:BoundField DataField="IGV" HeaderText="IGV" />
                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" />
                    <asp:BoundField DataField="FECHA_ANULADO" HeaderText="ANULADO" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

        </fieldset>
    </div>
</asp:Content>
