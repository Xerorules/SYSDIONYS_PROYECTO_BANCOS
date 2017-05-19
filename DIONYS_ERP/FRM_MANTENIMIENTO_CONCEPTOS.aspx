<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_CONCEPTOS.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web13" %>

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
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <link href="../ESTILOS/EstilosGeneral.css" rel="stylesheet" type="text/css" />
    <script src="../SCRIPT/jquerymenu.js" type="text/javascript"></script>
    <link href="../ESTILOS/ESTILOS_BARRA_ESTADO.css" rel="stylesheet" />

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
    <div class="container-fluid col-lg-5">
        <div class="form-horizontal" style="position: relative;">
            <h1 style="color: white;" class="main-text">MANTENIMIENTO DE CONCEPTOS BANCARIOS</h1>
            &nbsp
            <div class="form-group">
                <label class="control-label col-xs-3" style="color: white">CONCEPTO BANCARIO:</label>
                <div class="col-xs-8 col-md-6">
                    <asp:TextBox runat="server" ID="txtNOM" CssClass="form-control col-xs-12 col-sm-10" placeholder="Ingrese Concepto Bancario" MaxLength="100"></asp:TextBox>
                    <%--VALIDADOR--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtNOM"
                        ErrorMessage="(*)El Concepto es requerido"
                        ForeColor="Gold"
                        ValidationGroup="Registro"  Display="Dynamic">
                    </asp:RequiredFieldValidator>
                     &nbsp
                </div>
                <div class="col-xs-8">
                    <label id="lblvalidacion1" style="color: red; font-size: medium"></label>
                </div>
            </div>

            <div class="form-group" style="position: center;">
                

                <div class="col-md-3 col-sm-3 col-xs-12" style="text-align:center;">
                    <asp:Button runat="server" CssClass="form-control btn btn-primary col-xs-12 col-sm-12" Text="REGISTRAR" ValidationGroup="Registro"  ID="btnRegistrar" OnClick="btnRegistrar_Click" />
                    &nbsp
                </div>
                <div class="col-md-3 col-sm-3 col-xs-12" style="text-align:center;">
                    <asp:Button runat="server" CssClass="form-control btn btn-primary col-xs-12 col-sm-12" Text="ACTUALIZAR" ID="btnActualizar" OnClick="btnActualizar_Click" />
                    &nbsp
                </div>
                <div class="col-md-3 col-sm-3 col-xs-12" style="text-align:center">
                    <asp:Button runat="server" CssClass="form-control btn btn-primary col-xs-12 col-sm-12" Text="CANCELAR" ID="btnCancelar" OnClick="btnCancelar_Click" />


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



    <div class="container-fluid ">
        <div class="row">
            <div class="col-md-7 col-sm-10 col-xs-10">

                <h2 style="color: white" class="text-info">CONCEPTOS BANCARIOS:</h2>
                &nbsp
       
         <asp:GridView ID="dgvCONCEPTOS" runat="server" CssClass="table table-striped table-bordered  table-hover " BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_CONCEPTOS_BANCARIOS" PageSize="10"
             AllowPaging="true" OnPageIndexChanging="dgvCONCEPTOS_PageIndexChanging" OnRowCommand="dgvCONCEPTOS_RowCommand">
             <PagerSettings Mode="NumericFirstLast" />
             <PagerStyle CssClass="pagination-ys" Wrap="True" BorderStyle="None" HorizontalAlign="Center" />
             <Columns>
                 <asp:BoundField DataField="ID_CONCEPTOS_BANCARIOS" HeaderText="CODIGO" />
                 <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
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


    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
