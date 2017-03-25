<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_MANTENIMIENTO_BIEN.aspx.cs" Inherits="DIONYS_ERP.FRM_MANTENIMIENTO_BIEN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style58 {
            text-align: center;
            font-weight: 700;
        }
    </style>
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
    <div style="width: 589px; height: 455px;">
            

                    <table class="auto-style91">
                        <tr>
                            <td class="auto-style58">&nbsp;</td>
                            <td class="auto-style100">&nbsp;</td>
                            <td class="auto-style94">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style58" colspan="3">SELECCIONANDO BIENES </td>
                        </tr>
                        <tr>
                            <td class="auto-style58">&nbsp;</td>
                            <td class="auto-style100">&nbsp;</td>
                            <td class="auto-style94">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style58">&nbsp;</td>
                            <td class="auto-style100">&nbsp;</td>
                            <td class="auto-style94">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style58">BUSCAR_POR: </td>
                            <td class="auto-style100">
                                <asp:DropDownList ID="cboOPCION" runat="server" Height="33px" Width="211px" BackColor="#CC9900" Font-Bold="False" Font-Names="Tahoma">
                                    <asp:ListItem>-- SELECCIONAR --</asp:ListItem>
                                    <asp:ListItem Value="ID_BIEN">CODIGO</asp:ListItem>
                                    <asp:ListItem>DESCRIPCION</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style94">
                                <asp:TextBox ID="txtBUSCARBIEN" runat="server" Height="27px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style58">&nbsp;</td>
                            <td class="auto-style100">
                                &nbsp;</td>
                            <td class="auto-style94">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style102">
                            </td>
                            <td class="auto-style103">
                                <asp:Button ID="btnBUSCAR" runat="server" Text="BUSCAR" CssClass="Boton01" OnClick="btnBUSCAR_Click" BackColor="#669900" Height="29px" Width="82px" />
                            </td>
                            <td class="auto-style104">
                                <asp:Button ID="btnCANCELAR" runat="server" CssClass="Boton01" Text="CANCELAR" BackColor="#669900" OnClick="btnCANCELAR_Click" Height="30px" />
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style102">
                                &nbsp;</td>
                            <td class="auto-style103">
                                &nbsp;</td>
                            <td class="auto-style104">
                                &nbsp;</td>
                        </tr>

                        
                        <tr>   
                            <td class="auto-style102">

                                <asp:Label ID="Label2" runat="server" Text="PRECIO:"></asp:Label>

                            </td>
                            <td class="auto-style103">

                                <asp:TextBox ID="txtPRE" runat="server" Width="190px"></asp:TextBox>

                            </td>
                            <td class="auto-style104">

                                

                                <asp:Button ID="btnBOTON" runat="server" Text="GUARDAR" OnClick="btnBOTON_Click" BackColor="#669900" CssClass="auto-style58" ForeColor="White" />

                                

                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style102">
                                &nbsp;</td>
                            <td class="auto-style103">
                                &nbsp;</td>
                            <td class="auto-style104">
                                &nbsp;</td>
                        </tr>

                        <tr >
                            <td colspan="3" style="background-color:#778899;height:110px" >
                                <asp:GridView ID="dgvFILTRADOBIENES" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="134px" Width="588px" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="Small" DataKeyNames="ID_BIEN,DESCRIPCION,PRECIO,ESTADO" AutoGenerateSelectButton="True" OnSelectedIndexChanged="dgvFILTRADOBIENES_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ID_BIEN" HeaderText="ID_BIEN" ReadOnly="true"/>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" ReadOnly="true" />
                                        <asp:BoundField DataField="PRECIO" HeaderText="PRECIO" />
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Height="10px"/>
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Height="10px"/>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Height="10px"/>
                                    <RowStyle BackColor="#E3EAEB" Height="10px"/>
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        </table>
          
            <br />
            

        </div>
</asp:Content>
