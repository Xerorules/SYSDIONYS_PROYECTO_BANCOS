<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="Frm_CONTROLGALERIA_ESTADOSTIENDAS.aspx.cs" Inherits="DIONYS_ERP.Frm_CONTROLGALERIA_ESTADOSTIENDAS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style6 {
            width: 100%;
            text-align: center;
        }
        .auto-style8 {
            height: 24px;
        }
    </style>

    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div>
        <fieldset>
            <h2 style="font-family:Tahoma; font-weight:bold ; color: white">ESTADO DE TIENDAS</h2>


        </fieldset>
    </div>

    <div>
        <fieldset> 

            <table class="auto-style1" style="background-color:steelblue;border-color:white ">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="SELECCIONAR GALERIA"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="PROPIETARIO"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="TIENDA O STAND"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:RadioButtonList ID="rdbLISTAGALERIAS" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Tahoma" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" Width="449px" style="text-align: left" OnSelectedIndexChanged="rdbLISTAGALERIAS_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="GG">GALERIA VIRGEN GUADALUPE</asp:ListItem>
                                    <asp:ListItem Value="GM">GALERIA VIRGEN NUESTRA SEÑORA LA MERCED</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: center">
                                <asp:DropDownList ID="cboPROPIETARIOS" runat="server" Height="26px" Width="279px" Font-Bold="True" Font-Italic="True" Font-Names="Tahoma" OnSelectedIndexChanged="cboPROPIETARIOS_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center">
                                <asp:DropDownList ID="cboTIENDAS" runat="server" Height="26px" Width="259px" Font-Bold="True" Font-Italic="True" Font-Names="Tahoma">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center">
                                <asp:ImageButton ID="imgBtnCONSULTAR" runat="server" AlternateText="CONSULTAR" Height="56px" ImageAlign="Baseline" ImageUrl="~/ICONOS/BUSCAR.png" OnClick="imgBtnCONSULTAR_Click" Width="84px" />
                            </td>
                        </tr>
                        </table>

            <table class="auto-style1">
                <tr>
                    <td class="auto-style6">
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">
        
        
        
                    </td>
                </tr>
             </table>

        </fieldset>
    </div>

    <div>

          
           

            <asp:GridView ID="gvDATOSCONTROL_ESTADOGALERIA" runat="server" CellPadding="3" GridLines="None" Width="100%" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="10pt" Font-Bold="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" DataKeyNames="CGCODIGO,CGTIENDA,CGCLIENTE,CGPROPIETARIO,DISPONIBLE" OnRowDataBound="gvDATOSCONTROL_ESTADOGALERIA_RowDataBound" OnRowCancelingEdit="gvDATOSCONTROL_ESTADOGALERIA_RowCancelingEdit" OnRowEditing="gvDATOSCONTROL_ESTADOGALERIA_RowEditing" OnRowUpdating="gvDATOSCONTROL_ESTADOGALERIA_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="CGCODIGO" HeaderText="CODIGO" ReadOnly="True" />
                    <asp:BoundField DataField="CGTIENDA" HeaderText="TIENDA" ReadOnly="True" />
                    <asp:BoundField DataField="CGCLIENTE" HeaderText="CLIENTE" ReadOnly="True" />
                    <asp:BoundField DataField="CGPROPIETARIO" HeaderText="PROPIETARIO" ReadOnly="True" />
                    <asp:CheckBoxField DataField="DISPONIBLE" HeaderText="DISPONIBLE" >
                    <HeaderStyle Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:CheckBoxField DataField="OCUPADO" HeaderText="OCUPADO" >
                    <HeaderStyle Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:CheckBoxField DataField="MANTENIMIENTO" HeaderText="MANTENIMIENTO" >
                    <HeaderStyle Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:CommandField ShowEditButton="true" ButtonType="Image" CancelImageUrl="~/ICONOS/SALIR.png" EditImageUrl="~/ICONOS/EDITAR.png" UpdateImageUrl="~/ICONOS/ACTUALIZAR.png" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <EditRowStyle Font-Names="Tahoma" />
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="#E7E7FF" Font-Names="Tahoma" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" Height="50px" Font-Names="Tahoma" ForeColor="Black" Font-Bold="False" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>

          
           

    </div>

</asp:Content>
