<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM_CERRAR_CAJA.aspx.cs" Inherits="DIONYS_ERP.FRM_CERRAR_CAJA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {            text-align: center;
        }
        .auto-style3 {
            width: 143px;
            height: 38px;
        }
        .auto-style4 {
            height: 38px;
        }
        #form1 {
            width: 1084px;
        }
        .auto-style7 {
            width: 154px;
        }
        .auto-style8 {
            height: 38px;
            width: 154px;
        }
        .auto-style9 {
            width: 248px;
        }
        .auto-style10 {
            height: 38px;
            width: 248px;
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


</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1075px">
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label1" runat="server" Text="FECHA APERTURA" Font-Bold="True" Font-Names="Tahoma" Font-Size="15px" ForeColor="White"></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:TextBox ID="txtFECHA_APERTURA" runat="server" style="text-align: left" Width="197px" Font-Names="Tahoma" Font-Size="14px" TextMode="DateTime"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Label ID="Label3" runat="server" Text="ID CAJA" Font-Bold="True" Font-Names="Tahoma" Font-Size="15px" ForeColor="White"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtID_CAJA" runat="server" Font-Names="Tahoma" Font-Size="14px" Width="144px"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="btnABRIRCAJA" runat="server" OnClick="btnABRIRCAJA_Click" Text="ABRIR CAJA" Width="150px" Height="50px" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px" BorderStyle="None" style="text-align: center" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label2" runat="server" Text="FECHA CIERRE" Font-Bold="True" Font-Names="Tahoma" Font-Size="15px" ForeColor="White"></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:TextBox ID="txtFECHA_CIERRE" runat="server" Width="195px" Font-Names="Tahoma" Font-Size="14px" TextMode="DateTime"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Label ID="Label4" runat="server" Text="SALDO INICIAL" Font-Bold="True" Font-Names="Tahoma" Font-Size="15px" ForeColor="White"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtSALDO_INICIAL" runat="server" Font-Names="Tahoma" Font-Size="14px" Width="143px"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="btnCERRARCAJA" runat="server" Height="50px" OnClick="btnCERRARCAJA_Click" Text="CERRAR CAJA" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px" OnClientClick="return confirm(&quot;¿ESTA SEGURO DE CERRAR CAJA?&quot;);" BorderStyle="None" style="text-align: center" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label6" runat="server" Text="OBSERVACIONES" Font-Bold="True" Font-Names="Tahoma" Font-Size="15px" ForeColor="White"></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:TextBox ID="txtOBSERVACIONES" runat="server" Width="370px" Font-Names="Tahoma" Font-Size="14px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Label ID="Label5" runat="server" Text="SALDO FINAL" Font-Bold="True" Font-Names="Tahoma" Font-Size="15px" ForeColor="White"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtSALDO_FINAL" runat="server" Font-Names="Tahoma" Font-Size="14px" Width="143px"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="btnSALIR" runat="server" Height="50px" OnClick="btnSALIR_Click" Text="SALIR" Width="150px" Font-Bold="True" Font-Names="Tahoma" Font-Size="18px" BorderStyle="None" style="text-align: center" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="4">
                &nbsp;&nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
