using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CAPA_NEGOCIO;
using CAPA_ENTIDAD;

namespace DIONYS_ERP
{
    public partial class FRM_ABRIR_CAJA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Session["ID_CAJA"].ToString()!= string.Empty){
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }
                txtFECHA_APERTURA.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00:0000");
                Session["FECHA_APERTURA_CAJA"] = txtFECHA_APERTURA.Text.ToString();
            }
        }

        #region OBJETOS
        E_MANTENIMIENTO_CAJA OBJ_E_MANTCAJA = new E_MANTENIMIENTO_CAJA();
        N_VENTA OBJ_N_MANTCAJA = new N_VENTA();
        #endregion

        #region EVENTOS
        protected void btnCANCELAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("FRM_PRINCIPAL.aspx");
        }

        protected void btnABRIRCAJA_Click(object sender, EventArgs e)
        {
            if(txtSALDO_INICIAL.Text.ToString() != string.Empty)
            {
                OBJ_E_MANTCAJA.ID_CAJA = string.Empty;
                OBJ_E_MANTCAJA.SALDO_INICIAL =Convert.ToDouble(txtSALDO_INICIAL.Text.ToString());
                OBJ_E_MANTCAJA.OBSERVACION = txtOBSERVACION.Text.ToString();
                OBJ_E_MANTCAJA.ID_EMPLEADO = Session["ID_EMPLEADO"].ToString();
                OBJ_E_MANTCAJA.ID_PUNTOVENTA = Session["ID_PUNTOVENTA"].ToString();
                OBJ_E_MANTCAJA.OPCION = 1;

                OBJ_N_MANTCAJA.MANTENIMIENTO_CAJA(OBJ_E_MANTCAJA);
                Session["ID_CAJA"] = OBJ_E_MANTCAJA.ID_CAJA;
                Session["SALDO_INICIAL_CAJA"] = txtSALDO_INICIAL.Text.ToString();
            }
            txtFECHA_APERTURA.Text=string.Empty;
            txtSALDO_INICIAL.Text = string.Empty;
            txtOBSERVACION.Text = string.Empty;
            
            Response.Redirect("FRM_PRINCIPAL.aspx");
            
        }

        #endregion

        #region FUNCIONES
        
        #endregion
        
    }
}