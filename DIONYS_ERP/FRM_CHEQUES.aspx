<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CHEQUES.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web15" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <script src="assets/js/jquery-1.10.2.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <link rel='stylesheet prefetch' href='https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.2.3/animate.min.css' />
    <script type="text/javascript">


        function openModal() {
            $('#myModal').modal('show');
        }

        function openModal1() {
            $('#myModal1').modal('show');
        }

        function openModal2() {
            $('#myModal2').modal('show');
        }

        function Comma(Num) {
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;
        }
     
      



    </script>

    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 6px;
            padding-left: 10px;
            width: 460px;
            height: 480px;
            opacity: 50;
        }

        .lbl {
            font-size: 14px;
            font-style: normal;
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- ============================================= INICIO DE CODIGO PARA GENERAR EL AUTOCOMPLETAR ===================================================== -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtCLIENTE]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CLIENTES") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
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
                    $("[id$=txtCLIENTE]").val(i.item.text);
                    $("[id$=TXTid_cliente]").val(i.item.val);
                },
                minLength: 1
            });
        });


    </script>
     <script type="text/javascript">
        $(function () {
            $("[id$=txtmLUGAR]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_LUGAR") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
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
                    $("[id$=txtmLUGAR]").val(i.item.text);
                    $("[id$=txtmLUGAR]").val(i.item.val);
                },
                minLength: 1
                
            });
        });

         
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtFiltroCli]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CLIENTES") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
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
                    $("[id$=txtFiltroCli]").val(i.item.text);
                    $("[id$=txtfiltroid_cli]").val(i.item.val);
                },
                minLength: 1
            });
        });


    </script>

    <%--POUP AJAX --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <input id="Hid_Sno" type="hidden" name="hddclick" runat="server" />
    <!-- ModalPopupExtender -->
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Hid_Sno"
        CancelControlID="Button3"  BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
        <table>
            <tr style="height:60px">
                <p style="font-weight:600">GENERAR MOVIMIENTO</p>
                <td>
                    <div style="float:left; width:120px"><asp:Label runat="server" CssClass="lbl" Text="FECHA:"></asp:Label></div>
                </td>
                <td>
                    <div style="float:right;"><asp:TextBox TextMode="Date"  ID="txtmFECH" runat="server" Font-Size="14px" CssClass="form-control" Width="250px" Height="35"></asp:TextBox></div>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="BANCO:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cbomBANCO" CssClass="form-control" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbomBANCO_SelectedIndexChanged" ></asp:DropDownList>
                </td>
            </tr>
            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="CUENTA:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cbomCUENTA" CssClass="form-control" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbomCUENTA_SelectedIndexChanged">
                         <asp:ListItem Text="--SELECCIONE--" Value="NADA" />  
                    </asp:DropDownList>
                </td>
            </tr>

             <tr style="height:40px">
                <td>
                    <asp:Label ID="Label11" runat="server" CssClass="lbl" Text="TIPO MOV:"></asp:Label>
                </td>
                <td> 
                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" AutoPostBack="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="250px">
                        
                         <asp:ListItem Text="INGRESO" Value="INGRESO" />
                         
                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="N° OPERACIÓN:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmOPE" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="LUGAR:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmLUGAR" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="IMPORTE:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmIMPORTE" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"  onkeyup = "javascript:this.value=Comma(this.value);" ></asp:TextBox>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="DESCRIPCIÓN:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmDESC" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <asp:Label ID="lblid_cliente" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
            <asp:Label ID="lblid_cheque" runat="server"  CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
            <asp:Label ID="LBLID_MOV" runat="server"  CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>

        </table>
        <br />
        <div class="row">
            
            <div class="col-xs-3">
                <div class="col-md-2 col-xs-2"  style="margin-left:60px">
                   <asp:Button ID="Button2" runat="server" Text="ACEPTAR" CssClass="form-control btn-info" OnClick="Button2_Click" />
                </div>
            </div>
            <div class="col-xs-3" style="margin-left:40px">
                <div class="col-md-2 col-xs-2" >
                   <asp:Button ID="Button3" runat="server" Text="CANCELAR" CssClass="form-control btn-info" OnClick="Button3_Click"/>
                </div>
            </div>
            <div class="col-xs-3" style="margin-left:-5px">
                <div class="col-md-2 col-xs-2">
                    <asp:Button ID="Button4" runat="server" Text="ACEPTADO" CssClass="form-control btn-success" OnClick="Button4_Click" />
                </div>
            </div>
            <div class="col-xs-3" style="margin-left:-75px">
                <div class="col-md-3 col-xs-3" >
                   <asp:Button ID="Button1" runat="server" Text="REBOTADO" CssClass="form-control btn-danger" OnClick="Button1_Click" />
                </div>
            </div>
            
        </div>
        
            
            
        

    </asp:Panel>
    <%--POPUP AJAX--%>



    <div class="container-fluid col-lg-4 col-md-4">
        <div class="form-horizontal" style="position: relative;">
            <h1 style="color: white;" class="main-text">CHEQUES</h1>
            &nbsp
            <div class="form-group">
                <label class="control-label col-md-4" style="color: white">CLIENTE:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox ID="TXTid_cliente" runat="server" CssClass="visible-xs"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtCLIENTE" CssClass="form-control" placeholder="Busqueda automática de clientes" MaxLength="100" Width="300px"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtCLIENTE"
                        ErrorMessage="(*)El Cliente es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-md-4" style="color: white">FECHA DE GIRO:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtFGIRO" CssClass="form-control" type="date" MaxLength="45" Width="300px" Height="35px"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtFGIRO"
                        ErrorMessage="(*)La fecha de giro es requerida"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-md-4" style="color: white">FECHA DE COBRO:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtFCOBRO" CssClass="form-control" type="date" MaxLength="100" Width="300px" Height="35px"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtFCOBRO"
                        ErrorMessage="(*)La fecha de cobro es requerida"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-md-4" style="color: white">N° DE CHEQUE:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtNUMERO" CssClass="form-control" placeholder="Ingrese número de cheque" MaxLength="100" Width="300px"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ControlToValidate="txtNUMERO"
                        ErrorMessage="(*)El Número de cheque es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-md-4" style="color: white">BANCO:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:DropDownList runat="server" ID="cboBANCO" CssClass="form-control" AutoPostBack="true" Width="300px" OnSelectedIndexChanged="cboBANCO_SelectedIndexChanged">
                    </asp:DropDownList><%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="cboBANCO"
                        ErrorMessage="(*)Escoja un banco"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-md-4" style="color: white">IMPORTE:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtIMPORTE" CssClass="form-control" placeholder="Ingrese importe" MaxLength="100" Width="300px" onkeyup = "javascript:this.value=Comma(this.value);" ></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtIMPORTE"
                        ErrorMessage="(*)El importe es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-4" style="color: white; margin-top: -8px;">TIPO DE MONEDA:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:RadioButtonList ID="rdbMONEDA" runat="server" RepeatDirection="Horizontal" Width="200px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" AutoPostBack="True">
                        <asp:ListItem Value="S">&nbsp;&nbsp;SOLES</asp:ListItem>
                        <asp:ListItem Value="D">&nbsp;&nbsp;DOLARES</asp:ListItem>
                    </asp:RadioButtonList>

                </div>

            </div>



            <div class="form-group col-md-12 col-sm-12 col-xs-12" style="position: center;">
                &nbsp

                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center;">
                    <asp:Button runat="server" CssClass="form-control  btn-info" Text="REGISTRAR" ValidationGroup="Registro" ID="btnRegistrar" OnClick="btnRegistrar_Click" />
                    &nbsp
                </div>
                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center;">
                    <asp:Button runat="server" CssClass="form-control  btn-info" Text="ACTUALIZAR" ID="btnActualizar" OnClick="btnActualizar_Click" />
                    &nbsp
                </div>
                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center">
                    <asp:Button runat="server" CssClass="form-control  btn-info" Text="CANCELAR" ID="btnCancelar" OnClick="btnCancelar_Click" />
                    <asp:Button runat="server" type="hidden" Text="REGISTRARMOV" CssClass="visible-xs" ID="btnREGISTRARMOV" OnClick="btnREGISTRARMOV_Click" />

                </div>


                &nbsp
                
            </div>

        </div>
    </div>

    <!-- -----------------------------Modal Registro-------------------------------------------------------------->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-green" id="myModalLabel" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="success" style="text-align: center; font-family: 'Segoe UI'">&nbsp Operacion realizada correctamente</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal Registro-------------------------------------------------------------->
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="75" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel1" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se pudo realizar la operación</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->
    <!-- -----------------------------Modal Actualizar-------------------------------------------------------------->
    <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="80" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel2" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se llenaron los campos requeridos</h3>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- -----------------------------Modal RegistroError-------------------------------------------------------------->

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-10 col-sm-offset-0 col-sm-10 col-md-offset-0 col-md-8">

                <h2 style="color: white" class="text-info">CARTERA DE CHEQUES</h2>
                &nbsp;
                 
                <div class="form-group col-md-12 col-sm-12 col-xs-12 " style="text-align:center;margin-left:-20px; margin-top: 25px; ">

                    <div class="col-xs-2 col-md-2"  style="text-align: center; top: -15px;margin-left:60px; ">
                        <label style="color: white;  text-align: left; margin-right:90px;"> CLIENTE:</label>
                        <asp:TextBox runat="server" ID="txtFiltroCli" CssClass="form-control" Width="300px"  Font-Bold="true" placeholder="Ingresar cliente,busqueda automatica" MaxLength="70" OnTextChanged="txtFiltroCli_TextChanged"></asp:TextBox>
                    </div>
                    <asp:TextBox runat="server" ID="txtfiltroid_cli" CssClass="visible-xs" Width="300px"  Font-Bold="true" placeholder="Ingresar cliente,busqueda automatica" MaxLength="70"></asp:TextBox>
                    <div class="col-xs-2 col-md-2" style="text-align: center; top: -15px;margin-left:110px; ">
                        <label style="color: white; text-align: left; margin-right:90px; "> BANCO:</label>
                        <asp:DropDownList runat="server" ID="cboFiltroBanco" CssClass="form-control" AutoPostBack="true" Width="180px"></asp:DropDownList>
                    </div>
                    
                    <div class="col-xs-2 col-md-2" style="text-align: center; top: -15px;margin-left:-15px; ">
                        <label style="color: white;  text-align: left; margin-right:90px;"> MONEDA:</label>
                        <asp:DropDownList runat="server" ID="cboFiltroMoneda" CssClass="form-control" AutoPostBack="true" Width="150px">
                            <asp:ListItem Text="-SELECCIONE-" Value="" />
                            <asp:ListItem Text="SOLES" Value="S" />
                            <asp:ListItem Text="DOLARES" Value="D" />
                        </asp:DropDownList>
                                        
                    </div>

                   
                    <div class="col-xs-2 col-md-2" style="text-align: center; top: -15px;margin-left:-40px; ">
                        <label style="color: white;  text-align: left; margin-right:55px;">FECHA INICIAL:</label>
                        <asp:TextBox runat="server" ID="txtFiltroFechaIni" CssClass="form-control" Width="180px" Height="35px"  TextMode="Date" Font-Bold="true" placeholder="Ingrese Fecha inicial" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 col-md-2" style="text-align: center; top: -15px;margin-left:-10px; ">
                        <label style="color: white; text-align: left; margin-right:55px;">FECHA FINAL:</label>
                        <asp:TextBox runat="server" ID="txtFiltroFechaFin" CssClass="form-control" Width="180px" Height="35px"  TextMode="Date" Font-Bold="true" placeholder="Ingrese Fecha final" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-2 col-md-2" style="text-align: right; top: -12px;margin-left:1100px;">
                        <asp:Button ID="btnConsulta" runat="server" Text="FILTRAR TABLA" CssClass="btn btn-info" Width="150px" OnClick="btnConsulta_Click"/>
                    </div>
                    <%--<div style="float: left; margin-top: 9px; width: 10%;">
                       
                                <asp:Button ID="btnREPORTE" runat="server" Text="GENERAR REPORTE" CssClass="btn btn-info" Width="200px" OnClick="btnREPORTE_Click" />
                      </div>--%>
                    
                </div>


       
         <asp:GridView ID="dgvBANCOS" runat="server" CssClass="table table-striped table-bordered  table-hover" BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_CHEQUE" Font-Size="Small" OnRowCommand="dgvBANCOS_RowCommand">
             <PagerSettings Mode="NumericFirstLast" />
             <PagerStyle CssClass="pagination-ys" Wrap="True" BorderStyle="None" HorizontalAlign="Center" />
             <Columns>
                 <asp:BoundField DataField="ID_CHEQUE" HeaderText="CODIGO">
                     <ItemStyle Width="10px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="DESCRIPCION" HeaderText="CLIENTE">
                     <ItemStyle Width="250px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="FECHA_GIRO" HeaderText="FECHA GIRO">
                     <ItemStyle Width="100px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="FECHA_COBRO" HeaderText="FECHA COBRO">
                     <ItemStyle Width="115px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="NUM_CHEQUE" HeaderText="NUMERO" />
                 <asp:BoundField DataField="BANCO" HeaderText="BANCO" />
                 <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" />
                 <asp:BoundField DataField="MONEDA" HeaderText="MONEDA" />
                 <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />

                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkButtonActualizar" runat="server" CommandName="ACTUALIZAR">CAMBIAR ESTADO</asp:LinkButton>
                        
                     </ItemTemplate>
                 </asp:TemplateField>


                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkButtonEditar" runat="server" CommandName="EDITAR">EDITAR</asp:LinkButton>
                          <asp:LinkButton ID="LinkButtonEliminar" runat="server" CommandName="ELIMINAR">ELIMINAR</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>





             </Columns>
         </asp:GridView>
            </div>
        </div>
    </div>

    <script src='http://cdnjs.cloudflare.com/ajax/libs/mouse0270-bootstrap-notify/3.1.5/bootstrap-notify.min.js'></script>
    <script src="Scripts/index.js"></script>

    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
