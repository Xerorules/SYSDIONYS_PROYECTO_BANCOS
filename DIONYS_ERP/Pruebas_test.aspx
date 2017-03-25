<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="Pruebas_test.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/css_tab_panel_csspuro.css" rel="stylesheet" />
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tabbed">

   

        <input name="tabbed" id="tabbed1" type="radio" checked="checked" />
        <section>
            <h1>
                <label for="tabbed1">
                    REGISTRO MANUAL
                </label>
            </h1>
            <div>
               <%--ACA SE TRAE TODO EXCEL--%> 




            </div>
        </section>

        <input name="tabbed" id="tabbed2" type="radio" />
        <section>
            <h1>
                <label for="tabbed2">
                    CARGAR ARCHIVO EXCEL
                </label>
            </h1>
            <div>
                ACA CARGAMOS EL ARCHIVO EXCEL
            </div>
        </section>

        <!-- and so on -->

    </div>
   
</asp:Content>
