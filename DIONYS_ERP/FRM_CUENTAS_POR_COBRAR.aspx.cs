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
    public partial class FRM_CUENTAS_POR_COBRAR : System.Web.UI.Page
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
                if(Session["EMPLEADO_CARGO"].ToString()!="003") //SI EL CARGO DEL EMPLEADO LOGEAO NO ES DE ADMINISTRACION ENTONCES SE ASIGNA SU SERIE Y SE FILTRA POR SU SERIE
                {
                    cboSERIE.SelectedItem.Text = Session["SERIE"].ToString();
                    cboSERIE.Enabled = false;
                }                                                //EN CASO CONTRARIO LA ADMINISTRADORA LOGUEADA PUEDE ESCOGER LA SERIE POR LA QUE QUIERE HACER EL FILTRO
                
            }
        }

        N_VENTA N_OBJVENTAS = new N_VENTA();
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();

        public string CONCATENAR_CONDICION()
        {
            if (txtFECHA_INI.Text != string.Empty && txtFECHA_FIN.Text != string.Empty)
            {
                vFILTRO = @" CONVERT(CHAR(10),V_FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" +
                         txtFECHA_FIN.Text + "') AND V_ID_SEDE='" + Session["SEDE"].ToString() + "' AND V_SERIE = '" + Session["SERIE"].ToString() + "'";
            }

            if(txtCLIENTE.Text != string.Empty)
            {
                vFILTRO += " AND C_DESCRIPCION LIKE '%"+txtCLIENTE.Text+"%'";
            }
            if(txtRUC_DNI.Text != string.Empty)
            {
                vFILTRO += " AND C_RUC_DNI = '"+txtRUC_DNI.Text+"'";
            }
            if(cboSERIE.SelectedIndex != 0)
            {
                vFILTRO += " AND V_SERIE = '"+cboSERIE.SelectedItem.Text+"'";
            }
            if(txtNUMERO.Text != string.Empty)
            {
                vFILTRO += " AND V_NUMERO = '"+txtNUMERO.Text+"'";
            }
            if(cboESTADO_SALDO.SelectedIndex != 0)
            {
                if(cboESTADO_SALDO.SelectedIndex == 1) //AQUI FILTRAMOS TODAS LAS CUENTAS POR COBRAR CUYO SALDO ES MAYOR A 0.00
                {
                    vFILTRO += " AND V_SALDO > 0.00";
                }
                if (cboESTADO_SALDO.SelectedIndex == 2) // SQUI FILTRAMOS TODAS LA CUENTAS POR COBRAR CUYO SALDO ES IGUAL A 0.00
                {
                    vFILTRO += " AND V_SALDO = 0.00";
                }
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
                       "') AND V_ID_SEDE='" + Session["SEDE"].ToString() + "' AND V_SERIE = '" + Session["SERIE"].ToString() + "'");
            CARGAR_DATOS(CONCATENAR_CONDICION());
            dgvDETALLE_VENTAS.Visible = false; //CUANDO DESEO BUSCAR DESABILITO Y OCULTO LA GRILLA PARA NO VISUALIZARLO 
            dgvDETALLE_COBROS.Visible = false;
            dgvVENTAS.SelectedIndex= -1; //CON ESTO QUITO LA SELECCION DEL GRIDVIEW
            
        }
        public void CARGAR_DATOS(string pCONDICION)
        {
            dgvVENTAS.DataSource = N_OBJVENTAS.FILTROS_VENTAS(pCONDICION);
            dgvVENTAS.DataBind();
        }

        protected void dgvVENTAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID_VENTA = dgvVENTAS.SelectedRow.Cells[1].Text.ToString();
            //Session["ID_VENTA"] = ID_VENTA;  //AQUI GUARDO EL CODIGO DE LA VENTA PARA LUEGO CARGARLO EN MI FORMULARIO DE VENTA
            //Response.Redirect("FRM_VENTA.aspx");
            dgvDETALLE_VENTAS.Visible = true;
            dgvDETALLE_COBROS.Visible = true;
            GENERAR_VENTADETALLE(ID_VENTA);
            GENERAR_DETALLE_CUENTAS_XCOBRAR(ID_VENTA);
        
        }

        protected void dgvVENTAS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvVENTAS.PageIndex = e.NewPageIndex;
            CARGAR_DATOS(CONCATENAR_CONDICION());
        }

        void GENERAR_VENTADETALLE(string ID_VENTA)
        {
            DataTable DT = new DataTable();
            DT = N_OBJVENTAS.CAPTURAR_TABLA_VENTADETALLE(ID_VENTA);
            dgvDETALLE_VENTAS.DataSource = DT;
            dgvDETALLE_VENTAS.DataBind();
        }
        void GENERAR_DETALLE_CUENTAS_XCOBRAR(string ID_VENTA)
        {
            DataTable DT = new DataTable();
            DT = N_OBJVENTAS.GENERAR_DETALLE_CUENTAS_XCOBRAR(ID_VENTA);
            dgvDETALLE_COBROS.DataSource = DT;
            dgvDETALLE_COBROS.DataBind();
        }

        
       

        
    }
}