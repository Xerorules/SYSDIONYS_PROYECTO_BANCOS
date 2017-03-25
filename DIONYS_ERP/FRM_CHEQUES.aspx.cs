using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Data;
using System.Drawing;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFGIRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFCOBRO.Text = DateTime.Now.ToString("yyyy-MM-dd");
                rdbMONEDA.SelectedValue = "S";

                txtIMPORTE.Attributes["onBlur"] = "IsAccNumberValid(" + txtIMPORTE.ClientID + ")";

                llenar_combo_bancos();
                llenar_combo_bancos2();
                llenar_datos();
               
            }
        }

        #region OBJETOS
        N_VENTA OBJVENTA = new N_VENTA();
        E_CHEQUES OBJCHEQUE = new E_CHEQUES();
        E_MOVIMIENTOS OBJMOVS = new E_MOVIMIENTOS();
        #endregion

        void llenar_combo_bancos()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_BANCOS();

            cboBANCO.DataSource = dt;
            cboBANCO.DataValueField = "ID_BANCOS";
            cboBANCO.DataTextField = "NOMBRE";
            cboBANCO.DataBind();
        }

        void llenar_combo_bancos2()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_BANCOS();
            cbomBANCO.DataSource = dt;
            cbomBANCO.DataValueField = "ID_BANCOS";
            cbomBANCO.DataTextField = "NOMBRE";
            cbomBANCO.DataBind();
        }

        void llenar_combo_cuentas(string id_bancos,string id_empresa)
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_CUENTAS(id_bancos, id_empresa);
            
            cbomCUENTA.DataSource = dt;
            cbomCUENTA.DataValueField = "ID_CUENTASBANCARIAS";
            cbomCUENTA.DataTextField = "CUENTA";
            cbomCUENTA.DataBind();
        }

        void LIMPIAR1()
        {
            txtmDESC.Text = string.Empty;
            txtmFECH.Text = string.Empty;
            txtmIMPORTE.Text = string.Empty;
            txtmLUGAR.Text = string.Empty;
            txtmOPE.Text = string.Empty;
            cbomBANCO.SelectedIndex = 0;
            
            cbomCUENTA.SelectedIndex = 0;
            mp1.Dispose();
            mp1.Hide();

        }

        void LIMPIAR2()
        {
            txtNUMERO.Text = string.Empty;
            txtCLIENTE.Text = string.Empty;
            txtFCOBRO.Text = string.Empty;
            txtFGIRO.Text = string.Empty;
            txtIMPORTE.Text = string.Empty;
            cboBANCO.SelectedIndex = 0;
            rdbMONEDA.SelectedValue = "S";
        }



        void llenar_datos()
        {
           
            dgvBANCOS.DataSource = OBJVENTA.NLLENARGRILLACHEQUES(Session["ID_EMPRESA"].ToString());
            dgvBANCOS.DataBind();

            for (int i = 0; i < dgvBANCOS.Rows.Count; i++)
            {
                string caseEstado = dgvBANCOS.Rows[i].Cells[8].Text;
                dgvBANCOS.Rows[i].Cells[2].Text = Convert.ToDateTime(dgvBANCOS.Rows[i].Cells[2].Text).ToShortDateString();
                dgvBANCOS.Rows[i].Cells[3].Text = Convert.ToDateTime(dgvBANCOS.Rows[i].Cells[3].Text).ToShortDateString();

                if (caseEstado == "1/01/1900 12:00:00 a. m.") //si retorna "1/01/1900 12:00:00 a. m." no se ha ingresado una fecha esta en blanco
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "PENDIENTE";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Gold;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;

                }
                else if (caseEstado != "1/01/1900 12:00:00 a. m." && caseEstado != "1/01/3000 12:00:00 a. m.")//cualquier fecha indica deposito normal
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "DEPOSITADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Green;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                }
                else if (caseEstado == "1/01/3000 12:00:00 a. m.")//si retorna "1/01/1900 12:00:00 a. m." el estado es rebotado
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "REBOTADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Red;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                }



            }

        }

        
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {


                OBJCHEQUE.id_cliente = TXTid_cliente.Text;
                OBJCHEQUE.fecha_giro =  Convert.ToDateTime(txtFGIRO.Text).ToShortDateString();
                OBJCHEQUE.fecha_cobro =  Convert.ToDateTime(txtFCOBRO.Text).ToShortDateString();
                OBJCHEQUE.numero = txtNUMERO.Text;
                OBJCHEQUE.id_banco = cboBANCO.SelectedValue;
                OBJCHEQUE.importe = Convert.ToDecimal(txtIMPORTE.Text);
                OBJCHEQUE.moneda = rdbMONEDA.SelectedValue;
                OBJCHEQUE.estado = "";
                OBJCHEQUE.id_empresa = Session["ID_EMPRESA"].ToString();


                string res = OBJVENTA.NREGISTRARCHEQUE(OBJCHEQUE);

                if (res == "ok")
                {
                    Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();
                    LIMPIAR2();

                }
                else
                {
                    Response.Write("<script>alert('Error datos no Modificados')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            // else{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvBANCOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName.ToUpper() == "ACTUALIZAR")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                txtmIMPORTE.Text = row.Cells[6].Text;
                txtmFECH.Text = DateTime.Now.ToString("yyyy-MM-dd");
                
                lblid_cheque.Text = row.Cells[0].Text;

                mp1.Show();

            }
        }

        protected void btnREGISTRARMOV_Click(object sender, EventArgs e)
        {

        }

        protected void cboBANCO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mp1.Show();
        }

        protected void cbomBANCO_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenar_combo_cuentas(cbomBANCO.SelectedValue.ToString(),Session["ID_EMPRESA"].ToString());
            mp1.Show();
        }
        protected void cbomCUENTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            mp1.Show();
        }
        

        protected void Button2_Click(object sender, EventArgs e)
        {
            //registrar mov y actualizar estado cheques a depositado
            int validador = 0;//VARIABLE SI ES 0 NO SE REPITE
            List<String> lista = new List<string>();
            DataTable DT = OBJVENTA.NVALIDARROPERACION(cbomCUENTA.SelectedValue);//TREAEMOS LOS NUMEROS DE OPERACION
            DataTable dtcheque = OBJVENTA.NTABLADATOSCHEQUE(lblid_cheque.Text);
            lblid_cliente.Text = dtcheque.Rows[0][0].ToString();
            string n_ope1 = txtmOPE.Text;
            string imp2 = Convert.ToDecimal(txtmIMPORTE.Text).ToString("N2");
            string fecha3 = Convert.ToDateTime(txtmFECH.Text).ToShortDateString();
            string cptob = "1003";//EDITAR DE  ACUERDO AL CONCEPTO BANCARIO EXISTENTE

            try
            {
                for (int n = 0; n < DT.Rows.Count; n++)//COMPARAMOS LSO NUMERO DE OPERACION PARA QUE NO SE REPITAN
                {
                    string fechaex = Convert.ToDateTime(DT.Rows[n][2].ToString()).ToShortDateString();
                    string opeex = DT.Rows[n][0].ToString();
                    string impoex = Convert.ToDecimal(DT.Rows[n][1].ToString()).ToString("N2");
                    string concep = DT.Rows[n][3].ToString();

                    if (n_ope1 == opeex && imp2 == impoex && fecha3 == fechaex && cptob == concep)
                    {
                        validador += 1;
                        lista.Add(txtmOPE.Text);

                    }
                }


            }
            catch { validador = 0; }
            if (validador == 0)
            {
                OBJMOVS.id_mov = "";
                OBJMOVS.id_concepto_banc = "1003";//EDITAR DE  ACUERDO AL CODIGO DE CONCEPTO BANCARIO EXISTENTE
                OBJMOVS.fecha = Convert.ToDateTime(txtmFECH.Text).ToString("dd-MM-yyyy hh:mm:ss");
                OBJMOVS.lugar = txtmLUGAR.Text;
                OBJMOVS.tipo_mov = DropDownList1.SelectedValue;
                OBJMOVS.id_cuentasbancarias = cbomCUENTA.SelectedValue;
                
                /*-------------------------SALDO--------------------*/
                double impo = Convert.ToDouble(txtmIMPORTE.Text);
                if (DropDownList1.Text == "INGRESO")
                {
                    OBJMOVS.saldo = Convert.ToDecimal(impo);
                    OBJMOVS.importe = Convert.ToDecimal(impo);
                }
                else if (DropDownList1.Text == "EGRESO")
                {
                    OBJMOVS.saldo = Convert.ToDecimal(-impo);
                    OBJMOVS.importe = Convert.ToDecimal(-impo); ;
                }
                /*-----------------------------------------------------*/
                
                OBJMOVS.operacion = txtmOPE.Text;
                OBJMOVS.descripcion = txtmDESC.Text;
                OBJMOVS.id_cliente = lblid_cliente.Text;
                string empre = Session["ID_EMPRESA"].ToString();
                string res = OBJVENTA.NREGISTRARMOV_CHEQUE(OBJMOVS, "1", empre,lblid_cheque.Text);

                if (res == "ok")
                {
                    Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();
                    LIMPIAR1();
                    mp1.Dispose();
                    mp1.Hide();
                   
                    
                   
                }
                else
                {
                    Response.Write("<script>alert('Error datos no Modificados')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            else
            {
                Response.Write("<script>alert('El número de operación ya existe para esta Cuenta')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)/**//**//*REBOTADO/**//**/
        {
            //eliminar mov y actualizar estado cheque a rebotado
            OBJMOVS.id_mov = "1000";
            OBJMOVS.id_concepto_banc = "1003";//EDITAR DE  ACUERDO AL CODIGO DE CONCEPTO BANCARIO EXISTENTE
            OBJMOVS.fecha = "";
            OBJMOVS.lugar = "";
            OBJMOVS.tipo_mov = "INGRESO";
            OBJMOVS.id_cuentasbancarias = "";
            OBJMOVS.saldo = Convert.ToDecimal(0);
            OBJMOVS.importe = Convert.ToDecimal(0);
            OBJMOVS.operacion = "222222";
            OBJMOVS.descripcion = "";
            OBJMOVS.id_cliente = "";
            string empre = Session["ID_EMPRESA"].ToString();
            string res = OBJVENTA.NREGISTRARMOV_CHEQUE(OBJMOVS, "2", empre, lblid_cheque.Text);

            if (res == "ok")
            {
                Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                llenar_datos();
                LIMPIAR1();
                mp1.Dispose();
                mp1.Hide();
              


            }
            else
            {
                Response.Write("<script>alert('Error datos no Modificados')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            mp1.Dispose();
            mp1.Hide();
        }

        protected void txtIMPORTE_TextChanged(object sender, EventArgs e)
        {
            decimal deci = Convert.ToDecimal(txtIMPORTE.Text);
            txtIMPORTE.Text = String.Format("{0:0,0.00}", deci);
        }
    }
}