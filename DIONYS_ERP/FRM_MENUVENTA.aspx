<%@ Page Language="C#"  MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MENUVENTA.aspx.cs" Inherits="DIONYS_ERP.FRM_MENUVENTA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
        .auto-style1 {    
        }
        .auto-style35 {
            width: 147px;
            height: 23px;
            font-size:14px;
        }
        .auto-style40 {
            width: 36%;
        }
        .auto-style62 {
            width: 150px;
            text-align: right;
        }
        .auto-style63 {
            width: 713px;
        }
        .auto-style69 {
            width: 235px;
            margin-left: 120px;
        }
        .auto-style75 {
            width: 150px;
            height: 28px;
        }
        .BOTON_2COLORES {}
        .auto-style1005 {
            font-size:14px;
            text-align: left;
        }
        .auto-style1007 {
            width: 77px;
            font-size:14px;
            text-align: right;
        }
        .auto-style1008 {
            width: 427px;
            }
        .auto-style1009 {
            width: 1173px;
        }
        .auto-style1010 {
            width: auto;
            font-size:14px;
        }
        .auto-style1070 {
            width: 109px;
        }
        .auto-style1017 {
        }
        .auto-style1022 {
            width: 287px;
        }
        .auto-style1072 {
            width: 150px;
        }
        .auto-style1086 {
            width: 200px;
            font-size: 14px;
            text-align: right;
        }
        .auto-style1099{
            width:200px;
            text-align: center;
        }
        .auto-style1100 {
            height: 79px;
        }
        .auto-style1101 {
            width: 100px;
        }
        .auto-style1102 {
            width: 185px;
        }
        </style>
     <link href="Estilos/menu.css" rel="stylesheet" type="text/css" />
    <link href="ESTILOS/EstilosGeneral.css" rel="stylesheet" type="text/css" />
    <link href="ESTILOS/Estilos_Especiales.css" rel="stylesheet" type="text/css" />
    <script src="SCRIPT/jquerymenu.js" type="text/javascript"></script>
    <script src="SCRIPT/PASARVALORES_CLIENTE.js" type="text/javascript"></script>
    <link href="ESTILOS/ESTILOS_FRM_MENUVENTA.css" rel="stylesheet" type="text/css"/>

