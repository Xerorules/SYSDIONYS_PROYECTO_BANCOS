using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DIONYS_ERP.REPORTES
{
    public partial class FRM_REPORTE_CAJA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String valor = Request.QueryString["ID_EMPRESA"].ToString();
            String valor2 = Request.QueryString["ID_CAJA"].ToString();
            imprimir(valor, valor2);//, valor3, valor4);
        }

        void imprimir(string ID_EMPRESA, string ID_CAJA)//, string FECHA_INI, string FECHA_FIN)
        {
            N_VENTA objnego = new N_VENTA();
            DataSet ds = objnego.REPORTE_RESUMEN_CAJA_LIBROCAJA(ID_EMPRESA, ID_CAJA);//, FECHA_INI, FECHA_FIN);
            DataSet ds_reporte = new DataSet();

            DataTable dtcliente = ds.Tables[0].Copy();
            dtcliente.TableName = "EMPRESA";
            ds_reporte.Tables.Add(dtcliente);


            DataTable dtdet = ds.Tables[1].Copy();
            dtdet.TableName = "REPORTE_TOTAL_CAJA";
            ds_reporte.Tables.Add(dtdet);

            DataTable dtdetC = ds.Tables[2].Copy();
            dtdetC.TableName = "TOTALES_CAJA";
            ds_reporte.Tables.Add(dtdetC);

            ReportDocument rp = new ReportDocument();
            rp.Load(Server.MapPath("../REPORTES/rptREPORTE_CAJA.rpt"));
            rp.SetDataSource(ds_reporte);
            CrystalReportViewer_REPORTE_CAJA.ReportSource = rp;
            CrystalReportViewer_REPORTE_CAJA.DataBind();

            //exportar a pdf

            ReportDocument pdf = rp;
            pdf.ExportToHttpResponse(
                ExportFormatType.PortableDocFormat, Response, false, "REPORTE_CAJA");



        }

    }


}