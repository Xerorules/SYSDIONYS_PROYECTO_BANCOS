<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_BANCOS.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <script src="assets/js/jquery-1.10.2.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <link rel='stylesheet prefetch' href='https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.2.3/animate.min.css'/>
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

        function openModal33() {
            $('#myModal33').modal('show');
        }

        
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid col-lg-5">
        <div class="form-horizontal" style="position: relative;">
            <h1 style="color: white;" class="main-text">MANTENIMIENTO DE BANCOS</h1>
            &nbsp
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">Nombre o Razón Social:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtNOM" CssClass="form-control" placeholder="Nombre o Razon Social" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtNOM"
                        ErrorMessage="(*)El Nombre del Banco es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro"  Display="Dynamic">
                    </asp:RequiredFieldValidator>
                     &nbsp
                </div>

            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">RUC:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtRUC" CssClass="form-control" placeholder="RUC" MaxLength="45"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtRUC"
                        ErrorMessage="(*)El RUC del Banco es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro"  Display="Dynamic">
                    </asp:RequiredFieldValidator>
                     &nbsp
                </div>
                
            </div>

            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">Dirección:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtDIRECCION" CssClass="form-control" placeholder="Dirección" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtDIRECCION"
                        ErrorMessage="(*)La direccion es requerida"
                        ForeColor="Gold"
                        ValidationGroup="Registro"  Display="Dynamic">
                    </asp:RequiredFieldValidator>
                     &nbsp
                </div>
                
            </div>
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">Teléfono:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" placeholder="5555555" MaxLength="45"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="(*)El número de teléfono es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro"  Display="Dynamic">
                    </asp:RequiredFieldValidator>
                     &nbsp
                </div>
                
            </div>

            <div class="form-group col-md-12 col-sm-12 col-xs-12" style="position: center;">
                &nbsp

                <div class="col-md-3 col-sm-3 col-xs-12" style="position: center;">
                    <asp:Button runat="server" class="form-control btn btn-primary" Text="REGISTRAR"  ValidationGroup="Registro"  ID="btnRegistrar" OnClick="btnRegistrar_Click" />
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
    <!-- -----------------------------Modal Actualizar-------------------------------------------------------------->
    <div class="modal fade" id="myModal33" tabindex="-1" role="dialog" aria-labelledby="myModalLabel33">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <img src="ICONOS/LOGO_GRUPO_DIONYS.png" width="80" height="35" />
                    <h3 class="modal-title bg-color-red" id="myModalLabel33" style="text-align: center">"GRUPO DIONYS"</h3>

                </div>
                <div class="modal-body">
                    <h3 class="danger" style="text-align: center; font-family: 'Segoe UI'">&nbsp No se puede eliminar el Banco, existen cuentas vinculadas</h3>
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
            <div class="col-xs-10 col-sm-offset-0 col-sm-10 col-md-offset-0 col-md-6">

                <h2 style="color: white" class="text-info">LISTA DE BANCOS</h2>
                &nbsp
       
         <asp:GridView ID="dgvBANCOS" runat="server" CssClass="table table-striped table-bordered  table-hover " BackColor="White" OnRowCommand="dgvBANCOS_RowCommand1" AutoGenerateColumns="False" DataKeyNames="ID_BANCOS" OnPageIndexChanging="dgvBANCOS_PageIndexChanging" PageSize="10"
             AllowPaging="true">
             <PagerSettings Mode="NumericFirstLast" />
             <PagerStyle CssClass="pagination-ys" Wrap="True" BorderStyle="None" HorizontalAlign="Center" />
             <Columns>
                 <asp:BoundField DataField="ID_BANCOS" HeaderText="CODIGO" />
                 <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE O RAZON SOCIAL" />
                 <asp:BoundField DataField="RUC" HeaderText="RUC" />
                 <asp:BoundField DataField="DIRECCION" HeaderText="DIRECCION" />
                 <asp:BoundField DataField="TELEFONOS" HeaderText="TELEFONO(S)" />
                 <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" Visible="False" />
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
    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.2/jquery.min.js'></script>
    <script src='http://cdnjs.cloudflare.com/ajax/libs/mouse0270-bootstrap-notify/3.1.5/bootstrap-notify.min.js'></script>
    <script src="Scripts/index.js"></script>

    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
