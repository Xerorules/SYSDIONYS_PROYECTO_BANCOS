﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CHEQUES.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web15" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link rel="shortcut icon" href="../favicon.ico" />
    <link href="assets/css/demo.css" rel="stylesheet" />
    <link href="assets/css/style4.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <%--<script src="assets/js/jquery-1.10.2.js"></script>--%>
    <script src="assets/js/bootstrap.min.js"></script>
    <link rel='stylesheet prefetch' href='https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.2.3/animate.min.css' />

    <script type="text/javascript">
        function addCommas(clientID) {

            var nStr = document.getElementById(clientID.id).value;

            nStr += '';
            x = nStr.split('.');
            if (!x[0]) {
                x[0] = "0";

            }
            x1 = x[0];
            if (!x[1]) {
                x[1] = "00";
            }
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }

            document.getElementById(clientID.id).value = x1 + x2;
            return true;

        }
    </script>

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


    </script>
    <!--SCRIPT PARA VISUALIZAR Y ESCONDER EL POPUP DE LA IMAGEN DEL CHEQUE -->
    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgFull.style.width = "800px";
                imgFull.style.height = "500px";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width + 50) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "0%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
        }
    </script>



    <script>
        var prevRowIndex;
        function ChangeRowColor(row, rowIndex) {
            var parent = document.getElementById(row);
            var currentRowIndex = parseInt(rowIndex) + 1;

            if (prevRowIndex == currentRowIndex) {
                return;
            }
            else if (prevRowIndex != null) {
                parent.rows[prevRowIndex].style.backgroundColor = "#FFFFFF";
            }

            parent.rows[currentRowIndex].style.backgroundColor = "#a7e6ef";

            prevRowIndex = currentRowIndex;

            $('#<%= hfParentContainer.ClientID %>').val(row);
            $('#<%= hfCurrentRowIndex.ClientID %>').val(rowIndex);
        }

        $(function () {
            RetainSelectedRow();
        });



        function RetainSelectedRow() {
            var parent = $('#<%= hfParentContainer.ClientID %>').val();
            var currentIndex = $('#<%= hfCurrentRowIndex.ClientID %>').val();
            if (parent != null) {
                ChangeRowColor(parent, currentIndex);
            }
        }

        function RetainSelectedRow2() {
            var parent = $('#<%= hfParentContainer.ClientID %>').val();
            var currentIndex = -1;
            if (parent != null) {
                ChangeRowColor(parent, currentIndex);
            }
        }

    </script>

    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 1px;
        padding-left: 1px;
        width: 710px;
        height: 360px;
    }
