<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM_CHEQUE_FOTOS.aspx.cs" Inherits="DIONYS_ERP.FRM_CHEQUE_FOTOS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Consultas de Movimientos Bancarios</title>
    <!-- Bootstrap Styles-->
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <!-- FontAwesome Styles-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/css/BARRA.css" rel="stylesheet" />
    <!-- Morris Chart Styles-->
    <link href="assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <!-- Custom Styles-->
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <!-- Google Fonts-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />


</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <nav class="navbar navbar-default top-navbar" role="navigation">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
                        <%-- <span class="sidebar-collapse"></span>--%>
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand">GRUPO DIONYS</a>

                </div>
                <%--<a style=" font-size:35px; margin-left: 10px; color:azure;">Sistema de Control de GALERIA</a>--%>

                <ul class="nav navbar-top-links navbar-right">

                    <li class="dropdown">
                        <%--<a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                            <i class="fa fa-exclamation fa-fw"></i><i class="fa fa-caret-down"></i>
                        </a>--%>
                        <ul class="dropdown-menu dropdown-messages">
                            <li>
                                <%--<a>
                                    <div>
                                        <strong>ADMINISTRADOR</strong>
                                        <span class="pull-right text-muted">
                                            <em></em>
                                        </span>
                                    </div>
                                    <div>Buenos dias Sr. Humberto</div>
                                </a>--%>
                            </li>
                            <li class="divider"></li>
                            <%--<li>
                                <a class="text-center" href="FormCobranzasActivas.aspx">
                                    <strong>URGENTE</strong>
                                    <i class="fa fa-angle-right"></i>
                                </a>
                            </li>--%>
                        </ul>
                        <!-- /.dropdown-messages -->
                    </li>
                    <!-- /.dropdown -->

                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                            <i class="fa fa-user fa-fw"></i><i class="fa fa-caret-down"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            <%--<li><a href="#"><i class="fa fa-user fa-fw"></i>Perfil Usuario</a>
                            </li>--%>

                            <li><a href="default.aspx" class="small"><i class="fa fa-sign-out fa-fw"></i>Cerrar Sesión</a>
                            </li>
                        </ul>
                        <!-- /.dropdown-user -->
                    </li>
                    <!-- /.dropdown -->
                </ul>
            </nav>
            <!--/. NAV TOP  -->
            <nav class="navbar-default navbar-side" role="navigation">
                <div class="sidebar-collapse">
                    <ul class="nav" id="main-menu">

                        <li>
                            <a class="active-menu" href="FRM_PRINCIPAL.aspx"><i class="fa fa-home"></i>REGRESAR</a>
                        </li>
                        <%--<li>
                            <a href="#"><i class="fa fa-table"></i>CONSULTAS<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                               <%-- <li>
                                    <a href="FormMantBienes.aspx">Mantenimiento Bienes</a>
                                </li>
                                <li>
                                    <a href="FormConsultaVentas.aspx">CONSULTA MOVIMIENTOS</a>
                                </li>
                               <li>
                                    <a href="FormConsultarObservacion.aspx">CONSULTA POR OBSERVACION</a>
                                </li>
                            </ul>
                        </li>--%>
                        <li>
                            <a href="#"><i class="fa fa-camera"></i>CHEQUES<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="FRM_CHEQUE_FOTOS.aspx">CHEQUES EN CARTERA</a>
                                </li>
                                <%--<li>
                                    <a href="#">Anular Contrato</a>
                                </li>
                                <li>
                                    <a href="#">Consultar Contratos</a>
                                </li>
                                <li>
                                    <a href="#">Reporte de Contratos</a>
                                </li>--%>
                            </ul>
                        </li>
                        <%--<li>
                            <a href="#"><i class="fa fa-shopping-cart"></i>Ventas - Alquileres<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="#">Nueva Venta-Alquiler</a>
                                </li>
                                <li>
                                    <a href="#">Consulta de Alquileres</a>
                                </li>
                                <li>
                                    <a href="#">Reporte de Alquileres</a>
                                </li>
                            </ul>
                        </li>--%>

                        <%--<li>
                            <a href="#"><i class="fa fa-dollar"></i>Cobranzas<span class="fa arrow"></span> </a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="#">Cobranzas Activas</a>
                                </li>
                                <li>
                                    <a href="#">Reporte de Cobranzas</a>
                                </li>

                            </ul>
                        </li>--%>
                        <%--<li>
                            <a href="#"><i class="fa fa-table"></i>Reportes<span class="fa arrow"></span> </a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="#">Reporte de Cobranzas</a>
                                </li>
                                <li>
                                    <a href="#">Reporte de Contratos</a>
                                </li>
                                <li>
                                    <a href="#">Reporte de Bienes</a>
                                </li>
                                <li>
                                    <a href="#">Reporte de Alquileres</a>
                                </li>
                            </ul>
                        </li>--%>
                        <%--<li>
                            <a href="#"><i class="fa fa-users"></i>Clientes<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="#">Nuevo Cliente</a>
                                </li>
                                <li>
                                    <a href="#">Seguimiento de Cliente</a>
                                </li>
                                <li>
                                    <a href="#">Deudas por Cliente</a>
                                </li>
                            </ul>
                        </li>--%>
                    </ul>

                </div>

            </nav>
            <!-- /. NAV SIDE  -->
            <div id="page-wrapper">
                <div id="page-inner">





                    <div class="container" style="margin-top: -50px; margin-left: -30px;">

                        <div class="col-lg-12 col-sm-12 col-md-12 col-xs-12 text-primary" style="text-align: center;">
                            <p>CHEQUES EN CARTERA(Subir Imágenes)</p>
                        </div>
                        
                        <div class="container col-lg-12">

                            <div class="col-xs-12 col-md-6 col-md-offset-3">

                                <div class="form-group">
                                    <%--<label class="control-label" style="color: darkblue">N° COD:</label>--%>
                                    <div class="col-xs-3 col-md-8">
                                         <asp:TextBox runat="server" ID="TXTCHEQUE" CssClass="form-control " Font-Bold="true" placeholder="COD DE CHEQUE" MaxLength="70"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3 col-md-8" style="left:80px;">
                                        <asp:Button runat="server" ID="btnBUSCAR"  CssClass="btn btn-info" Text="Buscar" OnClick="btnBUSCAR_Click" />
                                    </div>
                                      <div class="col-xs-1 col-md-8" style="float:right;">
                                          <asp:ImageButton runat="server" ID="btnSUBIRE" Width="50"  ImageUrl="~/imagenes/outbox.png" OnClick="btnSUBIRE_Click" />
                                      </div>
                               
                              
                                    
                        
                                </div>
                                
                                <div class="col-xs-12 col-md-6 col-md-offset-3">
                                <label style="color: darkblue; font-size: smaller;">CLIENTE:</label>
                                <asp:TextBox runat="server" ID="TXTCLI" CssClass="form-control col-sm-12 col-xs-12" ReadOnly="true" Font-Bold="true" placeholder="" MaxLength="70"></asp:TextBox>
                            </div>

                            <div class="col-xs-6 col-md-6 col-md-offset-3">

                                <label style="color: darkblue; font-size: smaller;">N° CHEQUE:</label>
                                <asp:TextBox runat="server" ID="TCTNCH" CssClass="form-control col-sm-12 col-xs-12" ReadOnly="true" Font-Bold="true" placeholder="" MaxLength="70"></asp:TextBox>
                            </div>
                                <div class="col-xs-6 col-md-6 col-md-offset-3">

                                <label style="color: darkblue; font-size: smaller;">F. GIRO:</label>
                                <asp:TextBox runat="server" ID="TXTFEC1" CssClass="form-control col-sm-12 col-xs-12" ReadOnly="true" Font-Bold="true" placeholder="" MaxLength="70"></asp:TextBox>
                            </div>
                                
                            <div class="col-xs-12 col-md-6 col-md-offset-3">
                                <label style="color: darkblue; font-size: smaller;">IMPORTE:</label>
                                <asp:TextBox runat="server" ID="TXTIMPO" CssClass="form-control col-sm-12 col-xs-12" ReadOnly="true" Font-Bold="true" placeholder="" MaxLength="70"></asp:TextBox>
                            </div>
                            </div>

                            <div class="container col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <div class="form-group" style="margin-top: -5px;">
                                    <div class="col-lg-6">
                                        <fieldset>
                                            <br />
                                            <br />
                                            <asp:Image ID="imgSubida" runat="server" />
                                            <br />
                                            <br />
                                            <label>Seleccione Archivo:</label>
                                            <asp:FileUpload ID="uploadFile" runat="server" />

                                            <asp:Button ID="btnSubir" runat="server" Text="Cargar Imagen" OnClick="btnSubir_Click" />
                                            <asp:TextBox runat="server" ID="txtimagen" CssClass="visible-lg" placeholder="Imagen"></asp:TextBox>
                                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                                        </fieldset>
                                    </div>
                                    <asp:HiddenField runat="server" ID="hfruta" />

                                </div>
                            </div>



                            



                        </div>
                        
                    </div>


                </div>
                <!-- /. PAGE INNER  -->
            </div>
            <div class="row">
                <div class="col-md-12">
                    <p style="color: #ffffff; text-align: center;">Todos los Derechos Reservados. Desarrollado por: <a href="https://mail.google.com/mail/u/0/#inbox?compose=new">Area de Sistemas del Grupo Dionys</a></p>

                </div>
            </div>



        </div>



    </form>
    <script src="assets/js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <script src="assets/js/bootstrap.min.js"></script>
    <!-- Metis Menu Js -->
    <script src="assets/js/jquery.metisMenu.js"></script>
    <!-- Morris Chart Js -->
    <script src="assets/js/morris/raphael-2.1.0.min.js"></script>
    <script src="assets/js/morris/morris.js"></script>
    <!-- Custom Js -->
    <script src="assets/js/custom-scripts.js"></script>
</body>
</html>
