<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM_ABRIR_CAJA.aspx.cs" Inherits="DIONYS_ERP.FRM_ABRIR_CAJA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 138px;
        }
        .auto-style3 {}
        .auto-style4 {
            width: 138px;
            height: 23px;
        }
        .auto-style5 {
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



</head>
<body style="width: 478px">
    <form id="form1" runat="server">
    <div style="width: 471px">
        
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="FECHA APERTURA:"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtFECHA_APERTURA" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="SALDO INICIAL:"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtSALDO_INICIAL" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="OBSERVACION:"></asp:Label>
                </td>
                <td class="auto-style3" colspan="2">
                    <asp:TextBox ID="txtOBSERVACION" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style5"></td>
                <td class="auto-style5"></td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Button ID="btnABRIRCAJA" runat="server" OnClick="btnABRIRCAJA_Click" Text="ABRIR CAJA" Width="100px" />
                </td>
                <td>
                    <asp:Button ID="btnCANCELAR" runat="server" OnClick="btnCANCELAR_Click" Text="CANCELAR" Width="100px" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
