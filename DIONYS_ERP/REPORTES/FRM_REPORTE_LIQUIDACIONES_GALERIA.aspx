<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRM_REPORTE_LIQUIDACIONES_GALERIA.aspx.cs" Inherits="DIONYS_ERP.REPORTES.FRM_REPORTE_LIQUIDACIONES_GALERIA" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <CR:CrystalReportViewer ID="CrystalReportViewer_REPORTE_LIQUIDACIONES_GALERIA" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
