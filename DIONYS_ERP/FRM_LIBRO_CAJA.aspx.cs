using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace DIONYS_ERP
{
    public partial class FRM_LIBRO_CAJA : System.Web.UI.Page
    {
        string vFILTRO="";
        
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!Page.IsPostBack)
            {
                VALIDACION_USUARIOS();
                LISTA_PUNTOVENTA();
                LISTA_TIPOMOVIMIENTO();
                LISTA_TIPOPAGO();
                LISTA_EMPLEADOS();
                txtFECHA_INI.Text = DateTime.Now.ToShortDateString();
                txtFECHA_FIN.Text = DateTime.Now.ToShortDateString();
                CONCATENAR_CONDICION();//vFILTRO = " CONVERT(CHAR(10),FECHA,103) BETWEEN CONVERT(DATE,'"+txtFECHA_INI.Text+"') AND CONVERT(DATE,'"+txtFECHA_FIN.Text+"') AND ID_PUNTOVENTA";
                CARGAR_DATOS(vFILTRO);
            }
            //vFILTRO = "";
        }

        protected void cboESTADO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        N_VENTA N_OBJVENTAS = new N_VENTA();
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();
        
        public void CARGAR_DATOS(string pCONDICION)
        {
            string ID_SEDE = Session["SEDE"].ToString();
            dgvDATOS_LIBRO_CAJAKARDEX.DataSource = N_OBJVENTAS.FILTROS_LIBRO_CAJA(pCONDICION,ID_SEDE);
            dgvDATOS_LIBRO_CAJAKARDEX.DataBind();
        }

        private void LISTA_PUNTOVENTA()
        {
            string vSESSION = Session["SEDE"].ToString();

            cboPUNTO_VENTA.DataSource = N_OBJVENTAS.LISTA_PUNTOVENTA(vSESSION);
            cboPUNTO_VENTA.DataValueField = "ID_PUNTOVENTA";
            cboPUNTO_VENTA.DataTextField = "DESCRIPCION";
            cboPUNTO_VENTA.DataBind();
        }
        private void LISTA_TIPOMOVIMIENTO()
        {
            cboTIPOMOV.DataSource = N_OBJVENTAS.LISTA_TIPOMOVIMIENTO();
            cboTIPOMOV.DataValueField = "ID_TIPOMOV";
            cboTIPOMOV.DataTextField = "DESCRIPCION";
            cboTIPOMOV.DataBind();
        }
        private void LISTA_TIPOPAGO()
        {
            cboTIPOPAGO.DataSource = N_OBJVENTAS.LISTA_TIPOPAGO();
            cboTIPOPAGO.DataValueField = "ID_TIPOPAGO";
            cboTIPOPAGO.DataTextField = "DESCRIPCION";
            cboTIPOPAGO.DataBind();
        }
        private void LISTA_EMPLEADOS()
        {
            string vSESSION = Session["SEDE"].ToString();

            cboEMPLEADO.DataSource = N_OBJVENTAS.LISTAR_EMPLEADO(vSESSION);
            cboEMPLEADO.DataValueField = "ID_EMPLEADO";
            cboEMPLEADO.DataTextField = "EMPLEADO";
            cboEMPLEADO.DataBind();
        }
        private void VALIDACION_USUARIOS()
        {
            DataTable dtDATA = N_OBJVENTAS.VALIDAR_USUARIO_ADMIN_SEDE(Session["SEDE"].ToString());
            if(Session["USUARIO_EMPLEADO"].ToString()!=dtDATA.Rows[0]["DNI_USUARIO"].ToString())
            {
                cboPUNTO_VENTA.SelectedValue=Session["ID_PUNTOVENTA"].ToString();
                cboEMPLEADO.SelectedValue=Session["ID_EMPLEADO"].ToString();

                cboPUNTO_VENTA.Enabled = false;
                cboEMPLEADO.Enabled = false;
            }
            else
            {
                cboPUNTO_VENTA.SelectedIndex = 0;
                cboEMPLEADO.SelectedIndex = 0;

                cboPUNTO_VENTA.Enabled = true;
                cboEMPLEADO.Enabled = true;
            }

        }

        public string CONCATENAR_CONDICION()
        {
            if(txtFECHA_INI.Text !=string.Empty && txtFECHA_FIN.Text!=string.Empty)
            {
                vFILTRO = " CONVERT(CHAR(10),FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" + txtFECHA_FIN.Text + "')";
            }

            if(txtID_CAJA.Text!=string.Empty)
            {
                vFILTRO += " AND ID_CAJA='"+txtID_CAJA.Text+"'";
            }
            if (cboPUNTO_VENTA.SelectedIndex != 0)
            {
                vFILTRO += " AND ID_PUNTOVENTA ='" + cboPUNTO_VENTA.SelectedValue + "'";
            }
            if (txtDESCRIPCION_MOV.Text != string.Empty)
            {
                vFILTRO += " AND DESCRIPCION LIKE '%" + txtDESCRIPCION_MOV.Text.ToString() + "%'";
            }
            if (cboESTADO.SelectedIndex != 0)
            {
                if(cboESTADO.SelectedIndex==1)
                {
                    vFILTRO += " AND FECHA_ANULADO IS NULL";
                }
                if (cboESTADO.SelectedIndex == 2)
                {
                    vFILTRO += " AND FECHA_ANULADO IS NOT NULL";
                }
            }
            if (cboEMPLEADO.SelectedIndex != 0)
            {
                vFILTRO += " AND ID_EMPLEADO = '" +cboEMPLEADO.SelectedValue + "'";
            }
            if (cboMONEDA.SelectedIndex != 0)
            {
                if (cboMONEDA.SelectedIndex == 1)
                {
                    vFILTRO += " AND MONEDA = 'S'";
                }
                if (cboMONEDA.SelectedIndex == 2)
                {
                    vFILTRO += " AND MONEDA = 'D'";
                }
            }
            if (cboTIPOMOV.SelectedIndex != 0)
            {
                vFILTRO += " AND ID_TIPOMOV = '"+cboTIPOMOV.SelectedValue+"'";//TIPO DE MOVIMIENTO = TIPO MOV SELECCIONADO EN EL COMBO
            }
            if (cboTIPOPAGO.SelectedIndex != 0)
            {
                    vFILTRO += " AND ID_TIPOPAGO = '"+cboTIPOPAGO.SelectedValue+"'";//TIPO DE PAGO = TIPO PAGO SELECCIONADO EN EL COMBO
            }

            return vFILTRO;
        }

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            CARGAR_DATOS(" CONVERT(CHAR(10),FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" + txtFECHA_FIN.Text + "')");
            CARGAR_DATOS(CONCATENAR_CONDICION());
        }

        protected void ImgEXPORTAREXCEL_Click(object sender, ImageClickEventArgs e)
        {
            EXPORTAR_EXCEL(dgvDATOS_LIBRO_CAJAKARDEX);    //AQUI LLAMO A MI PROCEDIMIENTO EXPORTAR_EXCEL PARA GENERAR EL ARCHIVO EXCEL DE CADA CAJA 
        }

        

        void EXPORTAR_EXCEL(GridView tabla)
        {
          if (tabla.Rows.Count > 0)
          {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            HtmlForm form = new HtmlForm();
            //GridView dg = new GridView();
            //dg.EnableViewState = false;
            //dg.DataSource = tabla;
            //dg.DataBind();
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(dgvDATOS_LIBRO_CAJAKARDEX);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=REPORTE_CAJA.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
          }
        }

    }
}