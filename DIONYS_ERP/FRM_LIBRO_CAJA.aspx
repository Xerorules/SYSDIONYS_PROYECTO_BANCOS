<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_LIBRO_CAJA.aspx.cs" Inherits="DIONYS_ERP.FRM_LIBRO_CAJA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style3 {
        width: 241px;
        }
        .auto-style4 {
        height: 23px;
        width: 241px;
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
    <div>
        <fieldset>
            <legend>FILTROS</legend>

            <table class="auto-style1">
                 <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="RANGO FECHA:" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFECHA_INI" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="201px" TextMode="Date"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtFECHA_FIN" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="194px" TextMode="Date"></asp:TextBox>
                    </td>
                    <td rowspan="2">&nbsp;<asp:Button ID="btnBUSCAR" runat="server" Height="50px" Text="BUSCAR" Width="102px" OnClick="btnBUSCAR_Click" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px" ForeColor="#996600" />
                     </td>
                    <td rowspan="2">
                        <asp:ImageButton ID="ImgEXPORTAREXCEL" runat="server" CommandName="cmdEXPORTAR_EXCEL" Height="42px" ImageUrl="~/ICONOS/EXPORTAR_EXCEL.png" OnClick="ImgEXPORTAREXCEL_Click" Width="58px" />
                     </td>
                    <td rowspan="2">
                        &nbsp;</td>
                    <td rowspan="2">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="ID CAJA :" ForeColor="White"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="PUNTO VENTA :" ForeColor="White"></asp:Label>
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="DESCRIPCION MOVIMIENTO :" ForeColor="White"></asp:Label>
                    </td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtID_CAJA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="190px"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="cboPUNTO_VENTA" runat="server" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDESCRIPCION_MOV" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="190px"></asp:TextBox>
                    </td>
                    <td class="auto-style2" colspan="2"></td>
                    <td class="auto-style2" colspan="2"></td>
                    <td class="auto-style2"></td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="ESTADO" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="EMPLEADOS" ForeColor="White"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="MONEDA" ForeColor="White"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="TIPO MOVIMIENTO" ForeColor="White"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="TIPO PAGO" ForeColor="White"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cboESTADO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px" OnSelectedIndexChanged="cboESTADO_SelectedIndexChanged">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>ACTIVOS</asp:ListItem>
                            <asp:ListItem>ANULADOS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboEMPLEADO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="cboMONEDA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>SOLES</asp:ListItem>
                            <asp:ListItem>DOLARES</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="cboTIPOMOV" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem Value="AAA">TODOS</asp:ListItem>
                            <asp:ListItem Value="EPC">EGRESO POR COMPRA</asp:ListItem>
                            <asp:ListItem Value="EVA">EGRESOS VARIOS</asp:ListItem>
                            <asp:ListItem Value="IPV">INGRESO POR VENTA</asp:ListItem>
                            <asp:ListItem Value="IVA">INGRESOS VARIOS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="cboTIPOPAGO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem Value="0000">TODOS</asp:ListItem>
                            <asp:ListItem Value="0001">EFECTIVO</asp:ListItem>
                            <asp:ListItem Value="0002">TARJETA CREDITO</asp:ListItem>
                            <asp:ListItem Value="0003">TARJETA DEBITO</asp:ListItem>
                            <asp:ListItem Value="0004">DEPOSITO BANCARIO</asp:ListItem>
                            <asp:ListItem Value="0005">TRANSFERENCIA BANCARIA</asp:ListItem>
                            <asp:ListItem Value="0006">CHEQUE BANCARIO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="dgvDATOS_LIBRO_CAJAKARDEX" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="10px" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" PageSize="20">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="ID_MOVIMIENTO" HeaderText="ID_MOV" Visible="False" />
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="DESC" />
                                <asp:BoundField DataField="ID_COMPVENT" HeaderText="COD. COM/VTA" />
                                <asp:BoundField DataField="ID_TIPOPAGO" HeaderText="ID_TIPOPAGO" Visible="False" />
                                <asp:BoundField DataField="TP_DESCRIPCION" HeaderText="TP_DESCRIPCION" />
                                <asp:BoundField DataField="ID_TIPOMOV" HeaderText="ID_TIPOMOV" Visible="False" />
                                <asp:BoundField DataField="TM_DESCRIPCION" HeaderText="TM_DESCRIPCION" />
                                <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" />
                                <asp:BoundField DataField="MONEDA" HeaderText="M" />
                                <asp:BoundField DataField="TIPO_CAMBIO" HeaderText="T.CAMBIO" />
                                <asp:BoundField DataField="AMORTIZADO" HeaderText="AMORTIZADO" />
                                <asp:BoundField DataField="ID_CAJA" HeaderText="ID_CAJA" />
                                <asp:BoundField DataField="FECHA_INICIAL" HeaderText="F. INICIAL" />
                                <asp:BoundField DataField="FECHA_CIERRE" HeaderText="F. CIERRE" />
                                <asp:BoundField DataField="SALDO_INICIAL" HeaderText="SALDO_INICIAL" />
                                <asp:BoundField DataField="SALDO_FINAL" HeaderText="SALDO_FINAL" />
                                <asp:BoundField DataField="ID_EMPLEADO" HeaderText="ID_EMPLEADO" Visible="False" />
                                <asp:BoundField DataField="EMPLEADO" HeaderText="EMPLEADO" />
                                <asp:BoundField DataField="ID_PUNTOVENTA" HeaderText="ID_PUNTOVENTA" Visible="False" />
                                <asp:BoundField DataField="PV_DESCRIPCION" HeaderText="PV_DESCRIPCION" />
                                <asp:BoundField DataField="PV_ID_SEDE" HeaderText="PV_ID_SEDE" Visible="False" />
                                <asp:BoundField DataField="S_DESCRIPCION" HeaderText="S_DESCRIPCION" />
                                <asp:BoundField DataField="IMPORTE_CAJA" HeaderText="IMPORTE_CAJA" />
                                <asp:BoundField DataField="FECHA_ANULADO" HeaderText="ANULADO" />
                                <asp:BoundField DataField="FECHA" HeaderText="FECHA" />
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
                    <td colspan="2">&nbsp;</td>
                    <td colspan="2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </fieldset>
    </div>

</asp:Content>
