<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MOVIMIENTO_CAJA.aspx.cs" Inherits="DIONYS_ERP.FRM_MOVIMIENTO_CAJA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 20%;
        }
        .auto-style7 {
            width: 1%;
        }
        .auto-style9 {
            width: 158px;
        }
        .auto-style14 {
        }
        .auto-style15 {
            height: 30px;
        }
        .auto-style16 {
            width: 41%;
        }
        .auto-style17 {
            width: 20%;
            height: 34px;
        }
        .auto-style18 {
            height: 34px;
        }
        .auto-style19 {
            width: 41%;
            height: 34px;
            text-align: right;
        }
        .auto-style20 {
            width: 1%;
            height: 34px;
        }
        .auto-style22 {
            width: 174px;
        }
        .auto-style23 {
            width: 174px;
            height: 30px;
        }
        .auto-style24 {
            width: 174px;
            height: 38px;
        }
        .auto-style25 {
            height: 38px;
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

    <!--- ESTO ES PARA MI ENCABEZADO DONDE SOLO IRA EL NOMBRE DEL FORMULARIO EN DONDE SE ESTA -->
    <div>
        
        <table class="auto-style1">
            <tr>
                <td style="text-align:left">
                <asp:Label ID="Label15" runat="server" Text=".:. MOVIMIENTOS DE CAJA" Font-Bold="True" Font-Names="Tahoma" Font-Size="20px" ForeColor="White" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        
    </div>
    <!-- ==================================================================================== -->
    <div>
        <table class="auto-style1">
        <tr>
            <td class="auto-style17">
                <asp:Label ID="Label1" runat="server" Text="ID MOVIMIENTO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style18" colspan="4">
                <asp:TextBox ID="txtID_MOVIMIENTO" runat="server" Width="150px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
            </td>
            <td class="auto-style18" colspan="3">
                &nbsp;</td>
            <td class="auto-style18" colspan="2">
                &nbsp;</td>
            <td class="auto-style19">
                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#FF9900" style="text-align: right" Text="CODIGO AUTORIZACION"></asp:Label>
            </td>
            <td class="auto-style20">
                <asp:TextBox ID="txtCODANULACION" runat="server" BackColor="#FF9933" TextMode="Password" Width="198px"></asp:TextBox>
            </td>
            <td class="auto-style20"></td>
            <td class="auto-style20">
                <asp:Button ID="btnNUEVO" runat="server" Height="30px" Text="NUEVO" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnNUEVO_Click" />
            </td>
            <td class="auto-style20"></td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label2" runat="server" Text="FECHA" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style9" colspan="3">
                <asp:TextBox ID="txtFECHA" runat="server" Width="200px" Font-Names="Tahoma" Font-Size="14px" TextMode="DateTime"></asp:TextBox>
            </td>
            <td class="auto-style9" colspan="3">
                &nbsp;</td>
            <td class="auto-style9" colspan="3">
                &nbsp;</td>
            <td class="auto-style16">
                <asp:Label ID="Label5" runat="server" Text="FECHA ANULADO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style7">
                <asp:TextBox ID="txtFECHA_ANULADO" runat="server" Width="200px" Font-Bold="False" Font-Names="Tahoma" Font-Size="14px" TextMode="DateTime"></asp:TextBox>
            </td>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnGRABAR" runat="server" Height="30px" Text="GRABAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnGRABAR_Click" OnClientClick="return confirm(&quot;¿DESEA REALIZAR LA OPERACION?&quot;);" />
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label3" runat="server" Text="TIPO MOV" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style9">
                <asp:DropDownList ID="cboTIPO_MOV" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#666666" OnSelectedIndexChanged="cboTIPO_MOV_SelectedIndexChanged1" Width="200px">
                </asp:DropDownList>
            </td>
            <td class="auto-style9" colspan="4">
                <asp:TextBox ID="txtID_DOC" runat="server" Width="111px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
            </td>
            <td class="auto-style9" colspan="4">
                <asp:ImageButton ID="imgBUSCAR_VENTA" runat="server" Height="30px" ImageUrl="~/ICONOS/BUSCAR.png" Width="40px" OnClick="imgBUSCAR_VENTA_Click" />
            </td>
            <td class="auto-style16">
                <asp:Label ID="Label4" runat="server" Text="TIPO PAGO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style7">
                <asp:DropDownList ID="cboTIPO_PAGO" runat="server" Width="200px" Font-Bold="False" Font-Names="Tahoma" Font-Size="14px">
                </asp:DropDownList>
            </td>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnCANCELAR" runat="server" Height="30px" Text="CANCELAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnCANCELAR_Click" />
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style17">
                <asp:Label ID="Label14" runat="server" Text="DATOS VENTA" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style18" colspan="10">
                &nbsp;<asp:TextBox ID="txtPERSONA" runat="server" Width="230px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
                &nbsp;<asp:Label ID="Label19" runat="server" Text="DOC" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            &nbsp;<asp:TextBox ID="txtNUM_DOCUMENTO" runat="server" Width="120px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label17" runat="server" Text="MDA" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            &nbsp;
                <asp:TextBox ID="txtMONEDA" runat="server" Width="43px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
                &nbsp;<asp:Label ID="Label18" runat="server" Text="SALDO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            &nbsp;<asp:TextBox ID="txtSALDO" runat="server" Width="100px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
                <br />
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style20"></td>
            <td class="auto-style20">
                <asp:Button ID="btnANULAR" runat="server" Height="30px" Text="ANULAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnANULAR_Click" />
            </td>
            <td class="auto-style20"></td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label8" runat="server" Text="MONEDA" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td colspan="2">
                <asp:RadioButtonList ID="rdbMONEDA" runat="server" RepeatDirection="Horizontal" Width="200px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" AutoPostBack="True">
                    <asp:ListItem Value="SOLES">SOLES</asp:ListItem>
                    <asp:ListItem Value="DOLARES">DOLARES</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td colspan="6">
                <asp:Label ID="lblCONVERSION" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" style="text-align: center"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="IMPORTE" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMONTO" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style7">
                &nbsp;</td>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnIMPRIMIR" runat="server" Height="30px" Text="IMPRIMIR MOV" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnIMPRIMIR_Click" />
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label7" runat="server" Text="DESCRIPCION" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td colspan="9">
                <asp:TextBox ID="txtDESCRIPCION" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td class="auto-style7">
                <asp:RadioButtonList ID="rdbOPCION_IMPRESION" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" RepeatDirection="Horizontal" Width="203px">
                    <asp:ListItem>PDF</asp:ListItem>
                    <asp:ListItem Selected="True">ETICKET</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnIMPRIMIR_REPORTCAJA" runat="server" Font-Bold="True" Font-Names="Tahoma" OnClick="btnIMPRIMIR_REPORTCAJA_Click" Text="IMPRIMIR CAJA" Width="140px" />
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        </table>
    </div>
    
   

    <div>
        
        <fieldset>
            <legend style="font-family:Tahoma; font-size:14px;color:orange;font-weight:bold">MOVIMIENTOS DE CAJA</legend>
            <table class="auto-style1" >
                        <tr>
                            <td class="auto-style23">
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="ESTADO DOC:" ForeColor="White"></asp:Label>
                            </td>
                            <td class="auto-style15" colspan="6">
                                <asp:RadioButtonList ID="rdbLISTAOPCIONES" runat="server" AutoPostBack="True" Font-Bold="True" Font-Italic="False" Font-Names="Tahoma" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" RepeatDirection="Horizontal" Width="378px" Font-Size="14px" OnSelectedIndexChanged="rdbLISTAOPCIONES_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Activos</asp:ListItem>
                                    <asp:ListItem>Anulados</asp:ListItem>
                                    <asp:ListItem>Todos</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                 
                        <tr>
                            <td class="auto-style24">
                                <asp:DropDownList ID="cboTIPO_BUSQUEDA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" style="margin-top: 0px" Width="150px" OnSelectedIndexChanged="cboTIPO_BUSQUEDA_SelectedIndexChanged">
                                    <asp:ListItem>SELECCIONAR</asp:ListItem>
                                    <asp:ListItem>ID_MOV</asp:ListItem>
                                    <asp:ListItem>DESCRIPCION</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style25" colspan="3">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtDATA_BUSQUEDA" runat="server" Font-Names="Tahoma" Font-Size="14px" Width="239px"></asp:TextBox>
                            </td>
                            <td class="auto-style25" colspan="3">
                <asp:Button ID="btnBUSCAR" runat="server" Height="30px" Text="BUSCAR" Width="130px" Font-Bold="True" Font-Names="Tahoma" style="text-align: center; margin-left: 0px" OnClick="btnBUSCAR_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style22">&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style22">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="TOTAL SOLES:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox  ID="txtTOTALSOLES" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="TOTAL DOLARES:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTOTALDOLARES" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="TOTAL S/."></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTOTALCAJA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style22">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style14" colspan="7">
                                <asp:GridView ID="dgvMOV_CAJAKARDEX" runat="server" Font-Names="Tahoma" Font-Size="14px" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="dgvMOV_CAJAKARDEX_SelectedIndexChanged" PageSize="5" SelectedIndex="0">
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
                                        <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE">
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
