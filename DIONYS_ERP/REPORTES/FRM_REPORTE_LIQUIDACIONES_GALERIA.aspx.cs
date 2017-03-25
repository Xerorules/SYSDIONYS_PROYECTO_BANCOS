using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DIONYS_ERP.REPORTES
{
    public partial class FRM_REPORTE_LIQUIDACIONES_GALERIA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String valor = Request.QueryString["ID_EMPRESA"].ToString();
                String valor2 = Request.QueryString["ID_CAJA"].ToString();
                imprimir(valor, valor2);
            }
        }

        void imprimir(string ID_EMPRESA, string ID_CAJA)
        {
            N_VENTA objnego = new N_VENTA();
            DataSet ds = objnego.REPORTE_LIQUIDACIONES_GALERIA(ID_EMPRESA, ID_CAJA);
            DataSet ds_reporte = new DataSet();

            DataTable dtcliente = ds.Tables[0].Copy();
            dtcliente.TableName = "EMPRESA_GALERIA";
            ds_reporte.Tables.Add(dtcliente);

            DataTable dtdet = ds.Tables[1].Copy();
            dtdet.TableName = "CAJA_GALERIA";
            ds_reporte.Tables.Add(dtdet);

            DataTable dtdetI = ds.Tables[2].Copy();
            dtdetI.TableName = "VENTAS_GALERIA";
            ds_reporte.Tables.Add(dtdetI);

            DataTable dtdetE = ds.Tables[3].Copy();
            dtdetE.TableName = "MOVIMIENTOS_GALERIA";
            ds_reporte.Tables.Add(dtdetE);

            DataTable dtdetT = ds.Tables[4].Copy();
            dtdetT.TableName = "INGRESOS_GALERIA";
            ds_reporte.Tables.Add(dtdetT);

            DataTable dtdetC = ds.Tables[5].Copy();
            dtdetC.TableName = "EGRESOS_GALERIA";
            ds_reporte.Tables.Add(dtdetC);

            DataTable dtdetTG = ds.Tables[6].Copy();
            dtdetTG.TableName = "TOTALES_GALERIA";
            ds_reporte.Tables.Add(dtdetTG);

            ReportDocument rp = new ReportDocument();
            rp.Load(Server.MapPath("../REPORTES/rptREPORTE_LIQUIDACIONES_GALERIA.rpt"));
            rp.SetDataSource(ds_reporte);
            CrystalReportViewer_REPORTE_LIQUIDACIONES_GALERIA.ReportSource = rp;
            CrystalReportViewer_REPORTE_LIQUIDACIONES_GALERIA.DataBind();


            //exportar a pdf

            ReportDocument pdf = rp;
            pdf.ExportToHttpResponse(
                ExportFormatType.PortableDocFormat, Response, false, "REPORTE_TARJETAS");


        }
    }
}