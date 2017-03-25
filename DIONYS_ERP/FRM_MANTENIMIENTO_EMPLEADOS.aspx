<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_EMPLEADOS.aspx.cs" Inherits="DIONYS_ERP.FRM_MANTENIMIENTO_EMPLEADOS" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-family:Tahoma;
            text-align: left;
            width:100%;
        }
        .auto-style4 {
            width: 10%;
        }
        .auto-style5 {
            width: 17%;
        }
        .auto-style7 {
            width: 10%;
            text-align: center;
        }
        .auto-style8 {
            width: 10%;
        }
        .auto-style9 {
            font-family: Tahoma;
            text-align: left;
        }
        .auto-style10 {
            width: 308px;
        }
        .auto-style11 {
            font-family: Tahoma;
            text-align: left;
            height: 23px;
        }
        .auto-style12 {
            width: 308px;
            height: 23px;
        }
        .auto-style13 {
            height: 23px;
        }
    </style>
    <link href="ESTILOS/ESTILOS_FRM_MANT_EMPLEADOS.css" rel="stylesheet" />
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
        
        <table class="auto-style1">
            <tr>
                <td style="text-align:left">
                <asp:Label ID="Label15" runat="server" Text=".:. MANTENIMIENTO PERSONAL .:." Font-Bold="True" Font-Names="Tahoma" Font-Size="20px" ForeColor="White" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        
    </div>
    <!-- CUADRO NUMERO 01 -->
    <div id="DIV_MANT_TRAB">
        <fieldset>
            <legend style="color:#CC9900;font-weight:bold;font-family:Tahoma;font-size:14px">DATOS OBLIGATORIOS</legend>
            <table id="TABLA01">
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style10"></td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="White" Text="NOMBRES"></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtE_NOMBRES" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="TELEFONO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtE_TELEFONO" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="CARGO"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboE_CARGO" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="APELLIDOS"></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtE_APELLIDOS" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="MOVIL"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtE_MOVIL" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="AREA"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboE_AREA" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="DNI/USUARIO"></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtE_DNI" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="EMAIL"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtE_EMAIL" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="SEDE"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboE_SEDE" runat="server" Width="200px" OnSelectedIndexChanged="cboSEDE_TRAB_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="CONTRASEÑA"></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtE_CONTRASENA" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="DEPARTAMENTO"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboE_DEPARTAMENTO" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="cboE_DEPARTAMENTO_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="DIRECCIÓN"></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtE_DIRECCION" runat="server" Width="296px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="PROVINCIA"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboE_PROVINCIA" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="cboE_PROVINCIA_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="FECHA NAC."></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtE_FECHANAC" runat="server" Width="148px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="DISTRITO"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboE_DISTRITO" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style10">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1" colspan="6" style="text-align:center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnE_GUARDAR" runat="server" OnClick="btnE_GUARDAR_Click" Text="AGREGAR" Width="120px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" />
&nbsp;
                        <asp:Button ID="btnE_EDITAR" runat="server" OnClick="btnE_EDITAR_Click" Text="MODIFICAR" Width="120px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" />
&nbsp;
                        <asp:Button ID="btnE_ELIMINAR" runat="server" Text="ELIMINAR" Width="120px" OnClick="btnE_ELIMINAR_Click" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" />
&nbsp;
                        <asp:Button ID="btnE_LIMPIAR" runat="server" OnClick="btnE_LIMPIAR_Click" Text="LIMPIAR" Width="120px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11"></td>
                    <td class="auto-style12">
                        </td>
                    <td class="auto-style13">
                        </td>
                    <td class="auto-style13"></td>
                    <td class="auto-style13"></td>
                    <td class="auto-style13"></td>
                </tr>
                </table>
        </fieldset>
    </div>

    <!-- TABLA 02 -->
    <div id="DIV_LISTA_TRAB">
        <fieldset>
            <legend style="color:#CC9900;font-weight:bold;font-family:Tahoma;font-size:14px">LISTA DE PERSONAL</legend>
            <table id="TABLA02">
                <tr>
                    <td class="auto-style7">
                        <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="16px" ForeColor="White" Text="FILTRAR POR :"></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="cboBUSCAR" runat="server" Width="200px" Font-Bold="True" Font-Names="Tahoma">
                            <asp:ListItem>-- TODOS --</asp:ListItem>
                            <asp:ListItem>APELLIDOS</asp:ListItem>
                            <asp:ListItem Value="DNI_USUARIO">DNI/USUARIO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtFILTRO_DATA" runat="server" Width="242px" Font-Names="Tahoma" Font-Size="14px"></asp:TextBox>
                    </td>
                    <td class="auto-style5">
                        <asp:Button ID="btnBUSCAR" runat="server" Text="BUSCAR" Width="100px" OnClick="btnBUSCAR_Click" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="dgvLISTAR_EMPLEADOS" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Names="Tahoma" Font-Size="Small" AutoGenerateSelectButton="True" OnSelectedIndexChanged="dgvLISTAR_EMPLEADOS_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="ID_EMPLEADO" HeaderText="CODIGO" />
                                <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRES" />
                                <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" />
                                <asp:BoundField DataField="DNI_USUARIO" HeaderText="DNI/USUARIO" />
                                <asp:BoundField DataField="CONTRASEÑA" HeaderText="CONTRASEÑA" />
                                <asp:BoundField DataField="DIRECCION" HeaderText="DIRECCION" />
                                <asp:BoundField DataField="FECHA_NAC" HeaderText="F. NAC" />
                                <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" />
                                <asp:BoundField DataField="MOVIL" HeaderText="MOVIL" />
                                <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />
                                <asp:BoundField DataField="DISTRITO" HeaderText="DISTRITO" />
                                <asp:BoundField DataField="CARGO" HeaderText="CARGO" />
                                <asp:BoundField DataField="SEDE" HeaderText="SEDE" />
                                <asp:BoundField DataField="AREA" HeaderText="AREA" />
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
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5"></td>
                </tr>
             </table>
        </fieldset>
    </div>

</asp:Content>
