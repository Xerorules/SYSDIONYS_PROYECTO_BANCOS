<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_RECIBO.aspx.cs" Inherits="DIONYS_ERP.FRM_RECIBO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 19%;
        }
        .auto-style7 {
            width: 1%;
        }
        .auto-style17 {
            width: 19%;
            height: 34px;
        }
        .auto-style18 {
            height: 34px;
        }
        .auto-style20 {
            width: 1%;
            height: 34px;
        }
        .auto-style22 {
            width: 23%;
        }
        .auto-style27 {
            width: 247px;
            height: 34px;
        }
        .auto-style28 {
        }
        .auto-style38 {
            height: 34px;
            width: 138px;
        }
        .auto-style39 {
            width: 18%;
            height: 34px;
        }
        .auto-style40 {
            width: 118px;
        }
        .auto-style41 {
            width: 18%;
        }
        .auto-style42 {
            width: 118px;
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

    <!-- ============================================= INICIO DE CODIGO PARA GENERAR EL AUTOCOMPLETAR ===================================================== -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR DESCRIPCION ============ -->
    <script type="text/javascript">
        $(function () {
            $("[id$=txtV_CLIENTEDESCRIPCION]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/Service.asmx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1],
                                    text: item.split('-')[2],
                                    direc1: item.split('-')[3]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=txtV_IDCLIENTE]").val(i.item.val);
                    $("[id$=txtV_RUC]").val(i.item.text);
                    $("[id$=txtV_DIRECCION]").val(i.item.direc1);
                },
                minLength: 1
            });
        });
    </script>
    
    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR RUC ============ -->
    <script type="text/javascript">
        $(function () {
            $("[id$=txtV_RUC]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/Service2.asmx/clientes_ruc") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1],
                                    texto3: item.split('-')[2],
                                    direc2: item.split('-')[3]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=txtV_IDCLIENTE]").val(i.item.val);
                    $("[id$=txtV_CLIENTEDESCRIPCION]").val(i.item.texto3);
                    $("[id$=txtV_DIRECCION]").val(i.item.direc2);
                },
                minLength: 1
            });
        });
    </script>
    
    <!-- ========= AUTOCOMPLETAR DE PRODUCTOS ============ -->
    <script type="text/javascript">
        $(function () {

            $("[id$=txtPRODUCTO_DESCRIPCION]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/SA_PRODUCTO_XNOMBRE.asmx/productos") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1],
                                    texto3: item.split('-')[2],
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },

                select: function (e, i) {
                    //if ($("[id$=txtPRODUCTO_DESCRIPCION]").mousedown())
                    //{
                    $("[id$=txtPRODUCTO]").val(i.item.val);
                    $("[id$=txtPRECIO]").val(i.item.texto3);
                    ////}else
                    ////{
                    //    $("[id$=txtPRODUCTO]").val("");
                    //    $("[id$=txtPRECIO]").val("");
                    ////}

                },

                minLength: 1
            });
        });
    </script>

    <!-- ============================================= FIN DE CODIGO PARA GENERAR EL AUTOCOMPLETAR ===================================================== -->

    <div>
        
        <table class="auto-style1">
            <tr>
                <td style="text-align:left">
                <asp:Label ID="Label15" runat="server" Text=".:. RECIBO DE VENTA" Font-Bold="True" Font-Names="Tahoma" Font-Size="20px" ForeColor="White" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        
    </div>
    <div>
        <fieldset>
            <legend style="color:orange;font-weight:bold;font-family:Tahoma;font-size:14px">DATOS CLIENTE</legend>
        <table class="auto-style1">
        <tr>
            <td class="auto-style17">
                <asp:Label ID="Label27" runat="server" Text="DESCRIPCION     " Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style27">
                <asp:TextBox ID="txtV_CLIENTEDESCRIPCION" runat="server" Width="389px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" Font-Italic="False" Height="21px">DESCRIPCION DEL CLIENTE</asp:TextBox>
            </td>
            <td class="auto-style38">
                <asp:Label ID="Label6" runat="server" Text="DIRECCIÓN  " Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            &nbsp;</td>
            <td class="auto-style39">
                <asp:TextBox ID="txtV_DIRECCION" runat="server" Width="400px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" Height="22px"></asp:TextBox>
            </td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style20">
                <asp:Button ID="btnV_NUEVO" runat="server" Height="30px" Text="NUEVO" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnV_NUEVO_Click" />
            </td>
            <td class="auto-style20"></td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label16" runat="server" Text="RUC/DNI      " Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            </td>
            <td class="auto-style28">
                <asp:TextBox ID="txtV_RUC" runat="server" Width="255px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" Font-Italic="False" Height="20px">RUC / DNI</asp:TextBox>
            </td>
            <td class="auto-style40">
                &nbsp;<asp:Label ID="Label29" runat="server" Text="TELEFONO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            &nbsp;</td>
            <td class="auto-style41">
                <asp:TextBox ID="txtV_TELEFONO" runat="server" Width="255px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" Font-Italic="False" Height="21px"></asp:TextBox>
            </td>
            <td class="auto-style7">
                &nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnV_GRABAR" runat="server" Height="30px" Text="GRABAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnV_GRABAR_Click" OnClientClick="return confirm(&quot;¿DESEA REALIZAR LA VENTA?&quot;);" />
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label28" runat="server" Text="CODIGO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                </td>
            <td class="auto-style28">
                <asp:TextBox ID="txtV_IDCLIENTE" runat="server" Width="255px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" Font-Italic="False" Height="21px">CODIGO</asp:TextBox>
            </td>
            <td class="auto-style40">
                &nbsp;<asp:Label ID="Label18" runat="server" Text="MONEDA    " Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
            &nbsp;&nbsp;</td>
            <td class="auto-style41">
                <asp:RadioButtonList ID="rdbMONEDA" runat="server" RepeatDirection="Horizontal" Width="200px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White">
                    <asp:ListItem Value="S">SOLES</asp:ListItem>
                    <asp:ListItem Value="D">DOLARES</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="auto-style7">
                &nbsp;</td>
            <td class="auto-style7">
                <asp:Button ID="btnV_CANCELAR" runat="server" Height="37px" Text="CANCELAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnV_CANCELAR_Click" />
            </td>
            <td class="auto-style7">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style17">
                &nbsp;</td>
            <td class="auto-style18">
                &nbsp;</td>
            <td class="auto-style42">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td class="auto-style39">
                &nbsp;</td>
            <td class="auto-style20">
                &nbsp;</td>
            <td class="auto-style20">
                <asp:Button ID="btnV_ANULAR" runat="server" Height="30px" Text="ANULAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnV_ANULAR_Click" OnClientClick="return confirm(&quot;¿DESEA ANULAR LA VENTA?&quot;);" />
            </td>
            <td class="auto-style20"></td>
        </tr>
        </table>
        </fieldset>
        
    </div>

    <div>
        <fieldset>
            <legend style="color:orange;font-weight:bold;font-family:Tahoma;font-size:14px; width: 162px;">DATOS RECIBO</legend>

            <table class="ui-accordion">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                <asp:Label ID="Label25" runat="server" Text="ID VENTA " Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                <asp:TextBox ID="txtV_IDVENTA" runat="server" Width="179px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td><asp:Button ID="btnV_IMPRIMIR" runat="server" Height="30px" Text="IMPRIMIR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnV_IMPRIMIR_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                <asp:DropDownList ID="cboV_TIPO_DOC" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="224px" ForeColor="#666666" AutoPostBack="True" OnSelectedIndexChanged="cboV_TIPO_DOC_SelectedIndexChanged" Font-Italic="False">
                    <asp:ListItem Value="RG">RECIBO GARANTIA</asp:ListItem>
                    <asp:ListItem Value="RA">RECIBO ALQUILER</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboV_SERIE" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Width="93px" ForeColor="#666666" OnSelectedIndexChanged="cboV_SERIE_SelectedIndexChanged" AutoPostBack="True" Font-Italic="False">
                    <asp:ListItem>0001</asp:ListItem>
                    <asp:ListItem>0002</asp:ListItem>
                    <asp:ListItem>0003</asp:ListItem>
                    <asp:ListItem>0004</asp:ListItem>
                    <asp:ListItem>0005</asp:ListItem>
                    <asp:ListItem>0006</asp:ListItem>
                    <asp:ListItem>0007</asp:ListItem>
                    <asp:ListItem>0008</asp:ListItem>
                    <asp:ListItem>0009</asp:ListItem>
                    <asp:ListItem>0010</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtV_NUMERO" runat="server" Width="190px" Font-Names="Tahoma" Font-Size="14px" Font-Bold="True" ForeColor="#666666" ReadOnly="True" Font-Italic="False">NUMERO</asp:TextBox>
                    </td>
                    <td>
                <asp:Label ID="Label17" runat="server" Text="FECHA " Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                <asp:TextBox ID="txtV_FECHA" runat="server" Width="157px" Font-Names="Tahoma" Font-Size="14px" TextMode="DateTime" Font-Bold="True" ForeColor="#666666"></asp:TextBox>
                    </td>
                    <td>
                <asp:Label ID="Label26" runat="server" Text="FECHA ANULADO" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                <asp:TextBox ID="txtV_FECHAANULADO" runat="server" Width="180px" Font-Names="Tahoma" Font-Size="14px" TextMode="DateTime" Font-Bold="True" ForeColor="#666666"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                &nbsp;<asp:Button ID="btnV_CONSULTAR" runat="server" Height="30px" Text="CONSULTAR" Width="140px" Font-Bold="True" Font-Names="Tahoma" OnClick="btnV_CONSULTAR_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
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
    </div>

    <div>
        <fieldset>
            <legend style="color:orange;font-weight:bold;font-family:Tahoma;font-size:14px; width: 162px;">PRODUCTOS</legend>
            <table class="auto-style1">
                <tr>
                    <td>
                <asp:Label ID="Label21" runat="server" Text="PRODUCTO   :" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRODUCTO_DESCRIPCION" runat="server" Width="526px" OnTextChanged="txtPRODUCTO_DESCRIPCION_TextChanged" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#666666"></asp:TextBox>
                        <asp:TextBox ID="txtPRODUCTO" runat="server" Width="100px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#666666">CODIGO</asp:TextBox>
                    </td>
                    <td>
                <asp:Label ID="Label22" runat="server" Text="PRECIO  :" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPRECIO" runat="server" Width="70px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#666666"></asp:TextBox>
                    </td>
                    <td>
                <asp:Label ID="Label23" runat="server" Text="CANTIDAD  :" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCANTIDAD" runat="server" Width="100px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="#666666"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnV_AGREGARPRODUCTO" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" Text="AGREGAR" Width="140px" OnClick="btnV_AGREGARPRODUCTO_Click" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
        </fieldset>
    </div>
     <div>
        
        <fieldset>
            <legend style="color:orange;font-weight:bold;font-family:Tahoma;font-size:14px" >DETALLE DE RECIBO</legend>
            <table class="auto-style1">
                        <tr>
                            <td class="auto-style22">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="SUBTOTAL:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtV_SUBTOTAL" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ReadOnly="True" ForeColor="#666666"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="IGV :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtV_IGV" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ReadOnly="True" ForeColor="#666666"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" Text="TOTAL :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtV_TOTAL" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ReadOnly="True" ForeColor="#666666"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style14" colspan="7">
                                <asp:GridView ID="dgvDETALLE_VENTA" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="99px" Width="100%" OnSelectedIndexChanging="dgvBIEN_VENTA_SelectedIndexChanging" CaptionAlign="Top" Font-Names="Tahoma" Font-Size="Small" OnRowDataBound="dgvBIEN_VENTA_RowDataBound" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" OnRowDeleting="dgvBIEN_VENTA_RowDeleting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="ID_BIEN" HeaderText="CODIGO" ReadOnly="True" />
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" ReadOnly="True" />
                            <asp:TemplateField HeaderText="PRECIO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtprec" runat="server" Text='<%# Eval("PRECIO") %>' Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PRECIO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CANTIDAD" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcan" runat="server" Text='<%# Eval("CANT") %>' Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CANT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" ReadOnly="True"/>
                            
                        </Columns>
                        <EditRowStyle BackColor="#999999" Height="10px"/>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="10px" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="10px" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Height="10px"/>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="10px"/>
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#333333" Height="10px" />
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
