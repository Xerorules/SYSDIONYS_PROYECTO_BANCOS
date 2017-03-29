<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_CUENTAS.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <script src="assets/js/jquery-1.10.2.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
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
  

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid col-lg-4 col-md-4">
        <div class="form-horizontal" style="position: relative;">
            <h1 style="color: white;" class="main-text">MANTENIMIENTO DE CUENTAS</h1>
            &nbsp
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">EMPRESA:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:DropDownList runat="server" ID="CBOEMPRESA" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">CUENTA:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtCUENTA" CssClass="form-control" placeholder="INGRESE NUMERO DE CUENTA" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtCUENTA"
                        ErrorMessage="(*)El Numero de Cuenta es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">BANCO:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:DropDownList runat="server" ID="cboBANCO" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboBANCO_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                        ControlToValidate="cboBANCO"
                        ErrorMessage="(*)Escoja un Banco vinculado a la cuenta"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">CCI:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtCCI" CssClass="form-control" placeholder="CCI" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtCCI"
                        ErrorMessage="(*)El Numero de CCI es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white; margin-top: -7px;">TIPO DE MONEDA:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:RadioButtonList ID="rdbMONEDA" runat="server" RepeatDirection="Horizontal" Width="200px" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="White" AutoPostBack="True">
                        <asp:ListItem Value="SOLES">SOLES</asp:ListItem>
                        <asp:ListItem Value="DOLARES">DOLARES</asp:ListItem>
                    </asp:RadioButtonList>

                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">SALDO CONTABLE:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtSCONTABLE" CssClass="form-control" placeholder="" MaxLength="100" type="number"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtSCONTABLE"
                        ErrorMessage="(*)Ingrese el Saldo Contable"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">SALDO DISPONIBLE:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtSDISPONIBLE" CssClass="form-control" placeholder="" MaxLength="100" type="number"></asp:TextBox>
                    &nbsp
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">SECTORISTA:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtSECTOR" CssClass="form-control" placeholder="SECTORISTA" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtSECTOR"
                        ErrorMessage="(*)El Sectorista es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">OFICINA:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtOFICINA" CssClass="form-control" placeholder="DIRECCION DE OFICINA" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtOFICINA"
                        ErrorMessage="(*)La Oficina  es requerida"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">TELEFONO:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtTELEFONO" CssClass="form-control" placeholder="5555555" MaxLength="45"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ControlToValidate="txtTELEFONO"
                        ErrorMessage="(*)El número de telefono es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">E-MAIL:</label>
                <div class="col-xs-8 col-md-7">
                    <asp:TextBox runat="server" ID="txtEMAIL" CssClass="form-control" placeholder="ejemplo@gmail.com" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                        ControlToValidate="txtEMAIL"
                        ErrorMessage="(*)El correo electrónico requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    &nbsp
                </div>
            </div>

            <div class="form-group col-md-12 col-sm-12 col-xs-12" style="position: center;">
                &nbsp

                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center;">
                    <asp:Button runat="server" class="form-control btn btn-primary" ValidationGroup="Registro" Text="REGISTRAR" ID="btnRegistrar" OnClick="btnRegistrar_Click" />
                    &nbsp
                </div>
                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center;">
                    <asp:Button runat="server" class="form-control btn btn-primary" Text="ACTUALIZAR" ID="btnActualizar" OnClick="btnActualizar_Click" />
                    &nbsp
                </div>
                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center">
                    <asp:Button runat="server" class="form-control btn btn-primary" Text="CANCELAR" ID="btnCancelar" OnClick="btnCancelar_Click" />


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
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se pudo realizar la operación, existen movimientos vinculados  a esta cuenta</h3>
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

    <div class="container-fluid col-lg-8 col-md-8">
        <div class="row">
            <div class="col-xs-11 col-sm-offset-0 col-sm-11 col-md-offset-0 col-md-10">

                <h2 style="color: white" class="text-info">LISTA DE CUENTAS</h2>
                &nbsp
       <div style="width: 120%; height: 700px; overflow: scroll">
           <asp:GridView ID="dgvBANCOS" runat="server" CssClass="table table-striped table-bordered  table-hover " BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_CUENTASBANCARIAS" PageSize="1"
               AllowPaging="false" OnPageIndexChanging="dgvBANCOS_PageIndexChanging"  OnRowCommand="dgvBANCOS_RowCommand">
              
               <Columns>
                   <asp:BoundField DataField="ID_CUENTASBANCARIAS" HeaderText="CODIGO" />
                   <asp:BoundField DataField="N_CUENTA" HeaderText="NUMERO DE CUENTA" ><ItemStyle Width="150px"  /> </asp:BoundField>
                   <asp:BoundField DataField="NOMBRE" HeaderText="BANCO" />
                   <asp:BoundField DataField="N_CCI" HeaderText="CCI" />
                   <asp:BoundField DataField="MONEDA" HeaderText="MONEDA" />
                   <asp:BoundField DataField="SALDO_CONTABLE" HeaderText="SALDO CONTABLE" />
                   <asp:BoundField DataField="SALDO_DISPONIBLE" HeaderText="SALDO DISPONIBLE" />
                   <asp:BoundField DataField="SECTORISTA" HeaderText="SECTORISTA" />
                   <asp:BoundField DataField="OFICINA" HeaderText="OFICINA" />
                   <asp:BoundField DataField="TELEFONO" HeaderText="TELEFONO" />
                   <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                   <asp:TemplateField SortExpression="">
                       <ItemTemplate>
                           <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="EDITAR">Editar</asp:LinkButton>
                       </ItemTemplate>
                   </asp:TemplateField>

                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:LinkButton ID="LinkButtoneliminar" runat="server" CommandName="ELIMINAR">Eliminar</asp:LinkButton>
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
