using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.IO;


using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;

namespace DIONYS_ERP
{
    public partial class FRM_MOVIMIENTO_CAJA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //CON ESTO PUEDO VERIFICO SI TENGO UNA CAJA ABIERTA  Y SINO ES ASI, ABRIR UNA CAJA NUEVA
                if (Session["ID_CAJA"].ToString() == string.Empty)
                {
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }

                FILTRAR_CAJA_KARDEX(0, "1", "");
                ESTADO_TRANSACCION(1);
                LLENAR_COMBO_TIPOMOV_PAGO();
                SELECCIONAR_REGISTRO_CARGADATA(); //AQUI CARGO POR PRIMERA VEZ TODOS LOS CAMPOS SELECIONADOS DE LA GRILLA
                ESTADO_TEXBOX_VENTA(2); //PARA PONER EN ESTADO DE BLOQUEADO A LOS TEXBOX DE LA VENTA
            }    
        }


        #region OBJETOS
        N_VENTA N_OBJVENTAS = new N_VENTA();
        N_LOGUEO N_OBJEMPRESA = new N_LOGUEO();
        E_CAJA_KARDEX E_OBJCAJA_KARDEX = new E_CAJA_KARDEX();

        #endregion

        #region FUNCIONES


        private bool VALIDAR_DATOS_CAJA_KARDEX()
        {

            bool RESULTADO=false;
            //ESTE SEGMENTO HAY QUE MODIFICAR POSTERIORMENTE EL COMBO DE ID_TIPO_MOV  
            try
            {
                            if (cboTIPO_MOV.SelectedIndex != -1)
                            {
                                if(cboTIPO_PAGO.SelectedItem.Text != string.Empty)
                                {
                                    if(txtMONTO.Text!=string.Empty)
                                    {
                                        if(rdbMONEDA.SelectedIndex != -1)
                                        {
                                            if (txtDESCRIPCION.Text != string.Empty)
                                            {
                                                if (cboTIPO_MOV.SelectedValue == "IPV" || cboTIPO_MOV.SelectedValue == "EPC")
                                                {
                                                    if(txtID_DOC.Text != string.Empty)
                                                    {
                                                        if(txtSALDO.Text != string.Empty)
                                                        {
                                                            RESULTADO = true;
                                                        }
                                                        else
                                                        {
                                                            RESULTADO = false;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        RESULTADO = false;
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    RESULTADO = true;
                                                }
                                            }
                                            else
                                            {
                                                RESULTADO = false;
                                            }
                                            
                                        }
                                        else
                                        {
                                            RESULTADO = false;
                                        }
                                    }
                                    else
                                    {
                                        RESULTADO = false;
                                    }
                                }else{
                                    RESULTADO=false;
                                }
                            }
                            else
                            {
                                RESULTADO = false;
                            }
            }
            catch (Exception)
            {

                Response.Write("<script>window.alert('LOS DATOS ESTAN INCOMPLETOS');</script>");
            }
            

            return RESULTADO;
        }
        

        #endregion

        #region PROCEDIMIENTOS
        void FILTRAR_CAJA_KARDEX(int OPCION, string VER, string DESCRIPCION)
        {
            double TOTAL_CAJA, TOTAL_SOLES, TOTAL_DOLARES;
            TOTAL_CAJA = 0.00;
            TOTAL_SOLES = 0.00;
            TOTAL_DOLARES = 0.00;

            DataTable dt = new DataTable();
            string ID_MOVIMIENTO=string.Empty;
            string ID_CAJA=Session["ID_CAJA"].ToString();
            string TIPO_PAGO = string.Empty;
            string ID_TIPOMOV=string.Empty;
            string OPCION_USUARIO = string.Empty;

            //if (Session["ID_PUNTOVENTA"].ToString() == "PV010" || Session["ID_PUNTOVENTA"].ToString() == "PV005")
            //{
            //    OPCION_USUARIO = "ADMINISTRADOR";
            //}
            //else
            //{
            //    OPCION_USUARIO = "CAJERO";
            //}
            dt = N_OBJVENTAS.FILTRAR_CAJA_KARDEX(ID_MOVIMIENTO, ID_CAJA,DESCRIPCION,TIPO_PAGO, ID_TIPOMOV, OPCION, VER);
            dgvMOV_CAJAKARDEX.DataSource = dt;
            dgvMOV_CAJAKARDEX.DataBind();




            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TOTAL_CAJA = TOTAL_CAJA + Convert.ToDouble(dt.Rows[i]["IMPORTE_CAJA"]);
                if (dt.Rows[i]["MONEDA"].ToString() == "S")
                {
                    if (dt.Rows[i]["ID_TIPOMOV"].ToString().Substring(0, 1) == "I")
                    {
                        TOTAL_SOLES = TOTAL_SOLES + Convert.ToDouble(dt.Rows[i]["IMPORTE"]);
                    }
                    else
                    {
                        TOTAL_SOLES = TOTAL_SOLES - Convert.ToDouble(dt.Rows[i]["IMPORTE"]);
                    }


                }
                else
                {
                    if (dt.Rows[i]["ID_TIPOMOV"].ToString().Substring(0, 1) == "I")
                    {
                        TOTAL_DOLARES = TOTAL_DOLARES + Convert.ToDouble(dt.Rows[i]["IMPORTE"]);
                    }
                    else
                    {

                        TOTAL_DOLARES = TOTAL_DOLARES - Convert.ToDouble(dt.Rows[i]["IMPORTE"]);
                    }
                }
            }

            txtTOTALSOLES.Text = TOTAL_SOLES.ToString("N2");
            txtTOTALDOLARES.Text = TOTAL_DOLARES.ToString("N2");
            txtTOTALCAJA.Text = TOTAL_CAJA.ToString("N2");

        }


        void LLENAR_COMBO_TIPOMOV_PAGO()
        {
            cboTIPO_MOV.DataSource = N_OBJVENTAS.LISTAR_TIPO_MOVIMIENTO();
            cboTIPO_MOV.DataValueField = "ID_TIPOMOV";
            cboTIPO_MOV.DataTextField = "DESCRIPCION";
            cboTIPO_MOV.DataBind();



            cboTIPO_PAGO.DataSource = N_OBJVENTAS.LISTAR_TIPO_PAGO();
            cboTIPO_PAGO.DataValueField = "ID_TIPOPAGO";
            cboTIPO_PAGO.DataTextField = "DESCRIPCION";
            cboTIPO_PAGO.DataBind();
        }



        void ESTADO_TRANSACCION(int ESTADO)
        {
            if (ESTADO == 1) //ESTADO CONSULTA
            {
                txtID_MOVIMIENTO.ReadOnly = true;
                txtFECHA.ReadOnly = true;
                txtFECHA_ANULADO.ReadOnly = true;
                cboTIPO_MOV.Enabled = false;
                cboTIPO_PAGO.Enabled = false;
                txtMONTO.ReadOnly = true;
                rdbMONEDA.Enabled = false;
                txtDESCRIPCION.ReadOnly = true;
                btnNUEVO.Enabled = true;
                btnGRABAR.Enabled = false;
                btnCANCELAR.Enabled = false;
                btnANULAR.Enabled = true;
                btnIMPRIMIR.Enabled = true;
                cboTIPO_BUSQUEDA.Enabled = true;
                txtDATA_BUSQUEDA.ReadOnly = true;
                rdbLISTAOPCIONES.Enabled = true;
                dgvMOV_CAJAKARDEX.Enabled = true;
                btnBUSCAR.Enabled = true;
                txtDATA_BUSQUEDA.ReadOnly = false;

            }
            if (ESTADO == 2) //ESTADO NUEVO
            {
                //LIMPIARDO CONTROLES
                txtID_MOVIMIENTO.Text = string.Empty;
                txtFECHA.Text = DateTime.Now.ToShortDateString();
                txtFECHA_ANULADO.Text = string.Empty;
                cboTIPO_MOV.SelectedIndex = 0;
                cboTIPO_PAGO.SelectedIndex = 0;
                txtMONTO.Text = string.Empty;
                txtDESCRIPCION.Text = string.Empty;
                rdbMONEDA.SelectedIndex = 0;
                    
                //===================

                txtID_MOVIMIENTO.ReadOnly = true;
                txtFECHA.ReadOnly = true;
                txtFECHA_ANULADO.ReadOnly = true;
                cboTIPO_MOV.Enabled = true;
                cboTIPO_PAGO.Enabled = true;
                txtMONTO.ReadOnly = false;
                rdbMONEDA.Enabled = true;
                txtDESCRIPCION.ReadOnly = false;
                btnNUEVO.Enabled = false;
                btnGRABAR.Enabled = true;
                btnCANCELAR.Enabled = true;
                btnANULAR.Enabled = false;
                btnIMPRIMIR.Enabled = false;
                cboTIPO_BUSQUEDA.Enabled = false;
                txtDATA_BUSQUEDA.ReadOnly = false;
                rdbLISTAOPCIONES.Enabled = false;
                dgvMOV_CAJAKARDEX.Enabled = false;
                btnBUSCAR.Enabled = false;
                txtDATA_BUSQUEDA.ReadOnly = true;
            }
        }

        public void ANULAR_CAJA_KARDEX_REGISTRO()
        {
            E_OBJCAJA_KARDEX.ID_MOVIMIENTO = dgvMOV_CAJAKARDEX.SelectedRow.Cells[1].Text;
            E_OBJCAJA_KARDEX.DESCRIPCION = string.Empty;
            E_OBJCAJA_KARDEX.ID_COMPVENT = string.Empty;
            E_OBJCAJA_KARDEX.ID_TIPOMOV = string.Empty;
            E_OBJCAJA_KARDEX.ID_TIPOPAGO = string.Empty;
            E_OBJCAJA_KARDEX.IMPORTE = 0.00;
            E_OBJCAJA_KARDEX.MONEDA = string.Empty;
            E_OBJCAJA_KARDEX.TIPO_CAMBIO = 0.00;
            E_OBJCAJA_KARDEX.AMORTIZADO=0.00;
            E_OBJCAJA_KARDEX.ID_CAJA = string.Empty;
            E_OBJCAJA_KARDEX.IMPORTE_CAJA = 0.00;
            E_OBJCAJA_KARDEX.OPCION = 2; //ESTA OPCION 2 ANULA AMORTIZACION

            N_OBJVENTAS.CAJA_KARDEX_MANTENIMIENTO(E_OBJCAJA_KARDEX);
        }
        
        private void GRABAR_CAJA_KARDEX()
        {
            try
            {
                E_OBJCAJA_KARDEX.ID_MOVIMIENTO = string.Empty;
                E_OBJCAJA_KARDEX.DESCRIPCION = txtDESCRIPCION.Text.ToString();
                if (txtID_DOC.Text != string.Empty)
                {
                    E_OBJCAJA_KARDEX.ID_COMPVENT = txtID_DOC.Text;
                }
                else
                {
                    E_OBJCAJA_KARDEX.ID_COMPVENT = string.Empty;
                }
                
                E_OBJCAJA_KARDEX.ID_TIPOMOV = cboTIPO_MOV.SelectedValue.ToString();
                E_OBJCAJA_KARDEX.ID_TIPOPAGO = cboTIPO_PAGO.SelectedValue.ToString();

                E_OBJCAJA_KARDEX.IMPORTE = Convert.ToDouble(txtMONTO.Text.ToString());

                if (rdbMONEDA.SelectedIndex == 0)
                {
                    E_OBJCAJA_KARDEX.MONEDA = "S";
                }
                else
                {
                    if (rdbMONEDA.SelectedIndex == 1)
                    {
                        E_OBJCAJA_KARDEX.MONEDA = "D";
                    }
                }
                E_OBJCAJA_KARDEX.TIPO_CAMBIO = Convert.ToDouble(Session["TIPO_CAMBIO"].ToString());

                if (txtMONEDA.Text == "S" && rdbMONEDA.SelectedValue == "SOLES")
                {
                    E_OBJCAJA_KARDEX.AMORTIZADO = Convert.ToDouble(txtMONTO.Text.ToString());
                }
                if (txtMONEDA.Text == "S" && rdbMONEDA.SelectedValue == "DOLARES")
                {
                    E_OBJCAJA_KARDEX.AMORTIZADO =Convert.ToDouble(txtMONTO.Text.ToString()) * Convert.ToDouble(Session["TIPO_CAMBIO"].ToString());
                }
                if (txtMONEDA.Text == "D" && rdbMONEDA.SelectedValue == "SOLES")
                {
                    E_OBJCAJA_KARDEX.AMORTIZADO = Convert.ToDouble(txtMONTO.Text.ToString()) / Convert.ToDouble(Session["TIPO_CAMBIO"].ToString());
                }
                if (txtMONEDA.Text == "D" && rdbMONEDA.SelectedValue == "DOLARES")
                {
                    E_OBJCAJA_KARDEX.AMORTIZADO = Convert.ToDouble(txtMONTO.Text.ToString());
                }
                
                
                E_OBJCAJA_KARDEX.ID_CAJA = Session["ID_CAJA"].ToString();

                string var = cboTIPO_MOV.SelectedValue.Substring(0, 1);
                if (var == "I") //es un ingreso
                {

                    if (rdbMONEDA.SelectedIndex == 0) // esta en soles
                    {
                        E_OBJCAJA_KARDEX.IMPORTE_CAJA = Convert.ToDouble(txtMONTO.Text.ToString());
                    }
                    else //sino es dolares y mi importe caja siempre es soles
                    {
                        E_OBJCAJA_KARDEX.IMPORTE_CAJA = Math.Round(Convert.ToDouble(txtMONTO.Text.ToString()) * Convert.ToDouble(Session["TIPO_CAMBIO"].ToString()), 2);
                    }
                }
                else //entonces es un egreso y registro mi importe caja en negativo
                {
                    if (rdbMONEDA.SelectedIndex == 0) // esta en soles
                    {
                        E_OBJCAJA_KARDEX.IMPORTE_CAJA = (-1) * Convert.ToDouble(txtMONTO.Text.ToString());
                    }
                    else //sino es dolares y mi importe caja siempre es soles
                    {
                        E_OBJCAJA_KARDEX.IMPORTE_CAJA = (-1) * Math.Round(Convert.ToDouble(txtMONTO.Text.ToString()) * Convert.ToDouble(Session["TIPO_CAMBIO"].ToString()), 2);
                    }
                }

                E_OBJCAJA_KARDEX.OPCION = 1; //ESTA OPCION 1 INSERTA EL NUEVO REGISTRO

                N_OBJVENTAS.CAJA_KARDEX_MANTENIMIENTO(E_OBJCAJA_KARDEX);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        #endregion

        protected void btnNUEVO_Click(object sender, EventArgs e)
        {
            ESTADO_TRANSACCION(2); //CON ESTO CONTROLAMOS LA ACTIVIDAD O INACTIVIDAD DE LOS CONTROLES
            dgvMOV_CAJAKARDEX.SelectedIndex = -1; //ESTO NO SELECCIONA NINGUN REGISTRO
            LLENAR_COMBO_TIPOMOV_PAGO();
            ESTADO_TEXBOX_VENTA(1);
        }

        protected void btnCANCELAR_Click(object sender, EventArgs e)
        {
            ESTADO_TRANSACCION(1); //CON ESTO CONTROLAMOS LA ACTIVIDAD O INACTIVIDAD DE LOS CONTROLES
            FILTRAR_CAJA_KARDEX(0, "1", ""); //AQUI ACTUALIZO Y AGO QUE EL FILTRO SEA POR TODOS LO ACTIVOS 
            rdbLISTAOPCIONES.SelectedIndex = 0; 
            txtDATA_BUSQUEDA.Text = string.Empty; //LIMPIAR EL CAMPO DE BUSQUEDA
            dgvMOV_CAJAKARDEX.SelectedIndex = 0; //SELECCIONA EL PPRIMER REGISTRO
            SELECCIONAR_REGISTRO_CARGADATA(); //AQUI CARGO POR PRIMERA VEZ TODOS LOS CAMPOS SELECIONADOS DE LA GRILLA
            ESTADO_TEXBOX_VENTA(2);
        }

        protected void btnGRABAR_Click(object sender, EventArgs e)
        {
            if(VALIDAR_DATOS_CAJA_KARDEX())
            {
                GRABAR_CAJA_KARDEX();
                ESTADO_TRANSACCION(1);//CON ESTO CONTROLAMOS LA ACTIVIDAD O INACTIVIDAD DE LOS CONTROLES
                FILTRAR_CAJA_KARDEX(0,"1",""); //AQUI ACTUALIZO Y AGO QUE EL FILTRO SEA POR TODOS LO ACTIVOS 
                rdbLISTAOPCIONES.SelectedIndex = 0;
                txtDATA_BUSQUEDA.Text = string.Empty; //LIMPIAR EL CAMPO DE BUSQUEDA
                dgvMOV_CAJAKARDEX.SelectedIndex = 0; //SELECCIONA EL PPRIMER REGISTRO
                SELECCIONAR_REGISTRO_CARGADATA(); //AQUI CARGO POR PRIMERA VEZ TODOS LOS CAMPOS SELECIONADOS DE LA GRILLA

            }
            else
            {
                Response.Write("<script>window.alert('ERROR, FALTAN DATOS NECESARIOS POR INGRESAR');</script>"); 
            }
            ESTADO_TEXBOX_VENTA(2);
        }

        protected void btnIMPRIMIR_Click(object sender, EventArgs e)
        {

            if (dgvMOV_CAJAKARDEX.Rows.Count != 0 && rdbLISTAOPCIONES.SelectedIndex == 0  ) //AQUI VALIDO QUE EXISTAN DATOS EN MI GRIDVIEW PARA PODER IMPRIIMIR MIS DATOS
            {
                
                if(rdbOPCION_IMPRESION.SelectedIndex==0) //AQUI GENERO EL ARCHIVO PDF PARA SU IMPRESION
                {
                    string IDMOV = txtID_MOVIMIENTO.Text;
                    string IDEMP = Session["ID_EMPRESA"].ToString();
                    String url = String.Format("REPORTES/FRM_REPORTE_RECIBO_EGRESO_INGRESO.aspx?IDMOV={0}&IDEMP={1}",IDMOV,IDEMP);
                    //Response.Redirect(url);

                    string s = "window.open('" + url + "', 'popup_window', 'width=700,height=400,left=10%,top=10%,resizable=yes');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else //SINO GENERO LA IMPRESION DE LOS TICKET BOLETA
                {
                    IMPRIMIR_SPOOL(); //imprimo mi spool en mi impresora etiquetera
                }
            } 
        }

        //funcion para abrir un pfd generado
        public static void AgregarPrintScript(string Original, string Copia)
        {
            PdfReader reader = new PdfReader(Original);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(Copia, FileMode.Create));
            AcroFields fields = stamper.AcroFields;
            stamper.JavaScript = "this.print(true);\r";
            stamper.FormFlattening = true;
            stamper.Close();
            reader.Close();
        }
        //==============================================


        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            if (rdbLISTAOPCIONES.SelectedIndex == 0)
            {
                FILTRAR_CAJA_KARDEX(cboTIPO_BUSQUEDA.SelectedIndex, "1",txtDATA_BUSQUEDA.Text.ToString());
                dgvMOV_CAJAKARDEX.SelectedIndex = -1; //DESELECCIONO LA FILA SELECIONADA DEL GRIDVIEW
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 1)
            {
                FILTRAR_CAJA_KARDEX(cboTIPO_BUSQUEDA.SelectedIndex, "2", txtDATA_BUSQUEDA.Text.ToString());
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 2)
            {
                FILTRAR_CAJA_KARDEX(cboTIPO_BUSQUEDA.SelectedIndex, "3", txtDATA_BUSQUEDA.Text.ToString());
            }
        }

        protected void rdbLISTAOPCIONES_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbLISTAOPCIONES.SelectedIndex == 0)
            {
                FILTRAR_CAJA_KARDEX(0,"1","");
                SELECCIONAR_REGISTRO_CARGADATA();
                btnANULAR.Enabled = true;
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 1)
            {
                FILTRAR_CAJA_KARDEX(0,"2","");
                SELECCIONAR_REGISTRO_CARGADATA();
                btnANULAR.Enabled = false;
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 2)
            {
                FILTRAR_CAJA_KARDEX(0,"3","");
                SELECCIONAR_REGISTRO_CARGADATA();
                btnANULAR.Enabled = true;
            }
        }

        protected void cboTIPO_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SELECCIONAR_REGISTRO_CARGADATA()
        {
            try
            {
                 //========================================================================
                if (dgvMOV_CAJAKARDEX.Rows.Count != 0)
                {


                    txtID_MOVIMIENTO.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[1].Text;
                    txtFECHA.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[2].Text;
                    if (dgvMOV_CAJAKARDEX.SelectedRow.Cells[19].Text != "&nbsp;")
                    {
                        txtFECHA_ANULADO.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[19].Text;
                    }
                    else
                    {
                        txtFECHA_ANULADO.Text = string.Empty;
                    }
                    cboTIPO_PAGO.SelectedItem.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[4].Text;
                    cboTIPO_MOV.SelectedItem.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[5].Text;
                    if (dgvMOV_CAJAKARDEX.SelectedRow.Cells[6].Text == "S")
                    {
                        rdbMONEDA.SelectedIndex = 0;
                    }
                    if (dgvMOV_CAJAKARDEX.SelectedRow.Cells[6].Text == "D")
                    {
                        rdbMONEDA.SelectedIndex = 1;
                    }

                    txtMONTO.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[7].Text;
                    txtDESCRIPCION.Text = dgvMOV_CAJAKARDEX.SelectedRow.Cells[3].Text;
                }else
                {
                    txtID_MOVIMIENTO.Text = string.Empty;
                    txtFECHA.Text = string.Empty;
                    txtFECHA_ANULADO.Text = string.Empty;
                    cboTIPO_MOV.SelectedIndex = 0;
                    cboTIPO_PAGO.SelectedIndex = 0;
                    txtMONTO.Text = string.Empty;
                    rdbMONEDA.SelectedIndex = 0;
                    txtDESCRIPCION.Text = string.Empty;
                }
                //========================================================================
            }
            catch (Exception)
            {

                Response.Write("<script>window.alert('ERROR VERIFIQUE SUS DATOS');</script>");
            }
               
        }

        protected void dgvMOV_CAJAKARDEX_SelectedIndexChanged(object sender, EventArgs e)
        {
            SELECCIONAR_REGISTRO_CARGADATA();
        }

        protected void btnANULAR_Click(object sender, EventArgs e)
        {
            if (txtCODANULACION.Text.ToString() == "CODDIONYS2017")
            {
                ANULAR_CAJA_KARDEX_REGISTRO();
                FILTRAR_CAJA_KARDEX(0, "2", ""); //FILTRO TODOS Y POR ANULADOS
                rdbLISTAOPCIONES.SelectedIndex = 1;
                dgvMOV_CAJAKARDEX.SelectedIndex = 0; //SELECCIONA EL PPRIMER REGISTRO
                SELECCIONAR_REGISTRO_CARGADATA(); //AQUI CARGO POR PRIMERA VEZ TODOS LOS CAMPOS SELECIONADOS DE LA GRILLA


                //CON ESTO ANULO LA VENTA DEL CONTROL DE GALERIA PARA MANTENER EL ORDEN - ANULAR EL PAGO Y DEJARLO EN PENDIENTE
                //if (Session["SEDE"].ToString() == "004")
                //{
                //    N_OBJVENTAS.ACTUALIZAR_MODIFICACIONES_CONTROL_GALERIA(txtID_DOC.Text, "2");
                //}
            }
            else
            {
                Response.Write("<script>window.alert('ERROR INGRESAR CLAVE DE AUTORIZACION');</script>"); 
            }
            

        }

        




        // IMPRESIONES SPOOL
        // =============================================================================================================================================== 
        public void IMPRIMIR_SPOOL()
        {
            DataTable DATOS_EMPRESA = new DataTable();
            DATOS_EMPRESA = N_OBJEMPRESA.CONSULTAR_VISTA_EMPRESA(Session["ID_EMPRESA"].ToString()); //AQUI CARGO LOS DATOS DE MI VISTA V_EMPRESA

            DataTable DATOS_SEDE = new DataTable();
            DATOS_SEDE = N_OBJEMPRESA.CONSULTAR_VISTA_SEDE(Session["SEDE"].ToString()); //AQUI CARGO LOS DATOS DE MI VISTA V_SEDE 

            DataTable DATOS_CAJA_KARDEX = new DataTable();                         //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI CAJA KARDEX
            string ID_MOVIMIENTO = txtID_MOVIMIENTO.Text;                          //ESTO PERMITE GENERAR LA VARIABLE DEL ID_MOVIMIENTO
            
            DATOS_CAJA_KARDEX =  N_OBJVENTAS.LISTA_REGISTRO_CAJA_KARDEX(ID_MOVIMIENTO);        //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE DE MI 
                                                                                               //CAJA_KARDEX PARA PODER ACCEDER A ELLO EN TODO MOMENTO
                                                                
            //LIMPIANDO MI SPOOL SI ESQUE UBIERA IMPRESIONES PENDIENTES
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "2");
            // ========================================================================================


            //AQUI ESTOY OBTENIENDO TODOS LOS DATOS DE LA EMPRESA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_EMPRESA.Rows[0]["DESCRIPCION"].ToString(), "1");      //aqui va el nombre de la empresa
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RUC: " + DATOS_EMPRESA.Rows[0]["RUC"].ToString(), "1");    //aqui va el ruc de la empresa
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "DIRECCION: " + DATOS_EMPRESA.Rows[0]["DIRECCION"].ToString(), "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_EMPRESA.Rows[0]["UBIDSN"].ToString() + "-" + DATOS_EMPRESA.Rows[0]["UBIPRN"].ToString() + "-" + DATOS_EMPRESA.Rows[0]["UBIDEN"].ToString(), "1"); //DISTRITO PROVINCIA Y DEPARTAMENTO
            //AQUI ESTOY OBTENIENDO TODOS LOS DATOS DE LA SEDE
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                                        //imprime una linea de guiones
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"SEDE: " + DATOS_SEDE.Rows[0]["ID_SEDE"].ToString()+" "+ DATOS_SEDE.Rows[0]["DESCRIPCION"].ToString(), "1"); //aqui va el codigo y el nombre de la sede de la empresa 
            //AQUI ESTOY OBTENIENDO TODOS LOS DATOS DEL PUNTO DE VENTA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"PV: " + Session["ID_PUNTOVENTA"].ToString() + " " + Session["PUNTOVENTA"].ToString(), "1");                 //aqui va el codigo y el nombre del punto de venta
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            //N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "MAQ REG : " + DATOS_VENTA.Rows[0][48].ToString(), "1");          //AQUI SE COLOCA EL NOMBRE DE LA MAQUINA REGISTRADORA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_CAJA_KARDEX.Rows[0]["FECHA"].ToString(), "1");   //aqui va la fecha
            
            // AQUI VA EL NOMBRE  DEL MOVIMIENTO DE LA VENTA O COMPRA
            string TIP_MOV;
            TIP_MOV = DATOS_CAJA_KARDEX.Rows[0]["TM_DESCRIPCION"].ToString();
            //AQUI ESTOY OBTENIENDO EL MOTIVO DE MOVIMIENTO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RECIBO: " + TIP_MOV , "1"); 
            //AQUI ESTOY OBTENIENDO EL ID_MOVIMIENTO Y EL IMPORTE TOTAL DEL MOVIMIENTO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"#MOV : " + DATOS_CAJA_KARDEX.Rows[0]["ID_MOVIMIENTO"].ToString(), "1");

            
            if (DATOS_CAJA_KARDEX.Rows[0]["MONEDA"].ToString()=="S")
            {
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "IMPORTE : " + " S/. "+ Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[0]["IMPORTE"]).ToString("N2"), "1"); 
            }else{
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "IMPORTE : " + " US$. " + Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[0]["IMPORTE"]).ToString("N2"), "1");
            }
            

            //AQUI ESTOY OBTENIENDO GUIONES PARA GENERAR UNA LINEA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");

            //AQUI ESTOY OBTENIENDO LA DESCRIPCION DEL MOVIMIENTO DE LA CAJA_KARDEX
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_CAJA_KARDEX.Rows[0]["DESCRIPCION"].ToString(), "1");

            //AQUI ESTOY OBTENIENDO GUIONES PARA GENERAR UNA LINEA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "USUARIO: " + DATOS_CAJA_KARDEX.Rows[0]["EMPLEADO"].ToString(), "1"); //obtenemos la descripcion del cajero o empleado
            
            //AQUI ESTOY OBTENIENDO GUIONES PARA GENERAR UNA LINEA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
           
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RECEPTOR: ", "1");

            //AQUI ESTOY OBTENIENDO GUIONES PARA GENERAR UNA LINEA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "NOMBRE: ____________________________", "1");

            //AQUI ESTOY OBTENIENDO GUIONES PARA GENERAR UNA LINEA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "DNI: ___________________________", "1");

            
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1");

           
           
        }

        protected void btnIMPRIMIR_REPORTCAJA_Click(object sender, EventArgs e)
        {
            if(rdbOPCION_IMPRESION.SelectedIndex==1)
            {
                IMPRIMIR_SPOOL_TODOS_MOVCAJA(); //AQUI IMPRIMIMOS TODO LOS MOVIMIENTOS EN UN SOLO REPORTE O IMPRESION EN LA IMPRESORA ETICKETERA
            }
            else
            {
                string ID_EMPRESA = Session["ID_EMPRESA"].ToString();

                switch (ID_EMPRESA)
                {
                    case "001": //LIQUIDACION DE LA EMPRESA RECREACIONES DIONYS
                                FUNCION_GENERAR_LIQUIDACION("REPORTES/FRM_REPORTE_LIQUIDACION.aspx?ID_EMPRESA={0}&ID_CAJA={1}",ID_EMPRESA,Session["ID_CAJA"].ToString());
                        break;
                    case "002": //LIQUIDACION DE LA EMPRESA HOTEL DIONYS
                        /* AUN NO ESTA EN MARCHA ESTA EMPRESA
                                FUNCION_GENERAR_LIQUIDACION("REPORTES/FRM_REPORTE_LIQUIDACION.aspx?ID_EMPRESA={0}&ID_CAJA={1}",ID_EMPRESA,ID_CAJA);
                        */
                    case "003": //LIQUIDACION DE LA EMPRESA COMPLEJO
                                FUNCION_GENERAR_LIQUIDACION("REPORTES/FRM_REPORTE_LIQUIDACION.aspx?ID_EMPRESA={0}&ID_CAJA={1}",ID_EMPRESA,Session["ID_CAJA"].ToString());
                        break;
                    case "004": //LIQUIDACION DE LA EMPRESA GALERIA

                                FUNCION_GENERAR_LIQUIDACION("REPORTES/FRM_REPORTE_LIQUIDACIONES_GALERIA.aspx?ID_EMPRESA={0}&ID_CAJA={1}",ID_EMPRESA,Session["ID_CAJA"].ToString());
                        break;
                    case "005": //LIQUIDACION DE LA EMPRESA COMPLEJO
                                FUNCION_GENERAR_LIQUIDACION("REPORTES/FRM_REPORTE_LIQUIDACION.aspx?ID_EMPRESA={0}&ID_CAJA={1}", ID_EMPRESA, Session["ID_CAJA"].ToString());
                        break;

                }
            }
            
        }

        void FUNCION_GENERAR_LIQUIDACION(string DIRECCION,string ID_EMPRESA, string ID_CAJA)
        {
            //string FECHA_INI = DateTime.Now.ToShortDateString();
            //string FECHA_FIN = DateTime.Now.ToShortDateString();

            String url = String.Format(DIRECCION, ID_EMPRESA, ID_CAJA);//,FECHA_INI,FECHA_FIN);
            //Response.Redirect(url);

            string s = "window.open('" + url + "', 'popup_window', 'width=700,height=400,left=10%,top=10%,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }


        void IMPRIMIR_SPOOL_TODOS_MOVCAJA()
        {
            DataTable DATOS_EMPRESA = new DataTable();
            DATOS_EMPRESA = N_OBJEMPRESA.CONSULTAR_VISTA_EMPRESA(Session["ID_EMPRESA"].ToString()); //AQUI CARGO LOS DATOS DE MI VISTA V_EMPRESA

            DataTable DATOS_SEDE = new DataTable();
            DATOS_SEDE = N_OBJEMPRESA.CONSULTAR_VISTA_SEDE(Session["SEDE"].ToString()); //AQUI CARGO LOS DATOS DE MI VISTA V_SEDE 

            DataTable DATOS_CAJA_KARDEX = new DataTable();                         //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI CAJA KARDEX
            string ID_CAJA = Session["ID_CAJA"].ToString();                          //ESTO PERMITE GENERAR LA VARIABLE DEL ID_MOVIMIENTO

            DATOS_CAJA_KARDEX = N_OBJVENTAS.CONSULTA_IMPRESION_CAJA_KARDEX(ID_CAJA);        //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE DE MI 
            //CAJA_KARDEX PARA PODER ACCEDER A ELLO EN TODO MOMENTO
                                                  
            //LIMPIANDO MI SPOOL SI ESQUE UBIERA IMPRESIONES PENDIENTES
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "2");
            // ========================================================================================


            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "---- REPORTE DE CAJA ----", "1");

            //AQUI ESTOY OBTENIENDO TODOS LOS DATOS DE LA EMPRESA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_EMPRESA.Rows[0]["DESCRIPCION"].ToString(), "1");      //aqui va el nombre de la empresa
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RUC: " + DATOS_EMPRESA.Rows[0]["RUC"].ToString(), "1");    //aqui va el ruc de la empresa
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "DIRECCION: " + DATOS_EMPRESA.Rows[0]["DIRECCION"].ToString(), "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_EMPRESA.Rows[0]["UBIDSN"].ToString() + "-" + DATOS_EMPRESA.Rows[0]["UBIPRN"].ToString() + "-" + DATOS_EMPRESA.Rows[0]["UBIDEN"].ToString(), "1"); //DISTRITO PROVINCIA Y DEPARTAMENTO
            //AQUI ESTOY OBTENIENDO TODOS LOS DATOS DE LA SEDE
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                                        //imprime una linea de guiones
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "SEDE: " + DATOS_SEDE.Rows[0]["ID_SEDE"].ToString() + " " + DATOS_SEDE.Rows[0]["DESCRIPCION"].ToString(), "1"); //aqui va el codigo y el nombre de la sede de la empresa 
            //AQUI ESTOY OBTENIENDO TODOS LOS DATOS DEL PUNTO DE VENTA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "PV: " + Session["ID_PUNTOVENTA"].ToString() + " " + Session["PUNTOVENTA"].ToString(), "1");                 //aqui va el codigo y el nombre del punto de venta
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "FECHA APERTURA: " + Convert.ToDateTime(DATOS_CAJA_KARDEX.Rows[0]["FECHA_INICIAL"]).ToString("dd/MM/yy"), "1");          //FECHA_INICIAL
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");

            string ANULADO = string.Empty;
            double TOTALANU = 0.00;
            int CONTANU = 0, CONTTOTAL = 0, IPV_CANT = 0, EVA_CANT = 0;
            int IVA_CANT = 0, EGE_CANT = 0;
            double TOTALMOV = 0.00;
            double IPV_EFECTIVO = 0.00, EVA_EFECTIVO = 0.00, IPV_EFECTIVO_OTROS=0.00;
            double IVA_EFECTIVO = 0.00, EGE_EFECTIVO = 0.00;

            //GENERAR LOS REGISTROS DE INGRESOS POR VENTA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "---DETALLE INGRESOS POR VENTA---", "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "FECHA   MOV DOC NUMERO   M   IMPORTE A", "1");
            for (int i = 0; i < DATOS_CAJA_KARDEX.Rows.Count; i++)
            {
                ANULADO = " ";
                string varMOVIMIENTO = DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOMOV"].ToString();
                if (DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] != DBNull.Value && varMOVIMIENTO.ToString() == "IPV")
                {
                    ANULADO = "*";
                    CONTANU = CONTANU + 1;
                    TOTALANU = TOTALANU + Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"]); //TOTALIZANDO LOS ANULADOS
                }
                               
                //==============================================================================================================
               //OBTENGO EL VALOR DE MI CAMPO ID_TIPOMOV PARA VERIFICAR SI TIENE DATO O NO , PARA REALIZAR LA COMPARACIONES
                 string varID_TIPOPAGO = DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOPAGO"].ToString();
                 if (varMOVIMIENTO == "IPV")
                {
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), Convert.ToDateTime(DATOS_CAJA_KARDEX.Rows[i]["FECHA"]).ToString("dd/MM/yy") + " " + DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOMOV"].ToString() + " " +
                        DATOS_CAJA_KARDEX.Rows[i]["TIPODOC"].ToString() + " " + DATOS_CAJA_KARDEX.Rows[i]["NUMERO"].ToString() + "  " +DATOS_CAJA_KARDEX.Rows[i]["MONEDA"].ToString() +" "+ DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"].ToString() + " " + ANULADO, "1");
                }
                
                //AQUI PROCESO LA INFORMACION PARA OBTENER LAS SUMAS DE LOS INGRESOS POR VENTA EN EFECTIVO Y QUE NO ESTEN ANULADOS
                 if (varMOVIMIENTO == "IPV" && varID_TIPOPAGO == "0001" && DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] == DBNull.Value)
                {
                    IPV_EFECTIVO += Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"]);
                    IPV_CANT += 1;
                }

                //AQUI PROCESO LA INFORMACION PARA OBTENER LAS SUMAS DE LOS INGRESOS POR VENTA CON TARJETA CREDITO O DEBITO Y QUE NO ESTEN ANULADOS
                 if (varMOVIMIENTO == "IPV" && (varID_TIPOPAGO == "0002" || varID_TIPOPAGO == "0003") && DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] == DBNull.Value)
                {
                    IPV_EFECTIVO_OTROS += Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"]);
                }
          
                        //AQUI OBTENGO EL TOTAL DE ACTIVOS
                 if (DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] == DBNull.Value && varMOVIMIENTO == "IPV")
                        {
                            CONTTOTAL += 1;
                        }
                
            }
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");  
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL ANULADOS : " + CONTANU + " DOC  S/. " + TOTALANU.ToString("N2"), "1");//IMPRIMIENDO TOTAL DE ANULADOS
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL VENTAS   : " + CONTTOTAL + " DOC  S/. " + (IPV_EFECTIVO + IPV_EFECTIVO_OTROS).ToString("N2"), "1");//IMPRIMIENDO TOTAL DE ANULADOS
            
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");                          // imprime una espacio
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "T.V. EFECTIVO (S/.): " + IPV_EFECTIVO.ToString("N2"), "1");//IMPRIMIENDO TOTAL DE EFECTIVO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "T.V. OTROS (S/.): " + IPV_EFECTIVO_OTROS.ToString("N2"), "1");//IMPRIMIENDO TOTAL DE EFECTIVO
            
            //GENERAR LOS REGISTROS DE INGRESOS VARIOS
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");                          // imprime una espacio
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-----DETALLE INGRESOS OTROS-----", "1");

            for (int i = 0; i < DATOS_CAJA_KARDEX.Rows.Count; i++)
            {
                ANULADO = " ";
                if (DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] != DBNull.Value )
                {
                    ANULADO = "*";
                }

                string varID_TIPOMOV = DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOMOV"].ToString(); //OBTENGO EL VALOR DE MI CAMPO ID_COMVENTA PARA VERIFICAR SI TIENE DATO O NO , PARA REALIZAR LA COMPARACIONES
                if (varID_TIPOMOV == "IVA")
                {
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), Convert.ToDateTime(DATOS_CAJA_KARDEX.Rows[i]["FECHA"]).ToString("dd/MM/yy") + " " + DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOMOV"].ToString() + " " +
                    DATOS_CAJA_KARDEX.Rows[i]["ID_MOVIMIENTO"].ToString() + "  " + DATOS_CAJA_KARDEX.Rows[i]["MONEDA"].ToString() + "  " + DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"].ToString() + " " + ANULADO, "1");
                }

                //AQUI CALCULO EL TOTAL DE LOS EGRESOS GERENCIA
                if (varID_TIPOMOV == "IVA" && DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] == DBNull.Value)
                {
                   IVA_EFECTIVO += Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"]);
                   IVA_CANT += 1;
                }

            }

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");                          // imprime una espacio
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL IVA : " + IVA_EFECTIVO.ToString("N2"), "1");//IMPRIMIENDO TOTAL DE EFECTIVO
            
            //GENERAR LOS REGISTROS DE EGRESOS
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty, "1");                          // imprime una espacio
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-------DETALLE DE EGRESOS-------", "1");

            for (int i = 0; i < DATOS_CAJA_KARDEX.Rows.Count; i++)
            {
                ANULADO = " ";
                if (DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] != DBNull.Value)
                {
                    ANULADO = "*";
                }

                string varID_TIPOMOV = DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOMOV"].ToString(); //OBTENGO EL VALOR DE MI CAMPO ID_COMVENTA PARA VERIFICAR SI TIENE DATO O NO , PARA REALIZAR LA COMPARACIONES
                
                if (varID_TIPOMOV == "EGE" || varID_TIPOMOV == "EVA")
                {
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), Convert.ToDateTime(DATOS_CAJA_KARDEX.Rows[i]["FECHA"]).ToString("dd/MM/yy") + " " + DATOS_CAJA_KARDEX.Rows[i]["ID_TIPOMOV"].ToString() + " " +
                    DATOS_CAJA_KARDEX.Rows[i]["ID_MOVIMIENTO"].ToString() + "  " +DATOS_CAJA_KARDEX.Rows[i]["MONEDA"].ToString() + "  " + DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"].ToString() + " " + ANULADO, "1");
                }

                //AQUI CALCULO EL TOTAL DE LOS EGRESOS GERENCIA
                if (varID_TIPOMOV == "EGE" && DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] == DBNull.Value)
                {
                    EGE_EFECTIVO += Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"]);
                    EGE_CANT += 1;
                }

                //AQUI CALCULO EL TOTAL DE LOS EGRESOS VARIOS
                if (varID_TIPOMOV == "EVA" && DATOS_CAJA_KARDEX.Rows[i]["FECHA_ANULADO"] == DBNull.Value)
                {
                    EVA_EFECTIVO += Convert.ToDouble(DATOS_CAJA_KARDEX.Rows[i]["IMPORTE"]);
                    EVA_CANT += 1;
                }

            }
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");                          // imprime una espacio
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL EGE: " + EGE_EFECTIVO.ToString("N2"), "1");//IMPRIMIENDO TOTAL DE EFECTIVO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL EVA: " + EVA_EFECTIVO.ToString("N2"), "1");//IMPRIMIENDO TOTAL DE EFECTIVO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");            
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                          // imprime una linea de guiones
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "SALDO EFECTIVO CAJA: " + ((IPV_EFECTIVO + IVA_EFECTIVO) - (EGE_EFECTIVO + EVA_EFECTIVO)).ToString("N2"), "1");//IMPRIMIENDO TOTAL DE EFECTIVO
            //=======================================================================================================================================================
            
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                          // imprime una linea de guiones
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "V.B: " + Session["EMPLEADO"], "1"); // obtenemos el NOMBRE DEL EMPLEADO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "  " + Session["USUARIO_EMPLEADO"], "1"); // obtenemos el USUARIO/DNI DEL EMPLEADO
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                          // imprime una linea de guiones
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "V.B: ADMINISTRACION", "1");

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "FECHA IMPRESION : " + DateTime.Now.ToString("g"), "1"); //formato de fecha g = 6/15/2008 9:15 PM
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1");                          // imprime una linea de guiones

        }

        protected void imgBUSCAR_VENTA_Click(object sender, ImageClickEventArgs e)
        {
            CONSULTAR_VENTAS(txtID_DOC.Text);
        }

        public void CONSULTAR_VENTAS(string OPCION)
        {
            
            if(cboTIPO_MOV.SelectedValue == "IPV")
            {

                DataTable dt = new DataTable();
                dt = N_OBJVENTAS.CAPTURAR_TABLA_VENTA(OPCION, Session["SEDE"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        txtPERSONA.Text = dt.Rows[0]["C_DESCRIPCION"].ToString();
                        txtNUM_DOCUMENTO.Text = dt.Rows[0]["V_TIPO_DOC"].ToString() + dt.Rows[0]["V_SERIE"].ToString() + dt.Rows[0]["V_NUMERO"].ToString();
                        txtMONEDA.Text = dt.Rows[0]["V_MONEDA"].ToString();
                        txtSALDO.Text = dt.Rows[0]["V_SALDO"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>window.alert('ERROR, VERIFICAR SI EL NUMERO DE VENTA ES CORRECTO');</script>"); 
                    }
            }
            if (cboTIPO_MOV.SelectedValue == "EPC")
            {
                DataTable dt = new DataTable();
                dt = N_OBJVENTAS.CAPTURAR_TABLA_COMPRA(OPCION);
                    if (dt.Rows.Count > 0)
                    {
                        txtPERSONA.Text = dt.Rows[0]["P_DESCRIPCION"].ToString();
                        txtNUM_DOCUMENTO.Text = dt.Rows[0]["C_TIPO_DOC"].ToString() + dt.Rows[0]["C_SERIE"].ToString() + dt.Rows[0]["C_NUMERO"].ToString();
                        txtMONEDA.Text = dt.Rows[0]["C_MONEDA"].ToString();
                        txtSALDO.Text = dt.Rows[0]["C_SALDO"].ToString();
                    }
                    else
                    {
                        Response.Write("<script>window.alert('ERROR, VERIFICAR SI EL NUMERO DE COMPRA ES CORRECTO');</script>"); 
                    }
            }
            
       }


        protected void cboTIPO_MOV_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if(cboTIPO_MOV.SelectedValue == "EPC")
            {
                ESTADO_TEXBOX_VENTA(1);
            }
            if (cboTIPO_MOV.SelectedValue == "EVA")
            {
                ESTADO_TEXBOX_VENTA(2);
            }
            if (cboTIPO_MOV.SelectedValue == "IPV")
            {
                ESTADO_TEXBOX_VENTA(1);
            }
            if (cboTIPO_MOV.SelectedValue == "IVA")
            {
                ESTADO_TEXBOX_VENTA(2);
            }
            if (cboTIPO_MOV.SelectedValue == "EGE")
            {
                ESTADO_TEXBOX_VENTA(2);
            }
        }
        
        public void ESTADO_TEXBOX_VENTA(int ESTADO)
        {
            if(ESTADO == 1)//ESTADO DE INGRESO POR VENTA
            {
                txtID_DOC.ReadOnly = false;
                txtPERSONA.ReadOnly = true;
                txtNUM_DOCUMENTO.ReadOnly = true;
                txtMONEDA.ReadOnly = true;
                txtSALDO.ReadOnly = true;
            }
            if (ESTADO == 2)//ESTADOO DE BLOQUEADO
            {
                txtID_DOC.ReadOnly = true;
                txtPERSONA.ReadOnly = true;
                txtNUM_DOCUMENTO.ReadOnly = true;
                txtMONEDA.ReadOnly = true;
                txtSALDO.ReadOnly = true;
            }
            txtID_DOC.Text = string.Empty;
            txtPERSONA.Text = string.Empty;
            txtNUM_DOCUMENTO.Text = string.Empty;
            txtMONEDA.Text = string.Empty;
            txtSALDO.Text = string.Empty;
        }
        

       
      

      

        

      

        // ===============================================================================================================================================

    }
}