<!--  <script src="SCRIPT/ACTUALIZAR_GRIDVIEW_DETBIEN.js" type="text/javascript"></script>-->

    <!-- ================================================= -->
    <!-- SCRIPT PARA BLOQUEAR LAS FLECHAS DE NAVEGACION -->
    <script type="text/javascript">
    {
        if(history.forward(1))
        location.replace(history.forward(1))
    }
    </script>
    
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- ================================================= -->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- =========INICIO CODIGO JAVASCRIPT PARA OBTENER DATOS ==================-->
    <!-- ============================================= INICIO DE CODIGO PARA GENERAR EL AUTOCOMPLETAR ===================================================== -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR DESCRIPCION ============ -->
    <script type="text/javascript">
        $(function () {
            $("[id$=txtDESCRIPCION]").autocomplete({
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
                                    text: item.split('-')[2]
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
                    $("[id$=txtID_CLIENTE]").val(i.item.val);
                    $("[id$=txtCLIENTE]").val(i.item.text);
                },
                minLength: 1
            });
        });
    </script>
    
    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR RUC ============ -->
    <script type="text/javascript">
        $(function () {
            $("[id$=txtCLIENTE]").autocomplete({
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
                                    texto3: item.split('-')[2]
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
                    $("[id$=txtID_CLIENTE]").val(i.item.val);
                    $("[id$=txtDESCRIPCION]").val(i.item.texto3);
                },
                minLength: 1
            });
        });
    </script>

    <!--================= FIN CODIGO JAVASCRIPT =======================-->

    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>
        <fieldset>
        <table class="auto-style1009" style="background-color:transparent; border-bottom-width:medium;border-bottom-color:black ;border-bottom:2px;color:white;font-family:Tahoma;font-weight:bold;width: 100% ">
            <tr>
                <td class="auto-style1070">
                                       <asp:Label ID="Label7" runat="server" Width="115px" Height="21px" Text="TIPO DOC:" Font-Size="15px" ValidateRequestMode="Disabled" Font-Names="Tahoma"></asp:Label>
                                       </td>
                <td class="auto-style1010"><asp:DropDownList ID="cboTIPO_DOC" runat="server" Height="21px" Width="150px"  style="font-size:small" Font-Names="Tahoma" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="cboTIPO_DOC_SelectedIndexChanged">
                        <asp:ListItem Value="TB">TICKET BOLETA</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style35"  style="font-size:15px">
                                       <asp:Label ID="Label8" runat="server" Width="115px" Height="21px" Text="TIPO PAGO" Font-Size="15px" ValidateRequestMode="Disabled" Font-Names="Tahoma"></asp:Label>
                </td>
                <td class="auto-style1017"  style="font-size:14px">
                    <asp:DropDownList ID="cboTIPO_PAGO" runat="server" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="cboTIPO_PAGO_SelectedIndexChanged" Width="150px">
                        <asp:ListItem Value="0001">EFECTIVO</asp:ListItem>
                        <asp:ListItem Value="0002">TARJETA CREDITO</asp:ListItem>
                        <asp:ListItem Value="0003">TARJETA DEBITO</asp:ListItem>
                        <asp:ListItem Value="0004">DEPOSITO BANCARIO</asp:ListItem>
                        <asp:ListItem Value="0005">TRANSFERENCIA BANCARIA</asp:ListItem>
                        <asp:ListItem Value="0006">CHEQUE BANCARIO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style1017"  style="font-size:14px">
                    <asp:TextBox ID="txtTIPO_PAGO" runat="server" AutoPostBack="True" ReadOnly="True" Width="281px"></asp:TextBox>
                </td>
                <td class="auto-style1022">
                    <asp:Label ID="Label14" runat="server" Font-Names="Tahoma" Font-Size="16px" Text="CLIENTE:"></asp:Label>
                </td>
                <td class="auto-style1022">
                                       <asp:Button ID="btnBUSCAR_CLIENTE" runat="server" Height="30px" Text="..." Width="55px" BackColor="#006666" CssClass="Boton01" Font-Italic="False" OnClick="btnBUSCAR_CLIENTE_Click" />
                                       <asp:TextBox ID="txtID_CLIENTE" runat="server" Font-Names="Tahoma" Height="21px" Width="55px" Font-Size="Small"></asp:TextBox>
                &nbsp;<asp:TextBox ID="txtCLIENTE" runat="server" Height="21px" Width="137px" Font-Size="Small" style="text-align: right" ViewStateMode="Disabled" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1070">
                                       <asp:TextBox ID="txtVUELTO" runat="server" Height="16px"  Width="95px" Font-Names="Tahoma" Font-Size="Medium" ForeColor="Black" ReadOnly="True" Visible="False"></asp:TextBox>
                                   </td>
                <td class="auto-style1010">
                                       <asp:Button ID="btnVUELTO" runat="server" BackColor="#CC0000" Height="16px" OnClick="btnVUELTO_Click" Text="&lt;" Width="42px" Visible="False" />
                                       
                                       <asp:Label ID="Label5" runat="server" Width="30px" Height="30px" Text="S/." Font-Size="Large" ValidateRequestMode="Disabled" Visible="False"></asp:Label>
                </td>
                <td class="auto-style35"  style="font-size:15px">
                                       &nbsp;</td>
                <td class="auto-style1017"  style="font-size:14px">
                                       &nbsp;</td>
                <td class="auto-style1017"  style="font-size:14px">
                                       &nbsp;</td>
                <td class="auto-style1022">
                                       &nbsp;</td>
                <td class="auto-style1022">
                                       <asp:TextBox ID="txtDESCRIPCION" runat="server" Height="18px" Width="280px" Font-Names="Tahoma" Font-Size="Small"></asp:TextBox>
                </td>
            </tr>
            </table>
        </fieldset>
      <br />

    <div style="height: 88px; margin-bottom: 0px;" > <!-- TABLA DE TOTALES -->
       <fieldset>
        <table class="auto-style1" style="background-color:transparent;color:white;font-family:Tahoma;font-weight:bold;font-size:14px; width: 100%; height: 68px;">
            <tr>
                <td class="auto-style62" >SUB TOTAL
                    </td>
                <td class="auto-style1072"  style="text-align:right">
                    IGV </td>
                <td class="auto-style1086">
                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="16px" ForeColor="White" Text="TOTAL"></asp:Label>
                </td>
                <td class="auto-style1099" rowspan="2">
                    <asp:ImageButton ID="btnBUSCAR_BIEN" runat="server" Height="40px" ImageUrl="~/ICONOS/BUSCAR.png" Width="40px" CommandName="cmdBUSCAR_BIEN" OnClick="btnBUSCAR_BIEN_Click1" style="text-align: center" TabIndex="10" Visible="False" />
                </td>
                <td class="auto-style63" rowspan="2">
                      
                    <div style="width: 710px; height: 77px;"> <!-- CLIENTE -->
                           <table class="auto-style1100">
                               <tr>
                                   <td class="auto-style1005" >
                                       <asp:Label ID="Label9" runat="server" Width="78px" Height="21px" Text="CLIENTE " Font-Size="18px" ValidateRequestMode="Disabled" Font-Names="Tahoma" style="text-align: center"></asp:Label>
                                       </td>
                                   <td class="auto-style1005" >
                    <asp:TextBox ID="txtCLIENTE_VENTA" runat="server" style="margin-left: 0px" Width="277px" Font-Names="Tahoma" Font-Size="Small" Height="21px" BackColor="White"></asp:TextBox>
                                       </td>
                                   <td class="auto-style1007">
                                       <asp:Label ID="Label15" runat="server" Font-Size="18px" Text="PAGA:"></asp:Label>
                                   </td>
                                   <td class="auto-style1008">
                                       <asp:Label ID="Label3" runat="server" Width="30px" Height="30px" Text=" S/." Font-Names="Tahoma" Font-Size="Large"></asp:Label>
                                       <asp:TextBox ID="txtPAGA" runat="server" Height="24px" Width="77px" Font-Names="Tahoma" Font-Size="18px" ForeColor="#CC3300"></asp:TextBox>
                                       
                                   </td>
                                   <td class="auto-style69">
                                       &nbsp;&nbsp;
                                       <asp:Button ID="btnOK" runat="server" Font-Size="XX-Large" Height="53px" Text="OK" Width="118px" Font-Names="Tahoma" OnClick="btnOK_Click" OnClientClick="return confirm(&quot;¿DESEA REALIZAR LA VENTA?&quot;);" CssClass="BOTONES_OK" BackColor="#006666" />
                                   </td>
                               </tr>
                               </table>
                       </div>

                </td>
            </tr>
            <tr>
                <td class="auto-style1086"> 
                    <asp:Label ID="lblSUBTOTAL" runat="server" Text="0.00" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px"></asp:Label>
                </td>
                <td class="auto-style1086"  style="text-align:right"><asp:Label ID="lblIGV" runat="server" Text="0.00" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px"></asp:Label>
                </td>
                <td class="auto-style1086">
                    <asp:Label ID="lblTOTAL" runat="server" Text="0.00" Font-Bold="True" Font-Names="Tahoma" Font-Size="28px" ForeColor="#CC9900" ></asp:Label>
                </td>
            </tr>
                       
        </table>
        </fieldset>
       </div> <!-- FIN DE TOTALES -->
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <br />
     <div>
        <table class="auto-style1">

                        
            
            <tr>
                <td class="auto-style40" rowspan="7" >
                    <asp:GridView ID="dgvBIEN_VENTA" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="99px" Width="592px" OnSelectedIndexChanging="dgvBIEN_VENTA_SelectedIndexChanging" CaptionAlign="Top" Font-Names="Tahoma" Font-Size="14px" OnRowDataBound="dgvBIEN_VENTA_RowDataBound" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" OnRowCancelingEdit="dgvBIEN_VENTA_RowCancelingEdit" OnRowUpdating="dgvBIEN_VENTA_RowUpdating" OnRowDeleting="dgvBIEN_VENTA_RowDeleting" Font-Bold="False" OnSelectedIndexChanged="dgvBIEN_VENTA_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="ID_BIEN" HeaderText="CODIGO" ReadOnly="True" />
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" ReadOnly="True" />
                            <asp:TemplateField HeaderText="CANT" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcan" runat="server" Text='<%# Eval("CANT") %>' Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CANT") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRECIO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtprec" runat="server" Text='<%# Eval("PRECIO") %>' Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PRECIO") %>'></asp:Label>
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
                <td class="auto-style1102" rowspan="7">
                 </td>
                <td class="auto-style1072">
    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="16px" ForeColor="White" Text="CANTIDAD" style="text-align: center" Width="140px"></asp:Label>
                </td>
                <td class="auto-style1072">
    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="16px" ForeColor="White" Text="PRECIO" style="text-align: center" Width="150px"></asp:Label>
                </td>
                <td class="auto-style1072">
                                       &nbsp;</td>
                <td class="auto-style1072">
                                       <asp:Label ID="Label6" runat="server" Width="139px" Height="21px" Text="CLASE " Font-Size="15px" ValidateRequestMode="Disabled" Font-Names="Tahoma" Font-Bold="True" ForeColor="White" style="text-align: center"></asp:Label>
                </td>
                <td rowspan="7" class="auto-style1101">&nbsp;</td>
            </tr>
            
            
            
            <tr>
                <td class="auto-style1072">
                    <asp:TextBox ID="txtCANTIDAD_VENTA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px" style="text-align: center" Width="140px"></asp:TextBox>
                </td>
                <td class="auto-style1072">
                    <asp:TextBox ID="txtPRECIO_VENTA" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px" style="text-align: center" Width="140px"></asp:TextBox>
                </td>
                <td class="auto-style1072">
                                       &nbsp;</td>
                <td class="auto-style1072">
                    <asp:DropDownList ID="cboCLASE_BIEN" runat="server" Height="21px" Width="142px" AutoPostBack="True" OnSelectedIndexChanged="cboCLASE_BIEN_SelectedIndexChanged" Font-Names="Tahoma" BackColor="White">
                    </asp:DropDownList>
                </td>
            </tr>
            
            
            
            <tr>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN01" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" OnClick="btnBIEN01_Click" style="text-align: center; word-break: keep-all;"  BorderStyle="Solid" CssClass="BOTONES_BIEN" Font-Strikeout="False"  />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN02" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center;word-break: keep-all;" OnClick="btnBIEN02_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN03" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN03_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN04" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" OnClick="btnBIEN04_Click" style=" text-align: center; word-wrap:break-word;" CssClass="BOTONES_BIEN" />
                </td>
            </tr>
            
            
            
            <tr>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN05" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN05_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN06" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN06_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN07" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN07_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN08" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN08_Click" CssClass="BOTONES_BIEN" />
                </td>
            </tr>
            
            
            
            <tr>
                <td class="auto-style75">
                    <asp:Button ID="btnBIEN09" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN09_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style75">
                    <asp:Button ID="btnBIEN10" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN10_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style75">
                    <asp:Button ID="btnBIEN11" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN11_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style75">
                    <asp:Button ID="btnBIEN12" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN12_Click" CssClass="BOTONES_BIEN" />
                </td>
            </tr>
            
            
            
            <tr>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN13" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN13_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN14" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN14_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN15" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN15_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN16" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN16_Click" CssClass="BOTONES_BIEN" />
                </td>
            </tr>
            
            
            
            <tr>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN17" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN17_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN18" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN18_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN19" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN19_Click" CssClass="BOTONES_BIEN" />
                </td>
                <td class="auto-style1072">
                    <asp:Button ID="btnBIEN20" runat="server" Text="Button" Height="73px" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" BorderStyle="Solid" style="text-align: center; word-wrap:break-word;" OnClick="btnBIEN20_Click" CssClass="BOTONES_BIEN" />
                </td>
            </tr>
            
            
            
            </table>
      </div>
        <br />
        <br />
        <br />
        <br />

    <!-- VALIDADIONES TEXBOX -->



</asp:Content>
