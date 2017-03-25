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
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using Newtonsoft.Json;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaIni.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                llenar_combo_tipomov();
                llenar_combo_concepto();
                llenar_combo_filtro_concepto();
                deshabilitar();
                
            }

        }

        #region OBJETOS
        N_VENTA OBJVENTA = new N_VENTA();
        E_MOVIMIENTOS OBJMOVS = new E_MOVIMIENTOS();
        #endregion


        void LIMPIAR()
        {
            txtCLIENTE.Text = string.Empty;
            txtDESC.Text = string.Empty;
            cboCONCEPTO.SelectedIndex =0;
            cboTIPOMOV.SelectedIndex = 0;
            txtFECHA.Text = string.Empty;
            txtIMPORTE.Text = string.Empty;
            txtLugar.Text = string.Empty;
            txtOPE.Text = string.Empty;
            
        }

        void llenar_datos(string cod, string id_empresa,string id_cta)
        {
            dgvMOVIMIENTOS.DataSource = OBJVENTA.NLLENARGRILLAMOVIMIENTOS(cod, id_empresa, id_cta);
            dgvMOVIMIENTOS.DataBind();

        }

        void llenar_combo_tipomov()
        {

            List<E_COMBO> List = new List<E_COMBO>();
            List.Add(new E_COMBO { valor = "TIPO", nombre = "--SELECCIONE TIPO MOV--" });
            List.Add(new E_COMBO { valor = "INGRESO", nombre = "INGRESO" });
            List.Add(new E_COMBO { valor = "EGRESO", nombre = "EGRESO" });

            cboTIPOMOV.DataSource = List;
            cboTIPOMOV.DataTextField = "nombre";
            cboTIPOMOV.DataValueField = "valor";
            cboTIPOMOV.SelectedIndex = 0;
            cboTIPOMOV.DataBind();

        }

        void llenar_combo_concepto()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_CONCEPTOS();

            cboCONCEPTO.DataSource = dt;
            cboCONCEPTO.DataValueField = "ID_CONCEPTOS_BANCARIOS";
            cboCONCEPTO.DataTextField = "DESCRIPCION";
            cboCONCEPTO.DataBind();
        }

        void llenar_combo_filtro_concepto()
        {
            DataTable dt = OBJVENTA.CONSULTA_LISTA_CONCEPTOS();

            cboFiltroConc.DataSource = dt;
            cboFiltroConc.DataValueField = "ID_CONCEPTOS_BANCARIOS";
            cboFiltroConc.DataTextField = "DESCRIPCION";
            cboFiltroConc.DataBind();
        }


        void habilitar()
        {
            txtFECHA.Enabled = true;
            cboCONCEPTO.Enabled = true;
            cboTIPOMOV.Enabled = true;
            txtIMPORTE.Enabled = true;
            txtDESC.Enabled = true;
            txtLugar.Enabled = true;
            txtOPE.Enabled = true;
            txtCLIENTE.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        void deshabilitar()
        {
            txtFECHA.Enabled = false;
            cboCONCEPTO.Enabled = false;
            cboTIPOMOV.Enabled = false;
            txtIMPORTE.Enabled = false;
            txtDESC.Enabled = false;
            txtLugar.Enabled = false;
            txtOPE.Enabled = false;
            txtCLIENTE.Enabled = false;
            btnNuevo.Enabled = false;
            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = false;
            FileUpload1.Enabled = false;
            Button1.Enabled = false;
            //btnConsulta.Enabled = false;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {   //VALIDAMOS QUE EL NUMERO DE OPERACION NO SE REPITA PARA LA CUENTA 
            int validador = 0;//VARIABLE SI ES 0 NO SE REPITE
            List<String> lista = new List<string>();
            DataTable DT = OBJVENTA.NVALIDARROPERACION(Session["ID_CUENTA_MOV"].ToString());//TREAEMOS LOS NUMEROS DE OPERACION
            string n_ope1 = txtOPE.Text;
            string imp2 = Convert.ToDecimal(txtIMPORTE.Text).ToString("N2");
            string fecha3 = Convert.ToDateTime(txtFECHA.Text).ToShortDateString();
            string cptob = cboCONCEPTO.SelectedValue.ToString();
            
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
                        lista.Add(txtOPE.Text);

                    }
                }
            }
            catch { validador = 0; }
            if (validador == 0)
            {
                OBJMOVS.id_mov = "";
                OBJMOVS.id_concepto_banc = cboCONCEPTO.SelectedValue;
                OBJMOVS.fecha = Convert.ToDateTime(txtFECHA.Text).ToString("dd-MM-yyyy hh:mm:ss");
                OBJMOVS.lugar = txtLugar.Text;
                OBJMOVS.tipo_mov = cboTIPOMOV.SelectedValue;
                OBJMOVS.id_cuentasbancarias = TXTprueba.Text;
                
                /*-------------------------SALDO +- IMPORTE--------------------*/
                double impo = 0;
                if (cboTIPOMOV.SelectedValue == "EGRESO")
                {
                    impo = -1 * Convert.ToDouble(txtIMPORTE.Text);
                }
                else if (cboTIPOMOV.SelectedValue == "INGRESO")
                {
                    impo = Convert.ToDouble(txtIMPORTE.Text);
                }
                double saldoc = Convert.ToDouble(LBLSALDOC.Text);
                double saldod = Convert.ToDouble(LBLSALDOD.Text);
                saldod = saldod + impo;
                saldoc = saldoc + impo;
                double SALD = saldoc;
                OBJMOVS.saldod = Convert.ToDecimal(saldod);
                OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
                OBJMOVS.saldo = Convert.ToDecimal(SALD);
                /*-----------------------------------------------------*/
                OBJMOVS.importe = Convert.ToDecimal(impo);
                OBJMOVS.operacion = txtOPE.Text;
                OBJMOVS.descripcion = txtDESC.Text;
                OBJMOVS.id_cliente = TXTid_cliente.Text;
                string empre = Session["ID_EMPRESA"].ToString();
                string res = OBJVENTA.NREGISTRARMOV(OBJMOVS, "2", empre);

                if (res == "ok")
                {
                    Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
                    LIMPIAR();
                    txtFECHA.Text = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                    /*---------------------------------------------------------------------------*/
                    DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
                    string mone = dt.Rows[0][0].ToString();
                    if (mone == "S") { LBLMONEDA.Text = "SOLES"; } else if (mone == "D") { LBLMONEDA.Text = "DOLARES"; }
                    LBLBANCO.Text = dt.Rows[0][1].ToString();
                    LBLSALDOC.Text = dt.Rows[0][2].ToString();
                    LBLSALDOD.Text = dt.Rows[0][3].ToString();
                    /*---------------------------------------------------------------------------*/
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
        



        protected void btnNuevo_Click(object sender, EventArgs e)
        {
           habilitar();
            btnNuevo.Enabled = false;
            txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void txtcuentainvi_TextChanged(object sender, EventArgs e)
        {
          
        }

        protected void TXTid_cliente_TextChanged(object sender, EventArgs e)
        {
            //lamar a sp para llenar labels de cabecera
           
        }

        protected void TXTprueba_TextChanged(object sender, EventArgs e)
        {
            //btnNuevo_Click(new object(), new EventArgs());
            
            //DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
            //LBLMONEDA.Text = dt.Rows[0][0].ToString();
            //LBLBANCO.Text = dt.Rows[0][1].ToString();
            //LBLSALDOC.Text = dt.Rows[0][2].ToString();
            //LBLSALDOD.Text = dt.Rows[0][3].ToString();
           
        }

        protected void btnTraeDatos_Click(object sender, EventArgs e)
        {
            if (TXTprueba.Text != string.Empty && TXTprueba.Text.Length == 4)
            {
                Session["ID_CUENTA_MOV"] = TXTprueba.Text;
                //string fechini = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy");
                //string fechfin = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
                //filtrar_grilla(Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString(), fechini, fechfin, txtConsultaOpe.Text, cboFiltroConc.SelectedValue.ToString(), txtConsultaCli.Text);

                llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
                DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
                string mone = dt.Rows[0][0].ToString();
                if (mone == "S") { LBLMONEDA.Text = "SOLES"; } else if (mone == "D") { LBLMONEDA.Text = "DOLARES"; }
                LBLBANCO.Text = dt.Rows[0][1].ToString();
                LBLSALDOC.Text = dt.Rows[0][2].ToString();
                LBLSALDOD.Text = dt.Rows[0][3].ToString();
                LBLNCUENTA.Text = dt.Rows[0][4].ToString();
                btnNuevo.Enabled = true;
                FileUpload1.Enabled = true;
                Button1.Enabled = true;
                btnConsulta.Enabled = true;
                btnActualizar.Enabled = false;
            }
            else
            {

                Response.Write("<script>alert('Ingrese una cuenta Válida')</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                {
                                        
                    /*----------------------------------------------------------------------------------------------------------------------------*/
                    ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                    DataTable dtexcel = package.ToDataTable();
                    List<String> lista = new List<string>();
                    //ACA SE HACEN LAS OPERACIONES PARA EDITAR LA TABLA
                    for (int i = 0; i < dtexcel.Rows.Count; i++)
                    {//por la cantidad de filas de la tabla
                        int validador = 0;//VARIABLE SI ES 0 NO SE REPITE
                        /* -----------------------------------------CREAMOS COLUMNAS D ELA TABLA PLANTILLA-------------------------------------------*/
                        DataTable DT = OBJVENTA.NVALIDARROPERACION(Session["ID_CUENTA_MOV"].ToString());//TREAEMOS LOS NUMEROS DE OPERACION
                        string n_ope1 = dtexcel.Rows[i][4].ToString();
                        string imp2 = dtexcel.Rows[i][6].ToString();
                        string fecha3 = dtexcel.Rows[i][1].ToString();
                        string concpto = dtexcel.Rows[i][0].ToString();
                        try
                        {
                            for (int n = 0; n < DT.Rows.Count; n++)
                            {
                                string fechaex = Convert.ToDateTime(DT.Rows[n][2].ToString()).ToShortDateString();
                                string opeex = DT.Rows[n][0].ToString();
                                string impoex = DT.Rows[n][1].ToString();
                                string concepex = DT.Rows[n][3].ToString();

                                if (n_ope1 == opeex && imp2 == impoex && fecha3 == fechaex && concpto == concepex)
                                    {
                                        validador += 1;
                                        lista.Add(dtexcel.Rows[i][4].ToString());
                                    
                                    }
                                }
                            }
                            catch { validador = 0;  }
                            if (validador == 0)
                            {
                                if (dtexcel.Rows[i][4].ToString() != null && dtexcel.Rows[i][4].ToString() != string.Empty && dtexcel.Rows[i][4].ToString() != "")
                                {
                                    OBJMOVS.id_mov = ""; /*---CONCEPTO BANCARIO depende de la descripcion en el bcp---*/
                                    OBJMOVS.id_concepto_banc = dtexcel.Rows[i][0].ToString();
                                    OBJMOVS.fecha = Convert.ToDateTime(dtexcel.Rows[i][1].ToString()).ToString("dd-MM-yyyy hh:mm:ss");
                                    if (dtexcel.Rows[i][2].ToString() == null || dtexcel.Rows[i][2].ToString() == ""|| dtexcel.Rows[i][2].ToString() == string.Empty)
                                    {
                                        OBJMOVS.lugar = "";
                                    }
                                    else { OBJMOVS.lugar = dtexcel.Rows[i][2].ToString(); }

                                    decimal num = Convert.ToDecimal(dtexcel.Rows[i][6].ToString());
                                    if (num > 0) { OBJMOVS.tipo_mov = "INGRESO"; } else if (num < 0) { OBJMOVS.tipo_mov = "EGRESO"; }
                                    OBJMOVS.operacion = dtexcel.Rows[i][4].ToString();
                                    OBJMOVS.id_cuentasbancarias = Session["ID_CUENTA_MOV"].ToString();
                                    OBJMOVS.importe = Convert.ToDecimal(dtexcel.Rows[i][6].ToString());
                                    /*--------------- //SUMAR O RESTAR EL IMPORTE DEL MOV AL SALDO--------------------*/
                                    
                                    double impo = Convert.ToDouble(dtexcel.Rows[i][6].ToString());
                                    double saldoc = Convert.ToDouble(LBLSALDOC.Text);
                                    double saldod = Convert.ToDouble(LBLSALDOD.Text);
                                    saldod = saldod + impo;
                                    saldoc = saldoc + impo;
                                    OBJMOVS.saldod = Convert.ToDecimal(saldod);
                                    OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
                                    OBJMOVS.saldo = Convert.ToDecimal(saldoc);
                                /*-------------------------------------------------------------------------------*/
                                OBJMOVS.descripcion = dtexcel.Rows[i][5].ToString();
                                    OBJMOVS.id_cliente = "";
                                    string empre = Session["ID_EMPRESA"].ToString();
                                    string res = OBJVENTA.NREGISTRARMOV(OBJMOVS, "2", empre);
                                    if (res == "ok")
                                    {
                                       
                                    llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
                                        LIMPIAR();
                                        txtFECHA.Text = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                                        DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
                                        string mone = dt.Rows[0][0].ToString();
                                        if (mone == "S") { LBLMONEDA.Text = "SOLES"; } else if (mone == "D") { LBLMONEDA.Text = "DOLARES"; }
                                        LBLBANCO.Text = dt.Rows[0][1].ToString();
                                        LBLSALDOC.Text = dt.Rows[0][2].ToString();
                                        LBLSALDOD.Text = dt.Rows[0][3].ToString();
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Error datos no Modificados')</script>");
                                    }
                                }
                            }
                            else
                            {
                            if (i == dtexcel.Rows.Count - 1)
                            { /*Response.Write("<script>alert('Datos Modificados correctamente..')</script>");*/
                                var json = JsonConvert.SerializeObject(lista);
                                Response.Write("<script>alert('LOS SIGUIENTES NUMEROS DE OPERACION SON REPETIDOS Y NO SE INCLUYERON EN EL PROCESO DE REGISTRO: " + json + "')</script>");
                            }
                        }

                        
                    }


                }
                else if (Path.GetExtension(FileUpload1.FileName) != ".xlsx")
                {
                    Response.Write("<script>alert('El archivo no tiene el formato correcto')</script>");
                }
            }
        }


        public void filtrar_grilla(string id_empresa, string id_cta, string fechaini, string fechafin, string nrope, string concepto, string idcli)
        {
            dgvMOVIMIENTOS.DataSource = OBJVENTA.NFILTRARGRILLAMOVIMIENTOS(id_empresa, id_cta,fechaini, fechafin, nrope, concepto, idcli);
            dgvMOVIMIENTOS.DataBind();
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            deshabilitar();
            LIMPIAR();
            btnRegistrar.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = false;
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {

            string fechini = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy");
            string fechfin = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
            filtrar_grilla(Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString(), fechini, fechfin, txtConsultaOpe.Text, cboFiltroConc.SelectedValue.ToString(), txtConsultaCli.Text);
        }

        protected void dgvMOVIMIENTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataTable dt = OBJVENTA.CONSULTA_LISTA_CONCEPTOS();
                Session["CodigoSede"] = row.Cells[0].Text;
                txtFECHA.Text = Convert.ToDateTime(row.Cells[1].Text).ToString("yyyy-MM-dd");
                cboTIPOMOV.SelectedValue = row.Cells[10].Text;
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    string nombre = dt.Rows[i][1].ToString();
                    if (dt.Rows[i][1].ToString() == row.Cells[3].Text)
                    {
                        string cod = dt.Rows[i][0].ToString();
                        cboCONCEPTO.SelectedValue = dt.Rows[i][0].ToString();
                    }
                }
                Session["IMPORTE_MOV"] = row.Cells[5].Text;
                txtLugar.Text = row.Cells[9].Text.Replace("&nbsp;", "");
                txtOPE.Text = row.Cells[4].Text.Replace("&nbsp;", ""); ;
                txtIMPORTE.Text = row.Cells[5].Text;
                txtDESC.Text = row.Cells[2].Text;
                txtCLIENTE.Text = row.Cells[7].Text.Replace("&nbsp;", "");
                habilitar();
                btnRegistrar.Enabled = false;
                btnNuevo.Enabled = false;
                btnActualizar.Enabled = true;

            }
            else if (e.CommandName == "ELIMINAR")
            {
                GridViewRow raw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoBCOE"] = raw.Cells[0].Text;

                string codigo = Session["CodigoBCOE"].ToString();

                string res = OBJVENTA.NELIMINARMOVIMIENTO(codigo);

                if (res == "ok")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


                    llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
                    LIMPIAR();
                    txtFECHA.Text = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                    DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
                    string mone = dt.Rows[0][0].ToString();
                    if (mone == "S") { LBLMONEDA.Text = "SOLES"; } else if (mone == "D") { LBLMONEDA.Text = "DOLARES"; }
                    LBLBANCO.Text = dt.Rows[0][1].ToString();
                    LBLSALDOC.Text = dt.Rows[0][2].ToString();
                    LBLSALDOD.Text = dt.Rows[0][3].ToString();
                    LIMPIAR();
                    Session["CodigoBCOE"] = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            OBJMOVS.id_mov = Session["CodigoSede"].ToString();
            OBJMOVS.id_concepto_banc = cboCONCEPTO.SelectedValue;
            OBJMOVS.fecha = Convert.ToDateTime(txtFECHA.Text).ToString("dd-MM-yyyy HH:mm");
            OBJMOVS.lugar = txtLugar.Text;
            OBJMOVS.tipo_mov = cboTIPOMOV.SelectedValue;
            OBJMOVS.id_cuentasbancarias = TXTprueba.Text;
            OBJMOVS.importe = Convert.ToDecimal(txtIMPORTE.Text);
            double nvoimporte = Convert.ToDouble(txtIMPORTE.Text);
            double antimporte = Convert.ToDouble(Session["IMPORTE_MOV"].ToString());
            double saldod = Convert.ToDouble(LBLSALDOC.Text);
            double saldoc = Convert.ToDouble(LBLSALDOD.Text);
            saldod = saldod + nvoimporte - antimporte;
            saldoc = saldoc + nvoimporte - antimporte;
            OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
            OBJMOVS.saldod = Convert.ToDecimal(saldoc);
            OBJMOVS.saldo = Convert.ToDecimal(saldoc);

            /*-----------------------------------------------------*/
            OBJMOVS.operacion = txtOPE.Text;
            OBJMOVS.descripcion = txtDESC.Text;
            OBJMOVS.id_cliente = TXTid_cliente.Text;
            string empre = Session["ID_EMPRESA"].ToString();
            string res = OBJVENTA.NACTUALIZARMOV(OBJMOVS, "4", empre);

            if (res == "ok")
            {
                Response.Write("<script>alert('Datos Modificados correctamente..')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
                LIMPIAR();
                txtFECHA.Text = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                /*---------------------------------------------------------------------------*/
                DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
                string mone = dt.Rows[0][0].ToString();
                if (mone == "S") { LBLMONEDA.Text = "SOLES"; } else if (mone == "D") { LBLMONEDA.Text = "DOLARES"; }
                LBLBANCO.Text = dt.Rows[0][1].ToString();
                LBLSALDOC.Text = dt.Rows[0][2].ToString();
                LBLSALDOD.Text = dt.Rows[0][3].ToString();
                LIMPIAR();
                btnActualizar.Enabled = false;
                btnCancelar.Enabled = false;
                btnNuevo.Enabled = true;
                btnRegistrar.Enabled = false;
                TXTid_cliente.Text = string.Empty;
                /*---------------------------------------------------------------------------*/
            }
            else
            {
                Response.Write("<script>alert('Error datos no Actualizados')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
        }
    }
}