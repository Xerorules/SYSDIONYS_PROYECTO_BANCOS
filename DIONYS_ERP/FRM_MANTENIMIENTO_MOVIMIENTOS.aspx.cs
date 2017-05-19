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
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.DynamicData;
using System.Drawing;
using System.Threading;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class Formulario_web3 : System.Web.UI.Page
    {
        List<String> lista = new List<string>();
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaIni.Text = DateTime.Now.Date.AddMonths(-2).Date.ToString("yyyy-MM-dd");
                txtFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                llenar_combo_tipomov();
                llenar_combo_concepto();
                llenar_combo_filtro_concepto();
                deshabilitar();
                TextBoxssss.Text = DateTime.Now.Date.AddMonths(-2).Date.ToString("yyyy-MM-dd"); ;
                txtFechaFinDB.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Session["CodigoSede"] = "";
                //cargar_grilla_popup("", "", "", "1", Convert.ToDateTime(TextBoxssss.Text).ToShortDateString(), Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString(),txtClienteDBCom.Text);
                if (txtCuentaModal.Text == "" )
                {
                    txtCuentaModal.Focus();
                }
                lbltieneamarre.Text = "No existen ventas vinculadas a este movimiento";
                //lblCANTROWS.Text = "-";
                Session["ESTADO_CONSULTA"] = "";
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
            txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtIMPORTE.Text = string.Empty;
            txtLugar.Text = string.Empty;
            txtOPE.Text = string.Empty;
            TXTid_cliente.Text = string.Empty;
            txtObservacione.Text = string.Empty;
        }

        void llenar_datos(string cod, string id_empresa,string id_cta)
        {
            System.Data.DataTable ds = OBJVENTA.NLLENARGRILLAMOVIMIENTOS(cod, id_empresa, id_cta);
            dgvMOVIMIENTOS.DataSource = OBJVENTA.NLLENARGRILLAMOVIMIENTOS(cod, id_empresa, id_cta);
            dgvMOVIMIENTOS.DataBind();

           // lblCANTROWS.Text = ds.Rows.Count.ToString();

            DataTable tcheque = OBJVENTA.NSELECTCHEQUES();
            DataTable dt = OBJVENTA.NLLENARDESCRIPCIONCLIENTE("");

            for (int i = 0; i < dgvMOVIMIENTOS.Rows.Count; i++)
            {
                dgvMOVIMIENTOS.Rows[i].Cells[1].Text = Convert.ToDateTime(dgvMOVIMIENTOS.Rows[i].Cells[1].Text).ToShortDateString();
                if (dgvMOVIMIENTOS.Rows[i].Cells[10].Text == "EGRESO")
                {
                    dgvMOVIMIENTOS.Rows[i].Cells[16].Enabled = false;
                    
                }

                if (dgvMOVIMIENTOS.Rows[i].Cells[13].Text  == "0" )
                {
                    dgvMOVIMIENTOS.Rows[i].Cells[13].Text = "";
                   
                    dgvMOVIMIENTOS.Rows[i].Cells[13].HorizontalAlign = HorizontalAlign.Center;
                    dgvMOVIMIENTOS.Rows[i].Cells[13].ForeColor = Color.White;
                }
                else if (dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "1" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "2" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "3" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "4" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "5" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "6" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "7")
                {
                    dgvMOVIMIENTOS.Rows[i].Cells[13].Text = "AMARRADO";
                    dgvMOVIMIENTOS.Rows[i].Cells[13].BackColor = Color.LightPink;
                    
                    dgvMOVIMIENTOS.Rows[i].Cells[13].HorizontalAlign = HorizontalAlign.Center;
                    dgvMOVIMIENTOS.Rows[i].Cells[13].ForeColor = Color.Black;
                }

                string caseEstado = ds.Rows[i]["NOM_CLI"].ToString();

                for (int e = 0; e < dt.Rows.Count; e++) {
                    if (caseEstado == dt.Rows[e]["ID_CLIENTE"].ToString())
                    {
                        dgvMOVIMIENTOS.Rows[i].Cells[10].Text = dt.Rows[e]["DESCRIPCION"].ToString();
                    }
                }

                string movCH = ds.Rows[i]["ID_MOVIMIENTOS"].ToString();
                for (int h = 0; h < tcheque.Rows.Count; h++)
                {
                    string a = dgvMOVIMIENTOS.Rows[i].Cells[3].Text;
                    if (movCH == tcheque.Rows[h]["ID_MOVIMIENTOS"].ToString())
                    {
                        string esta = Convert.ToDateTime(tcheque.Rows[h]["ESTADO"].ToString()).ToShortDateString();
                       
                        if (a == "CHEQUE")
                        {
                            if (esta == "31/12/1900")
                            {
                            
                                dgvMOVIMIENTOS.Rows[i].Cells[3].BackColor = Color.Orange;
                           
                            
                            }
                             else if (esta != "01/01/1900" && esta != "01/01/3000" && esta != "31/12/1900")
                            {
                           
                                dgvMOVIMIENTOS.Rows[i].Cells[3].BackColor = Color.LimeGreen;
                            
                           
                            }
                        }
                    }
                }
                dgvMOVIMIENTOS.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }
            

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
            txtObservacione.Enabled = true;
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
            FileUpload1.Enabled = true;
            Button1.Enabled = true;
            txtObservacione.Enabled = false;
            //btnConsulta.Enabled = false;
        }


        void llenar_labels_cabecera()
        {
            /*---------------------------------------------------------------------------*/
            DataTable dt = OBJVENTA.NLLENAR_CABECERA_MOVIMIENTOS(TXTprueba.Text);
            string mone = dt.Rows[0][0].ToString();

            LBLBANCO.Text = dt.Rows[0][1].ToString();
            if (mone == "S")
            {
                LBLMONEDA.Text = "SOLES";
                LBLSALDOC.Text = "S/." + Convert.ToDecimal(dt.Rows[0][2].ToString()).ToString("#,###0.00");
                LBLSALDOD.Text = "S/." + Convert.ToDecimal(dt.Rows[0][3].ToString()).ToString("#,###0.00");
            }
            else if (mone == "D")
            {
                LBLMONEDA.Text = "DOLAR";
                LBLSALDOC.Text = "$  " + Convert.ToDecimal(dt.Rows[0][2].ToString()).ToString("#,###0.00");
                LBLSALDOD.Text = "$  " + Convert.ToDecimal(dt.Rows[0][3].ToString()).ToString("#,###0.00");
            }

            LBLNCUENTA.Text = dt.Rows[0][4].ToString();
            /*---------------------------------------------------------------------------*/
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            /*ACA EMPIEZA*/
            if (Session["CodigoSede"].ToString() == "") { 
            //VALIDAMOS QUE EL NUMERO DE OPERACION NO SE REPITA PARA LA CUENTA 
            int validador = 0;//VARIABLE SI ES 0 NO SE REPITE
            List<String> lista = new List<string>();
            DataTable DT = OBJVENTA.NVALIDARROPERACION(Session["ID_CUENTA_MOV"].ToString(), Convert.ToDateTime(txtFECHA.Text).ToShortDateString());//TREAEMOS LOS NUMEROS DE OPERACION
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
                OBJMOVS.fecha = Convert.ToDateTime(txtFECHA.Text).ToString("dd-MM-yyyy");
                OBJMOVS.lugar = txtLugar.Text.ToUpper();
                OBJMOVS.tipo_mov = cboTIPOMOV.SelectedValue;
                OBJMOVS.id_cuentasbancarias = TXTprueba.Text;

                /*-------------------------SALDO +- IMPORTE--------------------*/
                decimal impo = 0;
                if (cboTIPOMOV.SelectedValue == "EGRESO")
                {
                    impo = -1 * Convert.ToDecimal(txtIMPORTE.Text.Trim());
                }
                else if (cboTIPOMOV.SelectedValue == "INGRESO")
                {
                    impo = Convert.ToDecimal(txtIMPORTE.Text.Trim());
                }
                decimal saldoc = Convert.ToDecimal((LBLSALDOC.Text.Substring(3)));
                decimal saldod = Convert.ToDecimal((LBLSALDOD.Text.Substring(3)));
                saldod = saldod + impo;
                saldoc = saldoc + impo;

                OBJMOVS.saldod = Convert.ToDecimal(saldod);
                OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
                OBJMOVS.saldo = saldoc;
                /*-----------------------------------------------------*/
                OBJMOVS.importe = Convert.ToDecimal(impo);
                OBJMOVS.operacion = txtOPE.Text;
                string DESC = txtDESC.Text.ToUpper().Trim();
                    OBJMOVS.descripcion = DESC.ToUpperInvariant();
                    OBJMOVS.id_cliente = (txtCLIENTE.Text == "") ? "" : TXTid_cliente.Text;
                    /*CAMBIOS EN OPERACION 19-05-2017 -- AGREGAMOS GRILLA CON AMARRE DE DOCUMENTOS DE VENTA*/
                    DataTable dbt = (DataTable)Session["GRILLA_DOCS"];
                    string cadena = "";
                    for (int y=0; y<dbt.Rows.Count; y++)
                    {
                        cadena = cadena + dbt.Rows[y][0].ToString() + "/" + dbt.Rows[y][1].ToString() + "/" + dbt.Rows[y][2].ToString() + "//";
                    }

                    
                if (txtObservacione.Text == "" || txtObservacione.Text == "&nbsp;")
                {
                    OBJMOVS.observacion = cadena;
                }
                else
                {   
                        string obf = cadena +"#"+ txtObservacione.Text.ToUpper();
                        OBJMOVS.observacion = obf.ToUpperInvariant();
                }
                /*------------------------------------------------------------------------------------*/
                string empre = Session["ID_EMPRESA"].ToString();
                string res = OBJVENTA.NREGISTRARMOV(OBJMOVS, "2", empre);

                if (res == "ok")
                {
                    Response.Write("<script>alert('Datos Registrados correctamente..')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
                    LIMPIAR();
                    txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    llenar_labels_cabecera();
                    inicio();
                    btnRegistrar.Enabled = false;
                   
                        DataTable dt = (DataTable)Session["GRILLA_DOCS"];
                        dt.Clear();
                        dgvDATOS.DataSource = "";
                        dgvDATOS.DataBind();
                    }
                else
                {
                    Response.Write("<script>alert('Error datos no registrados,vuelva a intentar')</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
            }
            else
            {
                Response.Write("<script>alert('El número de operación ya existe para esta Cuenta')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
        }
        else if(Session["CodigoSede"].ToString() != "")
        {
            OBJMOVS.id_mov = Session["CodigoSede"].ToString();
            OBJMOVS.id_concepto_banc = cboCONCEPTO.SelectedValue;
            OBJMOVS.fecha = Convert.ToDateTime(txtFECHA.Text).ToString("dd-MM-yyyy");
            OBJMOVS.lugar = txtLugar.Text;
            OBJMOVS.tipo_mov = cboTIPOMOV.SelectedValue;
            OBJMOVS.id_cuentasbancarias = TXTprueba.Text;
            if (cboTIPOMOV.SelectedValue == "INGRESO")
                {
                    OBJMOVS.importe = Convert.ToDecimal(txtIMPORTE.Text);
                }
            else if (cboTIPOMOV.SelectedValue == "EGRESO")
                {
                    OBJMOVS.importe = Convert.ToDecimal(txtIMPORTE.Text) * -1;
                }
                //OBJMOVS.importe = Convert.ToDecimal(txtIMPORTE.Text);
            decimal nvoimporte = Convert.ToDecimal(txtIMPORTE.Text);
            decimal antimporte = Convert.ToDecimal(Session["IMPORTE_MOV"].ToString());
            decimal saldod = Convert.ToDecimal((LBLSALDOC.Text.Substring(3)));
            decimal saldoc = Convert.ToDecimal((LBLSALDOD.Text.Substring(3)));
            saldod = saldod + nvoimporte - antimporte;
            saldoc = saldoc + nvoimporte - antimporte;
            OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
            OBJMOVS.saldod = Convert.ToDecimal(saldoc);
            OBJMOVS.saldo = Convert.ToDecimal(saldoc);

            /*-----------------------------------------------------*/
            OBJMOVS.operacion = txtOPE.Text;
                string DESC = txtDESC.Text.ToUpper();
                OBJMOVS.descripcion = DESC.ToUpperInvariant();
                
            OBJMOVS.id_cliente = TXTid_cliente.Text;
                string obn = txtObservacione.Text.ToUpper();
                OBJMOVS.observacion = obn.ToUpperInvariant();
            string empre = Session["ID_EMPRESA"].ToString();
            string res = OBJVENTA.NACTUALIZARMOV(OBJMOVS, "4", empre);

            if (res == "ok")
            {
                    Response.Write("<script>alert('Datos Actualizados correctamente..')</script>");
                    llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
                    
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                
                LIMPIAR();

                /*---------------------------------------------------------------------------*/
                llenar_labels_cabecera();
               
                btnActualizar.Enabled = false;
                btnCancelar.Enabled = false;
                btnNuevo.Enabled = true;
                btnRegistrar.Enabled = false;
                
                TXTid_cliente.Text = string.Empty;
                txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    Session["CodigoSede"] = "";
                    inicio();
                    /*---------------------------------------------------------------------------*/
                }
            else
            {
                Response.Write("<script>alert('Error datos no Actualizados')</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
        }

        }
        

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
           habilitar();
            btnNuevo.Enabled = false;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
            txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtFECHA.Focus();

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
               
                llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
                llenar_labels_cabecera();
                btnNuevo.Enabled = true;
                FileUpload1.Enabled = true;
                Button1.Enabled = true;
                btnConsulta.Enabled = true;
                btnActualizar.Enabled = false;
                deshabilitar();
                LIMPIAR();

                btnRegistrar.Enabled = false;
                btnNuevo.Enabled = true;

                txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Button1.Enabled = true;
                cboFiltroConc.SelectedIndex = 0;
                txtConsultaOpe.Text = "";
                txtConsultaCli.Text = "";
            }
            else
            {

                Response.Write("<script>alert('Ingrese una cuenta Válida')</script>");
            }
             Session["ESTADO_CONSULTA"] = "";
            DataTable dtr = (DataTable)Session["GRILLA_DOCS"];
            dtr.Clear();
            dgvDATOS.DataSource = "";
            dgvDATOS.DataBind();
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
                   
                    //ACA SE HACEN LAS OPERACIONES PARA EDITAR LA TABLA
                    for (int i = 0; i < dtexcel.Rows.Count; i++)
                    {//por la cantidad de filas de la tabla
                        int validador = 0;//VARIABLE SI ES 0 NO SE REPITE
                        /* -----------------------------------------CREAMOS COLUMNAS D ELA TABLA PLANTILLA-------------------------------------------*/
                        DataTable DT = OBJVENTA.NVALIDARROPERACION(Session["ID_CUENTA_MOV"].ToString(), Convert.ToDateTime(dtexcel.Rows[i][1].ToString()).ToShortDateString());//TREAEMOS LOS NUMEROS DE OPERACION
                        string n_ope1 = dtexcel.Rows[i][4].ToString();
                        string imp2 = Convert.ToDecimal(dtexcel.Rows[i][6].ToString().Trim()).ToString("N2"); 
                        string fecha3 = dtexcel.Rows[i][1].ToString();
                        string concpto = dtexcel.Rows[i][0].ToString();
                        try
                        {
                            for (int n = 0; n < DT.Rows.Count; n++)
                            {
                                string fechaex = Convert.ToDateTime(DT.Rows[n][2].ToString()).ToShortDateString();
                                string opeex = DT.Rows[n][0].ToString();
                                string impoex = Convert.ToDecimal(DT.Rows[n][1].ToString()).ToString("N2");
                                string concepex = DT.Rows[n][3].ToString();

                                if (n_ope1 == opeex && imp2 == impoex && fecha3 == fechaex && concpto == concepex)
                                {
                                    validador += 1;
                                    lista.Add(dtexcel.Rows[i][4].ToString());

                                }
                            }
                        }
                        catch { validador = 0; }
                        if (validador == 0)
                            {
                                if (dtexcel.Rows[i][4].ToString() != null && dtexcel.Rows[i][4].ToString() != string.Empty && dtexcel.Rows[i][4].ToString() != "")
                                {
                                    OBJMOVS.id_mov = ""; /*---CONCEPTO BANCARIO depende de la descripcion en el bcp---*/
                                    OBJMOVS.id_concepto_banc = dtexcel.Rows[i][0].ToString();
                                    OBJMOVS.fecha = Convert.ToDateTime(dtexcel.Rows[i][1].ToString()).ToString("dd-MM-yyyy");
                                    if (dtexcel.Rows[i][2].ToString() == null || dtexcel.Rows[i][2].ToString() == ""|| dtexcel.Rows[i][2].ToString() == string.Empty)
                                    {
                                        OBJMOVS.lugar = "";
                                    }
                                    else { OBJMOVS.lugar = dtexcel.Rows[i][2].ToString(); }

                                    decimal num = Convert.ToDecimal(dtexcel.Rows[i][6].ToString().Trim());
                                    if (num >= 0) { OBJMOVS.tipo_mov = "INGRESO"; } else if (num < 0) { OBJMOVS.tipo_mov = "EGRESO"; }
                                    OBJMOVS.operacion = dtexcel.Rows[i][4].ToString();
                                    OBJMOVS.id_cuentasbancarias = Session["ID_CUENTA_MOV"].ToString();
                                    decimal imp = Convert.ToDecimal(dtexcel.Rows[i][6].ToString().Trim());
                                    OBJMOVS.importe = imp;
                                    /*--------------- //SUMAR O RESTAR EL IMPORTE DEL MOV AL SALDO--------------------*/
                                    
                                    decimal impo = Convert.ToDecimal(dtexcel.Rows[i][6].ToString().Trim());
                                    decimal saldoc = Convert.ToDecimal(LBLSALDOC.Text.Substring(3));
                                    decimal saldod = Convert.ToDecimal(LBLSALDOD.Text.Substring(3));
                                    saldod = saldod + impo;
                                    saldoc = saldoc + impo;
                                    OBJMOVS.saldod = Convert.ToDecimal(saldod);
                                    OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
                                    OBJMOVS.saldo = Convert.ToDecimal(saldoc);
                                /*-------------------------------------------------------------------------------*/
                                OBJMOVS.descripcion = dtexcel.Rows[i][5].ToString();
                                OBJMOVS.id_cliente = "";
                                OBJMOVS.observacion = "";
                                    string empre = Session["ID_EMPRESA"].ToString();
                                    string res = OBJVENTA.NREGISTRARMOV(OBJMOVS, "2", empre);
                                    if (res == "ok")
                                    {
                                        
                                        llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
                                        LIMPIAR();
                                        txtFECHA.Text = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
                                        llenar_labels_cabecera();
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Error datos no Modificados')</script>");
                                    }
                                }
                            }
                            else
                            {
                            
                             
                               
                            
                        }

                        
                    }
                    if (lista.Count > 0)
                    {
                        
                        registrar_evento();/*ESTE CODIGO REGISTRA UN LOG DE NUMEROS DE OPERACION RPETIDOS*/
                        Response.Write("<script>alert('CARGA REALIZADA CON ÉXITO ...!! DURANTE EL PROCESO SE DETECTÓ ALGUNOS REGISTROS DUPLICADOS, QUE NO SE SUBIERON A LA BASE DE DATOS!!... !')</script>");
                        /*---ESTE CODIGO PERMITE MOSTRAR LOS NUMEROS DE OPERACION REPETIDOS---*/
                        //if (System.Windows.Forms.MessageBox.Show("EXISTEN REGISTROS REPETIDOS EN LA CARGA, ¿DESEA VER LOS REGISTROS REPETIDOS?: ",
                        //"RESUMEN DE ERRORES",
                        //System.Windows.Forms.MessageBoxButtons.YesNo,
                        //System.Windows.Forms.MessageBoxIcon.Information)

                        //    == DialogResult.Yes)
                        //{
                        //    System.Diagnostics.Process.Start(@"C:\temp\LOG_REPETIDOS.txt");
                        //}

                    }
                    else
                    {
                        Response.Write("<script>alert('CARGA REALIZADA CON ÉXITO.. !')</script>");// VERFICAR SI SE REPITE
                    }


                }
                else if (Path.GetExtension(FileUpload1.FileName) != ".xlsx")
                {
                    Response.Write("<script>alert('El archivo no tiene el formato correcto')</script>");
                }
            }
            Session["ESTADO_CONSULTA"] = "";
        }


        public void filtrar_grilla(string id_empresa, string id_cta, string fechaini, string fechafin, string nrope, string concepto, string idcli)
        {
            System.Data.DataTable ds = OBJVENTA.NFILTRARGRILLAMOVIMIENTOS(id_empresa, id_cta, fechaini, fechafin, nrope, concepto, idcli);
            dgvMOVIMIENTOS.DataSource = OBJVENTA.NFILTRARGRILLAMOVIMIENTOS(id_empresa, id_cta,fechaini, fechafin, nrope, concepto, idcli);
            dgvMOVIMIENTOS.DataBind();

            DataTable tcheque = OBJVENTA.NSELECTCHEQUES();
            DataTable dt = OBJVENTA.NLLENARDESCRIPCIONCLIENTE("");
            for (int i = 0; i < dgvMOVIMIENTOS.Rows.Count; i++)
            {
                dgvMOVIMIENTOS.Rows[i].Cells[1].Text = Convert.ToDateTime(dgvMOVIMIENTOS.Rows[i].Cells[1].Text).ToShortDateString();

                if (dgvMOVIMIENTOS.Rows[i].Cells[10].Text == "EGRESO")
                {
                    dgvMOVIMIENTOS.Rows[i].Cells[16].Enabled = false;

                }

                if (dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "0")
                {
                    dgvMOVIMIENTOS.Rows[i].Cells[13].Text = "";

                    dgvMOVIMIENTOS.Rows[i].Cells[13].HorizontalAlign = HorizontalAlign.Center;
                    dgvMOVIMIENTOS.Rows[i].Cells[13].ForeColor = Color.White;
                }
                else if (dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "1" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "2" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "3" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "4" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "5" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "6" || dgvMOVIMIENTOS.Rows[i].Cells[13].Text == "7")
                {
                    dgvMOVIMIENTOS.Rows[i].Cells[13].Text = "AMARRADO";
                    dgvMOVIMIENTOS.Rows[i].Cells[13].BackColor = Color.LightPink;

                    dgvMOVIMIENTOS.Rows[i].Cells[13].HorizontalAlign = HorizontalAlign.Center;
                    dgvMOVIMIENTOS.Rows[i].Cells[13].ForeColor = Color.Black;
                }

                string caseEstado = ds.Rows[i]["NOM_CLI"].ToString();

                for (int e = 0; e < dt.Rows.Count; e++)
                {
                    if (caseEstado == dt.Rows[e]["ID_CLIENTE"].ToString())
                    {
                        dgvMOVIMIENTOS.Rows[i].Cells[10].Text = dt.Rows[e]["DESCRIPCION"].ToString();

                    }
                }

                string movCH = ds.Rows[i]["ID_MOVIMIENTOS"].ToString();
                for (int h = 0; h < tcheque.Rows.Count; h++)
                {
                    if (movCH == tcheque.Rows[h]["ID_MOVIMIENTOS"].ToString())
                    {
                        string esta = Convert.ToDateTime(tcheque.Rows[h]["ESTADO"].ToString()).ToShortDateString();

                        if (esta == "31/12/1900")
                        {
                            string a = dgvMOVIMIENTOS.Rows[i].Cells[3].Text;
                            if (a == "CHEQUE")
                            {
                                dgvMOVIMIENTOS.Rows[i].Cells[3].BackColor = Color.Orange;
                            }

                        }
                        else if (esta != "01/01/1900" && esta != "01/01/3000" && esta != "31/12/1900")
                        {
                            if (dgvMOVIMIENTOS.Rows[i].Cells[3].Text == "CHEQUE")
                            {
                                dgvMOVIMIENTOS.Rows[i].Cells[3].BackColor = Color.LimeGreen;
                            }

                        }
                    }
                }
                dgvMOVIMIENTOS.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }
            DataTable dtr = (DataTable)Session["GRILLA_DOCS"];
            dtr.Clear();
            dgvDATOS.DataSource = "";
            dgvDATOS.DataBind();

        }

        /*------------------------------------------------------------------*/
        void registrar_evento()
        {

            /*-------------------------PRUEBA DE MONITOREO DE RED(LOGS)----------------------*/
            string path = @"c:\temp\LOG_REPETIDOS.txt";
            string fechad = DateTime.Now.ToShortDateString();
            // This text is always added, making the file longer over time
            // if it is not deleted.
            try
            {
                StreamWriter sw = File.AppendText(path);
                string hora = DateTime.Now.ToLongTimeString();
                var json = JsonConvert.SerializeObject(lista);
                sw.WriteLine(fechad +"  "+" NUMEROS DE OPERACION REPETIDOS :  "  + json);
                sw.Dispose();

            }
            catch { }
        }
        /*------------------------------------------------------------------*/
        void EXPORTAR_EXCEL(GridView TABLA)
        {
            

            //if (TABLA.Rows.Count > 0)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    StringWriter sw = new StringWriter(sb);
            //    HtmlTextWriter htw = new HtmlTextWriter(sw);
            //    Page pagina = new Page();
            //    HtmlForm form = new HtmlForm();
            //    pagina.EnableEventValidation = false;
            //    pagina.DesignerInitialize();
            //    pagina.Controls.Add(form);
            //    form.Controls.Add(dg);
            //    pagina.RenderControl(htw);
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=REPORTE_MOVIMIENTOS.xls");
            //    Response.Charset = "UTF-8";
            //    Response.ContentEncoding = Encoding.Default;
            //    Response.Write(sb.ToString());
            //    Response.End();
            //}
        }

        void inicio()
        {
            LIMPIAR();
            deshabilitar();
            
            btnRegistrar.Enabled = true;
            btnNuevo.Enabled = true;
            txtObservacione.Enabled = false;
            txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");/*DDDD*/
            txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");/*DDDD*/
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            deshabilitar();
            LIMPIAR();
            
            btnRegistrar.Enabled = false;
            btnNuevo.Enabled = true;
            txtObservacione.Enabled = false;
            txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Session["CodigoSede"] = "";

            DataTable dtr = (DataTable)Session["GRILLA_DOCS"];
            dtr.Clear();
            dgvDATOS.DataSource = "";
            dgvDATOS.DataBind();
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            Session["ESTADO_CONSULTA"] = "1";
            string fechini = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy");
            string fechfin = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
            string ID_CLIENTE2 = "";
            if (txtConsultaCli.Text == "") { ID_CLIENTE2 = ""; } else { ID_CLIENTE2 = txtConsultaCliValor.Text; }
            filtrar_grilla(Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString(), fechini, fechfin, txtConsultaOpe.Text.Trim(), cboFiltroConc.SelectedValue.ToString(), ID_CLIENTE2.ToString());
        }

        protected void dgvMOVIMIENTOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDITAR")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                string VERIFICADOR = "";

                DataTable dtr = (DataTable)Session["GRILLA_DOCS"];
                dtr.Clear();
                dgvDATOS.DataSource = "";
                dgvDATOS.DataBind();

                DataTable dt = OBJVENTA.CONSULTA_LISTA_CONCEPTOS();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string nombre = dt.Rows[i][1].ToString();
                    if (dt.Rows[i][1].ToString() == row.Cells[3].Text)
                    {
                        string cod = dt.Rows[i][0].ToString();

                         VERIFICADOR = dt.Rows[i][0].ToString();

                        //Response.Write("<script>'alert('Los cheques deben ser editados por el ódulod e CHEQUES EN CARTERA')'</script>");

                    }
                }
                string tipo_mov = row.Cells[10].Text;

                if ((VERIFICADOR != "1003") || ((VERIFICADOR == "1003" && tipo_mov == "EGRESO"))) {
                    habilitar();


                    Session["CodigoSede"] = row.Cells[0].Text;
                    txtFECHA.Text = Convert.ToDateTime(row.Cells[1].Text).ToString("yyyy-MM-dd");
                    cboTIPOMOV.SelectedValue = row.Cells[10].Text;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string nombre = dt.Rows[i][1].ToString();
                        if (dt.Rows[i][1].ToString() == row.Cells[3].Text)
                        {
                            string cod = dt.Rows[i][0].ToString();

                            cboCONCEPTO.SelectedValue = dt.Rows[i][0].ToString();

                            //Response.Write("<script>'alert('Los cheques deben ser editados por el ódulod e CHEQUES EN CARTERA')'</script>");

                        }
                    }
                    /**/
                    string clinom = row.Cells[7].Text.Trim();
                    string cliq = "&#160;";
                    bool clibool = clinom.Contains(cliq);
                    int indexinicio = 0;
                    if (clibool) { indexinicio = clinom.IndexOf(cliq); }
                    if (indexinicio > 0)
                    { txtCLIENTE.Text = clinom.Substring(0, indexinicio); }
                    else { txtCLIENTE.Text = HttpUtility.HtmlDecode(row.Cells[7].Text.Trim()); }
                    //txtCLIENTE.Text = row.Cells[7].Text.Trim();
                    /**/
                    string lugnom = row.Cells[9].Text.Trim();
                    string lugq = "&#160;";
                    bool lugbool = clinom.Contains(lugq);
                    int indexinicio1 = 0;
                    if (lugbool) { indexinicio1 = lugnom.IndexOf(lugq); }
                    if (indexinicio1 > 0)
                    { txtLugar.Text = lugnom.Substring(0, indexinicio1); }
                    else { txtLugar.Text = HttpUtility.HtmlDecode(row.Cells[9].Text.Trim()); }
                    //txtLugar.Text = row.Cells[9].Text.Trim();
                    /**/
                    string desnom = row.Cells[2].Text.Trim();
                    string desq = "&#160;";
                    bool desbool = desnom.Contains(desq);
                    int indexdesc = 0;
                    if (desbool) { indexdesc = desnom.IndexOf(desq); }
                    if (indexdesc > 0)
                    { txtDESC.Text = desnom.Substring(0, indexdesc); }
                    else { txtDESC.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim()); }
                    txtDESC.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());
                    //txtDESC.Text = row.Cells[2].Text.Trim();
                    /**/
                    string openom = row.Cells[4].Text.Trim();
                    string opeq = "&#160;";
                    bool opebool = openom.Contains(opeq);
                    int indexope = 0;
                    if (opebool) { indexope = openom.IndexOf(opeq); }
                    if (indexope > 0)
                    { txtOPE.Text = openom.Substring(0, indexope); }
                    else { txtOPE.Text = HttpUtility.HtmlDecode(row.Cells[4].Text.Trim()); }
                    //txtOPE.Text = row.Cells[4].Text.Trim();
                    /**/
                    String cadena = "";
                    string obsnom = row.Cells[12].Text.Trim();
                    string obsq = "&#160;";
                    bool obsbool = obsnom.Contains(obsq);
                    int indexobs = 0;
                    if (obsbool) { indexobs = obsnom.IndexOf(obsq); }

                    string michi = "#";
                    bool ver = obsnom.Contains(michi);
                    int indx = 0;
                    if (ver) { indx = obsnom.IndexOf(michi); }

                    if (indexobs > 0)
                    { txtObservacione.Text = obsnom.Substring(0, indexobs); }
                    else if (indx > 0) { txtObservacione.Text = HttpUtility.HtmlDecode(obsnom.Substring(indx+1)); cadena = obsnom.Substring(0, indx); }
                    else if (indx == 0) { txtObservacione.Text = "";  cadena = obsnom; }
                    /*-----------------------------LLENAMOS LA GRILLA OBSERVACION ----------------------------*/
                    
                   
                    int ocurrencias = 0;
                    ocurrencias = cadena.Split(new String[] { "//" }, StringSplitOptions.None).Length - 1;
                    int final = 0;
                    int inicio = 0;
                    for (int o = 0; o < ocurrencias; o++)
                    {
                        final = 14;
                        string CADE_LARGA = obsnom.Substring(inicio, final);
                        inicio = inicio + 14;
                        /*capturamos data para llenar en las filas de la tabla de session*/
                        
                        string doc = CADE_LARGA.Substring(0, 2);
                        string serie = CADE_LARGA.Substring(3, 3);
                        string numero = CADE_LARGA.Substring(7, 5);

                        /*llenamos la tabla de session*/
                        DataTable dts = (DataTable)Session["GRILLA_DOCS"];

                        DataRow raw = dts.NewRow();
                        raw["DOC"] = doc;
                        raw["SERIE"] = serie;
                        raw["NUMERO"] = numero;

                        dts.Rows.Add(raw);
                        dts.AcceptChanges();
                        LLENAR_GRILLA();

                    }

                    /*llenamos la grilla con la tabla de session*/
                    



                    /*----------------------------------------------------------------------------------------*/






                if (row.Cells[9].Text == "&nbsp;") { txtLugar.Text = row.Cells[9].Text.Replace("&nbsp;", ""); }
                if (row.Cells[4].Text == "&nbsp;") { txtOPE.Text = row.Cells[4].Text.Replace("&nbsp;", ""); }
                if (row.Cells[2].Text == "&nbsp;") { txtDESC.Text = row.Cells[2].Text.Replace("&nbsp;", ""); }
                if (row.Cells[7].Text == "&nbsp;") { txtCLIENTE.Text = row.Cells[7].Text.Replace("&nbsp;", ""); }
                if (row.Cells[12].Text == "&nbsp;") { txtObservacione.Text = row.Cells[12].Text.Replace("&nbsp;", ""); }
                double importeact = Convert.ToDouble(row.Cells[5].Text);

                if (importeact > 0)
                    {
                        
                        txtIMPORTE.Text = Convert.ToDecimal(importeact).ToString("#,###0.00");
                        Session["IMPORTE_MOV"] = Convert.ToDecimal(importeact).ToString("#,###0.00");
                    }
                    else
                    {
                        txtIMPORTE.Text = Convert.ToDecimal(importeact * -1).ToString("#,###0.00"); 
                        Session["IMPORTE_MOV"] = Convert.ToDecimal(importeact * -1).ToString("#,###0.00");
                    }

                if (row.Cells[9].Text == "&#160;") { txtLugar.Text = row.Cells[9].Text.Replace("&#160;", ""); }
                if (row.Cells[4].Text == "&#160;") { txtOPE.Text = row.Cells[4].Text.Replace("&#160;", ""); }
                if (row.Cells[2].Text == "&#160;") { txtDESC.Text = row.Cells[2].Text.Replace("&#160;", ""); }
                if (row.Cells[7].Text == "&#160;") { txtCLIENTE.Text = row.Cells[7].Text.Replace("&#160;", ""); }
                if (row.Cells[12].Text == "&#160;") { txtObservacione.Text = row.Cells[12].Text.Replace("&#160;", ""); }


                btnRegistrar.Enabled = true;
                btnNuevo.Enabled = false;
                btnActualizar.Enabled = false;
                btnCancelar.Enabled = true;

                }
                else
                {
                    //System.Console.Write("Los cheques deben ser editados por el módulo de CHEQUES EN CARTERA");
                    //Response.Write(HttpUtility.HtmlEncode("<script>'alert('Los cheques deben ser editados por el módulo de CHEQUES EN CARTERA')'</script>"));
                    Response.Write("<script>alert('LOS INGRESOS CON CHEQUE DEBEN SER MODIFICADOS POR EL MÓDULO DE CHEQUES EN CARTERA')</script>");
                    //MessageBox.Show("Los cheques deben ser editados por el módulo de CHEQUES EN CARTERA");
                    LIMPIAR();
                    llenar_labels_cabecera();
                    Session["CodigoBCOE"] = "";
                    //postback forzado
                    txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    
                }
            }
            else if (e.CommandName == "ELIMINAR")
            {
                GridViewRow raw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoBCOE"] = raw.Cells[0].Text;

                string codigo = Session["CodigoBCOE"].ToString();
                string id_cta = TXTprueba.Text;
                string fcha = Convert.ToDateTime(raw.Cells[1].Text).ToString("dd-MM-yyyy");
                string res = OBJVENTA.NELIMINARMOVIMIENTO(codigo,id_cta,fcha);

                if (res == "ok")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Response.Write("<script>alert('EL REGISTRO FUE ELIMINADO CORRECTAMENTE')</script>");

                    llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
                    LIMPIAR();
                    
                    llenar_labels_cabecera();

                    Session["CodigoBCOE"] = "";
                    //postback forzado
                    txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                    Response.Write("<script>alert('HUBO UN ERROR NO S EPUEDO ELIMINAR EL REGISTRO, INTENTE DE NUEVO')</script>");

                }
            }
            else if (e.CommandName == "AMARRE")
            {
                GridViewRow rew = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["CodigoMOVAMARRE"] = rew.Cells[0].Text;
                string co = rew.Cells[0].Text;
                Session["OBSAMARRE"] = rew.Cells[12].Text;
                Session["DV_NUMEROINT"] = rew.Cells[13].Text;

                string CODVTA = Session["DV_NUMEROINT"].ToString();
                if (Session["DV_NUMEROINT"].ToString() == "" || Session["DV_NUMEROINT"].ToString() == "&nbsp;" || Session["DV_NUMEROINT"].ToString() == "DISPONIBLE") 
                {
                    CODVTA = "";
                    Session["DV_NUMEROINT"] = "";
                    txtClienteDBCom.Text = "";
                    cbomTipoDoc.Text = "";
                }
                else { CODVTA = Session["DV_NUMEROINT"].ToString(); }

                 
                
                if (CODVTA == "")
                {
                    string codv = cbomTipoDoc.Text;
                    string fechai = "";
                    string fechaf = "";
                    string cod_cli_dbco = "";
                    cod_cli_dbco = txtClienteDBCom.Text;
                    fechaf = Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString();
                    fechai = Convert.ToDateTime(TextBoxssss.Text).ToShortDateString();
                    cargar_grilla_popup("", codv, "", "1", fechai, fechaf, cod_cli_dbco);
                    lbltieneamarre.Text = "No existen ventas vinculadas a este movimiento";
                    lbltieneamarre.Visible = true;
                    txtClienteDBCom.Text = "";
                    cbomTipoDoc.Text = "";
                    prueba_carga(co);
                }
                else if (CODVTA != "")
                {
                    lbltieneamarre.Visible = false;
                    string codv = cbomTipoDoc.Text;
                    string fechai = "";
                    string fechaf = "";
                    string cod_cli_dbco = "";
                    cod_cli_dbco = txtClienteDBCom.Text;
                    fechaf = Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString();
                    fechai = Convert.ToDateTime(TextBoxssss.Text).ToShortDateString();
                    cargar_grilla_popup("", codv, "", "1", fechai, fechaf, cod_cli_dbco);
                    //cargar_grilla_popup(co, "", "", "1", Convert.ToDateTime(TextBoxssss.Text).ToShortDateString(), Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString(), txtClienteDBCom.Text);
                    txtClienteDBCom.Text = "";
                    cbomTipoDoc.Text = "";
                    prueba_carga(co);

                }
                //Session["DV_NUMEROINT"] = "";
                
                mp1.Show();
                cbomTipoDoc.Focus();
            }

        }

        

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //OBJMOVS.id_mov = Session["CodigoSede"].ToString();
            //OBJMOVS.id_concepto_banc = cboCONCEPTO.SelectedValue;
            //OBJMOVS.fecha = Convert.ToDateTime(txtFECHA.Text).ToString("dd-MM-yyyy");
            //OBJMOVS.lugar = txtLugar.Text;
            //OBJMOVS.tipo_mov = cboTIPOMOV.SelectedValue;
            //OBJMOVS.id_cuentasbancarias = TXTprueba.Text;
            //OBJMOVS.importe = Convert.ToDecimal(txtIMPORTE.Text);
            //decimal nvoimporte = Convert.ToDecimal(txtIMPORTE.Text);
            //decimal antimporte = Convert.ToDecimal(Session["IMPORTE_MOV"].ToString());
            //decimal saldod = Convert.ToDecimal((LBLSALDOC.Text.Substring(3)));
            //decimal saldoc = Convert.ToDecimal((LBLSALDOD.Text.Substring(3)));
            //saldod = saldod + nvoimporte - antimporte;
            //saldoc = saldoc + nvoimporte - antimporte;
            //OBJMOVS.saldoc = Convert.ToDecimal(saldoc);
            //OBJMOVS.saldod = Convert.ToDecimal(saldoc);
            //OBJMOVS.saldo = Convert.ToDecimal(saldoc);

            ///*-----------------------------------------------------*/
            //OBJMOVS.operacion = txtOPE.Text;
            //OBJMOVS.descripcion = txtDESC.Text;
            //OBJMOVS.id_cliente = TXTid_cliente.Text;
            //OBJMOVS.observacion = txtObservacione.Text;
            //string empre = Session["ID_EMPRESA"].ToString();
            //string res = OBJVENTA.NACTUALIZARMOV(OBJMOVS, "4", empre);

            //if (res == "ok")
            //{
            //    Response.Write("<script>alert('Datos Actualizados correctamente..')</script>");
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            //    llenar_datos("1", empre, Session["ID_CUENTA_MOV"].ToString());
            //    LIMPIAR();

            //    /*---------------------------------------------------------------------------*/
            //    llenar_labels_cabecera();
            //    LIMPIAR();
            //    btnActualizar.Enabled = false;
            //    btnCancelar.Enabled = false;
            //    btnNuevo.Enabled = true;
            //    btnRegistrar.Enabled = false;
                
            //    TXTid_cliente.Text = string.Empty;
            //    txtFECHA.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //    /*---------------------------------------------------------------------------*/
            //}
            //else
            //{
            //    Response.Write("<script>alert('Error datos no Actualizados')</script>");
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            //}
        }

        protected void btnREPORTE_Click(object sender, EventArgs e)
        {
            string ID_CUENTA_MOV = TXTprueba.Text;
            string FECHA_INI = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy"); 
            string FECHA_FIN = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
            string OPE = txtConsultaOpe.Text;
            string CONBANC = cboFiltroConc.SelectedValue;
            string ID_CLIENTE = "";
            if (txtConsultaCli.Text == "") { ID_CLIENTE = ""; } else { ID_CLIENTE = txtConsultaCliValor.Text; }
            object[] args = new object[] { ID_CUENTA_MOV, FECHA_INI, FECHA_FIN, OPE, CONBANC, ID_CLIENTE };
            String url = String.Format("REPORTES/FROM_REPORTE_MOVIMIENTOS.aspx?ID_CUENTA_MOV={0}&FECHA_INI={1}&FECHA_FIN={2}&OPE={3}&CONBANC={4}&ID_CLIENTE={5}", args);
            // Response.Redirect(url);
            string s = "window.open('" + url + "', 'popup_window', 'width=700,height=800,left=10%,top=10%,resizable=yes');"; //con esto muestro la venta en una nueva ventana 
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            
        }

        protected void txtIMPORTE_TextChanged(object sender, EventArgs e)
        {
            decimal deci = Convert.ToDecimal(txtIMPORTE.Text);
            txtIMPORTE.Text = String.Format("{0:0,0.00}", deci);
        }

        protected void btnSALDOS_Click(object sender, EventArgs e)
        {
            string codcta = Session["ID_CUENTA_MOV"].ToString();
            
            string res = OBJVENTA.NRECALCULARSALDOS(codcta);

            if (res == "ok")
            {
                Response.Write("<script>alert('Saldos Actualizados!!')</script>");
                llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
            }
        }

        protected void reportePDF_Click(object sender, ImageClickEventArgs e)
        {
            string ID_CUENTA_MOV = TXTprueba.Text;
            string FECHA_INI = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy");
            string FECHA_FIN = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
            string OPE = txtConsultaOpe.Text;
            string CONBANC = cboFiltroConc.SelectedValue;
            string ID_CLIENTE = "";
            if (txtConsultaCli.Text == "") { ID_CLIENTE = ""; } else { ID_CLIENTE = txtConsultaCliValor.Text; }
            object[] args = new object[] { ID_CUENTA_MOV, FECHA_INI, FECHA_FIN, OPE, CONBANC, ID_CLIENTE };
            String url = String.Format("REPORTES/FROM_REPORTE_MOVIMIENTOS.aspx?ID_CUENTA_MOV={0}&FECHA_INI={1}&FECHA_FIN={2}&OPE={3}&CONBANC={4}&ID_CLIENTE={5}", args);
            // Response.Redirect(url);
            string s = "window.open('" + url + "', 'popup_window', 'width=700,height=800,left=10%,top=10%,resizable=yes');"; //con esto muestro la venta en una nueva ventana 
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        protected void reporteEXCEL_Click(object sender, ImageClickEventArgs e)
        {
            string fechini = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy");
            string fechfin = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
            string ID_CLIENTE2 = "";
            if (txtConsultaCli.Text == "") { ID_CLIENTE2 = ""; } else { ID_CLIENTE2 = txtConsultaCliValor.Text; }
            DataTable tab = (DataTable)OBJVENTA.NFILTRARGRILLAMOVIMIENTOS(Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString(), fechini, fechfin, txtConsultaOpe.Text, cboFiltroConc.SelectedValue.ToString(), ID_CLIENTE2.ToString());
            System.Web.UI.WebControls.GridView dg = new System.Web.UI.WebControls.GridView();
            dg.DataSource = tab;
            dg.DataBind();

            EXPORTAR_EXCEL(dg);
        }

        protected void dgvMOVIMIENTOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", string.Format("ChangeRowColor('{0}','{1}');", e.Row.ClientID, e.Row.RowIndex));
               
            }

           

        }

        protected void dgvPOPUPAMARRE_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
        }

        void cargar_grilla_popup(string ID_MOV, string ID_VENTA, string OBS, string COND, string FECHAV, string FECHAF, string CODDBC)
        {
            try
            {
                DataTable ds = OBJVENTA.NTABLATEMPORALDATA(Session["CodigoMOVAMARRE"].ToString());
                dgvPOPUPAMARRE.DataSource = OBJVENTA.NLLENARGRILLAPOPUP(ID_MOV, ID_VENTA, OBS, COND, FECHAV, FECHAF, CODDBC);
                dgvPOPUPAMARRE.DataBind();

                for (int i = 0; i < dgvPOPUPAMARRE.Rows.Count; i++)
                {
                    for (int e = 0; e < ds.Rows.Count; e++)
                    {
                        if (dgvPOPUPAMARRE.Rows[i].Cells[1].Text == ds.Rows[e][1].ToString())
                        {
                            dgvPOPUPAMARRE.Rows[i].Visible = false;
                        }
                    }


                }
            }
            catch { }
        }

        void prueba_carga(string cod)
        {

            DataTable dsf = OBJVENTA.Npruebacarga(cod);
            if (dsf.Rows.Count > 0) { lbltieneamarre.Visible = false; }
            else { lbltieneamarre.Visible = true; }
            dgvPOPUP_AMARRADOS.DataSource = OBJVENTA.Npruebacarga(cod);
            dgvPOPUP_AMARRADOS.DataBind();
            
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            mp1.Show();
        }


        protected void dgvPOPUP_AMARRADOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DESUNIR")
            {
                GridViewRow rew = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string COD_VENT = rew.Cells[1].Text;
                string COD_MOVI = Session["CodigoMOVAMARRE"].ToString();
                string condi = "4";
                string resp = "";
                try
                {
                    resp = OBJVENTA.NELIMINARVENTAMOVIMIENTO(COD_MOVI, COD_VENT, "", condi, "01-01-2017", "01-01-2017", "");
                }
                catch
                {

                }

                if (resp == "ok")
                {
                    lbltieneamarre.Visible = false;
                    string codv = cbomTipoDoc.Text;
                    string fechai = "";
                    string fechaf = "";
                    string cod_cli_dbco = "";
                    cod_cli_dbco = txtClienteDBCom.Text;
                    fechaf = Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString();
                    fechai = Convert.ToDateTime(TextBoxssss.Text).ToShortDateString();
                    cargar_grilla_popup("", codv, "", "1", fechai, fechaf, cod_cli_dbco);

                    
                    prueba_carga(COD_MOVI);
                    
                    mp1.Show();

                }
                else { Response.Write("<script>alert('NO EXISTE UN AMARRE CON ESTA VENTA')</script>"); }
                
            }
        }


        protected void dgvPOPUPAMARRE_RowCommand(object sender, GridViewCommandEventArgs e)
       {
            if (e.CommandName == "UNIR")
            {
                GridViewRow raw = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string COD_VENT = raw.Cells[1].Text;
                string COD_MOVI = Session["CodigoMOVAMARRE"].ToString();
                
                
                string resp = OBJVENTA.NAMARRARVENTAMOVIMIENTO(COD_MOVI, COD_VENT, "", "3", "01-01-2017", "01-01-2017", "");

                if (resp == "ok")
                {
                    lbltieneamarre.Visible = false;
                    string codv = cbomTipoDoc.Text;
                    string fechai = "";
                    string fechaf = "";
                    string cod_cli_dbco = "";
                    cod_cli_dbco = txtClienteDBCom.Text;
                    fechaf = Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString();
                    fechai = Convert.ToDateTime(TextBoxssss.Text).ToShortDateString();
                    cargar_grilla_popup("", codv, "", "1", fechai, fechaf, cod_cli_dbco);
                    prueba_carga(COD_MOVI);

                    mp1.Show();


                }
                else
                {
                    Response.Write("<script>alert('HUBO UN ERROR NO SE PUEDO AMARRAR EL REGISTRO, INTENTE DE NUEVO')</script>");
                    mp1.Show();
                }
            }
            

        }

        protected void btnBuscarPopUp_Click(object sender, EventArgs e)
        {
            string codv = cbomTipoDoc.Text;
            string fechai = "";
            string fechaf = "";
            string cod_cli_dbco = "";
            cod_cli_dbco = txtClienteDBCom.Text;
            fechaf = Convert.ToDateTime(txtFechaFinDB.Text).ToShortDateString();
            fechai = Convert.ToDateTime(TextBoxssss.Text).ToShortDateString();
            cargar_grilla_popup("",codv,"","1",fechai,fechaf, cod_cli_dbco);
            //Session["CodigoMOVAMARRE"] = "";
            mp1.Show();
        }

        protected void btnSalirPopUp_Click(object sender, EventArgs e)
        {
            llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
        }


        protected void cboTIPOMOV_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCONCEPTO.Items.Clear();
            if (cboTIPOMOV.SelectedValue == "EGRESO") {
                DataTable dt = OBJVENTA.CONSULTA_LISTA_CONCEPTOS();

                cboCONCEPTO.DataSource = dt;
                cboCONCEPTO.DataValueField = "ID_CONCEPTOS_BANCARIOS";
                cboCONCEPTO.DataTextField = "DESCRIPCION";
                cboCONCEPTO.DataBind();

                complemento.Text = "PROVEEDOR:";
                txtCLIENTE.Enabled = false;

                cboTIPOMOV.Focus();
            }else if(cboTIPOMOV.SelectedValue == "INGRESO")
            {
                DataTable dt = OBJVENTA.CONSULTA_LISTA_CONCEPTOS2();
                
                cboCONCEPTO.DataSource = dt;
                cboCONCEPTO.DataValueField = "ID_CONCEPTOS_BANCARIOS";
                cboCONCEPTO.DataTextField = "DESCRIPCION";
                cboCONCEPTO.DataBind();

                complemento.Text = "CLIENTE:";
                txtCLIENTE.Enabled = true;

                cboTIPOMOV.Focus();

            }
        }

        protected void cboCONCEPTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboCONCEPTO.SelectedValue == "1003")
            {
                
                cboCONCEPTO.Focus();
            }
            else if (cboCONCEPTO.SelectedValue != "1003")
            {
                
                cboCONCEPTO.Focus();
            }
                
        }

        protected void dgvMOVIMIENTOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMOVIMIENTOS.PageIndex = e.NewPageIndex;

            if (Session["ESTADO_CONSULTA"].ToString() == "")
            {
                llenar_datos("1", Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString());
            }
            else if(Session["ESTADO_CONSULTA"].ToString() == "1")
            {
                string fechini = Convert.ToDateTime(txtFechaIni.Text).ToString("dd-MM-yyyy");
                string fechfin = Convert.ToDateTime(txtFechaFin.Text).ToString("dd-MM-yyyy");
                string ID_CLIENTE2 = "";
                if (txtConsultaCli.Text == "") { ID_CLIENTE2 = ""; } else { ID_CLIENTE2 = txtConsultaCliValor.Text; }
                filtrar_grilla(Session["ID_EMPRESA"].ToString(), Session["ID_CUENTA_MOV"].ToString(), fechini, fechfin, txtConsultaOpe.Text.Trim(), cboFiltroConc.SelectedValue.ToString(), ID_CLIENTE2.ToString());
            }

            

        }

        protected void dgvMOVIMIENTOS_DataBound(object sender, EventArgs e)
        {
         
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            LLENAR_TABLA_SESSION();
        }

        void LLENAR_TABLA_SESSION()
        {
            DataTable dt = (DataTable)Session["GRILLA_DOCS"];

            try
            {
                DataRow row = dt.NewRow();
                row["DOC"] = cboFVBV.SelectedValue;
                row["SERIE"] = txtserie.Text; 
                row["NUMERO"] = txtnumero.Text;

                dt.Rows.Add(row);
                dt.AcceptChanges();

                LLENAR_GRILLA();

                //aqui limpio la data de ingreso de precio y cantidad de cada bien
                txtserie.Text = string.Empty; ;
                txtnumero.Text = string.Empty;
                cboFVBV.SelectedIndex = 0;
                cboFVBV.Focus();
            }
            catch (Exception)
            {

                // Response.Write("<script>window.alert('EL BIEN YA ESTA EN LA LISTA');</script>");

            }
        }

        void LLENAR_GRILLA()
        {
            DataTable dt = (DataTable)Session["GRILLA_DOCS"];
            dgvDATOS.DataSource = dt;
            dgvDATOS.DataBind();
        }
    }
}