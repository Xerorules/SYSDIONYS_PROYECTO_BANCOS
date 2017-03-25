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
    public partial class FRM_REPORTE_BOLETA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String valor = Request.QueryString["IDVENTA"].ToString();
                imprimir(valor);
            }

        }

        void imprimir(string IDVENTA)
        {
            N_VENTA objnego = new N_VENTA();
            DataSet ds = objnego.REPORTE_GENERAR_FACTURA_BOLETA(IDVENTA);
            DataSet ds_reporte = new DataSet();

            DataTable dtcliente = ds.Tables[0].Copy();
            dtcliente.TableName = "DATOS";
            ds_reporte.Tables.Add(dtcliente);


            DataTable dtdet = ds.Tables[1].Copy();
            dtdet.TableName = "DETALLE";
            ds_reporte.Tables.Add(dtdet);

            ReportDocument rp = new ReportDocument();
            rp.Load(Server.MapPath("../REPORTES/rptREPORTE_GENERAR_BOLETA.rpt"));
            rp.SetDataSource(ds_reporte);
            CrystalReportViewer_GENERAR_BOLETA.ReportSource = rp;
            CrystalReportViewer_GENERAR_BOLETA.DataBind();
            //exportar a pdf
            ReportDocument pdf = rp;
            pdf.ExportToHttpResponse(
                ExportFormatType.PortableDocFormat, Response, false, "REPORTE_BOLETA");



        }
    }
}