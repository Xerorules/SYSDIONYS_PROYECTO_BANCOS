<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_MOVIMIENTOS.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="assets/css/ptoledo.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />

    <link href="assets/css/css_tab_panel_csspuro.css" rel="stylesheet" />

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
        //function openModal() {
        //    $('#myModalCUENTA').modal('show');
        //}

     <%--   function validar_prueba() {
            var txtcuenta = $('#<%=txtCuentaModal.ClientID%>');
            if (txtcuenta.val == "a") {
                $('#myModalCUENTA').modal('hide');
                return true;
            } else {
                clickEvent.preventDefault();
                clickEvent.stopPropagation();
                document.getElementById('Label2').InnerHTML = 'INGRESE UN NUMERO DE CUENTA';
                return false;
            }
        }--%>

        <%--function validar() {
            var error = false;
            var mensaje = "";
            if ($('#<%=txtCuentaModal.ClientID%>').val() == '' || $('#<%=txtCuentaModal.ClientID%>').val() == undefined) {
                mensaje = "Debe ingresar el N° de cuenta";
                $('#Label2').text(mensaje);
                error = true;
            }
            if ($('#<%=txtCuentaModal.ClientID%>').val().length < 1 && error === false) {
                mensaje = "Debe ingresar el N° de cuenta";
                $('#Label2').text(mensaje);
                error = true;
            }

        }--%>

        function mostrarpopUp() {
            $find("mp1").show();
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
            width: 1220px;
            height: 600px;
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
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
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

    <input id="Hid_Sno" type="hidden" name="hddclick" runat="server" />
    <!-- ModalPopupExtender -->
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Hid_Sno"
        CancelControlID="Button3" BackgroundCssClass="Background" BehaviorID="mp1">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
        <div class="container col-lg-12 col-md-12" style="text-align: center">
            <h2>Amarre de movimientos</h2>
            &nbsp
        </div>
        
        <div class="container col-lg-12 col-md-12">
            <div class="form-group col-lg-2 col-md-2">
                <asp:TextBox runat="server"  TextMode="Date" ID="id_cliente_dbcomercial" CssClass="visible-xs" placeholder="" MaxLength="100"  Height="35px"></asp:TextBox>
                <div class="col-lg-12 col-md-12" style="left:-25px;">
                    <label>Cod Venta:</label>
                    <asp:TextBox ID="cbomTipoDoc" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-lg-2 col-md-2">
                <div class="col-lg-12 col-md-12">
                    <label>Fcha Inicio:</label>
                    <asp:TextBox runat="server" TextMode="Date" ID="TextBoxssss" CssClass="form-control" placeholder="" MaxLength="100"  Height="35px"></asp:TextBox>
                </div>
            </div>
           
            <div class="form-group col-lg-2 col-md-2">
               <div class="col-lg-12 col-md-12">
                <label>Fcha Fin:</label>
                   <asp:TextBox runat="server" TextMode="Date" ID="txtFechaFinDB" CssClass="form-control" placeholder="" MaxLength="100"  Height="35px"></asp:TextBox>

                </div>

            </div>
             <div class="form-group col-lg-2 col-md-2">
                <div class="col-lg-12 col-md-12">
                    <label>Cliente:</label>
                    <asp:TextBox ID="txtClienteDBCom" runat="server" CssClass="form-control" Width="280px"></asp:TextBox>
                </div>
            </div>
            <!-- -->



            <div class="form-group col-lg-1 col-md-1" style="left:100px; top:25px;">
                 <div class="col-lg-12 col-md-12">
                     <asp:Button ID="btnBuscarPopUp" runat="server" Text="BUSCAR" CssClass="form-control btn-warning" Width="120px" OnClick="btnBuscarPopUp_Click" />
                 </div>
            </div>

            <div class="form-group col-lg-1 col-md-1" style="left:140px; top:25px;">
            <div class="col-lg-12 col-md-12">
                     <asp:Button ID="Button3" runat="server" Text="CANCELAR" Width="120px" CssClass="form-control btn btn-info" />
                </div>
            </div>
           
        </div>
        <div class="container col-lg-8 col-md-8">

            <div style="width: 1195px; height: 450px; overflow-y: scroll; margin-left: -12px;">
                <div class="container">
                    <asp:GridView ID="dgvPOPUPAMARRE" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-responsive table-hover" OnRowCommand="dgvPOPUPAMARRE_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUnir" runat="server" CommandName="UNIR" Font-Size="Small">&nbsp;Vincular</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:ButtonField Text="Vincular" CommandName="UNIR"/>--%>
                            <asp:BoundField DataField="DV_NUMEROINT" HeaderText="COD VENTA" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA" HeaderText="FECHA" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MONEDA" HeaderText="MON" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SALDO" HeaderText="SALDO" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N}">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA_VCTO" HeaderText="FECHA VCTO" HeaderStyle-HorizontalAlign="Center" Visible="false" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br/>
        </div>
                

    </asp:Panel>
    <%--POPUP AJAX--%>
    
    <input type="button" id="btnSubmit" name="btnSubmit" class="visible-xs" />
    <asp:TextBox ID="txtConsultaCliValor" runat="server" CssClass="visible-xs"></asp:TextBox>
    <div class="visible-xs" style="height: 5px; width: 1px; border-color: darkcyan; background-color: darkcyan; color: white;">
        <asp:TextBox ID="TXTprueba" runat="server" BackColor="DarkCyan" BorderColor="DarkCyan" BorderStyle="None" ForeColor="DarkCyan" OnTextChanged="TXTprueba_TextChanged" AutoPostBack="True"></asp:TextBox>
        <asp:TextBox ID="TXTid_cliente" runat="server" BackColor="DarkCyan" BorderColor="DarkCyan" BorderStyle="None" ForeColor="DarkCyan" OnTextChanged="TXTid_cliente_TextChanged"></asp:TextBox>

    </div>
    <div class="form-group col-md-12 col-sm-12 col-xs-12 center-block">

        <div class="col-xs-4 col-md-4" style="text-align: right; margin-top: -10px;">
            <label class="col-xs-3 col-md-3" style="color: white;">N° CUENTA:</label>
            <asp:TextBox runat="server" ID="txtCuentaModal" CssClass="form-control col-xs-9 col-md-9" BackColor="AliceBlue" Font-Bold="true" placeholder="Ingrese el número de cuenta, búsqueda automática" MaxLength="150"></asp:TextBox>
        </div>

        <div class="col-xs-1 col-md-1" style="text-align: right; margin-top: -10px;">
            <asp:Button ID="btnTraeDatos" runat="server" Text="FILTRAR" CssClass="btn btn-info col-sm-12 col-xs-12" Height="35px" OnClick="btnTraeDatos_Click" />
        </div>

        <div class="col-xs-4 col-md-4" style="text-align: center; margin-top: -10px;">
            <label style="color: white; top: -10px;">BANCO:</label>&nbsp
            <asp:Label ID="LBLBANCO" runat="server" Text="--" ForeColor="White" CssClass="form-control" Font-Bold="true" Font-Size="Large" BackColor="CornflowerBlue" Height="35px"></asp:Label>
        </div>

        <div class="col-xs-3 col-md-3" style="text-align: center; margin-top: -10px;">
            <label style="color: white; top: 0px; text-align: left;">N° CUENTA:</label>&nbsp
            <asp:Label ID="LBLNCUENTA" runat="server" Text="--" ForeColor="White" CssClass="form-control" Font-Bold="true" Font-Size="Large" BackColor="CornflowerBlue" Height="35px"></asp:Label>
        </div>






    </div>
    <div class="col-xs-4 col-md-12" style="text-align: right; left: 350px; margin-top: 4px; float: right;">

        <div class="col-xs-4 col-md-3 right">
            <label style="color: white; top: 0px;">MONEDA:</label>&nbsp
            <asp:Label ID="LBLMONEDA" runat="server" Text="--" ForeColor="White" CssClass="form-control" Font-Bold="true" Font-Size="Large" BackColor="CornflowerBlue" Height="35px"></asp:Label>
        </div>

        <div class="col-xs-4 col-md-3 right">
            <label style="color: white; top: 0px;">SALDO CONTABLE:</label>&nbsp
            <asp:Label ID="LBLSALDOC" runat="server" Text="--" ForeColor="White" CssClass="form-control" Font-Bold="true" Font-Size="Large" DataFormatString="{0:N}" BackColor="Orange" Height="35px"></asp:Label>
        </div>
        <div class="col-xs-4 col-md-3 right">
            <label style="color: white; top: 0px;">SALDO DISPONIBLE:</label>&nbsp
            <asp:Label ID="LBLSALDOD" runat="server" Text="--" ForeColor="White" CssClass="form-control" Font-Bold="true" Font-Size="Large" DataFormatString="{0:N}" BackColor="CornflowerBlue" Height="35px"></asp:Label>
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

    <div class="container col-lg-12 col-md-12" style="margin-top: -30px">
        <div class="row">

            <div class="col-md-12">
                <div class="tabbed">



                    <input name="tabbed" id="tabbed1" type="radio" checked="checked" />
                    <section>
                        <h1>
                            <label for="tabbed1">REGISTRO MANUAL</label>
                        </h1>
                        <div style="background-color: grey">
                            <%--aca empieza el form manual--%>

                            <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative; background-color: grey">

                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white;">FECHA:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:TextBox runat="server" TextMode="Date" ID="txtFECHA" CssClass="form-control" placeholder="" MaxLength="100" Width="335px" Height="35px"></asp:TextBox>

                                    </div>

                                </div>


                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white">MOVIMIENTO:</label>
                                    <div class="col-xs-6 col-md-8">
                                        <asp:DropDownList runat="server" ID="cboTIPOMOV" CssClass="form-control" AutoPostBack="false" Width="335px">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white">C. BANCARIO:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:DropDownList runat="server" ID="cboCONCEPTO" CssClass="form-control" AutoPostBack="false" Width="335px" EnableViewState="true" AppendDataBoundItems="True" CausesValidation="True">
                                        </asp:DropDownList>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white;">LUGAR:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtLugar" CssClass="form-control" placeholder="Escriba el lugar" MaxLength="100" Width="335px"></asp:TextBox>

                                    </div>

                                </div>
                            </div>
                            <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative; background-color: grey">
                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white">N° OPERACION:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtOPE" CssClass="form-control" placeholder="Ingrese la operación" MaxLength="100" Width="336px"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white">IMPORTE:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtIMPORTE" CssClass="form-control" placeholder="00.00" MaxLength="100" type="numeric" Width="336px" OnBlur="addCommas(this)"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white">DESCRIPCION:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtDESC" CssClass="form-control" placeholder="Ingrese una descripción" MaxLength="100" Width="336px"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="control-label col-xs-3" style="color: white">CLIENTE:</label>
                                    <div class="col-xs-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtCLIENTE" CssClass="form-control" placeholder="Ingrese Cliente busqueda automática" Text="" MaxLength="45" Width="335px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            &nbsp
                          <div class="form-horizontal col-xs-12 col-md-4 col-lg-4" style="position: relative; background-color: grey">
                              <div class="form-group">
                                  <label class="control-label col-xs-3" style="color: white">OBSERVACIÓN:</label>
                                  <div class="col-xs-8 col-md-8">
                                      <asp:TextBox runat="server" ID="txtObservacione" CssClass="form-control" placeholder="Ingrese observación" MaxLength="100" Width="336px"></asp:TextBox>
                                  </div>

                              </div>


                          </div>


                            <div class="form-group col-md-12 col-sm-12 col-xs-12 center-block">

                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <asp:Button runat="server" CssClass="form-control  btn-info col-md-10 col-sm-10 col-xs-12" Font-Bold="true" Text="NUEVO" ID="btnNuevo" OnClick="btnNuevo_Click" />

                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <asp:Button runat="server" CssClass="form-control  btn-info col-md-10 col-sm-10 col-xs-12" Font-Bold="true" Text="REGISTRAR" ID="btnRegistrar" OnClick="btnRegistrar_Click" />

                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <asp:Button runat="server" CssClass="form-control  btn-info col-md-10 col-sm-10 col-xs-12" Font-Bold="true" Text="ACTUALIZAR" ID="btnActualizar" OnClick="btnActualizar_Click" />

                                </div>

                                <div class="col-md-3 col-sm-3 col-xs-12">
                                    <asp:Button runat="server" CssClass="form-control  btn-info col-md-10 col-sm-10 col-xs-12" Font-Bold="true" Text="CANCELAR" ID="btnCancelar" OnClick="btnCancelar_Click" />
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
                        <div style="background-color: grey">


                            <div class="col-xs-3 col-md-3" style="width: 10%; text-align: right; margin-top: 9px;">
                                <label style="color: white; top: 5px; left: 0px; text-align: center;">CARGA POR EXCEL:</label>
                            </div>
                            <div style="float: left; margin-top: 9px;">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                            </div>
                            <div style="float: left; margin-top: 9px;">
                                &nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" Text="CARGAR DATOS" CssClass="btn btn-info" OnClick="Button1_Click" />
                                &nbsp;&nbsp;
                            </div>

                        </div>
                    </section>
                    &nbsp
                     &nbsp
               
        <!-- and so on -->

                </div>
                &nbsp
                


            </div>
            <div class="tab-pane fade" id="tab2primary">
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 col-sm-offset-0 col-sm-12 col-md-offset-0 col-md-12">
                <div class="form-group col-md-12 col-sm-12 col-xs-12 center-block" style="text-align: center">

                    <div class="col-xs-4 col-md-4" style="width: 15%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-left: -60px;">FILTROS:</label>
                        <asp:TextBox runat="server" ID="txtConsultaOpe" CssClass="form-control col-xs-12 col-sm-12" Font-Bold="true" placeholder="Ingrese N° operación" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 15%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">CLIENTE:</label>
                        <asp:TextBox runat="server" ID="txtConsultaCli" CssClass="form-control col-xs-12 col-sm-12" Font-Bold="true" placeholder="Búsqueda de cliente" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 15%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">CONCEPTO:</label>
                        <asp:DropDownList runat="server" ID="cboFiltroConc" CssClass="form-control col-xs-12 col-sm-12" AutoPostBack="false"></asp:DropDownList>

                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 14%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">F.INICIAL:</label>
                        <asp:TextBox runat="server" ID="txtFechaIni" CssClass="form-control col-xs-12 col-sm-12" Height="35px" TextMode="Date" Font-Bold="true" placeholder="Ingrese Fecha inicial" MaxLength="70"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-md-4" style="width: 14%; text-align: center; top: -15px;">
                        <label style="color: white; text-align: left; margin-right: 60px;">F.FINAL:</label>
                        <asp:TextBox runat="server" ID="txtFechaFin" CssClass="form-control col-xs-12 col-sm-12" Height="35px" TextMode="Date" Font-Bold="true" placeholder="Ingrese Fecha final" MaxLength="70"></asp:TextBox>
                    </div>
                    <div style="float: left; margin-top: 9px; width: 8%;">

                        <asp:Button ID="btnConsulta" runat="server" Text="BUSCAR" CssClass="btn btn-info" Font-Bold="true" OnClick="btnConsulta_Click" />

                    </div>
                    <div style="float: left; margin-top: 0px; width: 8%; margin-left: -5px;">

                        <%-- <asp:Button ID="btnREPORTE" runat="server" Text="VER REPORTE" CssClass="btn btn-info"  Font-Bold="true" OnClick="btnREPORTE_Click" />--%>
                        <asp:ImageButton ID="reportePDF" runat="server" Height="50px" ImageUrl="~/ICONOS/Graphicloads-Filetype-Pdf.ico" OnClick="reportePDF_Click" Width="50px" />
                        <%--<asp:ImageButton ID="reporteEXCEL" runat="server" Height="50px" ImageUrl="~/ICONOS/Graphicloads-Filetype-Excel-xls.ico" OnClick="reporteEXCEL_Click" Width="50px" />--%>
                    </div>
                    <div style="float: left; margin-top: 9px; width: 8%; margin-left: 5px;">

                        <asp:Button ID="btnSALDOS" runat="server" Text="ACTUALIZAR SALDOS" CssClass="btn btn-warning text-box" Width="175px" Font-Bold="true" OnClick="btnSALDOS_Click" />
                    </div>

                </div>

                <div style="width: 100%; height:400px; overflow-y: scroll">


                    <asp:GridView ID="dgvMOVIMIENTOS" runat="server"  CssClass="table table-striped table-bordered  table-hover table-condensed table-responsive" BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_MOVIMIENTOS" OnRowCommand="dgvMOVIMIENTOS_RowCommand" OnRowDataBound="dgvMOVIMIENTOS_RowDataBound">

                        <Columns>
                            <asp:BoundField DataField="ID_MOVIMIENTOS" HeaderText="CODIGO" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHA" HeaderText="FECHA MOV" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OPERACION" HeaderText="N° OPERACION" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" DataFormatString="{0:N}" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MONEDA" HeaderText="MON" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NOM_CLI" HeaderText="CLIENTE" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NOMBRE" HeaderText="BANCO" Visible="false" />
                            <asp:BoundField DataField="LUGAR" HeaderText="LUGAR" HeaderStyle-HorizontalAlign="Center">

                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="TIPO_MOV" HeaderText="TIPO MOV" />


                            <asp:BoundField DataField="SALDO" HeaderText="SALDO" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField DataField="OBSERVACION" HeaderText="OBSERVACION">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField DataField="COD_VENTA" HeaderText="COD_VENTA"></asp:BoundField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtoneditar" runat="server" CommandName="EDITAR" Font-Size="Small"><asp:Image ID="Imageasdasd" runat="server" ImageUrl="~/ICONOS/EDITAR.png" style="border-width: 0px;" Height="20px" Width="20px" />&nbsp;EDITAR</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtoneliminar" runat="server" CommandName="ELIMINAR" Font-Size="Small"><asp:Image ID="Imageddsdsds" runat="server" ImageUrl="~/ICONOS/ELIMINAR.png" style="border-width: 0px;" Height="20px" Width="20px" />&nbsp;ELIMINAR</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ValidateRequestMode="Disabled">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonamarre" runat="server" CommandName="AMARRE" Font-Size="Small"><asp:Image ID="Imagefffff" runat="server" ImageUrl="~/ICONOS/58603181-enlace-icono.jpg" style="border-width: 0px;" Height="20px" Width="20px"/>&nbsp;AMARRE</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                  
                </div>
                
            </div>
        </div>
    </div>

    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
