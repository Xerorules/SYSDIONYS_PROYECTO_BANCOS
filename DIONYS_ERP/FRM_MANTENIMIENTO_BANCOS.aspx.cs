using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_NEGOCIO;
using CAPA_ENTIDAD;
using System.Data;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                llenar_datos();
            }
        }
        #region OBJETOS
        N_VENTA OBJVENTA = new N_VENTA();
        E_BANCO OBJBANCO = new E_BANCO();
        #endregion


        protected void dgvBANCOS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void LIMPIAR()
        {
            txtDIRECCION.Text = string.Empty;
            txtNOM.Text = string.Empty;
            txtRUC.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            
        }

        void llenar_datos()
        {
            dgvBANCOS.DataSource = OBJVENTA.NLLENARGRILLABANCOS();
            dgvBANCOS.DataBind();

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
                {
                    
                
                OBJBANCO.NOMBRE = txtNOM.Text;
                OBJBANCO.RUC = txtRUC.Text;
                OBJBANCO.DIRECCION = txtDIRECCION.Text;
                OBJBANCO.TELEFONO = txtTelefono.Text;

                string res = OBJVENTA.NREGISTRARBANCO(OBJBANCO);

                if (res == "ok")
                {
                    //Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();
                    LIMPIAR();

                }
                else
                {
                    //Response.Write("<script>alert('Error datos no Modificados')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
           // else{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true); }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LIMPIAR();
        }

        
       
        protected void dgvBANCOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dgvBANCOS_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EDITAR")
            {
                
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoSede"] = row.Cells[0].Text;
                txtNOM.Text = row.Cells[1].Text;
                txtRUC.Text = row.Cells[2].Text;
                txtDIRECCION.Text = row.Cells[3].Text;
                txtTelefono.Text = row.Cells[4].Text;

            }
            else if (e.CommandName == "ELIMINAR")
            {
                GridViewRow raw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoBCOE"] = raw.Cells[0].Text;

                string codigo = Session["CodigoBCOE"].ToString();

                string res = OBJVENTA.NELIMINARBANCO(codigo);

                if (res == "ok")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal33();", true);


                    llenar_datos();
                    LIMPIAR();
                    Session["CodigoBCOE"] = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal33();", true);
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OBJBANCO.ID_BANCO = Session["CodigoSede"].ToString();
                OBJBANCO.NOMBRE = txtNOM.Text;
                OBJBANCO.RUC = txtRUC.Text;
                OBJBANCO.DIRECCION = txtDIRECCION.Text;
                OBJBANCO.TELEFONO = txtTelefono.Text;

                string res = OBJVENTA.NACTUALIZARBANCO(OBJBANCO);

                if (res == "ok")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                    llenar_datos();
                    LIMPIAR();
                    Session["CodigoSede"] = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
        }

        protected void dgvBANCOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvBANCOS.PageIndex = e.NewPageIndex;
            llenar_datos();
        }
    }
}