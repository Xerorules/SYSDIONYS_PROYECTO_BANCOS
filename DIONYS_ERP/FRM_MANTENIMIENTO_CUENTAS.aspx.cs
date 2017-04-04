using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using iTextSharp.text;
using System.Data;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                llenar_datos("1",Session["ID_EMPRESA"].ToString());
                llenar_combo_empresa();
                llenar_combo_bancos();
                CBOEMPRESA.Enabled = false;
                rdbMONEDA.SelectedValue = "SOLES";
            }
        }

        #region OBJETOS
        N_VENTA OBJVENTA = new N_VENTA();
        E_CUENTAS OBJCUENTAS = new E_CUENTAS();
        #endregion

        void LIMPIAR()
        {
            txtCCI.Text = string.Empty;
            txtCUENTA.Text = string.Empty;
            txtEMAIL.Text = string.Empty;
            //cboBANCO.SelectedIndex =0;
            txtOFICINA.Text = string.Empty;
            
            txtSECTOR.Text = string.Empty;
            txtTELEFONO.Text = string.Empty;
        }

        void llenar_datos(string cod,string id_empresa)
        {
            dgvBANCOS.DataSource = OBJVENTA.NLLENARGRILLACUENTAS(cod, id_empresa);
            dgvBANCOS.DataBind();

        }

        void llenar_combo_empresa()
        {

            List<E_COMBO> List = new List<E_COMBO>();

            List.Add(new E_COMBO { valor = Session["ID_EMPRESA"].ToString(), nombre = Session["NOMBRE_EMPRESA"].ToString() });

            CBOEMPRESA.DataSource = List;
            CBOEMPRESA.DataTextField = "nombre";
            CBOEMPRESA.DataValueField = "valor";
            CBOEMPRESA.SelectedIndex = 0;
            CBOEMPRESA.DataBind();
                        
        }

        void llenar_combo_bancos()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_BANCOS();
             
            cboBANCO.DataSource = dt;
            cboBANCO.DataValueField = "ID_BANCOS";
            cboBANCO.DataTextField = "NOMBRE";
            cboBANCO.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                OBJCUENTAS.ID_CUENTASBANCARIAS = 0;
                OBJCUENTAS.ID_EMPRESA = CBOEMPRESA.SelectedValue;
                OBJCUENTAS.N_CUENTA = txtCUENTA.Text;
                OBJCUENTAS.N_CCI = txtCCI.Text;
                OBJCUENTAS.SALDO_CONTABLE = Convert.ToDecimal(0);
                OBJCUENTAS.SALDO_DISPONIBLE = Convert.ToDecimal(0);
                if (rdbMONEDA.SelectedValue == "SOLES") { OBJCUENTAS.MONEDA = "S"; } else if(rdbMONEDA.SelectedValue == "DOLARES") { OBJCUENTAS.MONEDA = "D"; }
                OBJCUENTAS.SECTORISTA = txtSECTOR.Text;
                OBJCUENTAS.OFICINA = txtOFICINA.Text;
                OBJCUENTAS.TELEFONO = txtTELEFONO.Text;
                OBJCUENTAS.EMAIL = txtEMAIL.Text;
                OBJCUENTAS.ESTADO = "1";
                OBJCUENTAS.ID_BANCOS = cboBANCO.SelectedValue;

                string res = OBJVENTA.NREGISTRARCUENTA(OBJCUENTAS);

                if (res == "ok")
                {
                    //Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos("1", Session["ID_EMPRESA"].ToString());
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OBJCUENTAS.ID_CUENTASBANCARIAS = Convert.ToInt32(Session["CodigoCuen"]);
                OBJCUENTAS.ID_EMPRESA = CBOEMPRESA.SelectedValue;
                OBJCUENTAS.N_CUENTA = txtCUENTA.Text;
                OBJCUENTAS.N_CCI = txtCCI.Text;
                OBJCUENTAS.SALDO_CONTABLE = Convert.ToDecimal(0);
                OBJCUENTAS.SALDO_DISPONIBLE = Convert.ToDecimal(0);
                OBJCUENTAS.SECTORISTA = txtSECTOR.Text;
                OBJCUENTAS.OFICINA = txtOFICINA.Text;
                OBJCUENTAS.TELEFONO = txtTELEFONO.Text;
                OBJCUENTAS.EMAIL = txtEMAIL.Text;
                OBJCUENTAS.ESTADO = "1";
                OBJCUENTAS.ID_BANCOS = cboBANCO.SelectedValue;
                if (rdbMONEDA.SelectedValue == "SOLES") { OBJCUENTAS.MONEDA = "S"; } else if (rdbMONEDA.SelectedValue == "DOLARES") { OBJCUENTAS.MONEDA = "D"; }

                string res = OBJVENTA.NACTUALIZARCUENTA(OBJCUENTAS);

                if (res == "ok")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                    llenar_datos("1", Session["ID_EMPRESA"].ToString());
                    LIMPIAR();
                    Session["CodigoCuen"] = "";
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

        protected void dgvBANCOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoCuen"] = row.Cells[0].Text;
                txtCUENTA.Text = row.Cells[1].Text;
                cboBANCO.SelectedItem.Text = row.Cells[2].Text;
                txtCCI.Text = row.Cells[3].Text;
                string d = row.Cells[4].Text;
                if (row.Cells[4].Text == "S") { rdbMONEDA.SelectedValue = "SOLES"; }
                else if(row.Cells[4].Text == "D")  { rdbMONEDA.SelectedValue = "DOLARES"; }
                txtSECTOR.Text = row.Cells[7].Text;
                txtOFICINA.Text = row.Cells[8].Text;
                txtTELEFONO.Text = row.Cells[9].Text;
                txtEMAIL.Text = row.Cells[10].Text;
            }
            else if (e.CommandName == "ELIMINAR")
            {
                GridViewRow raw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoEx"] = raw.Cells[0].Text;

                string codigo = Session["CodigoEx"].ToString();

                string res = OBJVENTA.NELIMINARCUENTA(codigo);

                if (res == "ok")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                    llenar_datos("1", Session["ID_EMPRESA"].ToString());
                    LIMPIAR();
                    Session["CodigoEx"] = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
        }

        protected void dgvBANCOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvBANCOS.PageIndex = e.NewPageIndex;
            llenar_datos("1", Session["ID_EMPRESA"].ToString());
        }

        protected void cboBANCO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}