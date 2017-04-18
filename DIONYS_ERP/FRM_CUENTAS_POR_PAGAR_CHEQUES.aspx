<%@ Page Title="" Language="C#" MasterPageFile="~/PLANTILLAS/MENU_SUPERIOR.Master" AutoEventWireup="true" CodeBehind="FRM_CUENTAS_POR_PAGAR_CHEQUES.aspx.cs" Inherits="DIONYS_ERP.PLANTILLAS.Formulario_web17" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/custom-styles.css" rel="stylesheet" />
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
    <link href="ESTILOS/ESTILOS_FRM_PRINCIPAL.css" rel="stylesheet" />
    <script src="assets/js/jquery-1.10.2.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <%--POUP AJAX --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <input id="Hid_Sno" type="hidden" name="hddclick" runat="server" />
    <!-- ModalPopupExtender -->
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Hid_Sno"
        CancelControlID="Button3"  BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
        <table>
            <tr style="height:60px">
                <p style="font-weight:600">GENERAR MOVIMIENTO</p>
                <td>
                    <div style="float:left; width:120px"><asp:Label runat="server" CssClass="lbl" Text="FECHA:"></asp:Label></div>
                </td>
                <td>
                    <div style="float:right;"><asp:TextBox TextMode="Date"  ID="txtmFECH" runat="server" Font-Size="14px" CssClass="form-control" Width="250px" Height="35"></asp:TextBox></div>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="BANCO:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cbomBANCO" CssClass="form-control" AutoPostBack="true" Width="250px"  ></asp:DropDownList>
                </td>
            </tr>
            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="CUENTA:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cbomCUENTA" CssClass="form-control" AutoPostBack="true" Width="250px" >
                         <asp:ListItem Text="--SELECCIONE--" Value="NADA" />  
                    </asp:DropDownList>
                </td>
            </tr>

             <tr style="height:40px">
                <td>
                    <asp:Label ID="Label11" runat="server" CssClass="lbl" Text="TIPO MOV:"></asp:Label>
                </td>
                <td> 
                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" AutoPostBack="false"  Width="250px">
                        
                         <asp:ListItem Text="INGRESO" Value="INGRESO" />
                         
                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="N° OPERACIÓN:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmOPE" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="LUGAR:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmLUGAR" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="IMPORTE:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmIMPORTE" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"   ></asp:TextBox>
                </td>
            </tr>

            <tr style="height:40px">
                <td>
                    <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="DESCRIPCIÓN:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmDESC" runat="server" Font-Size="14px" CssClass="form-control" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <asp:Label ID="lblid_cliente" runat="server" CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
            <asp:Label ID="lblid_cheque" runat="server"  CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>
            <asp:Label ID="LBLID_MOV" runat="server"  CssClass="visible-xs" Text="DESCRIPCIÓN:"></asp:Label>

        </table>
        <br />
        <div class="row">
            
            <div class="col-xs-3">
                <div class="col-md-2 col-xs-2"  style="margin-left:60px">
                   <asp:Button ID="Button2" runat="server" Text="ACEPTAR" CssClass="form-control btn-info"  />
                </div>
            </div>
            <div class="col-xs-3" style="margin-left:40px">
                <div class="col-md-2 col-xs-2" >
                   <asp:Button ID="Button3" runat="server" Text="CANCELAR" CssClass="form-control btn-info" />
                </div>
            </div>
            <div class="col-xs-3" style="margin-left:-5px">
                <div class="col-md-2 col-xs-2">
                    <asp:Button ID="Button4" runat="server" Text="ACEPTADO" CssClass="form-control btn-success"  />
                </div>
            </div>
            <div class="col-xs-3" style="margin-left:-75px">
                <div class="col-md-3 col-xs-3" >
                   <asp:Button ID="Button1" runat="server" Text="REBOTADO" CssClass="form-control btn-danger"  />
                </div>
            </div>
            
        </div>
        
            
            
        

    </asp:Panel>
    <%--POPUP AJAX--%>






    <h1 style="color: white; text-align:center;" class="main-text">&nbsp;CUENTAS POR PAGAR</h1>&nbsp;
    <div class="container-fluid">

        <div class="form-group col-xs-12 center-block">
            
            <div class="col-md-3 col-sm-3 col-xs-10" >
                <asp:TextBox runat="server" ID="txtfiltro1" CssClass="form-control col-md-11 col-sm-11 col-xs-12" placeholder="Ingrese filtro1"></asp:TextBox>
            </div>
             <div class="col-md-3 col-sm-3 col-xs-10" >
                <asp:TextBox runat="server" ID="txtfiltro2" CssClass="form-control col-md-11 col-sm-11 col-xs-12" placeholder="Ingrese filtro2"></asp:TextBox>
            </div>
             <div class="col-md-2 col-sm-2 col-xs-10" >
                <asp:TextBox runat="server" ID="txtfiltro3" CssClass="form-control col-md-11 col-sm-11 col-xs-12" placeholder="Ingrese filtro3"></asp:TextBox>
            </div>
             <div class="col-md-2 col-sm-2 col-xs-10" >
                <asp:TextBox runat="server" ID="txtfiltro4" CssClass="form-control col-md-11 col-sm-11 col-xs-12" placeholder="Ingrese filtro4"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-2 col-xs-10" >
                <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-info col-md-11 col-sm-11 col-xs-12" Text="BUSCAR"></asp:Button>
            </div>
        </div>

        <div style="width: 100%; height: 400px; overflow-y: scroll">
                    <asp:GridView ID="dgvMOVIMIENTOS" runat="server" CssClass="table table-striped table-bordered  table-hover " BackColor="White" AutoGenerateColumns="False" DataKeyNames="ID_MOVIMIENTOS" >
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle CssClass="pager" Wrap="True" BorderStyle="None" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="ID_MOVIMIENTOS" HeaderText="CODIGO" />
                             <asp:BoundField DataField="FECHA" HeaderText="FECHA MOV" />
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                            <asp:BoundField DataField="CONCEPTO" HeaderText="CONCEPTO" />
                            <asp:BoundField DataField="OPERACION" HeaderText="N° OPERACION" > <ItemStyle HorizontalAlign="Center"/></asp:BoundField>
                            <asp:BoundField DataField="IMPORTE" HeaderText="IMPORTE" DataFormatString="{0:N}" ><ItemStyle HorizontalAlign="Right"/></asp:BoundField>
                            <asp:BoundField DataField="MONEDA" HeaderText="MON" Visible="false" />
                            <asp:BoundField DataField="NOM_CLI" HeaderText="CLIENTE" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="BANCO"  Visible="false"/>
                            <asp:BoundField DataField="LUGAR" HeaderText="LUGAR" />
                           
                            <asp:BoundField DataField="TIPO_MOV" HeaderText="TIPO MOV" />
                            

                            <asp:BoundField DataField="SALDO"  HeaderText="SALDO" DataFormatString="{0:N}" ><ItemStyle HorizontalAlign="Right"/></asp:BoundField>
                            

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtoneditar" runat="server" CommandName="EDITAR" CssClass="fa fa-edit">&nbsp;Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtoneliminar" runat="server" CommandName="ELIMINAR" CssClass="fa fa-trash-o">&nbsp;Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

    </div>
    <script src="Scripts/index.js"></script>

    <script src="assets/js/bootstrap.min.js"></script>
</asp:Content>
