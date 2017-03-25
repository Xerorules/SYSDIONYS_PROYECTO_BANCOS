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
    public partial class FRM_REPORTE_LIQUIDACION : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String valor = Request.QueryString["ID_EMPRESA"].ToString();
             String valor2 = Request.QueryString["ID_CAJA"].ToString();
            imprimir(valor, valor2);
        }

        void imprimir(string ID_EMPRESA, string ID_CAJA)
        {
            N_VENTA objnego = new N_VENTA();
            DataSet ds = objnego.REPORTE_LIQUIDACION_CAJA(ID_EMPRESA, ID_CAJA);
            DataSet ds_reporte = new DataSet();

            DataTable dtcliente = ds.Tables[0].Copy();
            dtcliente.TableName = "CAJA_2";
            ds_reporte.Tables.Add(dtcliente);

            DataTable dtdet = ds.Tables[1].Copy();
            dtdet.TableName = "EMPRESA_2";
            ds_reporte.Tables.Add(dtdet);

            DataTable dtdetI = ds.Tables[2].Copy();
            dtdetI.TableName = "TARJETAS_2";
            ds_reporte.Tables.Add(dtdetI);

            DataTable dtdetE = ds.Tables[3].Copy();
            dtdetE.TableName = "INGRESOS_2";
            ds_reporte.Tables.Add(dtdetE);

            DataTable dtdetT = ds.Tables[4].Copy();
            dtdetT.TableName = "EGRESOS_2";
            ds_reporte.Tables.Add(dtdetT);

            DataTable dtdetC = ds.Tables[5].Copy();
            dtdetC.TableName = "TOTALES_2";
            ds_reporte.Tables.Add(dtdetC);


            ReportDocument rp = new ReportDocument();
            rp.Load(Server.MapPath("../REPORTES/rptREPORTE_LIQUIDACION.rpt"));
            rp.SetDataSource(ds_reporte);
            CrystalReportViewer_REPORTE_LIQUIDACION.ReportSource = rp;
            CrystalReportViewer_REPORTE_LIQUIDACION.DataBind();

            
            //exportar a pdf

            ReportDocument pdf = rp;
            pdf.ExportToHttpResponse(
                ExportFormatType.PortableDocFormat, Response, false, "REPORTE TARJETAS");


        }
        }
    }
