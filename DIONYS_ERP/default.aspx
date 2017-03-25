<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DIONYS_ERP.FRM_INICIO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style8 {
            height: 30px;
        }
        
        #LOGO {
            width: 363px;
        }

        .auto-style12 {
            width: 129px;
        }
        .auto-style13 {
            height: 30px;
            width: 129px;
        }
        .auto-style14 {
            width: 72%;
            height: 30px;
        }

        </style>
    <link href="ESTILOS/ESTILOS_LOGIN.css" rel="stylesheet" type="text/css"/>
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
    
    <div id="LOGO">
        <img style="width:100%;height:100%" src="ICONOS/LOGO_GRUPO_DIONYS.png" />
    </div>

 <div class="">
    <div class="LOGIN_FORMULARIO">
        

        <table class="TABLA_LOGEAR">
             <tr>
                <td class="auto-style13">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="EMPRESA"></asp:Label>
                </td>
                <td class="auto-style14">
                    <asp:DropDownList ID="cboEMPRESA" runat="server" Width="100%" Font-Size="Large" Height="100%" Font-Names="Tahoma" AutoPostBack="True" OnSelectedIndexChanged="cboEMPRESA_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="SEDE"></asp:Label>
                </td>
                <td class="auto-style14">
                    <asp:DropDownList ID="cboSEDE" runat="server" Width="100%" Font-Size="Large" Height="100%" Font-Names="Tahoma" AutoPostBack="True" OnSelectedIndexChanged="cboSEDE_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">
                    <asp:Label ID="lblPUNTOVENTA" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Text="PUNTO VENTA"></asp:Label>
                </td>
                <td class="auto-style14">
                    <asp:DropDownList ID="cboPUNTOVENTA" runat="server" Width="100%" Font-Size="Large" Height="100%" Font-Names="Tahoma">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">
                    <asp:Label ID="Label1" runat="server" Text="USUARIO    " Font-Bold="True" ForeColor="White" Font-Names="Tahoma"></asp:Label>
                </td>
                <td class="auto-style14">
                    <asp:TextBox ID="txtDNI_USUARIO" runat="server" Height="100%" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style13">
                    <asp:Label ID="Label2" runat="server" Text="CONTRASEÑA" Font-Bold="True" ForeColor="White" Font-Names="Tahoma"></asp:Label>
                </td>
                <td class="auto-style14">
                    <asp:TextBox ID="txtCLAVE" runat="server" TextMode="Password" Height="100%" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style8" colspan="2" style="text-align:center">
                    <asp:Label ID="lblMENSAJE_ERROR" runat="server" Font-Bold="False" Font-Italic="True" Font-Names="Tahoma" ForeColor="#0099CC"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="auto-style14">
                    &nbsp;<asp:Button ID="btnINGRESAR" runat="server" Text="INGRESAR" OnClick="btnINGRESAR_Click" Height="39px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium" Width="105px"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnLIMPIAR" runat="server" Text="LIMPIAR" Height="41px" Width="105px" Font-Bold="True" Font-Names="Tahoma" Font-Size="Medium" />
                </td>
            </tr>
        </table>
        
    </div>
 </div>

    </form>
</body>
</html>