</style>


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

    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modal {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            opacity: 0.8;
            min-height: 100%;
        }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 500px;
            width: 800px;
            padding: 3px;
            border: solid 1px black;
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
                                    label: item.split('**')[0],
                                    val: item.split('**')[1]
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
                                    label: item.split('**')[0],
                                    val: item.split('**')[1]
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
        CancelControlID="Button3" BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
        <div class="col-sm-12">
            <asp:Label ID="label_confirmar" runat="server" Text="CONFIRMAR CHEQUE PENDIENTE"></asp:Label>

        </div>
        <div class="col-sm-12">
            <asp:Label ID="label_deposito" runat="server" Text="CONFIRMAR DEPÓSITO"></asp:Label>

        </div>



        <table>
            <tr style="height: 60px">
                <td>
                    <div style="float: left; width: 120px">
                        <asp:Label runat="server" CssClass="lbl" Text="FECHA:"></asp:Label>
                    </div>
                </td>
                <td>
                    <div style="float: right;">
                        <asp:TextBox TextMode="Date" ID="txtmFECH" runat="server" Font-Size="14px" CssClass="form-control" Width="250px" Height="35"></asp:TextBox>
                    </div>
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="BANCO:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cbomBANCO" CssClass="form-control" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="cbomBANCO_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="CUENTA:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cbomCUENTA" CssClass="form-control" AutoPostBack="false" Width="250px" OnSelectedIndexChanged="cbomCUENTA_SelectedIndexChanged">
                        <asp:ListItem Text="--SELECCIONE--" Value="NADA" />
                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label11" runat="server" CssClass="lbl" Text="TIPO MOV:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" AutoPostBack="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="250px">

                        <asp:ListItem Text="INGRESO" Value="INGRESO" />

                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="N° OPERACIÓN:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmOPE" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="LUGAR:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmLUGAR" runat="server" Font-Size="14px" Style="text-transform: uppercase" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="IMPORTE:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmIMPORTE" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                    <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="DESCRIPCIÓN:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmDESC" runat="server" Font-Size="14px" Style="text-transform: uppercase" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <asp:Label ID="lblid_cliente" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
            <asp:Label ID="lblid_cheque" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
            <asp:Label ID="LBLID_MOV" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>

        </table>
        <br />
        <div class="col-md-12" style="left: -20px">

            <div class="col-md-3 col-sm-3 col-lg-3">

                <asp:Button ID="Button2" runat="server" Text="ACEPTAR" CssClass="form-control btn-info" OnClick="Button2_Click" />

            </div>
            <div class="col-md-3 col-sm-3 col-lg-3" style="left: -10px">

                <asp:Button ID="Button3" runat="server" Text="CANCELAR" CssClass="form-control btn-info" OnClick="Button3_Click" />

            </div>
            <div class="col-md-3 col-sm-3 col-lg-3">

                <asp:Button ID="Button4" runat="server" Text="ACEPTADO" CssClass="form-control btn-success" OnClick="Button4_Click" />

            </div>
            <div class="col-md-3 col-sm-3 col-lg-3">

                <asp:Button ID="Button1" runat="server" Text="REBOTADO" CssClass="form-control btn-danger" OnClick="Button1_Click" />

            </div>

        </div>





    </asp:Panel>
    <%----------------------------%>
    <%------------popupimagen-----------%>
    <input id="hidden23" type="hidden" name="hidden23" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel12" TargetControlID="hidden23"
        CancelControlID="btnClose2" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel12" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <button type="button" class="close" id="btnClose2" data-dismiss="modal" style="font-size:25px;"  aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <%--<asp:Button ID="btnClose2" runat="server" Text="" Visible="false" />--%>
        
        <asp:Image runat="server" ID="imagenPOPUP" Width="700" Height="325"/>
        
    </asp:Panel>
    <%----------------------------%>
    <%----------------------------%>


    <div class="container-fluid col-lg-4 col-md-4" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">
        <div class="form-horizontal" style="position: relative;">
            <h1 style="color: white;" class="main-text">CHEQUES</h1>
            <div>
                &nbsp;
            &nbsp
            &nbsp
            </div>
            <div class="container col-lg-10 col-md-10" style="background-color: lightgray; color: black; border-radius: 5px 5px 5px 5px;">
                &nbsp
            <div class="form-group">
                <label class="control-label col-md-4" style="color: black">CLIENTE:</label>
                <div class="col-xs-8 col-md-6">

                    <asp:TextBox runat="server" ID="txtCLIENTE" CssClass="form-control" Style="text-transform: uppercase" placeholder="Busqueda automática de clientes" MaxLength="100" Width="300px"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtCLIENTE"
                        ErrorMessage="(*)El Cliente es requerido"
                        ForeColor="Red"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>

            </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">FECHA DE GIRO:</label>
                    <div class="col-xs-8 col-md-6">
                        <asp:TextBox runat="server" ID="txtFGIRO" CssClass="form-control" type="date" MaxLength="45" Width="300px" Height="35px"></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtFGIRO"
                            ErrorMessage="(*)La fecha de giro es requerida"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        &nbsp
                    </div>

                </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">FECHA DE COBRO:</label>
                    <div class="col-xs-8 col-md-6">
                        <asp:TextBox runat="server" ID="txtFCOBRO" CssClass="form-control" type="date" MaxLength="100" Width="300px" Height="35px"></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txtFCOBRO"
                            ErrorMessage="(*)La fecha de cobro es requerida"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        &nbsp
                    </div>

                </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">N° DE CHEQUE:</label>
                    <div class="col-xs-8 col-md-6">
                        <asp:TextBox runat="server" ID="txtNUMERO" CssClass="form-control" placeholder="Ingrese número de cheque" MaxLength="100" Width="300px"></asp:TextBox>
                        <%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                            ControlToValidate="txtNUMERO"
                            ErrorMessage="(*)El Número de cheque es requerido"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        &nbsp
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">BANCO:</label>
                    <div class="col-xs-8 col-md-6">
                        <asp:DropDownList runat="server" ID="cboBANCO" CssClass="form-control" AutoPostBack="false" Width="300px" OnSelectedIndexChanged="cboBANCO_SelectedIndexChanged">
                        </asp:DropDownList><%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="cboBANCO"
                            ErrorMessage="(*)Escoja un banco"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        &nbsp
                    </div>

                </div>
                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black; margin-top: -8px;">TIPO DE MONEDA:</label>
                    <div class="col-xs-8 col-md-7">
                        <asp:RadioButtonList ID="rdbMONEDA" runat="server" RepeatDirection="Horizontal" Width="200px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="Black" AutoPostBack="false">
                            <asp:ListItem Value="S">&nbsp;&nbsp;SOLES</asp:ListItem>
                            <asp:ListItem Value="D">&nbsp;&nbsp;DOLARES</asp:ListItem>
                        </asp:RadioButtonList>

                    </div>

                </div>
                <div class="form-group">
                    <label class="control-label col-md-4" style="color: black">IMPORTE:</label>
                    <div class="col-xs-8 col-md-6">
                        <asp:TextBox runat="server" ID="txtIMPORTE" CssClass="form-control" placeholder="Ingrese importe" type="numeric" MaxLength="100" Width="300px" OnBlur="addCommas(this)"></asp:TextBox>
                        <%--<%--<%--VALIDADOR--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="txtIMPORTE"
                            ErrorMessage="(*)El importe es requerido"
                            ForeColor="Red"
                            ValidationGroup="Registro" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        &nbsp
                    </div>
                </div>



                <asp:TextBox ID="TXTid_cliente" runat="server" CssClass="visible-xs"></asp:TextBox>
                &nbsp
            <div class="form-group col-md-12 col-sm-12 col-xs-12" style="position: center;">


                <div class="col-md-4 col-md-offset-2 col-sm-4 col-xs-12" style="position: center;">
                    <asp:Button runat="server" CssClass="a_demo_four1 col-xs-12 col-sm-12" ForeColor="White" Font-Bold="true" Text="GRABAR" ValidationGroup="Registro" ID="btnRegistrar" AccessKey="G" OnClick="btnRegistrar_Click" />
                    &nbsp
                </div>
                <div class="col-md-0 col-sm-0 col-xs-0" style="position: center;">
                    <asp:Button runat="server" Visible="false" CssClass="a_demo_four1 col-xs-12 col-sm-12" ForeColor="White" Font-Bold="true" Text="ACTUALIZAR" ID="btnActualizar" OnClick="btnActualizar_Click" />

                </div>
                <div class="col-md-4 col-sm-4 col-xs-12" style="position: center">
                    <asp:Button runat="server" CssClass="a_demo_four1 col-xs-12 col-sm-12" Text="CANCELAR" Font-Bold="true" ForeColor="White" ID="btnCancelar" AccessKey="C" OnClick="btnCancelar_Click" />
                    <asp:Button runat="server" BackColor="DarkCyan" BorderColor="DarkCyan" ForeColor="DarkCyan" type="hidden" Text="REGISTRARMOV" CssClass="visible-xs" ID="btnREGISTRARMOV" OnClick="btnREGISTRARMOV_Click" />

                </div>


                &nbsp
                
            </div>
            </div>
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

    <div class="container-fluid" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
        <div class="row">
            <div class="col-xs-12 col-sm-12  col-md-8">
                <asp:TextBox runat="server" ID="txtfiltroid_cli" CssClass="visible-xs" Font-Bold="true" placeholder="Ingresar cliente,busqueda automatica" MaxLength="70"></asp:TextBox>
                <h2 style="color: white" class="text-info">CARTERA DE CHEQUES</h2>
                <div style="float: right; margin-top: -25px; width: 7%; margin-left: -5px;">


                    <asp:ImageButton ID="reportePDF" runat="server" Height="50px" ImageUrl="~/ICONOS/Graphicloads-Filetype-Pdf.ico" Width="50px" OnClick="reportePDF_Click" />

                </div>
                &nbsp;
                 
                <div class="form-group col-md-12 col-sm-12 col-xs-12">

                    <div class="col-xs-4 col-md-3">
                        <label style="color: white; font-size: smaller;">CLIENTE:</label>
                        <asp:TextBox runat="server" ID="txtFiltroCli" CssClass="form-control col-sm-12 col-xs-12" Font-Bold="true" placeholder="Ingresar cliente,busqueda automatica" MaxLength="70" OnTextChanged="txtFiltroCli_TextChanged"></asp:TextBox>
                    </div>

                    <div class="col-xs-4 col-md-2">
                        <label style="color: white; font-size: smaller;">BANCO:</label>
                        <asp:DropDownList runat="server" ID="cboFiltroBanco" CssClass="form-control col-sm-12 col-xs-12" AutoPostBack="false"></asp:DropDownList>
                    </div>

                    <div class="col-md-1" style="left: -15px">
                        <label style="color: white; font-size: smaller;">MONEDA:</label>
                        <asp:DropDownList runat="server" ID="cboFiltroMoneda" CssClass="form-control col-sm-12 col-xs-12" Width="98px" AutoPostBack="false">
                            <asp:ListItem Text="-MON-" Value="" />
                            <asp:ListItem Text="SOLES" Value="S" />
                            <asp:ListItem Text="DOLAR" Value="D" />
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-1" style="left: -10px">
                        <label style="color: white; font-size: smaller;">ESTADO:</label>
                        <asp:DropDownList runat="server" ID="cboEstado" CssClass="form-control col-sm-12 col-xs-12" Width="125px" AutoPostBack="false">
                            <asp:ListItem Text="-ESTADO-" Value="TODO" />
                            <asp:ListItem Text="PENDIENTE" Value="PENDIENTE" />
                            <asp:ListItem Text="DEPOSITADO" Value="DEPOSITADO" />
                            <asp:ListItem Text="ACEPTADO" Value="ACEPTADO" />
                            <asp:ListItem Text="REBOTADO" Value="REBOTADO" />

                        </asp:DropDownList>

                    </div>

                    <%--<asp:CheckBoxList ID="CheckBoxList1" runat="server">
                       <asp:ListItem Value="GIRO">&nbsp;&nbsp;F. GIRO</asp:ListItem>
                        <asp:ListItem Value="COBRO">&nbsp;&nbsp;F. COBRO</asp:ListItem>
                    </asp:CheckBoxList>--%>

                    <div class="col-xs-4 col-md-2" style="left: 30px">
                        <label style="color: white; font-size: smaller;">F. GIRO INICIAL:</label>
                        <asp:TextBox runat="server" ID="txtFiltroFechaIni" CssClass="form-control col-sm-12 col-xs-12" Width="150px" Height="35px" TextMode="Date" placeholder="Ingrese Fecha inicial" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-2" style="left: 10px">
                        <label style="color: white; font-size: smaller;">F. GIRO FINAL:</label>
                        <asp:TextBox runat="server" ID="txtFiltroFechaFin" CssClass="form-control col-sm-12 col-xs-12" Width="150px" Height="35px" TextMode="Date" placeholder="Ingrese Fecha final" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-1" style="top: 25px">
                        <asp:Button ID="btnConsulta" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnConsulta_Click" />
                    </div>

                    &nbsp
                    
                </div>
                <div id="divBackground" class="modal">
                </div>
                <div id="divImage">
                    <table style="height: 50%; width: 50%">
                        <tr>
                            <td style="vertical-align: middle; align-content: center;">
                                <img id="imgLoader" alt="" src="images/loader.gif" />
                                <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: bottom; align-content: center;">
                                <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                            </td>
                        </tr>
                    </table>
                </div>

                &nbsp
             <div>
                 &nbsp 

                  <asp:HiddenField ID="hfCurrentRowIndex" runat="server"></asp:HiddenField>
                 <asp:HiddenField ID="hfParentContainer" runat="server"></asp:HiddenField>
                 <div style="width: 100%; height: 700px; overflow-y: scroll; margin-left: -0px;">
                     <asp:GridView ID="dgvBANCOS" runat="server" CssClass="table-striped table-bordered table-responsive table-condensed" BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_CHEQUE" Font-Size="Small" OnRowCommand="dgvBANCOS_RowCommand" OnRowDataBound="dgvBANCOS_RowDataBound">

                         <Columns>
                             <asp:BoundField DataField="ID_CHEQUE" HeaderText="COD" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="30px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="DESCRIPCION" HeaderText="CLIENTE" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="200px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="FECHA_GIRO" HeaderText="FECHA GIRO" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="90px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="FECHA_COBRO" HeaderText="FECHA COBRO" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="90px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="NUM_CHEQUE" HeaderText="N°" HeaderStyle-Font-Size="Smaller" ItemStyle-Width="50px" />
                             <asp:BoundField DataField="BANCO" HeaderText="BANCO" HeaderStyle-Font-Size="Smaller" />
                             <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" DataFormatString="{0:N}" HeaderStyle-Font-Size="Smaller" />
                             <asp:BoundField DataField="MONEDA" HeaderText="MON" HeaderStyle-Font-Size="Smaller">
                                 <ItemStyle Width="30px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" HeaderStyle-Font-Size="Smaller" />

                             <asp:TemplateField>
                                 <ItemTemplate>
                                     <asp:LinkButton ID="LinkButtonActualizar" runat="server" CommandName="ACTUALIZAR" Width="55px" Font-Size="Smaller">CAMBIAR ESTADO</asp:LinkButton>

                                 </ItemTemplate>
                             </asp:TemplateField>


                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                 <ItemTemplate>
                                     <asp:LinkButton ID="LinkButtonEditar" runat="server" CommandName="EDITAR" OnClientClick="return GetSelectedRow(this)" Font-Size="Smaller">EDITAR</asp:LinkButton>
                                     <asp:LinkButton ID="LinkButtonEliminar" runat="server" CommandName="ELIMINAR" OnClientClick="if (!confirm('Esta seguro de eliminar el registro?')) return false;" Font-Size="Smaller">ELIMINAR</asp:LinkButton>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:BoundField DataField="" HeaderText="BCO DEPOSITO" HeaderStyle-BackColor="#ffcccc" HeaderStyle-Font-Size="Smaller" />
                             <asp:BoundField DataField="ID_MOVIMIENTOS" HeaderText="ID MOV" HeaderStyle-BackColor="#ffcccc" HeaderStyle-Font-Size="Smaller" />

                             <asp:TemplateField HeaderText="IMG" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="ImageButton1" CommandName="IMAGEN" runat="server" ImageAlign="Middle" ImageUrl="~/imagenes/camera.png"
                                         Width="20px" Height="20px" Style="cursor: pointer" OnCommand="lnkCustDetails_Click" />
                                 </ItemTemplate>
                             </asp:TemplateField>








                         </Columns>
                     </asp:GridView>
                 </div>
             </div>
            </div>
        </div>
    </div>

    <script src='http://cdnjs.cloudflare.com/ajax/libs/mouse0270-bootstrap-notify/3.1.5/bootstrap-notify.min.js'></script>
    <script src="Scripts/index.js"></script>





    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
