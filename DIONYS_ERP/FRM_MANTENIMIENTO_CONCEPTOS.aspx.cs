using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web13 : System.Web.UI.Page
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
        E_CONCEPTO OBJCONCEPTO = new E_CONCEPTO();
        #endregion

        void LIMPIAR()
        {
            txtNOM.Text = string.Empty;

        }

        void llenar_datos()
        {
            dgvCONCEPTOS.DataSource = OBJVENTA.NLLENARGRILLACONCEPTO();
            dgvCONCEPTOS.DataBind();

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            OBJCONCEPTO.DESCRIPCION = txtNOM.Text;
            OBJCONCEPTO.ESTADO = "1";

            string res = OBJVENTA.NREGISTRARCONCEPTO(OBJCONCEPTO);

            if (res == "ok")
            {
                //Response.Write("<script>alert('Concepto registrado exitosamente')</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                llenar_datos();
                LIMPIAR();

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OBJCONCEPTO.ID_CONCEPTOS_BANCARIOS = Session["Codigo"].ToString();
                OBJCONCEPTO.DESCRIPCION = txtNOM.Text;

            string res = OBJVENTA.NACTUALIZARCONCEPTO(OBJCONCEPTO);

            if (res == "ok")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                llenar_datos();
                LIMPIAR();
                Session["Codigo"] = "";

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LIMPIAR();
        }

        protected void dgvCONCEPTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["Codigo"] = row.Cells[0].Text;
                txtNOM.Text = row.Cells[1].Text;

            }
            else if (e.CommandName == "ELIMINAR")
            {
                GridViewRow raw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["Codigoe"] = raw.Cells[0].Text;

                string codigo = Session["Codigoe"].ToString();

                string res = OBJVENTA.NELIMINARCONCEPTO(codigo);

                if (res == "ok")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                    llenar_datos();
                    LIMPIAR();
                    Session["Codigoe"] = "";
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
        }

        protected void dgvCONCEPTOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}