<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_PRINCIPAL.aspx.cs" Inherits="DIONYS_ERP.FRM_PRINCIPAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
    .auto-style1 {
        width: 100%;
        color:white;
            font-family:Tahoma;
    }
    .auto-style2 {
        width: 150px;
            text-align: center;
            color:white;
            font-family:Tahoma;
            
        }
        .auto-style3 {
            width: 150px;
            color:white;
            font-family:Tahoma;
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 150px;
            text-align: center;
            color: white;
            font-family: Tahoma;
            height: 23px;
        }
        .auto-style6 {
            width: 150px;
            color: white;
            font-family: Tahoma;
            height: 23px;
        }
    </style>
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />

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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table class="auto-style1">
    <tr>
        <td colspan="9" style="text-align: center">
            <asp:Label ID="lblMENSAJES" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style4"></td>
        <td class="auto-style5"></td>
        <td class="auto-style6">&nbsp;</td>
        <td class="auto-style5"></td>
        <td class="auto-style6"></td>
        <td class="auto-style5"></td>
        <td class="auto-style6"></td>
        <td class="auto-style5"></td>
        <td class="auto-style4"></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">
            <asp:ImageButton ID="ImageButton1" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/REALIZAR_VENTA.png" OnClick="ImageButton1_Click" />
        </td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">
            <asp:ImageButton ID="ImageButton2" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/MANT_CLIENTE.png" OnClick="ImageButton2_Click" />
        </td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">
            <asp:ImageButton ID="ImageButton3" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/MANT_BIEN.png" OnClick="ImageButton3_Click" />
        </td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">
            <asp:ImageButton ID="ImageButton4" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/MANT_EMPLEADOS.png" OnClick="ImageButton4_Click" />
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">
            <asp:Label ID="Label1" runat="server" Text="REALIZAR VENTA"></asp:Label>
        </td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">
            <asp:Label ID="Label2" runat="server" Text="CLIENTES"></asp:Label>
        </td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">
            <asp:Label ID="Label3" runat="server" Text="BIENES"></asp:Label>
        </td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">
            <asp:Label ID="Label4" runat="server" Text="TRABAJADORES"></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">
            &nbsp;</td>
        <td class="auto-style3">
            <asp:ImageButton ID="ImageButton5" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/REPORTE_BIENES.png" OnClick="ImageButton5_Click" />
        </td>
        <td class="auto-style2">
            &nbsp;</td>
        <td class="auto-style3">
            <asp:ImageButton ID="ImageButton6" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/REPORTE_VENTAS.png" OnClick="ImageButton6_Click" />
        </td>
        <td class="auto-style2">
            &nbsp;</td>
        <td class="auto-style3">
            <asp:ImageButton ID="ImageButton7" runat="server" Height="200px" Width="150px" ImageUrl="~/ICONOS/REIMPRIMIR_VENTA.png" OnClick="ImageButton7_Click" />
        </td>
        <td class="auto-style2">
            &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">
            <asp:Label ID="Label5" runat="server" style="text-align: center" Text="REPORTE BIENES"></asp:Label>
        </td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">
            <asp:Label ID="Label6" runat="server" Text="REPORTE VENTAS"></asp:Label>
        </td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">
            <asp:Label ID="Label7" runat="server" Text="REIMPRESIONES VENTAS"></asp:Label>
        </td>
        <td class="auto-style2">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style3">&nbsp;</td>
        <td class="auto-style2">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>


</asp:Content>
