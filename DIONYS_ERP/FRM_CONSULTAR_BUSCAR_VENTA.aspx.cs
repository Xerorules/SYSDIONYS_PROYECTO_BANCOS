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
    public partial class FRM_CONSULTAR_BUSCAR_VENTA : System.Web.UI.Page
    {
        string vFILTRO = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFECHA_INI.Text = DateTime.Now.ToShortDateString();
                txtFECHA_FIN.Text = DateTime.Now.ToShortDateString();
                CONCATENAR_CONDICION();//vFILTRO = " CONVERT(CHAR(10),FECHA,103) BETWEEN CONVERT(DATE,'"+txtFECHA_INI.Text+"') AND CONVERT(DATE,'"+txtFECHA_FIN.Text+"') AND ID_PUNTOVENTA";
                CARGAR_DATOS(vFILTRO);
            }
        }

        N_VENTA N_OBJVENTAS = new N_VENTA();
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();

        public string CONCATENAR_CONDICION()
        {
            if (txtFECHA_INI.Text != string.Empty && txtFECHA_FIN.Text != string.Empty)
            {
                vFILTRO = @" CONVERT(CHAR(10),V_FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" + 
                         txtFECHA_FIN.Text + "') AND V_ID_SEDE='"+Session["SEDE"].ToString()+"' AND V_SERIE = '"+Session["SERIE"].ToString()+"'";
            }

            
            if (cboESTADO.SelectedIndex != 0)
            {
                if (cboESTADO.SelectedIndex == 1)
                {
                    vFILTRO += " AND V_FECHA_ANULADO IS NULL";
                }
                if (cboESTADO.SelectedIndex == 2)
                {
                    vFILTRO += " AND V_FECHA_ANULADO IS NOT NULL";
                }
            }
           
            if (cboMONEDA.SelectedIndex != 0)
            {
                if (cboMONEDA.SelectedIndex == 1)
                {
                    vFILTRO += " AND V_MONEDA = 'S'";
                }
                if (cboMONEDA.SelectedIndex == 2)
                {
                    vFILTRO += " AND V_MONEDA = 'D'";
                }
            }
            if (cboTIPODOC.SelectedIndex != 0)
            {
                vFILTRO += " AND V_TIPO_DOC = '" + cboTIPODOC.SelectedValue + "'";//TIPO DE DOC = TIPO DOC SELECCIONADO EN EL COMBO
            }
            

            return vFILTRO;
        }

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            CARGAR_DATOS(" CONVERT(CHAR(10),V_FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" + txtFECHA_FIN.Text + 
                        "') AND V_ID_SEDE='"+Session["SEDE"].ToString()+"' AND V_SERIE = '"+Session["SERIE"].ToString()+"'");
            CARGAR_DATOS(CONCATENAR_CONDICION());
        }
        public void CARGAR_DATOS(string pCONDICION)
        {
            dgvDATOS_VENTAS.DataSource = N_OBJVENTAS.FILTROS_VENTAS(pCONDICION);
            dgvDATOS_VENTAS.DataBind();
        }

        protected void dgvDATOS_VENTAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID_VENTA = dgvDATOS_VENTAS.SelectedRow.Cells[1].Text;
            Session["ID_VENTA"] = ID_VENTA;  //AQUI GUARDO EL CODIGO DE LA VENTA PARA LUEGO CARGARLO EN MI FORMULARIO DE VENTA
            Response.Redirect("FRM_VENTA.aspx");
        }

        

        

        
    }
}