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
    public partial class FRM_CUENTAS_POR_PAGAR : System.Web.UI.Page
    {
        string vFILTRO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtFECHA_INI.Text = DateTime.Now.ToShortDateString();
                txtFECHA_FIN.Text = DateTime.Now.ToShortDateString();
                CONCATENAR_CONDICION();//vFILTRO = " CONVERT(CHAR(10),FECHA,103) BETWEEN CONVERT(DATE,'"+txtFECHA_INI.Text+"') AND CONVERT(DATE,'"+txtFECHA_FIN.Text+"') AND ID_PUNTOVENTA";
                CARGAR_DATOS(vFILTRO);                                              //EN CASO CONTRARIO LA ADMINISTRADORA LOGUEADA PUEDE ESCOGER LA SERIE POR LA QUE QUIERE HACER EL FILTRO

            }
        }

        N_VENTA N_OBJVENTAS = new N_VENTA();
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();

        public string CONCATENAR_CONDICION()
        {
            if (txtFECHA_INI.Text != string.Empty && txtFECHA_FIN.Text != string.Empty)
            {
                vFILTRO = @" CONVERT(CHAR(10),C_FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" +
                         txtFECHA_FIN.Text + "') AND C_ID_SEDE='" + Session["SEDE"].ToString() + "'";
            }

            if (txtPROVEEDOR.Text != string.Empty)
            {
                vFILTRO += " AND P_DESCRIPCION LIKE '%" + txtPROVEEDOR.Text + "%'";
            }
            if (txtRUC_DNI.Text != string.Empty)
            {
                vFILTRO += " AND P_RUC_DNI  = '" + txtRUC_DNI.Text + "'";
            }
            if (txtNUMERO.Text != string.Empty)
            {
                vFILTRO += " AND C_NUMERO = '" + txtNUMERO.Text + "'";
            }
            if (cboESTADO_SALDO.SelectedIndex != 0)
            {
                if (cboESTADO_SALDO.SelectedIndex == 1) //AQUI FILTRAMOS TODAS LAS CUENTAS POR COBRAR CUYO SALDO ES MAYOR A 0.00
                {
                    vFILTRO += " AND C_SALDO > 0.00";
                }
                if (cboESTADO_SALDO.SelectedIndex == 2) // SQUI FILTRAMOS TODAS LA CUENTAS POR COBRAR CUYO SALDO ES IGUAL A 0.00
                {
                    vFILTRO += " AND C_SALDO = 0.00";
                }
            }
            if (cboESTADO.SelectedIndex != 0)
            {
                if (cboESTADO.SelectedIndex == 1)
                {
                    vFILTRO += " AND C_FECHA_ANULADO IS NULL";
                }
                if (cboESTADO.SelectedIndex == 2)
                {
                    vFILTRO += " AND C_FECHA_ANULADO IS NOT NULL";
                }
            }

            if (cboMONEDA.SelectedIndex != 0)
            {
                if (cboMONEDA.SelectedIndex == 1)
                {
                    vFILTRO += " AND C_MONEDA = 'S'";
                }
                if (cboMONEDA.SelectedIndex == 2)
                {
                    vFILTRO += " AND C_MONEDA = 'D'";
                }
            }
            if (cboTIPODOC.SelectedIndex != 0)
            {
                vFILTRO += " AND C_TIPO_DOC = '" + cboTIPODOC.SelectedValue + "'";//TIPO DE DOC = TIPO DOC SELECCIONADO EN EL COMBO
            }
            return vFILTRO;
        }

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            CARGAR_DATOS(" CONVERT(CHAR(10),C_FECHA,103) BETWEEN CONVERT(DATE,'" + txtFECHA_INI.Text + "') AND CONVERT(DATE,'" + txtFECHA_FIN.Text +
                       "') AND C_ID_SEDE='" + Session["SEDE"].ToString() + "'");
            CARGAR_DATOS(CONCATENAR_CONDICION());
            dgvDETALLE_COMPRA.Visible = false; //CUANDO DESEO BUSCAR DESABILITO Y OCULTO LA GRILLA PARA NO VISUALIZARLO 
            dgvDETALLE_COBROS.Visible = false;
            dgvCOMPRAS.SelectedIndex = -1; //CON ESTO QUITO LA SELECCION DEL GRIDVIEW

        }
        public void CARGAR_DATOS(string pCONDICION)
        {
            dgvCOMPRAS.DataSource = N_OBJVENTAS.FILTROS_COMPRAS(pCONDICION);
            dgvCOMPRAS.DataBind();
        }


        void GENERAR_COMPRADETALLE(string ID_COMPRA)
        {
            DataTable DT = new DataTable();
            DT = N_OBJVENTAS.CAPTURAR_TABLA_COMPRADETALLE(ID_COMPRA);
            dgvDETALLE_COMPRA.DataSource = DT;
            dgvDETALLE_COMPRA.DataBind();
        }
        void GENERAR_DETALLE_CUENTAS_XCOBRAR(string ID_COMPVENT)
        {
            DataTable DT = new DataTable();
            DT = N_OBJVENTAS.GENERAR_DETALLE_CUENTAS_XPAGAR(ID_COMPVENT);
            dgvDETALLE_COBROS.DataSource = DT;
            dgvDETALLE_COBROS.DataBind();
        }

        protected void dgvCOMPRA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCOMPRAS.PageIndex = e.NewPageIndex;
            CARGAR_DATOS(CONCATENAR_CONDICION());
        }

        protected void dgvCOMPRA_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID_COMPRA = dgvCOMPRAS.SelectedRow.Cells[1].Text.ToString();
            dgvDETALLE_COMPRA.Visible = true;
            dgvDETALLE_COBROS.Visible = true;
            GENERAR_COMPRADETALLE(ID_COMPRA);
            GENERAR_DETALLE_CUENTAS_XCOBRAR(ID_COMPRA);
        }



    }
}