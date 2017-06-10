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
    public partial class Formulario_web19 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String id_empresa = Request.QueryString["ID_EMPRESA"].ToString();
                String id_banco = Request.QueryString["ID_BANCO"].ToString();
                String moneda = Request.QueryString["MONEDA"].ToString();
                String id_cliente = Request.QueryString["ID_CLIENTE"].ToString();
                String fecha_ini = Request.QueryString["FECHA_INI"].ToString();
                String fecha_fin = Request.QueryString["FECHA_FIN"].ToString();
                String estado = Request.QueryString["ESTADO"].ToString();
                imprimir(id_empresa, id_banco, moneda, id_cliente, fecha_ini, fecha_fin, estado);
            }
        }

        void imprimir(string ID_EMPRESA, string ID_BANCO, string MONEDA, string ID_CLIENTE, string FECHA_INI, string FECHA_FIN, string ESTADO)
        {
            try
            {
                N_VENTA objnego = new N_VENTA();
                DataSet ds = objnego.REPORTE_CHEQUES_CABECERA(ID_EMPRESA, ID_BANCO, MONEDA, ID_CLIENTE, FECHA_INI, FECHA_FIN, ESTADO);
                DataSet ds_reporte = new DataSet();

                DataSet ds2 = objnego.REPORTE_CHEQUES_DETALLE(ID_EMPRESA, ID_BANCO, MONEDA, ID_CLIENTE, FECHA_INI, FECHA_FIN, ESTADO);
                DataTable dtcliente = ds.Tables[0].Copy();
                dtcliente.TableName = "ENCA_CH";
                ds_reporte.Tables.Add(dtcliente);


                DataTable dtdet = ds2.Tables[0].Copy();

                //DataTable dt_sup = new DataTable();
                //DataColumn colum = dt_sup.Columns.Add("ID_CHEQUE", typeof(int));
                //dt_sup.Columns.Add(new DataColumn("DESCRIPCION", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("FECHA_GIRO", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("FECHA_COBRO", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("NUM_CHEQUE", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("BANCO", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("IMPORTE", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("MONEDA", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("ESTADO", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("BANCO_DEPOSITO", typeof(String)));
                //dt_sup.Columns.Add(new DataColumn("ID_MOVIMIENTOS", typeof(String)));

                /*-----------------------------------CAMBIANDO LA COLUMNA ESTADO--------------------------------------*/
                for (int i = 0; i < dtdet.Rows.Count; i++)
                {
                    string caseEstado = Convert.ToDateTime(dtdet.Rows[i]["ESTADO"].ToString()).ToString("dd-MM-yyyy");
                    
                    if (caseEstado == "01-01-1900") //si retorna "01/01/1900" no se ha ingresado una fecha esta en blanco
                    {
                        dtdet.Rows[i]["ESTADO"] = "PENDIENTE";
                    }
                    else if (caseEstado == "31-12-1900")//cualquier fecha indica deposito normal
                    {
                        dtdet.Rows[i]["ESTADO"] = "DEPOSITADO";
                       
                    }
                    else if (caseEstado != "01-01-1900" && caseEstado != "01-01-3000" && caseEstado != "31-12-1900")//cualquier fecha indica deposito normal
                    {
                        dtdet.Rows[i]["ESTADO"] = "ACEPTADO";
                        
                    }
                    else if (caseEstado == "01-01-3000")//si retorna "1/01/3000 12:00:00 a. m." el estado es rebotado
                    {
                        dtdet.Rows[i]["ESTADO"] = "REBOTADO";
                       
                    }



                }
                /*-------------------------------------------------------------------------------------------------------------------------*/


                dtdet.TableName = "DETA_CH";
                ds_reporte.Tables.Add(dtdet);

                ReportDocument rp = new ReportDocument();
                rp.Load(Server.MapPath("../REPORTES/rptREPORTE_CHEQUES.rpt"));
                rp.SetDataSource(ds_reporte);
                CrystalReportViewer_REPORTE_CHEQUES.ReportSource = rp;
                CrystalReportViewer_REPORTE_CHEQUES.DataBind();
                //exportar a pdf
                ReportDocument pdf = rp;
                pdf.ExportToHttpResponse(
                     ExportFormatType.PortableDocFormat, Response, false, "REPORTE_CHEQUES");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
                Response.Write("<script>window.alert('ERROR, NO HAY DATOS QUE IMPRIMIR, O BIEN LOS DATOS SON ERRONEOS');</script>");
            }
        }

    }
}