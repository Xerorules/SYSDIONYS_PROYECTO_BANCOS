<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CUENTAS_POR_PAGAR.aspx.cs" Inherits="DIONYS_ERP.FRM_CUENTAS_POR_PAGAR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>

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
    <%--<div>
        <fieldset>
            <legend style="font-family:Tahoma;font-size:14PX;color:orange;font-weight:bold">DATOS</legend>
            <table class="auto-style1">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>--%>
    <div>
        <fieldset>
            <legend style="font-family:Tahoma;font-size:14PX;color:orange;font-weight:bold">FILTROS</legend>
            
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="FECHA INICIAL" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFECHA_INI" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="160px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="FECHA FINAL" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFECHA_FIN" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="160px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="DESCRIPCION / PROVEEDOR" ForeColor="White"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPROVEEDOR" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="366px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="RUC /DNI" ForeColor="White"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtRUC_DNI" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="284px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="ESTADO" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="NUMERO" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="ESTADO SALDOS:" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="TIPO DOC" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="MONEDA" ForeColor="White"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cboESTADO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="150px">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>ACTIVOS</asp:ListItem>
                            <asp:ListItem>ANULADOS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtNUMERO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="160px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboESTADO_SALDO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>PENDIENTES</asp:ListItem>
                            <asp:ListItem>PAGADOS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTIPODOC" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem Value="AA">TODOS</asp:ListItem>
                            <asp:ListItem Value="FT">FACTURA</asp:ListItem>
                            <asp:ListItem Value="BV">BOLETA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboMONEDA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="200px">
                            <asp:ListItem>TODOS</asp:ListItem>
                            <asp:ListItem>SOLES</asp:ListItem>
                            <asp:ListItem>DOLARES</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnBUSCAR" runat="server" Text="BUSCAR" OnClick="btnBUSCAR_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="dgvCOMPRAS" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CellPadding="4" Font-Names="Tahoma" Font-Size="10px" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="dgvCOMPRA_SelectedIndexChanged" Width="100%" AllowPaging="True" OnPageIndexChanging="dgvCOMPRA_PageIndexChanging" DataKeyNames="C_ID_COMPRA,C_SERIE,C_NUMERO,C_TIPO_DOC,C_FECHA,C_MONEDA,C_VALOR_VENTA,C_IGV,C_TOTAL,C_SALDO,C_FECHA_ANULADO,C_OBSERVACIONES,C_ID_PROVEEDOR,P_DESCRIPCION,P_RUC_DNI,P_DIRECCION,P_TELEFONO_1">
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
                                <asp:BoundField DataField="C_ID_PROVEEDOR" HeaderText="ID PROVEEDOR" />
                                <asp:BoundField DataField="P_DESCRIPCION" HeaderText="NOMBRE PROV" />
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
                    <td colspan="8">

                    </td>
                </tr>
                </table>
              </fieldset>
            </div>
            
            <div>
                <fieldset>
                    <legend style="font-family:Tahoma;font-size:14PX;color:orange;font-weight:bold; width: 154px;">DETALLE DE COMPRAS</legend>

                    <table class="auto-style1">
                        <tr>
                            <td>

                                <asp:GridView ID="dgvDETALLE_COMPRA" runat="server" Font-Names="Tahoma" Font-Size="10px" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" PageSize="5" SelectedIndex="0">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="CD_ID_COMPRA" HeaderText="ID COMPRA">
                                        <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="B_DESCRIPCION" HeaderText="PRODUCTO">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CD_CANTIDAD" HeaderText="CANTIDAD" />
                                        <asp:BoundField DataField="CD_PRECIO" HeaderText="PRECIO" />
                                        <asp:BoundField DataField="CD_IMPORTE" HeaderText="IMPORTE" />
                                        <asp:BoundField DataField="UM_DESCRIPCION" HeaderText="UNIDAD MEDIDA">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CB_DESCRIPCION" HeaderText="CLASE BIEN">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TB_DESCRIPCION" HeaderText="TIPO BIEN">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CD_SALDO_CANTIDAD" HeaderText="SALDO CANTIDAD">
                                        </asp:BoundField>
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
                        </tr>
                    </table>

                </fieldset>
            </div>

            <div>
             <fieldset>
                 <legend style="font-family:Tahoma;font-size:14PX;color:orange;font-weight:bold">DETALLE DE COBROS</legend>
            <table class="auto-style1">
                <tr>
                    <td>

                                <asp:GridView ID="dgvDETALLE_COBROS" runat="server" Font-Names="Tahoma" Font-Size="10px" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" PageSize="5" SelectedIndex="0">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="ID_MOVIMIENTO" HeaderText="MOVIMIE">
                                        <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA" HeaderText="FECHA">
                                        <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION">
                                        <ItemStyle Width="170px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TP_DESCRIPCION" HeaderText="T. PAGO" />
                                        <asp:BoundField DataField="TM_DESCRIPCION" HeaderText="T. MOV" />
                                        <asp:BoundField DataField="MONEDA" HeaderText="MON" />
                                        <asp:BoundField DataField="IMPORTE" HeaderText="PAGADO">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_CAMBIO" HeaderText="TC">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AMORTIZADO" HeaderText="AMORT" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IMPORTE_CAJA" HeaderText="IMPORTE SOLES">
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID_CAJA" HeaderText="ID_CAJA" Visible="False" />
                                        <asp:BoundField DataField="FECHA_INICIAL" HeaderText="FECHA_INICIAL" Visible="False" />
                                        <asp:BoundField DataField="FECHA_CIERRE" HeaderText="FECHA_CIERRE" Visible="False" />
                                        <asp:BoundField DataField="SALDO_INICIAL" HeaderText="SALDO_INICIAL" Visible="False" />
                                        <asp:BoundField DataField="SALDO_FINAL" HeaderText="SALDO_FINAL" Visible="False" />
                                        <asp:BoundField DataField="EMPLEADO" HeaderText="EMPLEADO" Visible="False" />
                                        <asp:BoundField DataField="PV_DESCRIPCION" HeaderText="PV_DESCRIPCION" Visible="False" />
                                        <asp:BoundField DataField="S_DESCRIPCION" HeaderText="S_DESCRIPCION" Visible="False" />
                                        <asp:BoundField DataField="FECHA_ANULADO" HeaderText="ANULADO" />
                                        <asp:BoundField DataField="ID_COMPVENT" HeaderText="COMPRA/VENTA">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
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
            </table>
            
        </fieldset>
    </div>
</asp:Content>
