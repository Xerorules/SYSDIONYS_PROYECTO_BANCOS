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
    public partial class FRM_REPORTE_RECIBOS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                String valor1 = Request.QueryString["ID_COMPVTA"].ToString();
                String valor2 = Request.QueryString["ID_EMPRESA"].ToString();
                imprimir(valor1,valor2);
            }
        }
        void imprimir(string ID_COMPVTA, string ID_EMPRESA)
        {
            try
            {
                    N_VENTA objnego = new N_VENTA();
                    DataSet ds = objnego.REPORTE_DOCVTA_RECIBOS(ID_COMPVTA, ID_EMPRESA);
                    DataSet ds_reporte = new DataSet();

                    DataTable dtcliente = ds.Tables[0].Copy();
                    dtcliente.TableName = "RECIBO_VENTAS";
                    ds_reporte.Tables.Add(dtcliente);


                    DataTable dtdet = ds.Tables[1].Copy();
                    dtdet.TableName = "RECIBO_DETALLE";
                    ds_reporte.Tables.Add(dtdet);

                    DataTable dtdetR = ds.Tables[2].Copy();
                    dtdetR.TableName = "RECIBO_EMPRESA";
                    ds_reporte.Tables.Add(dtdetR);

                    ReportDocument rp = new ReportDocument();
                    rp.Load(Server.MapPath("../REPORTES/rptREPORTE_RECIBOS.rpt"));
                    rp.SetDataSource(ds_reporte);
                    CrystalReportViewer_GENERAR_RECIBOS.ReportSource = rp;
                    CrystalReportViewer_GENERAR_RECIBOS.DataBind();
                    //exportar a pdf
                    ReportDocument pdf = rp;
                    pdf.ExportToHttpResponse(
                        ExportFormatType.PortableDocFormat, Response, false, "REPORTE_RECIBO");
                    }
            catch (Exception)
            {

                Response.Write("<script>window.alert('ERROR, NO HAY DATOS QUE IMPRIMIR, O BIEN LOS DATOS SON ERRONEOS');</script>");
            }
            



        }
        }
    }
