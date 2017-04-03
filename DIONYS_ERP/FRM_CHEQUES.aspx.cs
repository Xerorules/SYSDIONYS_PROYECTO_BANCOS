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

                txtFiltroFechaIni.Text = DateTime.Now.Date.AddMonths(-2).Date.ToString("yyyy-MM-dd");
                txtFiltroFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");

                txtIMPORTE.Attributes["onBlur"] = "IsAccNumberValid(" + txtIMPORTE.ClientID + ")";
                llenar_combo_filtro_bancos();
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

       

        void llenar_combo_filtro_bancos()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_BANCOS();

            cboFiltroBanco.DataSource = dt;
            cboFiltroBanco.DataValueField = "ID_BANCOS";
            cboFiltroBanco.DataTextField = "NOMBRE";
            cboFiltroBanco.DataBind();
        }

        void llenar_combo_bancos2()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_BANCOS();
            cbomBANCO.DataSource = dt;
            cbomBANCO.DataValueField = "ID_BANCOS";
            cbomBANCO.DataTextField = "NOMBRE";
            cbomBANCO.DataBind();
        }

        void llenar_combo_cuentas(string id_bancos,string id_empresa,string moneda)
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_CUENTAS(id_bancos, id_empresa, moneda);
            
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
            OBJCHEQUE.id_empresa = Session["ID_EMPRESA"].ToString();
            OBJCHEQUE.id_banco = "";
            OBJCHEQUE.moneda = "";
            OBJCHEQUE.id_cliente = "";
            OBJCHEQUE.fecha_giro = Convert.ToDateTime(txtFiltroFechaIni.Text).ToString("dd-MM-yyyy");
            OBJCHEQUE.fecha_cobro = Convert.ToDateTime(txtFiltroFechaFin.Text).ToString("dd-MM-yyyy");
            dgvBANCOS.DataSource = OBJVENTA.NLLENARGRILLACHEQUES(OBJCHEQUE);
            dgvBANCOS.DataBind();

            ///necesito un estado en la tabla movimientos para cheques

            for (int i = 0; i < dgvBANCOS.Rows.Count; i++)
            {
                string caseEstado = dgvBANCOS.Rows[i].Cells[8].Text.Substring(0,19);
                dgvBANCOS.Rows[i].Cells[2].Text = Convert.ToDateTime(dgvBANCOS.Rows[i].Cells[2].Text).ToShortDateString();
                dgvBANCOS.Rows[i].Cells[3].Text = Convert.ToDateTime(dgvBANCOS.Rows[i].Cells[3].Text).ToShortDateString();

                if (caseEstado == "1/01/1900 12:00:00 ") //si retorna "1/01/1900 12:00:00 a. m." no se ha ingresado una fecha esta en blanco
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "PENDIENTE";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.DeepSkyBlue;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;

                }
                else if (caseEstado == "31/12/1900 12:00:00")//cualquier fecha indica deposito normal
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "DEPOSITADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Orange;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                    dgvBANCOS.Rows[i].Cells[10].Enabled = false;
                }
                else if (caseEstado != "1/01/1900 12:00:00 " && caseEstado != "1/01/3000 12:00:00 " && caseEstado != "31/12/1900 12:00:00")//cualquier fecha indica deposito normal
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "ACEPTADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.LimeGreen;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                    dgvBANCOS.Rows[i].Cells[10].Enabled = false;
                    dgvBANCOS.Rows[i].Cells[9].Enabled = false;
                }
                else if (caseEstado == "1/01/3000 12:00:00 ")//si retorna "1/01/3000 12:00:00 a. m." el estado es rebotado
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "REBOTADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Red;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                    dgvBANCOS.Rows[i].Cells[9].Enabled = false;
                    dgvBANCOS.Rows[i].Cells[10].Enabled = false;
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
                    Response.Write("<script>alert('Cheque registrado correctamente..')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();
                    LIMPIAR2();

                }
                else
                {
                    Response.Write("<script>alert('Error cheque no registrado')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            // else{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true); }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

            OBJCHEQUE.id_cliente = TXTid_cliente.Text;
            OBJCHEQUE.fecha_giro = Convert.ToDateTime(txtFGIRO.Text).ToShortDateString();
            OBJCHEQUE.fecha_cobro = Convert.ToDateTime(txtFCOBRO.Text).ToShortDateString();
            OBJCHEQUE.numero = txtNUMERO.Text;
            OBJCHEQUE.id_banco = cboBANCO.SelectedValue;
            OBJCHEQUE.importe = Convert.ToDecimal(txtIMPORTE.Text);
            OBJCHEQUE.moneda = rdbMONEDA.SelectedValue;
            OBJCHEQUE.estado = Convert.ToDateTime(Session["ESTADO_CH"].ToString()).ToShortDateString(); 
            OBJCHEQUE.id_empresa = Session["ID_EMPRESA"].ToString();
            string id_cheque = Session["ID_CHEQUE"].ToString();

            string res = OBJVENTA.NACTUALIZARCHEQUE(OBJCHEQUE, id_cheque);

            if (res == "ok")
            {
                Response.Write("<script>alert('Datos actualizados correctamente..')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                btnRegistrar.Enabled = true;
                llenar_datos();
                LIMPIAR2();

            }
            else
            {
                Response.Write("<script>alert('Error cheque no actualizado')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LIMPIAR2();
            btnRegistrar.Enabled = true;

        }

        protected void dgvBANCOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName.ToUpper() == "ACTUALIZAR")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                txtmIMPORTE.Text = row.Cells[6].Text;
                txtmFECH.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtmIMPORTE.Enabled = false;
                lblid_cheque.Text = row.Cells[0].Text;
                Session["MONEDA_CHEQUE"] = row.Cells[7].Text;
                if (row.Cells[8].Text == "PENDIENTE")
                {
                    Button1.Enabled = false;
                    Button4.Enabled = false;
                }
                
                else if (row.Cells[8].Text == "DEPOSITADO")
                {
                    Button1.Enabled = true;
                    Button4.Enabled = true;
                }
                Session["ID_CHEQUE"] = row.Cells[0].Text;
                mp1.Show();

            }else if (e.CommandName == "EDITAR")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataTable dt = OBJVENTA.NLLENARDATOSACTUALIZAR(row.Cells[0].Text);
                txtCLIENTE.Text = dt.Rows[0][1].ToString();
                txtFGIRO.Text = Convert.ToDateTime(dt.Rows[0][2]).ToString("yyyy-MM-dd"); 
                txtFCOBRO.Text = Convert.ToDateTime(dt.Rows[0][3]).ToString("yyyy-MM-dd");
                txtNUMERO.Text = dt.Rows[0][4].ToString() ;
                cboBANCO.SelectedValue = dt.Rows[0][5].ToString();
                txtIMPORTE.Text = dt.Rows[0][6].ToString(); 
                rdbMONEDA.SelectedValue = dt.Rows[0][7].ToString();
                TXTid_cliente.Text = dt.Rows[0][0].ToString();
                btnRegistrar.Enabled = false;
                Session["ID_CHEQUE"] = row.Cells[0].Text;
                Session["ESTADO_CH"] = dt.Rows[0][8].ToString();
            }
            else if (e.CommandName == "ELIMINAR")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["ID_CHEQUE"] = row.Cells[0].Text;
                OBJCHEQUE.id_cheque = Session["ID_CHEQUE"].ToString();
                OBJCHEQUE.id_cliente = "00001";
                OBJCHEQUE.fecha_giro = Convert.ToDateTime("31-12-1900").ToShortDateString();
                OBJCHEQUE.fecha_cobro = Convert.ToDateTime("31-12-1900").ToShortDateString();
                OBJCHEQUE.numero = "";
                OBJCHEQUE.id_banco = "1000";
                OBJCHEQUE.importe = Convert.ToDecimal(0);
                OBJCHEQUE.moneda = "S";
                OBJCHEQUE.estado = "";
                OBJCHEQUE.id_empresa = Session["ID_EMPRESA"].ToString();
                string empre = Session["ID_EMPRESA"].ToString();
                string res = OBJVENTA.NELIMINARCHEQUE(OBJCHEQUE);
               
                if (res == "ok")
                {
                    Response.Write("<script>alert('Cheque correctamente eliminado')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();
                    LIMPIAR1();
                    mp1.Dispose();
                    mp1.Hide();



                }
                else
                {
                    Response.Write("<script>alert('Error movimiento no registrado')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }




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
            llenar_combo_cuentas(cbomBANCO.SelectedValue.ToString(),Session["ID_EMPRESA"].ToString(), Session["MONEDA_CHEQUE"].ToString());
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
                OBJMOVS.fecha = Convert.ToDateTime("31-12-1900").ToString("dd-MM-yyyy hh:mm:ss");
                string FECHA2 = Convert.ToDateTime(txtmFECH.Text).ToString("dd-MM-yyyy hh:mm:ss");
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
                string res = OBJVENTA.NREGISTRARMOV_CHEQUE(OBJMOVS, "1", empre,lblid_cheque.Text,FECHA2);

                if (res == "ok")
                {
                    Response.Write("<script>alert('Movimiento registrado correctamente')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos();
                    LIMPIAR1();
                    mp1.Dispose();
                    mp1.Hide();
                   
                    
                   
                }
                else
                {
                    Response.Write("<script>alert('Error movimiento no registrado')</script>");
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
            string res = OBJVENTA.NREGISTRARMOV_CHEQUE(OBJMOVS, "2", empre, lblid_cheque.Text,"");

            if (res == "ok")
            {
                Response.Write("<script>alert('El estado ha sido cambiado con exito, se eliminó el movimiento vinculado al cheque')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                llenar_datos();
                LIMPIAR1();
                mp1.Dispose();
                mp1.Hide();
              


            }
            else
            {
                Response.Write("<script>alert('Error no se pudo cambiar el estado')</script>");
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

        protected void Button4_Click(object sender, EventArgs e)
        {
            string respuesta = OBJVENTA.NACTUALIZARESTADOCHEQUE(Session["ID_CHEQUE"].ToString());
            if (respuesta == "ok")
            {
                Response.Write("<script>alert('El estado fue cambiado con exito')</script>");
            }
            else
            {
                Response.Write("<script>alert('Error ...No se pudo cambiar el estado ...')</script>");
            }
            llenar_datos();
            mp1.Dispose();
            mp1.Hide();
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            if (txtFiltroCli.Text == string.Empty) { txtfiltroid_cli.Text = ""; }
            OBJCHEQUE.id_empresa = Session["ID_EMPRESA"].ToString();
            OBJCHEQUE.id_banco = cboFiltroBanco.SelectedValue;
            OBJCHEQUE.moneda = cboFiltroMoneda.SelectedValue;
            OBJCHEQUE.id_cliente = txtfiltroid_cli.Text;
            OBJCHEQUE.fecha_giro = Convert.ToDateTime(txtFiltroFechaIni.Text).ToString("yyyy-dd-MM");
            OBJCHEQUE.fecha_cobro = Convert.ToDateTime(txtFiltroFechaFin.Text).ToString("yyyy-dd-MM");
            
            dgvBANCOS.DataSource = OBJVENTA.NLLENARGRILLACHEQUES(OBJCHEQUE);
            dgvBANCOS.DataBind();

            ///necesito un estado en la tabla movimientos para cheques

            for (int i = 0; i < dgvBANCOS.Rows.Count; i++)
            {
                string caseEstado = dgvBANCOS.Rows[i].Cells[8].Text.Substring(0, 19);
                dgvBANCOS.Rows[i].Cells[2].Text = Convert.ToDateTime(dgvBANCOS.Rows[i].Cells[2].Text).ToShortDateString();
                dgvBANCOS.Rows[i].Cells[3].Text = Convert.ToDateTime(dgvBANCOS.Rows[i].Cells[3].Text).ToShortDateString();

                if (caseEstado == "1/01/1900 12:00:00 ") //si retorna "1/01/1900 12:00:00 a. m." no se ha ingresado una fecha esta en blanco
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "PENDIENTE";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.DeepSkyBlue;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;

                }
                else if (caseEstado == "31/12/1900 12:00:00")//cualquier fecha indica deposito normal
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "DEPOSITADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Orange;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                    dgvBANCOS.Rows[i].Cells[10].Enabled = false;
                }
                else if (caseEstado != "1/01/1900 12:00:00 " && caseEstado != "1/01/3000 12:00:00 " && caseEstado != "31/12/1900 12:00:00")//cualquier fecha indica deposito normal
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "ACEPTADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.LimeGreen;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                    dgvBANCOS.Rows[i].Cells[10].Enabled = false;
                    dgvBANCOS.Rows[i].Cells[9].Enabled = false;
                }
                else if (caseEstado == "1/01/3000 12:00:00 ")//si retorna "1/01/3000 12:00:00 a. m." el estado es rebotado
                {
                    dgvBANCOS.Rows[i].Cells[8].Text = "REBOTADO";
                    dgvBANCOS.Rows[i].Cells[8].BackColor = Color.Red;
                    dgvBANCOS.Rows[i].Cells[8].ForeColor = Color.White;
                    dgvBANCOS.Rows[i].Cells[8].Font.Bold = true;
                    dgvBANCOS.Rows[i].Cells[8].HorizontalAlign = HorizontalAlign.Center;
                    dgvBANCOS.Rows[i].Cells[8].VerticalAlign = VerticalAlign.Middle;
                    dgvBANCOS.Rows[i].Cells[9].Enabled = false;
                    dgvBANCOS.Rows[i].Cells[10].Enabled = false;
                }



            }
            
        }

        protected void txtFiltroCli_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}