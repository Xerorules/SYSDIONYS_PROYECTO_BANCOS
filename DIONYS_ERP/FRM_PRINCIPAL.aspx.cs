using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;

namespace DIONYS_ERP
{
    public partial class FRM_PRINCIPAL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        #region OBJETOS
        N_VENTA N_OBJVENTAS = new N_VENTA();
        E_MANTENIMIENTO_CAJA E_OBJ_MANTCAJA = new E_MANTENIMIENTO_CAJA();
        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
                        
            if(Session["ID_CAJA"].ToString()!=string.Empty) //SI ES DIFERENTE DE VACIO ES PORQUE EL USUARIO  ID_USUARIO Y EL PUNTO DE VENTA ID_PUNTOVENTA TIENE CAJA APERTURADA
            {
                Response.Redirect("FRM_MENUVENTA.aspx");
            }
            else
            {
                lblMENSAJES.Text = "¡¡NO TIENES CAJA ABIERTA, PRIMERO APERTURA UNA CAJA!!";
            }
           
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

            //Response.Redirect("FRM_MANTENIMIENTO.aspx");
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FRM_MANTENIMIENTO_BIEN.aspx");
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FRM_MANTENIMIENTO_EMPLEADOS.aspx");
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FRM_REPORTE_BIEN.aspx");
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FRM_REPORTE.aspx");
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FRM_REIMPRESIONES.aspx");
        }
    }
}