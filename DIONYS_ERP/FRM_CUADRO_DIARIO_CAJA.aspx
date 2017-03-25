<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CUADRO_DIARIO_CAJA.aspx.cs" Inherits="DIONYS_ERP.FRM_CUADRO_DIARIO_CAJA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ESTILOS/EstilosGeneral.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        
        <table class="auto-style1">
            <tr>
                <td style="text-align:left">
                <asp:Label ID="Label15" runat="server" Text=".:. LIQUIDACIONES DIARIAS" Font-Bold="True" Font-Names="Tahoma" Font-Size="20px" ForeColor="White" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        
    </div>
    <div>   
        <fieldset>
            <legend style="color:#CC9900;font-weight:bold;font-family:Tahoma;font-size:14px">INGRESOS</legend>
        </fieldset>
    </div>

    <div>   
        <fieldset>
            <legend style="color:#CC9900;font-weight:bold;font-family:Tahoma;font-size:14px">EGRESOS</legend>
        </fieldset>
    </div>
</asp:Content>
