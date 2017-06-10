<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_MOVIMIENTOS.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web3" %>

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
    <link href="assets/css/paginador.css" rel="stylesheet" />
    <link href="assets/css/ptoledo.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/css/stylosheader.css" rel="stylesheet" />
    <link href="assets/css/css_tab_panel_csspuro.css" rel="stylesheet" />

    <link rel="shortcut icon" href="../favicon.ico" />
    <link href="assets/css/demo.css" rel="stylesheet" />
    <link href="assets/css/style4.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />


    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <link href="../ESTILOS/EstilosGeneral.css" rel="stylesheet" type="text/css" />
    <script src="../SCRIPT/jquerymenu.js" type="text/javascript"></script>
    <link href="../ESTILOS/ESTILOS_BARRA_ESTADO.css" rel="stylesheet" />






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
        function button_click(objTextBox, objBtnID) {
            if (window.event.keyCode == 13) {
                document.getElementById(objBtnID).focus();
                document.getElementById(objBtnID).click();
            }
        }
    </script>
    


    <script type="text/javascript">
        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 109)) {
                    //Aca tenemos que reemplazar "Decimals" por "NoDecimals" si queremos que no se permitan decimales
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        // Función para las teclas especiales
        //------------------------------------------
        function jsIsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, y Delete
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, y flechas
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {  //Check dot key code should be allowed
                    return true;
                }
            }
            // The rest
            return false;
        }

    </script>




    <script type="text/javascript">
        function mostrarpopUp() {
            $find("mp1").show();
        }


        function clickMe(obj) {
            var textVal = obj.value;
            if (textVal.match(/\-/g).length > 0) {
                document.getElementById(obj.id).value = '';
            }
        }

        function filterDigits(eventInstance) {
            eventInstance = eventInstance || window.event;
            key = eventInstance.keyCode || eventInstance.which;
            if ((47 < key) && (key < 58) || (key = 45) || (key == 8)) {
                return true;
            } else {
                if (eventInstance.preventDefault) eventInstance.preventDefault();
                eventInstance.returnValue = false;
                return false;
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
            } else if (prevRowIndex != null) {
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

        //FUNCION PARA ACTUALIZAR EL UPDATE PANEL DE LA GRILLA MOVIMIENTOS
        function actualizarGrilla() {
            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
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
            border-radius: 5px;
            border-style: ridge;
            border-color: black;
            padding-top: 6px;
            padding-left: 10px;
            width: 1220px;
            height: 700px;
            opacity: 50;
        }

        .lbl {
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <!-- ============================================= INICIO DE CODIGO PARA GENERAR EL AUTOCOMPLETAR ===================================================== -->


    <!-- ========= AUTOCOMPLETAR DE CLIENTES POR DESCRIPCION ============ -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtCuentaModal]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CUENTAS") %>',
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
                    $("[id$=txtCuentaModal]").val(i.item.text);
                    $("[id$=TXTprueba]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>

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

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(function () {
                        $("[id$=txtCLIENTE]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CLIENTES") %>',
                                    data: "{ 'prefix': '" + request.term +
                                        "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (
                                            item) {
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
                }
            });
        };
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=txtClienteDBCom]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CLIENTES_DBCOMERCIAL") %>',
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
                    $("[id$=txtClienteDBCom]").val(i.item.text);
                    $("[id$=id_cliente_dbcomercial]").val(i.item.val);
                },
                minLength: 1
            });
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(function () {
                        $("[id$=txtClienteDBCom]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_CLIENTES_DBCOMERCIAL") %>',
                                    data: "{ 'prefix': '" + request.term +
                                        "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (
                                            item) {
                                            return {
                                                label: item
                                                    .split(
                                                        '-'
                                                    )[0],
                                                val: item.split(
                                                    '-'
                                                )[1]
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
                                $("[id$=txtClienteDBCom]").val(i.item.text);
                                $("[id$=id_cliente_dbcomercial]").val(i.item.val);
                            },
                            minLength: 1
                        });
                    });



                }
            });
        };
    </script>


    <script type="text/javascript">
        $(function () {
            $("[id$=txtLugar]").autocomplete({
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
                    $("[id$=txtLugar]").val(i.item.text);
                    $("[id$=txtLugar]").val(i.item.val);
                },
                minLength: 1

            });
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(function () {
                        $("[id$=txtLugar]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/SERVICES/AUTOCOMPLETAR_MOVIMIENTOS.asmx/AUTOCOMPLETAR_LUGAR") %>',
                                    data: "{ 'prefix': '" + request.term +
                                        "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (
                                            item) {
                                            return {
                                                label: item
                                                    .split(
                                                        '-'
                                                    )[0],
                                                val: item.split(
                                                    '-'
                                                )[1]
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
                                $("[id$=txtLugar]").val(i.item.text);
                                $("[id$=txtLugar]").val(i.item.val);
                            },
                            minLength: 1

                        });
                    });


                }
            });
        };
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=txtConsultaCli]").autocomplete({
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
                    $("[id$=txtConsultaCli]").val(i.item.text);
                    $("[id$=txtConsultaCliValor]").val(i.item.val);
                },
                minLength: 1

            });
        });
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>


    <%--POUP AJAX --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="udpOutterUpdatePanel" runat="server">
            <ContentTemplate>
                <input id="Hid_Sno" type="hidden" name="hddclick" runat="server" />
                <!-- ModalPopupExtender -->
                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Hid_Sno" BackgroundCssClass="Background"
                    BehaviorID="mp1">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none" >

                    <div class="container col-lg-12 col-md-12" style="text-align: center">
                        <h2 style="font-size: large;">CUENTAS POR COBRAR DBCOMERCIAL (Sólo referencial)</h2>
                        &nbsp
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="Server">
                        <ContentTemplate>
                            <div class="container col-lg-12 col-md-12">
                                <asp:Label ID="lbltieneamarre" runat="server" Text=""></asp:Label>
                                <div style="width: 1195px; height: 130px; overflow-y: scroll; margin-left: -12px;">
                                    <asp:GridView ID="dgvPOPUP_AMARRADOS" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover"
                                        Font-Size="Small" OnRowCommand="dgvPOPUP_AMARRADOS_RowCommand" Height="10">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButtonDESUnir" CommandName="DESUNIR" runat="server" ImageUrl="~/ICONOS/down-arrow.png" Width="19"
                                                        Height="19" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DV_NUMEROINT" HeaderText="N° VENTA" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA" HeaderText="FECHA" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                                                ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N}" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MONEDA" HeaderText="MON" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SALDO" HeaderText="SALDO" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N}" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_VCTO" HeaderText="FECHA VCTO" HeaderStyle-HorizontalAlign="Center" Visible="false" DataFormatString="{0:dd/MM/yyyy}"
                                                ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    &nbsp
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dgvPOPUP_AMARRADOS" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="container col-lg-12 col-md-12">
                                <div class="form-group col-lg-2 col-md-2">
                                    <asp:TextBox runat="server" TextMode="Date" ID="id_cliente_dbcomercial" CssClass="visible-xs" placeholder="" MaxLength="100"
                                        Height="35px"></asp:TextBox>
                                    <div class="col-lg-12 col-md-12" style="left: -25px;">
                                        <label>Cod Venta:</label>
                                        <asp:TextBox ID="cbomTipoDoc" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col-lg-2 col-md-2">
                                    <div class="col-lg-12 col-md-12">
                                        <label>Fcha Inicio:</label>
                                        <asp:TextBox runat="server" TextMode="Date" ID="TextBoxssss" CssClass="form-control" placeholder="" MaxLength="100" Height="35px"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group col-lg-2 col-md-2">
                                    <div class="col-lg-12 col-md-12">
                                        <label>Fcha Fin:</label>
                                        <asp:TextBox runat="server" TextMode="Date" ID="txtFechaFinDB" CssClass="form-control" placeholder="" MaxLength="100" Height="35px"></asp:TextBox>

                                    </div>

                                </div>
                                <div class="form-group col-lg-2 col-md-2">
                                    <div class="col-lg-12 col-md-12">
                                        <label>Cliente:</label>
                                        <asp:TextBox ID="txtClienteDBCom" runat="server" CssClass="form-control" Style="text-transform: uppercase" Width="280px"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- -->



                                <div class="form-group col-lg-1 col-md-1" style="left: 100px; top: 25px;">
                                    <div class="col-lg-12 col-md-12">

                                        <asp:Button ID="btnBuscarPopUp" runat="server" Text="BUSCAR" CssClass="form-control btn-warning" Width="120px" OnClick="btnBuscarPopUp_Click" />


                                    </div>
                                </div>

                                <div class="form-group col-lg-1 col-md-1" style="left: 140px; top: 25px;">
                                    <div class="col-lg-12 col-md-12">
                                        <asp:Button ID="btnSalirPopUp" runat="server" Text="SALIR" Width="120px" CssClass="form-control btn btn-info" OnClick="btnSalirPopUp_Click" />
                                    </div>
                                </div>

                            </div>
                            <div class="container col-lg-8 col-md-8">

                                <div style="width: 1200px; height: 420px; overflow-y: scroll; margin-left: -18px;">
                                    <div class="container">

                                        <asp:GridView ID="dgvPOPUPAMARRE" runat="server" AutoGenerateColumns="false" Font-Size="Small" CssClass="table table-bordered table-condensed table-responsive table-hover"
                                            OnRowCommand="dgvPOPUPAMARRE_RowCommand" OnRowDataBound="dgvPOPUPAMARRE_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>

                                                        <asp:ImageButton ID="LinkButtonUnir" CommandName="UNIR" runat="server" ImageUrl="~/ICONOS/up-arrow.png" Width="19" Height="19" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="DV_NUMEROINT" HeaderText="N° VENTA" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FECHA" HeaderText="FECHA" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                                                    ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N}" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MONEDA" HeaderText="MON" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SALDO" HeaderText="SALDO" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N}" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" HeaderStyle-HorizontalAlign="Center" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FECHA_VCTO" HeaderText="FECHA VCTO" HeaderStyle-HorizontalAlign="Center" Visible="false" DataFormatString="{0:dd/MM/yyyy}"
                                                    ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                                <br />
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarPopUp" EventName="Click" />
                            <asp:PostBackTrigger ControlID="btnSalirPopUp" />
                            <asp:AsyncPostBackTrigger ControlID="dgvPOPUPAMARRE" EventName="RowCommand" />
                            <%--VERIFICAR SI SALE EVENTO Y RECARGA EL POPUP--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--POPUP AJAX--%>
    <div style="top:-9px">
    <input type="button" id="btnSubmit" name="btnSubmit" class="visible-xs" />
    <asp:TextBox ID="txtConsultaCliValor" runat="server" CssClass="visible-xs"></asp:TextBox>
    <div class="visible-xs" style="height: 5px; width: 1px; border-color: darkcyan; background-color: darkcyan; color: white;">
        <asp:TextBox ID="TXTprueba" runat="server" BackColor="DarkCyan" BorderColor="DarkCyan" BorderStyle="None" ForeColor="DarkCyan"
            OnTextChanged="TXTprueba_TextChanged" AutoPostBack="True"></asp:TextBox>
        <asp:TextBox ID="TXTid_cliente" runat="server" BackColor="DarkCyan" BorderColor="DarkCyan" BorderStyle="None" ForeColor="DarkCyan"
            OnTextChanged="TXTid_cliente_TextChanged"></asp:TextBox>

    </div>
    <div class="form-group col-md-6 col-sm-6 col-xs-6">

        <div class="col-md-8" style="text-align: right; margin-top: 0px;">
            <label class="col-xs-3 col-md-3" style="color: white;">N° CUENTA:</label>
            <asp:TextBox runat="server" ID="txtCuentaModal" CssClass="form-control col-xs-9 col-md-9" BackColor="AliceBlue" Style="text-transform: uppercase"
                Font-Bold="true" placeholder="Ingrese el número de cuenta, búsqueda automática" MaxLength="150"></asp:TextBox>
        </div>
        <div class="col-md-2" style="text-align: right; margin-top: 0px;">
            <asp:Button ID="btnTraeDatos" runat="server" Text="FILTRAR" CssClass="a_demo_four1 col-sm-12 col-xs-12" Height="35px" OnClick="btnTraeDatos_Click" />
        </div>
    </div>
    <div class="form-group col-md-6 col-sm-6 col-xs-6 center-block">


        <div class="col-md-5">
            <label style="color: white;">BANCO:</label>&nbsp;&nbsp;

                            <asp:Label ID="LBLBANCO" runat="server" Text="--" ForeColor="White" CssClass="label" Font-Bold="true" Font-Size="Medium" BackColor="CornflowerBlue"
                                Height="30px"></asp:Label>


        </div>

        <div class="col-md-2" style="text-align: center; margin-top: 0px;">
            <label style="color: white;">M:</label>&nbsp
                            <asp:Label ID="LBLMONEDA" runat="server" Text="--" ForeColor="White" CssClass="label" Font-Bold="true" Font-Size="Medium"
                                BackColor="CornflowerBlue" Height="30px"></asp:Label>
        </div>

        <div class="col-md-5" style="left: 50px">
            <label style="color: white; top: -10px;">SALDO CONTABLE:</label>
            <asp:Label ID="LBLSALDOC" runat="server" Text="--" ForeColor="White" CssClass="label" Font-Bold="true" Font-Size="Medium"
                DataFormatString="{0:N}" BackColor="black" Height="30px"></asp:Label>
        </div>

    </div>
    <div class="col-md-12" style="top:-8px;">
        <div class="col-xs-3 col-md-3 col-md-offset-6">
            <label style="color: white; top: 0px;">N° CUENTA:</label>&nbsp
                            <asp:Label ID="LBLNCUENTA" runat="server" Text="--" ForeColor="White" CssClass="label" Font-Bold="true" Font-Size="Medium"
                                BackColor="CornflowerBlue" Height="30px"></asp:Label>
        </div>

        <div class="col-xs-4 col-md-3 col-md-offset-9" style="text-align: right; margin-top: -30px;">
            <label style="color: white; top: 0px;">SALDO DISPONIBLE:</label>&nbsp
                            <asp:Label ID="LBLSALDOD" runat="server" Text="--" ForeColor="White" CssClass="label" Font-Bold="true" Font-Size="Medium"
                                DataFormatString="{0:N}" BackColor="CornflowerBlue" Height="30px"></asp:Label>
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

    <div class="container col-lg-12 col-md-12" style="margin-top: -40px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
        <div class="row">

            <div class="col-md-12">
                <div class="tabbed">



                    <input name="tabbed" id="tabbed1" type="radio" checked="checked" />
                    <section>
                        <h1>
                            <label for="tabbed1">REGISTRO MANUAL</label>
                        </h1>
                        <div style="background-color: lightgray; height:220px;" >
                            <%--aca empieza el form manual--%>

                            <div class="form-horizontal col-xs-12 col-md-3 col-lg-3" style="position: relative; background-color: lightgray">

                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black;">FECHA:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>

                                                <asp:TextBox runat="server" TextMode="Date" ID="txtFECHA" CssClass="form-control" placeholder="" MaxLength="100" Width="305px"
                                                    Height="35px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>


                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black">T. MOV:</label>
                                    <div class="col-xs-6 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="cboTIPOMOV" CssClass="form-control" AutoPostBack="true" Width="305px" OnSelectedIndexChanged="cboTIPOMOV_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboTIPOMOV" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black">CONCEPTO:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="cboCONCEPTO" CssClass="form-control" AutoPostBack="true" Width="305px" EnableViewState="true"
                                                    AppendDataBoundItems="True" CausesValidation="True" OnSelectedIndexChanged="cboCONCEPTO_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black;">LUGAR:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>

                                                <asp:TextBox runat="server" ID="txtLugar" CssClass="form-control" Style="text-transform: uppercase" placeholder="Escriba el lugar"
                                                    MaxLength="100" Width="305px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>



                                    </div>

                                </div>

                            </div>
                            <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative; background-color: lightgray">

                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black">N° OPE:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>

                                                <asp:TextBox runat="server" ID="txtOPE" CssClass="form-control" placeholder="INGRESE N° DE OPERACION" MaxLength="100" Width="306px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <%--VALIDADOR--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOPE" ErrorMessage="(*)El Número de Operacion es requerido"
                                            ForeColor="Red" ValidationGroup="Registro" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black">IMPORTE:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>

                                                <asp:TextBox runat="server" ID="txtIMPORTE" CssClass="form-control" placeholder="00.00" MaxLength="100" type="numeric" Width="306px"
                                                    OnBlur="addCommas(this)" onKeyUp="clickMe(this)"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <%--VALIDADOR--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIMPORTE" ErrorMessage="(*)El Importe es requerido"
                                            ForeColor="Red" ValidationGroup="Registro" Display="Dynamic">
                                        </asp:RequiredFieldValidator>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: black">DESCRIPCION:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtDESC" CssClass="form-control" Style="text-transform: uppercase" placeholder="Ingrese una descripción"
                                                    MaxLength="100" Width="306px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <%-- <label class="control-label col-xs-3" id="AAA"  style="color: white">CLIENTE:</label>--%>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="complemento" CssClass="control-label col-xs-3" runat="server" ForeColor="black" Font-Names="Segoe UI" Font-Bold="true">CLIENTE:</asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtCLIENTE" CssClass="form-control" AutoPostBack="false" Style="text-transform: uppercase"
                                                    placeholder="Ingrese Cliente busqueda automática"
                                                    Text="" MaxLength="45" Width="305px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative; background-color: none">

                                <div class="form-group">
                                    <label class="control-label col-xs-2 col-md-2" style="color: black; margin-left: 50px;">DATOS:</label>
                                    <div class="col-xs-10 col-md-10" style="margin-left: -50px;">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>

                                                <div class="row col-md-12" style="float: left">

                                                    <asp:DropDownList runat="server" ID="cboFVBV" CssClass="form-control col-md-2" Width="65" Height="32" AutoPostBack="true" OnSelectedIndexChanged="cboFVBV_SelectedIndexChanged">
                                                        <asp:ListItem Value="">--</asp:ListItem>
                                                        <asp:ListItem Value="FT">FT</asp:ListItem>
                                                        <asp:ListItem Value="BV">BV</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:TextBox runat="server" ID="txtserie" CssClass="form-control col-md-3" AutoPostBack="true" MaxLength="3" Width="60" Height="32" onkeydown="return jsDecimals(event);" placeholder="SERIE" OnTextChanged="txtserie_TextChanged"></asp:TextBox>
                                                    &nbsp;
                                        <asp:TextBox runat="server" ID="txtnumero" AutoPostBack="true" CssClass="form-control col-md-3" Width="75" Height="32" MaxLength="5" onkeydown="return jsDecimals(event);" placeholder="NUM" OnTextChanged="txtnumero_TextChanged"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtnumero2" AutoPostBack="true" Visible="false" CssClass="form-control col-md-3" Width="75" Height="32" MaxLength="5" onkeydown="return jsDecimals(event);" placeholder="FINAL" OnTextChanged="txtnumero2_TextChanged"></asp:TextBox>

                                                    <asp:Button runat="server" CssClass="btn btn-warning" Height="35" ID="btnagregar" Width="45" Font-Bold="true" Text="OK" OnClick="btnagregar_Click" />

                                                    <asp:CheckBox ID="chkMULTIPLE" runat="server" AutoPostBack="true" Text="Rango" Font-Size="Smaller" ToolTip="Chequear si desea registrar un rango de BV" OnCheckedChanged="chkMULTIPLE_CheckedChanged" />
                                                </div>

                                                <div style="width: 100%; height: 120px; margin-top: 0px; overflow-y: scroll;">
                                                    <asp:GridView ID="dgvDATOS" runat="server" AutoGenerateColumns="False"
                                                        Font-Size="Smaller" AutoGenerateEditButton="false" CssClass="table table-responsive table-condensed table-bordered" BackColor="White" EmptyDataText="No existen datos" OnRowCommand="dgvDATOS_RowCommand">
                                                        <Columns>

                                                            <asp:BoundField HeaderText="DOC" DataField="DOC" HeaderStyle-Font-Size="Smaller" HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle Font-Size="Smaller" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="SERIE" DataField="SERIE" HeaderStyle-Font-Size="Smaller" HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle Font-Size="Smaller" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="NUMERO" DataField="NUMERO" HeaderStyle-Font-Size="Smaller" HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle Font-Size="Smaller" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Buttoneliminar" runat="server" CommandName="ELIM" CssClass="btn btn-danger fa fa-trash-o" Height="21" Text="" Font-Size="Large" />
                                                                </ItemTemplate>
                                                                <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#66CCFF" />
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnagregar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="dgvDATOS" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="chkMULTIPLE" EventName="CheckedChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="cboFVBV" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtserie" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtnumero" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>


                                <div class="form-group"  style="background-color:none">
                                    <label class="control-label col-xs-3" style="color: black">OBSERVACIÓN:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>

                                                <asp:TextBox runat="server" ID="txtObservacione" CssClass="form-control" Style="text-transform: uppercase" placeholder="Ingrese observación"
                                                    MaxLength="500" Width="336px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>


                            </div>


                            <div class="form-horizontal col-xs-12 col-md-1 col-lg-1" style="position: relative; background-color: lightgray">

                                <div class="col-md-12 col-sm-12  col-xs-12">

                                    <asp:Button runat="server" CssClass="a_demo_four1 col-md-12 col-sm-12 col-xs-12" Font-Bold="true" Text="NUEVO" ID="btnNuevo"
                                        AccessKey="N" Width="120px" OnClick="btnNuevo_Click" />

                                </div>
                                <div>&nbsp;</div>
                                <div class="col-md-12 col-sm-12  col-xs-12" style="margin-top: -5px;">

                                    <asp:Button runat="server" CssClass="a_demo_four1 col-md-12 col-sm-12 col-xs-12" Font-Bold="true" ValidationGroup="Registro"
                                        AccessKey="G" Width="120px" Text="GRABAR" ID="btnRegistrar" OnClick="btnRegistrar_Click" />

                                </div>
                                
                                <div class="col-md-1 col-sm-1  col-xs-1" style="margin-top: -5px;">

                                    <asp:Button runat="server" CssClass="a_demo_four1 col-md-12 col-sm-12 col-xs-12" Font-Bold="true" Visible="false" Text="ACTUALIZAR"
                                        ID="btnActualizar" Width="1px" OnClick="btnActualizar_Click" />

                                </div>
                                <div>&nbsp;</div>
                                <div class="col-md-12 col-sm-12  col-xs-12" style="margin-top: -5px;">
                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" CssClass="a_demo_four1 col-md-12 col-sm-12 col-xs-12" Font-Bold="true" Text="CANCELAR" ID="btnCancelar"
                                                AccessKey="C" Width="120px" OnClick="btnCancelar_Click" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnCancelar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#<%= btnCancelar.ClientID %>').click(
                                                function (e) {
                                                    $(function () {
                                                        RetainSelectedRow2();
                                                    });
                                            return true;
                                            });
                                        });
                                    </script>
                                    <script type="text/javascript">
                                        $(document).ready(function () {
                                            $('#<%= btnActualizar.ClientID %>').click(
                                                function (e) {
                                                    $(function () {
                                                        RetainSelectedRow2();
                                                    });
                                            return true;
                                            });
                                        });
                                    </script>
                                </div>
                                <!-- ACA VA EL BOTON RECALCULAR SALDOS-->
                                <div>&nbsp;</div>
                                <div style="float: left; margin-top: 0px; width: 10%; margin-left: -35px;">
                                <asp:Button ID="btnSALDOS" runat="server" Text="ACTUALIZAR SALDOS" CssClass="btn btn-warning text-box" Width="165px" Font-Bold="true" OnClick="btnSALDOS_Click" />
                                </div>

                            </div>
                        </div>
                    </section>


                    <input name="tabbed" id="tabbed2" type="radio" />
                    <section>
                        <h1>
                            <label for="tabbed2">
                                CARGAR ARCHIVO EXCEL
                            </label>
                        </h1>
                        <div style="background-color: lightgray">


                            <div class="col-xs-3 col-md-3" style="width: 10%; text-align: right; margin-top: 9px;">
                                <label style="color: black; top: 5px; left: 0px; text-align: center;">CARGA POR EXCEL:</label>
                            </div>
                            <div style="float: left; margin-top: 9px;">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                            </div>
                            <div style="float: left; margin-top: 9px;">
                                &nbsp;&nbsp;
                                 <asp:Button ID="Button1" runat="server" Text="CARGAR DATOS" CssClass="a_demo_four1" OnClick="Button1_Click" />
                                &nbsp;&nbsp;
                            </div>


                        </div>
                    </section>
                    &nbsp &nbsp

                                    <!-- and so on -->

                </div>
                &nbsp



            </div>
            <div class="tab-pane fade" id="tab2primary">
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnConsulta">
        <div class="row">
            <div class="col-xs-12 col-sm-offset-0 col-sm-12 col-md-offset-0 col-md-12" style="height:60px;">
                <div class="form-group col-md-12 col-sm-12 col-xs-12 center-block" style="text-align: center">

                    <div class="col-xs-4 col-md-4" style="width: 10%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-left: -60px;">FILTROS:</label>
                        <asp:TextBox runat="server" ID="txtConsultaOpe" CssClass="form-control col-xs-12 col-sm-12" Font-Bold="true" placeholder="N° OPERACION"
                            MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 18%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">CLIENTE:</label>
                        <asp:TextBox runat="server" ID="txtConsultaCli" CssClass="form-control col-xs-12 col-sm-12" Style="text-transform: uppercase"
                            Font-Bold="true" placeholder="Búsqueda de cliente" MaxLength="250"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 13%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">CONCEPTO:</label>
                        <asp:DropDownList runat="server" ID="cboFiltroConc" CssClass="form-control col-xs-12 col-sm-12" AutoPostBack="false"></asp:DropDownList>

                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 11%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">F.INICIAL:</label>
                        <asp:TextBox runat="server" ID="txtFechaIni" CssClass="form-control col-xs-12 col-sm-12" Height="35px" TextMode="Date" Font-Size="Small"
                            placeholder="Ingrese Fecha inicial" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 11%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">F.FINAL:</label>
                        <asp:TextBox runat="server" ID="txtFechaFin" CssClass="form-control col-xs-12 col-sm-12" Height="35px" TextMode="Date" Font-Size="Small"
                            placeholder="Ingrese Fecha final" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 11%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-left: -30px;">OBSERVACION:</label>
                        <asp:TextBox runat="server" ID="txtFiltroOBS" CssClass="form-control col-xs-12 col-sm-12" Font-Bold="true" placeholder="OBSERVACION"
                            MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-md-5" style="width: 14%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-left: -30px;">RANGO IMPORTE:</label>&nbsp;
                        <div>
                            <asp:TextBox runat="server" ID="txtMINIMPO" CssClass="form-control col-xs-5 col-sm-5" Font-Bold="true" placeholder="MIN"
                            MaxLength="150"  OnBlur="addCommas(this)" onkeydown="return jsDecimals(event);" ></asp:TextBox>
                        </div>
                       
                        <div >
                            <asp:TextBox runat="server" ID="txtMAXIMPO" CssClass="form-control col-xs-6 col-sm-6" Font-Bold="true" placeholder="MAX"
                            MaxLength="150"  OnBlur="addCommas(this)" onkeydown="return jsDecimals(event);"></asp:TextBox>
                        </div>
                        
                    </div>
                   
                    <div style="float:left; margin-top: 8px; width: 3%; margin-left:-25px;">

                         <asp:CheckBox ID="chkITF" runat="server" AutoPostBack="false" CssClass="checkbox" ForeColor="White" Text="ITF" Font-Size="Small" ToolTip="Chequear si desea mostrar ITF" />
                    
                    </div>
                    <div style="float:left; margin-top: 8px; width: 6%;">

                        <asp:Button ID="btnConsulta" runat="server" Text="BUSCAR" CssClass="a_demo_four1" Font-Bold="true" OnClick="btnConsulta_Click" />

                    </div>
                    <div style="float:right; margin-top: 0px; width: 3%; margin-left: 0px;">


                        <asp:ImageButton ID="reportePDF" runat="server" Height="50px" ImageUrl="~/ICONOS/Graphicloads-Filetype-Pdf.ico" OnClick="reportePDF_Click"
                            Width="50px" />

                    </div>
                    
                    

                </div>
            </div>
            <div style="width: 100%; height: 400px; top:-90px;">
                <!-- overflow-y: scroll -->

                <%--<div style="top:-20px;">
                         <span style="float:right;"><small style="color:white;">N° de Registros:</small> <asp:Label ID="lblCANTROWS" runat="server" CssClass="label label-warning" /></span>
                    </div>--%>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <asp:GridView ID="dgvMOVIMIENTOS" runat="server" CssClass="table table-bordered table-responsive table-condensed" BackColor="White"
                            AutoGenerateColumns="False" DataKeyNames="ID_MOVIMIENTOS" OnRowCommand="dgvMOVIMIENTOS_RowCommand"
                            OnRowDataBound="dgvMOVIMIENTOS_RowDataBound" SelectedRowStyle-BackColor="GreenYellow"
                            Font-Size="Smaller" AllowPaging="True" OnPageIndexChanging="dgvMOVIMIENTOS_PageIndexChanging" PageSize="20" OnDataBound="dgvMOVIMIENTOS_DataBound">
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />

                            <Columns>
                                <asp:BoundField DataField="ID_MOVIMIENTOS" HeaderText="CODIGO" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" Font-Size="Small" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA" HeaderText="FECHA DEPO" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px"
                                    ConvertEmptyStringToNull="true">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OPERACION" HeaderText="N° OPERACION" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px"
                                    ConvertEmptyStringToNull="true">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" DataFormatString="{0:N}" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MONEDA" HeaderText="MON" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOM_CLI" HeaderText="CLIENTE" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px" ConvertEmptyStringToNull="true">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NOMBRE" HeaderText="BANCO" Visible="false" />
                                <asp:BoundField DataField="LUGAR" HeaderText="LUGAR" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="35px">

                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TIPO_MOV" HeaderText="TIPO MOV" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller"></HeaderStyle>
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="SALDO" HeaderText="SALDO" DataFormatString="{0:N}" ItemStyle-Height="35px">
                                    <HeaderStyle HorizontalAlign="Right" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller" />
                                    <ItemStyle Height="35px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" ItemStyle-Height="35px" ItemStyle-Width="500px" ItemStyle-Wrap="true">
                                    <HeaderStyle HorizontalAlign="Right" BackColor="#0066cc" ForeColor="White" Font-Size="Smaller" />
                                    <ItemStyle Height="35px" Width="500" />
                                </asp:BoundField>

                                <asp:BoundField DataField="COD_VENTA" HeaderText="AMARRE">
                                    <HeaderStyle HorizontalAlign="Right" BackColor="#0066cc" ForeColor="White"
                                        Font-Size="Smaller" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White" ItemStyle-Width="80">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="LinkButtoneditar" runat="server" CommandName="EDITAR"  CssClass="fa fa-edit" Font-Size="Small" OnClientClick="return GetSelectedRow(this)">
                                              EDITAR
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="LinkButtoneliminar" runat="server" usesubmitbehavior="false" CommandName="ELIMINAR" CssClass="fa fa-trash-o" Font-Size="Small" OnClientClick="if (!confirm('Esta seguro de eliminar el registro?')) return false;">
                                               ELIMINAR
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="#0066cc" HeaderStyle-ForeColor="White" ItemStyle-Width="90">
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="LinkButtonamarre" runat="server" CommandName="AMARRE" usesubmitbehavior="false" Font-Size="Small" CssClass="fa fa-chain" OnClientClick="">
                                                AMARRE
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                                </asp:TemplateField>
                            </Columns>

                            <PagerSettings Mode="NumericFirstLast" />
                            <SelectedRowStyle BackColor="GreenYellow" />

                        </asp:GridView>
                        &nbsp
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="dgvMOVIMIENTOS" EventName="RowCommand"/>--%>
                        <asp:PostBackTrigger ControlID="dgvMOVIMIENTOS" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:HiddenField ID="hfCurrentRowIndex" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfParentContainer" runat="server"></asp:HiddenField>
        </div>
            </asp:Panel>
    </div>
   

    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
