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

namespace DIONYS_ERP
{
    public partial class FRM_MANTENIMIENTO_BIEN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* == ACTIVAR CONTROLES DE EDICION == */
            txtPRE.Enabled = false;
            btnBOTON.Enabled = false;
            /* ================================== */
            LLENAR_GRILLA();
        }

        #region OBJETOS
        N_VENTA N_OBJVENTAS = new N_VENTA();
        E_VENTA E_OBJVENTAS = new E_VENTA();
        E_MANT_CLIENTE E_OBJMANT_CLIENTE = new E_MANT_CLIENTE();
        E_VENTA_Y_DETALLE E_OBJMANT_VENTADET = new E_VENTA_Y_DETALLE();
        #endregion

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            FILTRAR_BIEN_XCODIGO_XDESCRIPCION(); 
        }

        protected void btnCANCELAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("FRM_MENUVENTA.aspx");

        }

        
       
        void FILTRAR_BIEN_XCODIGO_XDESCRIPCION()
        {
            DataTable dt = new DataTable();
            if (Session["SERIE"].ToString() != "0003")
            {
                E_OBJVENTAS.OPCION_CLASE_BUSCAR = "1"; //SI ES DIFERENTE DE LA SERIE 0004 FILTRO LOS BIENES DE SERVICIO
            }
            else
            {
                E_OBJVENTAS.OPCION_CLASE_BUSCAR = "2"; //SINO FILTRO LOS BIENES DE CONSUMO
            }
            if (cboOPCION.SelectedIndex == 1)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = txtBUSCARBIEN.Text;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = null;
            }
            if (cboOPCION.SelectedIndex == 2)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = null;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = txtBUSCARBIEN.Text;
            }

            dt = N_OBJVENTAS.FILTRAR_BIEN_XCODIGO_XDESCRIPCION(E_OBJVENTAS);
            dgvFILTRADOBIENES.DataSource = dt;
            dgvFILTRADOBIENES.DataBind();
        }

        void LLENAR_GRILLA()
        {
            DataTable dt = new DataTable();
            if (Session["SERIE"].ToString() != "0003")
            {
                E_OBJVENTAS.OPCION_CLASE_BUSCAR = "1"; //SI ES DIFERENTE DE LA SERIE 0004 FILTRO LOS BIENES DE SERVICIO
            }
            else
            {
                E_OBJVENTAS.OPCION_CLASE_BUSCAR = "2"; //SINO FILTRO LOS BIENES DE CONSUMO
            }
            if (cboOPCION.SelectedIndex == 0)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = null;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = txtBUSCARBIEN.Text;
            }
            if (cboOPCION.SelectedIndex == 1)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = txtBUSCARBIEN.Text;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = null;
            }
            if (cboOPCION.SelectedIndex == 2)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = null;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = txtBUSCARBIEN.Text;
            }
            dt = N_OBJVENTAS.FILTRAR_BIEN_XCODIGO_XDESCRIPCION(E_OBJVENTAS);
            dgvFILTRADOBIENES.DataSource = dt;
            dgvFILTRADOBIENES.DataBind();
        }

       

        protected void btnBOTON_Click(object sender, EventArgs e)
        {
            if(txtPRE.Text.ToString() !=  string.Empty)
            {
                if (dgvFILTRADOBIENES.Rows.Count != 0) 
                {
                        if(Convert.ToDouble(txtPRE.Text.ToString()) > 0.00 )
                        {
                            N_OBJVENTAS.MANTENIMIENTO_BIEN(dgvFILTRADOBIENES.SelectedRow.Cells[1].Text,Convert.ToDouble(txtPRE.Text));
                            LLENAR_GRILLA();
                        }
                }
                
            }
            
        }

        protected void dgvFILTRADOBIENES_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* == ACTIVAR CONTROLES DE EDICION == */
            txtPRE.Enabled = true;
            btnBOTON.Enabled = true;
            /* ================================== */
            string ID_BIEN = dgvFILTRADOBIENES.SelectedRow.Cells[1].Text;
            string PRECIO = dgvFILTRADOBIENES.SelectedRow.Cells[3].Text;
            txtPRE.Text = PRECIO;
        }

    }
}