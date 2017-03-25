<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CONTROLGALERIA.aspx.cs" Inherits="DIONYS_ERP.FRM_CONTROLGALERIA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 101px;
        }
        
        .auto-style5 {
        }
        .auto-style9 {
            height: 27px;
            width: 30px;
        }
        .auto-style12 {
            height: 27px;
            width: 82px;
        }
        .auto-style15 {
            height: 27px;
            width: 107px;
        }
        .auto-style18 {
            width: 30px;
        }
        .auto-style20 {
            width: 82px;
        }
        .auto-style21 {
            width: 87px;
        }
        .auto-style23 {
            width: 107px;
        }
        .auto-style24 {
            height: 20px;
            width: 76px;
        }
        .auto-style25 {
            height: 27px;
            width: 76px;
        }
        .auto-style26 {
            height: 20px;
        }
        .auto-style27 {
            width: 87px;
            height: 20px;
        }
        .auto-style28 {
            height: 20px;
        }
                        
    </style>
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    <h2 style="color:white;font-family: Tahoma ">CONTROL DE TIENDAS GALERIA</h2>
    <div>
     <fieldset>
            <legend style="outline-color:white;color:white;font-weight:bold;font-family:Tahoma">FILTROS</legend>
                    <table class="auto-style1" style="background-color:steelblue;border-color:white ">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="PERIODO"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="SELECCIONAR GALERIA"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="PROPIETARIO"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="TIENDA O STAND"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="CONDICIÓN"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:RadioButtonList ID="rdbPERIODO" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Tahoma" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="150px" ForeColor="White" style="text-align: center" OnSelectedIndexChanged="rdbLISTAGALERIAS_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="2016" Selected="True">2016</asp:ListItem>
                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: center">
                                <asp:RadioButtonList ID="rdbLISTAGALERIAS" runat="server" Font-Bold="True" Font-Italic="True" Width="450PX" Font-Names="Tahoma" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" style="text-align: left" OnSelectedIndexChanged="rdbLISTAGALERIAS_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="GG">GALERIA VIRGEN GUADALUPE</asp:ListItem>
                                    <asp:ListItem Value="GM">GALERIA VIRGEN NUESTRA SEÑORA LA MERCED</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: center">
                                <asp:DropDownList ID="cboPROPIETARIOS" CssClass="form form-control" runat="server"  Font-Bold="True" Width="250PX" Font-Italic="True" Font-Names="Tahoma" OnSelectedIndexChanged="cboPROPIETARIOS_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center">
                                <asp:DropDownList ID="cboTIENDAS" CssClass="form form-control" runat="server"  Font-Bold="True" Width="250PX" Font-Italic="True" Font-Names="Tahoma">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: center">
                                <asp:DropDownList ID="cboESTADO" CssClass="form form-control" runat="server" Font-Bold="True" Width="150PX" Font-Italic="True" Font-Names="Tahoma" >
                                    <asp:ListItem Value="%">TODOS</asp:ListItem>
                                    <asp:ListItem Value="OCUPADO">OCUPADO</asp:ListItem>
                                    <asp:ListItem Value="DISPONIBLE">DISPONIBLE</asp:ListItem>
                                    <asp:ListItem>MANTENIMIENTO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
         </fieldset>
    </div>
    <div>
        <fieldset>
           

            <table class="auto-style1">
                <tr>
                    <td style="text-align: center" class="auto-style26">
                        </td>
                    <td style="text-align: center" class="auto-style27">
                        </td>
                    <td style="text-align: center" class="auto-style28" colspan="4">
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Tahoma" Font-Size="14pt" ForeColor="White" Text="LEYENDA"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" class="auto-style5" rowspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" AlternateText="CONSULTAR" Height="56px" ImageAlign="Baseline" ImageUrl="~/ICONOS/BUSCAR.png" OnClick="imgBtnCONSULTAR_Click" Width="84px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton2" runat="server" Height="58px" ImageUrl="~/ICONOS/EXPORTAR_EXCEL.png" OnClick="imgBtnEXCEL_Click" Width="83px" />
                        &nbsp;&nbsp;
                        &nbsp;</td>
                    <td style="text-align: center" class="auto-style21" rowspan="2">
                        &nbsp;</td>
                    <td style="text-align: center;background-color:green" class="auto-style23">
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="White" Text="MES PAGADO"></asp:Label>
                    </td>
                    <td style="text-align: center" class="auto-style24">
                        &nbsp;</td>
                    <td style="text-align: center" class="auto-style18">
                        <asp:Image ID="Image3" runat="server" Height="31px" ImageUrl="~/ICONOS/OCUPADO.png" Width="30px" />
                    </td>
                    <td style="text-align: left" class="auto-style20">
                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" style="text-align: center" Text="VACIO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center ;background-color:darkred" class="auto-style15">
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" ForeColor="White" Text="MES EN PROCESO"></asp:Label>
                    </td>
                    <td style="text-align: center" class="auto-style25">
                        &nbsp;</td>
                    <td style="text-align: center" class="auto-style9">
                        <asp:Image ID="Image4" runat="server" Height="27px" ImageUrl="~/ICONOS/VACIO.png" Width="29px" />
                    </td>
                    <td style="text-align: left" class="auto-style12">
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="OCUPADO"></asp:Label>
                    </td>
                </tr>
            </table>
           

        </fieldset>
    </div>
 
    
    <div style="width:100%;overflow-x:scroll">
        <fieldset><asp:Label runat="server" ID="lblREFERENCIA" Font-Bold="True" Font-Size="Medium" ForeColor="#FF9900" ></asp:Label>
           <legend style="outline-color:white;color:white;font-weight:bold;font-family:Tahoma"></legend>

          
           

            <asp:GridView ID="gvDATOSCONTROL_GALERIA" runat="server" CellPadding="3" GridLines="None" Width="100%" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="10pt" OnRowDataBound="gvDATOSCONTROL_GALERIA_RowDataBound" Font-Bold="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" DataKeyNames="CGESTADO,CGTIENDA,CGCLIENTE,CGPROPIETARIO,CGENERO 2016,CGFEBRERO 2016,CGMARZO 2016,CGABRIL 2016,CGMAYO 2016,CGJUNIO 2016,CGJULIO 2016,CGAGOSTO 2016,CGSEPTIEMBRE 2016,CGOCTUBRE 2016,CGNOVIEMBRE 2016,CGDICIEMBRE 2016,CGENERO 2017,CGFEBRERO 2017,CGMARZO 2017,CGABRIL 2017,CGMAYO 2017,CGJUNIO 2017,CGJULIO 2017,CGAGOSTO 2017,CGSEPTIEMBRE 2017,CGOCTUBRE 2017,CGNOVIEMBRE 2017,CGDICIEMBRE 2017" style="margin-top: 0px">
                <Columns>
                    <asp:BoundField DataField="CGCODIGO" HeaderText="CODIGO" />
                    <asp:BoundField DataField="CGTIENDA" HeaderText="TIENDA" />
                    <asp:BoundField DataField="CGCLIENTE" HeaderText="CLIENTE" />
                    <asp:BoundField DataField="CGPROPIETARIO" HeaderText="PROPIETARIO" />
                    <asp:BoundField DataField="CGENERO 2016" HeaderText="ENERO" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGFEBRERO 2016" HeaderText="FEBRERO" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGMARZO 2016" HeaderText="MARZO" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGABRIL 2016" HeaderText="ABRIL" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGMAYO 2016" HeaderText="MAYO">
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGJUNIO 2016" HeaderText="JUNIO" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGJULIO 2016" HeaderText="JULIO" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGAGOSTO 2016" HeaderText="AGOSTO" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGSEPTIEMBRE 2016" HeaderText="SEPTIEMBRE" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGOCTUBRE 2016" HeaderText="OCTUBRE" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGNOVIEMBRE 2016" HeaderText="NOVIEMBRE" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGDICIEMBRE 2016" HeaderText="DICIEMBRE" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGENERO 2017" HeaderText="ENERO" >
                    <ItemStyle Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGFEBRERO 2017" HeaderText="FEBRERO" >
                    <ItemStyle Width="6%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CGMARZO 2017" HeaderText="MARZO" />
                    <asp:BoundField DataField="CGABRIL 2017" HeaderText="ABRIL" />
                    <asp:BoundField DataField="CGMAYO 2017" HeaderText="MAYO" />
                    <asp:BoundField DataField="CGJUNIO 2017" HeaderText="JUNIO" />
                    <asp:BoundField DataField="CGJULIO 2017" HeaderText="JULIO" />
                    <asp:BoundField DataField="CGAGOSTO 2017" HeaderText="AGOSTO" />
                    <asp:BoundField DataField="CGSEPTIEMBRE 2017" HeaderText="SEPTIEMBRE" />
                    <asp:BoundField DataField="CGOCTUBRE 2017" HeaderText="OCTUBRE" />
                    <asp:BoundField DataField="CGNOVIEMBRE 2017" HeaderText="NOVIEMBRE" />
                    <asp:BoundField DataField="CGDICIEMBRE 2017" HeaderText="DICIEMBRE" />
                    <asp:BoundField DataField="CGESTADO" HeaderText="CGESTADO" Visible="False" />
                    <asp:TemplateField HeaderText="ESTADO">
                        <ItemTemplate>
                            <asp:Image ID="imgESTADO" runat="server" Height="31px" Width="33px" />
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle Font-Strikeout="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
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

          
           

        </fieldset>
    </div>

</asp:Content>
