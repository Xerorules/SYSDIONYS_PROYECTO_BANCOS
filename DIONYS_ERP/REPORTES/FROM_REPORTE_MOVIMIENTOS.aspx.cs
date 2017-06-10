using CAPA_NEGOCIO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String valor = Request.QueryString["ID_CUENTA_MOV"].ToString();
                String fecha_ini = Request.QueryString["FECHA_INI"].ToString();
                String fecha_fin = Request.QueryString["FECHA_FIN"].ToString();
                String oper = Request.QueryString["OPE"].ToString();
                String conbanca = Request.QueryString["CONBANC"].ToString();
                String idcli = Request.QueryString["ID_CLIENTE"].ToString();
                String obs = Request.QueryString["OBS"].ToString();
                imprimir(valor, fecha_ini, fecha_fin, oper, conbanca, idcli,obs);
            }
        }

        void imprimir(string ID_CUENTA_MOV,string FECHA_INI,string FECHA_FIN,string OPE,string CONBANC,string ID_CLIENTE,string OBS)
        {
            try
            {
                N_VENTA objnego = new N_VENTA();
                DataSet ds = objnego.REPORTE_MOVIMIENTOS_CUENTAS_BANCARIAS(ID_CUENTA_MOV, FECHA_INI, FECHA_FIN, OPE, ID_CLIENTE,CONBANC);
                DataSet ds_reporte = new DataSet();

                DataSet ds2 = objnego.REPORTE_MOVIMIENTOS_CUENTAS_BANCARIAS_DETALLE(ID_CUENTA_MOV, FECHA_INI, FECHA_FIN, OPE,CONBANC,ID_CLIENTE,OBS);
                //DataSet ds_reporte2 = new DataSet();
                //

                DataTable dtcliente = ds.Tables[0].Copy();
                dtcliente.TableName = "DATOSM";
                ds_reporte.Tables.Add(dtcliente);


                DataTable dtdet = ds2.Tables[0].Copy();
                dtdet.TableName = "DETALLEM";
                ds_reporte.Tables.Add(dtdet);

                ReportDocument rp = new ReportDocument();
                rp.Load(Server.MapPath("../REPORTES/rptREPORTE_MOVIMIENTOS.rpt"));
                rp.SetDataSource(ds_reporte);
                CrystalReportViewer_REPORTE_MOVIMIENTOS.ReportSource = rp;
                CrystalReportViewer_REPORTE_MOVIMIENTOS.DataBind();
                //exportar a pdf
                ReportDocument pdf = rp;
                pdf.ExportToHttpResponse(
                     ExportFormatType.PortableDocFormat, Response, false, "REPORTE_MOVIMIENTOS");
            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('ERROR, NO HAY DATOS QUE IMPRIMIR, O BIEN LOS DATOS SON ERRONEOS');</script>");
            }




        }


    }
}