<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_REPORTE_BIEN.aspx.cs" Inherits="DIONYS_ERP.FRM_REPORTE_BIEN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

    <div> <!-- INICIO TABLA 2 -->
        
        <fieldset>
            <legend style="font-family:Tahoma">.:. BUSQUEDA</legend>
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
                <td class="auto-style6">FECHA INICIAL:</td>
                
                <td class="auto-style6">
                    <asp:TextBox ID="txtFECHAINI" runat="server" Font-Names="Tahoma"></asp:TextBox>
                </td>
                
                <td class="auto-style6">FECHA FINAL :</td>
                <td class="auto-style5"><asp:TextBox ID="txtFECHAFINAL" runat="server" Width="140px" Font-Names="Tahoma"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style12">

                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">CLASE BIEN&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;</td>
                <td colspan="2" class="auto-style4">
                    <asp:DropDownList ID="cboCLASEBIEN" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style14"></td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td colspan="2" class="auto-style4"><asp:Button ID="btnFILTRARBIEN" runat="server" Text="FILTRAR" OnClick="btnFILTRARBIEN_Click" /></td>
                <td class="auto-style4">

                    <asp:Button ID="btnIMPRIMIR" runat="server" Text="IMPRIMIR REPORTE" Width="152px" OnClick="btnIMPRIMIR_Click" />

                </td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style14">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
            </tr>
        </table>
      </fieldset>

    </div> <!-- FIN TABLA 2 -->



    <div>
        <fieldset >
            <legend>.:. RESULTADOS</legend>
            <table>
                <tr>
                    <td>
                       
                        TOTAL:
                       
                    </td>
                    <td>
                       
                        <asp:Label ID="lblTOTCANTIDAD" runat="server" Text="0.00"></asp:Label>
                       
                    </td>
                    <td>
                       
                        <asp:Label ID="LBLTOTAL" runat="server" Text="0.00" ></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="3">

                        <asp:GridView ID="dgvLISTARBIENES" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID_BIEN" HeaderText="CODIGO" />
                            <asp:BoundField DataField="CLASE" HeaderText="CLASE" Visible="False" />
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="BIEN" />
                            <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" />
                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" />
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
                    </td>
                </tr>
                
                
                            
            </table>
        </fieldset>
    </div>
</asp:Content>
