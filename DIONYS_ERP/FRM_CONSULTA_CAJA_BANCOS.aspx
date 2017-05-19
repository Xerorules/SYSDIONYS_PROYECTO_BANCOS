<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CONSULTA_CAJA_BANCOS.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web18" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/css/style4.css" rel="stylesheet" />
    <link href="assets/css/demo.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <link href="../ESTILOS/EstilosGeneral.css" rel="stylesheet" type="text/css" />
    <script src="../SCRIPT/jquerymenu.js" type="text/javascript"></script>
    <link href="../ESTILOS/ESTILOS_BARRA_ESTADO.css" rel="stylesheet" />
    <link href="../ESTILOS/menu.css" rel="stylesheet" type="text/css" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>CONSULTA DE MOVIMIENTOS</h2>
    <div class="container col-lg-1 col-lg-12 col-sm-12 col-md-12">
        <div class="form-group col-md-12 col-sm-12 col-xs-12 center-block" style="text-align: center">

            <div class="col-md-2 col-sm-2 col-lg-2">
                <label style="color: white;">FECHA:</label>
                <asp:TextBox runat="server" ID="txtFECHA" CssClass="form-control col-xs-12 col-sm-12" TextMode="Date"
                    Font-Bold="true" placeholder="" MaxLength="70"></asp:TextBox>
                

            </div>
             <div class="col-md-2 col-sm-2 col-lg-2">
                <label style="color: white;">BANCO:</label>
                <asp:DropDownList runat="server" ID="cboBANCO" CssClass="form-control col-xs-12 col-sm-12" AutoPostBack="false"></asp:DropDownList>
            </div>
             <div class="col-md-2 col-sm-2 col-lg-2">
                <label style="color: white;">IMPORTE:</label>
                <asp:TextBox runat="server" ID="txtIMPORTE" CssClass="form-control col-xs-12 col-sm-12" Style="text-transform: uppercase"
                    Font-Bold="true" placeholder="INGRESE IMPORTE" MaxLength="70"></asp:TextBox>
                

            </div>
            <div class="col-md-2 col-sm-2 col-lg-2">
                <label style="color: white;">MONEDA:</label>
                 <asp:DropDownList runat="server" ID="cboMON" CssClass="form-control col-xs-12 col-sm-12" AutoPostBack="false">
                     <asp:ListItem>SOLES</asp:ListItem>
                     <asp:ListItem>DOLARES</asp:ListItem>
                 </asp:DropDownList>
            </div>
             <div class="col-md-2 col-sm-2 col-lg-2">
                <label style="color: white;">N° OPERACION:</label>
                <asp:TextBox runat="server" ID="txtOPE" CssClass="form-control col-xs-12 col-sm-12" Height="35px"  Font-Bold="true"
                    placeholder="Ingrese Fecha final" MaxLength="70"></asp:TextBox>
            </div>
             <div class="col-md-2 col-sm-2 col-lg-2">

                <asp:Button ID="btnCONSULTA" runat="server" Text="BUSCAR" CssClass="a_demo_four1" Font-Bold="true"/>

            </div>
           

        </div>

    </div>
    <div class="container col-lg-12 col-lg-12 col-sm-12 col-md-12">
        <div class="panel">
            <div class="panel-heading">
                <span>MOVIMIENTOS BANCARIOS</span>
            </div>
            <div class="panel-body">
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </div>

        </div>

    </div>
    <script src="assets/js/jquery-1.10.2.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
