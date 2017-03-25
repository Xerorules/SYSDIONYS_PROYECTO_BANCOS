using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using iTextSharp; //ESTE USING PERMITE JALAR LA LIBRERIA PARA PODER IMPRIMIR


using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;

namespace DIONYS_ERP
{
    
    
    public partial class FRM_MENUVENTA : System.Web.UI.Page
    {
    
        string [] valor = new string[20] ;
        string [] idbien = new string[20];
        string [] PRECIO_BIEN = new string[20];
        public String MON= "";
        public String WEB = "";
        public double VUELTO = 0.00,PAGA=0.00;

        protected void Page_Load(object sender, EventArgs e)
        {

            LLENAR_MENU_BIENES();
            
            if (!Page.IsPostBack)
            {
                //CON ESTO PUEDO VERIFICO SI TENGO UNA CAJA ABIERTA  Y SINO ES ASI, ABRIR UNA CAJA NUEVA
                if (Session["ID_CAJA"].ToString() == string.Empty)
                {
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }

                Session["TIPO_DOC"] = cboTIPO_DOC.SelectedValue.ToString();
                LLENAR_CLASE_BIEN();
                LLENAR_MENU_BIENES();
                cboTIPO_DOC.SelectedIndex =Convert.ToInt32(Session["INDICE_TIPODOC"].ToString());
                
                txtCANTIDAD_VENTA.Text = "1";
                txtPRECIO_VENTA.Text = string.Empty;
                
               
                if (Session["LLAMABIEN"].ToString() != string.Empty) //significa que regresa del mantenimiento del bien o del mantenimiento del cliente
                  {
                      Session["LLAMABIEN"] = string.Empty;                //INICIALIZANDO MI SESSION LLAMABIEN EN VACIO PORQUE YA REGRESO DE LOS MANTENIMIENTOS
                      if (Session["ID_BIEN"].ToString() != string.Empty)  //AQUI EJECUTO EL LLENADO DEL BIEN SI ESQUE UBIERAN DATOS QUE REGISTRAR
                      {

                          OBTENER_ID_BIEN_Y_LLENAR_GRILLA(Session["ID_BIEN"].ToString(), Session["DESCRIPCION_BIEN"].ToString(), Session["PRECIO_BIEN"].ToString());
                          
                          Session["ID_BIEN"] = string.Empty;
                          Session["DESCRIPCION_BIEN"] = string.Empty;
                          Session["PRECIO_BIEN"] = string.Empty;
                      }
                      if (Session["ID_CLIENTE"].ToString() != string.Empty)  //ESTO ES PARA VERIFICAR Y PROCESAR EL LLENADO DE LOS TEXBOX CON LOS DATOS DEL CLIENTE
                      {

                          txtID_CLIENTE.Text = Session["ID_CLIENTE"].ToString();
                          txtDESCRIPCION.Text=Session["DESCRIPCION_CLIENTE"].ToString();
                          txtCLIENTE.Text = Session["RUCDNI_CLIENTE"].ToString();
                          //LUEGO LIMPIO MIS SESIONES DE CLIENTE
                          Session["ID_CLIENTE"] = string.Empty;
                          Session["DESCRIPCION_CLIENTE"] = string.Empty;
                          Session["RUCDNI_CLIENTE"] = string.Empty;

                          LLENAR_GRILLA(); //ESTO PERMITE QUE SE MUESTRE LOS DATOS DE LA GRILLA A PESAR QUE SE AGA EL AUTOPOSBAC
                          ACTUALIZAR_TOTALES();
                      }
                      LLENAR_GRILLA(); //ESTO PERMITE QUE SE MUESTRE LOS DATOS DE LA GRILLA A PESAR QUE SE AGA EL AUTOPOSBAC
                      ACTUALIZAR_TOTALES(); //ESTO DEVUELVE LA ACTUALIZACION DE TOTALES
                  }
                else //significa que regresa de los menus de navegacion del sistema, por tan motivo debo limpiar todo como una nueva venta
                {
                            DataTable dt = (DataTable)Session["detalleBien"]; //ESTOY LIMPIANDO LA SESSION DEL DETALLEBIEN CADA VEZ QUE CARGO LA PAGINA FRM_MENUVENTA
                            dt.Clear();
                }
                                                    
            }
        }

        
        void LLENAR_CLASE_BIEN()
        {
            if((Session["ID_PUNTOVENTA"].ToString() == "PV003") || (Session["ID_PUNTOVENTA"].ToString() == "PV008" ) || (Session["ID_PUNTOVENTA"].ToString() == "PV009")){ //AQUI VAN LOS BIENES PARA RESTAURANT
                    string[] prod = {"BEBIDAS","COMIDA CRIOLLA","COMIDA TIPICA","COMIDA MARINA","POLLOS Y PARRILLAS" };
                    string[] pre = {"C2","C3","C4","C5","C6"};
                    for (int pos = 0; pos < prod.Length; pos++)
                    {
                        cboCLASE_BIEN.Items.Add(new ListItem(prod[pos].ToString(), pre[pos].ToString()));
                    }
            }
            else
            {
                string[] prod = { "SERVICIOS"};//AQUI SON LAS DEMAS SERIES 0001 Y 0002 Y 0004
                string[] pre = { "C1"};
                for (int pos = 0; pos < prod.Length; pos++)
                {
                    cboCLASE_BIEN.Items.Add(new ListItem(prod[pos].ToString(), pre[pos].ToString()));
                }
            }
            
        }

        

        #region OBJETOS
        N_VENTA N_OBJVENTAS = new N_VENTA();
        E_VENTA E_OBJVENTAS = new E_VENTA();
        E_MANT_CLIENTE E_OBJMANT_CLIENTE = new E_MANT_CLIENTE();
        E_VENTA_Y_DETALLE E_OBJMANT_VENTADET = new E_VENTA_Y_DETALLE();
        E_CAJA_KARDEX E_OBJCAJA_KARDEX = new E_CAJA_KARDEX();
        #endregion

        #region PROCEDIMIENTOS

        void LLENAR_MENU_BIENES()
        {
            
            DataTable dt = new DataTable();
            E_OBJVENTAS.ID_CLASE = cboCLASE_BIEN.SelectedValue; 
            E_OBJVENTAS.ID_EMPRESA = Session["ID_EMPRESA"].ToString();
            dt=N_OBJVENTAS.BIEN_X_CLASE(E_OBJVENTAS); //llenar el datatable con los datos del filtrado de bienes por clase

            for (int i = 0; i < dt.Rows.Count;i++ )
            {

                if (i < 20) //esto controla los 16 botones asignados para los platos
                {
                    valor[i] = dt.Rows[i][1].ToString();     //esto permite obtener la descripcion y el precio de los bienes
                   idbien[i] = dt.Rows[i][0].ToString();     //esto permite obtener los codigos de cada bien que contiene el datatable
                   PRECIO_BIEN[i] = dt.Rows[i][2].ToString(); 
                }
                
            }

            btnBIEN01.Text = valor[0];
            btnBIEN02.Text = valor[1]; 
            btnBIEN03.Text = valor[2];
            btnBIEN04.Text = valor[3] ;
            btnBIEN05.Text = valor[4] ;
            btnBIEN06.Text = valor[5] ;
            btnBIEN07.Text = valor[6] ;
            btnBIEN08.Text = valor[7] ;
            btnBIEN09.Text = valor[8] ;
            btnBIEN10.Text = valor[9] ;
            btnBIEN11.Text = valor[10] ;
            btnBIEN12.Text = valor[11] ;
            btnBIEN13.Text = valor[12] ;
            btnBIEN14.Text = valor[13] ;
            btnBIEN15.Text = valor[14] ;
            btnBIEN16.Text = valor[15] ;
            btnBIEN17.Text = valor[16];
            btnBIEN18.Text = valor[17];
            btnBIEN19.Text = valor[18];
            btnBIEN20.Text = valor[19];
        }

        

        void OBTENER_ID_BIEN_Y_LLENAR_GRILLA(string ID_BIEN,string DESCRIPCION, string PRECIO)
        {
            
            DataTable dt = (DataTable)Session["detalleBien"];

            try
            {   
                DataRow row = dt.NewRow();
                    row["ID_BIEN"] = ID_BIEN;
                    row["CANT"] = Convert.ToDouble(txtCANTIDAD_VENTA.Text); //
                    row["DESCRIPCION"] = DESCRIPCION;
                    if(txtPRECIO_VENTA.Text != string.Empty) // si es vacio tomo el precio del texbox precioventa
                    {
                        row["PRECIO"] =Convert.ToDouble(txtPRECIO_VENTA.Text);
                    }else //sino tomo el precio de la base de datos q esta en el parametro PRECIO
                    {
                        row["PRECIO"] =Convert.ToDouble(PRECIO);
                    }
                    
                    row["IMPORTE"] = Convert.ToDouble(row["PRECIO"]) * Convert.ToDouble(row["CANT"]); 
                    dt.Rows.Add(row);
                    dt.AcceptChanges();

                LLENAR_GRILLA();
                ACTUALIZAR_TOTALES();
                
                //aqui limpio la data de ingreso de precio y cantidad de cada bien
                txtCANTIDAD_VENTA.Text = "1";
                txtPRECIO_VENTA.Text = string.Empty;

                txtCANTIDAD_VENTA.Focus();
            }
            catch (Exception )
            {
               
               // Response.Write("<script>window.alert('EL BIEN YA ESTA EN LA LISTA');</script>");
                
            }
        }
        
        void LLENAR_GRILLA()
        {
            DataTable dt = (DataTable)Session["detalleBien"];
            dgvBIEN_VENTA.DataSource = dt;
            dgvBIEN_VENTA.DataBind(); 
        }

       public void IMPRIMIR_SPOOL()
        {

            DataTable DATOS_VENTA = new DataTable();                         //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA
            string ID_VENTA = E_OBJMANT_VENTADET.ID_VENTA;                   // ESTO PERMITE GENERAR LA VARIABLE DEL ID_VENTA
            DATOS_VENTA = N_OBJVENTAS.CAPTURAR_TABLA_VENTA(ID_VENTA, Session["SEDE"].ToString());        //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO

            DataTable DATOS_VENTADETALLE = new DataTable();                  //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA_DETALLE
            string COD_VENTA = E_OBJMANT_VENTADET.ID_VENTA;            // ESTO PERMITE GENERAR LA VARIABLE DEL ID_VENTA
            DATOS_VENTADETALLE = N_OBJVENTAS.CAPTURAR_TABLA_VENTADETALLE(COD_VENTA); //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO


            //LIMPIANDO MI SPOOL SI ESQUE UBIERA IMPRESIONES PENDIENTES
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "2");
           // ========================================================================================

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][36].ToString(),"1"); //aqui va el nombre de la empresa


            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RUC: " + DATOS_VENTA.Rows[0][37].ToString(),"1");    //aqui va el ruc de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA EMPRESA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                          // imprime una linea de guiones
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][28].ToString(),"1"); //aqui va el nombre de la sede de la empresa 
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][29].ToString(),"1"); //aqui va la direccion de la sede de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA SEDE
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0]["S_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIDEN"].ToString(),"1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");     
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"MAQ REG : " + DATOS_VENTA.Rows[0][48].ToString(),"1");          //AQUI SE COLOCA EL NOMBRE DE LA MAQUINA REGISTRADORA
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][4].ToString(),"1");   //aqui va la fecha

            string TIP_DOC;
            TIP_DOC = DATOS_VENTA.Rows[0][3].ToString();/* AQUI BA EL NOMBRE  DEL TIPO DE DOCUMENTO */

            //P_SERIE_Y_NUMERO_CORRELATIVO_POR_PTOVENTA(TIP_DOC, CBOPTOVENTA.Text);
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"TICKET: " + TIP_DOC + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString(),"1"); //aqui va el tipo_documento / el numero de serie / y el numero correlativo

            if (DATOS_VENTA.Rows[0]["V_ID_CLIENTE"] != DBNull.Value)   //ESTO ME PERMITE IMPRIMIR LOS DATOS CLIENTES SI ESQUE EXISTIERA UN CLIENTE
            {
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                              // imprime una linea de guiones
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CLIENTE: " + DATOS_VENTA.Rows[0]["C_DESCRIPCION"].ToString(),"1"); //OBTENIENDO EL NOMBRE DEL CLIENTE
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"RUC/DNI: " + DATOS_VENTA.Rows[0]["C_RUC_DNI"].ToString(),"1"); //OBTENIENDO EL RUC DEL CLIENTE
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0]["C_DIRECCION"].ToString(),"1"); //OBTENIENDO LA DIRECCION DEL CLIENTE
                //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DEL CLIENTE
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0]["C_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIDEN"].ToString(),"1");
            }

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");  


            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CANT   DETALLE                IMPORTE","1");
            for (int i = 0; i < DATOS_VENTADETALLE.Rows.Count; i++)
            {
               N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTADETALLE.Rows[i][3].ToString() + "   " + DATOS_VENTADETALLE.Rows[i][7].ToString() +"   "+ DATOS_VENTADETALLE.Rows[i][5].ToString(),"1");
            }

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"SUBTOTAL: S/. " + DATOS_VENTA.Rows[0][6].ToString(),"1"); //obtenemos el sub_total
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"IGV: S/. " + DATOS_VENTA.Rows[0][7].ToString(),"1");  //obtenemos el igv
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"TOTAL: S/. " + DATOS_VENTA.Rows[0][8].ToString(),"1"); //obtenemos el total
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "P.V: " + Session["PUNTOVENTA"].ToString(), "1"); // obtenemos el punto de venta
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CAJERO: " +  Session["EMPLEADO"].ToString(),"1"); //obtenemos la descripcion del cajero

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][41].ToString(),"1"); //aqui obtenemos el email de la empresa
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][42].ToString(),"1"); //aqui obtenemos la pagina web de la empresa

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "PAGO CON: S/. " + PAGA.ToString("N2"), "1"); //obtenemos el total
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "VUELTO: S/. " + VUELTO.ToString("N2"), "1"); //obtenemos el total
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");

            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString(),"1"); //obtenemos la descripcion del cajero
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"AGRADECEMOS SU PREFERENCIA!!!","1"); // imprime en el centro "Venta mostrador"
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"VUELVA PRONTO!! LO ESPERAMOS!!","1");
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty,"1");
            if (DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString() != string.Empty)
            {
                N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString(),"1");
            }
            N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1"); 

            //METODO PARA EMITIR TICKET INDIVIDUALES POR PRODUCTO QUE ESTAN CONFIGURADOS EN LA TABLA BIEN

            for (int f = 0; f < DATOS_VENTADETALLE.Rows.Count; f++)
            {
                if (DATOS_VENTADETALLE.Rows[f]["B_EMITE_TICKET"].Equals(true))
                {
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][36].ToString(),"1"); //aqui va el nombre de la empresa
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][28].ToString(),"1"); //nombre de la sede
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"TICKET DESPACHO","1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"REFERENCIA: " + DATOS_VENTA.Rows[0][3].ToString() + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString(),"1"); //aqui va el tipo_documento / el numero de serie / y el numero correlativo
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty,"1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"**" + DATOS_VENTADETALLE.Rows[f]["VD_CANTIDAD"].ToString() + "**","1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTADETALLE.Rows[f]["B_DESCRIPCION"].ToString(),"1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty,"1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString(),"1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][4].ToString(),"1");   //aqui va la fecha Y EL ID_VENTA
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString(), "1");//aqui va el codigo de venta
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"AGRADECEMOS SU PREFERENCIA!!!","1");
                    N_OBJVENTAS.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1"); 
                }
            }
        }
                        

        /*========== FIN PROCEDIMIENTO IMPRIMIR TICKET ======================== */

        void ACTUALIZAR_TOTALES()
        {
            double subTotal,igv,total=0;
            DataTable dt = (DataTable)Session["detalleBien"];
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                total = total + Convert.ToDouble(dt.Rows[i][4].ToString());
                
            }
            subTotal = (total / 1.18);
            igv = total - subTotal;


            lblSUBTOTAL.Text = subTotal.ToString("N2");
            lblIGV.Text = igv.ToString("N2");
            lblTOTAL.Text = total.ToString("N2");



        }

        void LIMPIAR_VENTA()
        {
            DataTable dt= (DataTable)Session["detalleBien"];
            cboTIPO_DOC.SelectedValue = "TB";
            txtCLIENTE_VENTA.Text = string.Empty;
            lblSUBTOTAL.Text = string.Empty;
            lblIGV.Text = string.Empty;
            lblTOTAL.Text = string.Empty;
            dt.Clear();
            //Response.Redirect("FRM_MENUVENTA.aspx");
            
        }
        
        private bool VALIDAR_DATOS()
        {
            bool retorno=false;
            if(dgvBIEN_VENTA.Rows.Count > 0)
            {
           /*
                      if (cboTIPO_DOC.SelectedIndex == 1) //si es ticket factura?
                      {
                        if (Convert.ToDouble(lblTOTAL.Text) < 700) //solo permitir hacer ticket factura <= a 700
                          {
                           if (txtID_CLIENTE.Text != string.Empty)
                             {
                                if(cboTIPO_PAGO.SelectedItem.Text != "EFECTIVO") //SI EL PAGO ES EN EFECTIVO
                                    {
                                          if (txtTIPO_PAGO.Text != string.Empty) //SI EL CAMPO DONDE SE LLENA LA OPERACION YL NUMERO Y TODOS DATOS DEL DOCUMENTO DE OPERACION ESTA LLENO
                                          {
                                              if (txtPAGA.Text != string.Empty)
                                              {

                                                  retorno = true;

                                              }
                                              else
                                              {
                                                  retorno = false;
                                              }
                                          }
                                              else
                                              {
                                                  retorno = false;
                                              }
                                    }
                                    else
                                    {
                                      retorno = true;
                                    }

                                  }
                              else //el id cliente esta vacio y es factura 
                              {
                                  retorno = false;
                              }
                          }
                          else //en este caso la factura es > a 700 entonces no se debe generar la venta
                          {
                              retorno = false;
                          }

                      }
                */
                      if (cboTIPO_DOC.SelectedIndex == 0)//si es boleta entonces
                      {
                          if (Convert.ToDouble(lblTOTAL.Text) >= 700)  //tiene q escoger un cliente si la boleta es >= que 700
                          {
                              if (txtID_CLIENTE.Text != string.Empty) //
                              {
                                  if(cboTIPO_PAGO.SelectedItem.Text != "EFECTIVO") //SI EL PAGO ES EN EFECTIVO
                                    {
                                        if (txtTIPO_PAGO.Text != string.Empty) //SI EL CAMPO DONDE SE LLENA LA OPERACION YL NUMERO Y TODOS DATOS DEL DOCUMENTO DE OPERACION ESTA LLENO
                                        {
                                            if (txtPAGA.Text != string.Empty)
                                            {
                                                retorno = true;
                                            }
                                            else
                                            {
                                                retorno = false;
                                            }
                                        }
                                        else
                                        {
                                            retorno = false;
                                        }
                                    }
                                    else
                                    {
                                      retorno = true;
                                    }
                              }
                              else //el id_cliente est vacio
                              {
                                  retorno = false;
                              }
                          }
                          else // es boleta y < de 700 entonces no interesa los datos del cliente
                          {
                              if (txtPAGA.Text != string.Empty)
                              {
                                    
                                  if (Convert.ToDouble(txtPAGA.Text.ToString()) >= Convert.ToDouble(lblTOTAL.Text.ToString()))
                                  {
                                      if(cboTIPO_PAGO.SelectedItem.Text != "EFECTIVO") //SI EL PAGO ES EN EFECTIVO
                                    {
                                        if (txtTIPO_PAGO.Text != string.Empty) //SI EL CAMPO DONDE SE LLENA LA OPERACION YL NUMERO Y TODOS DATOS DEL DOCUMENTO DE OPERACION ESTA LLENO
                                        {
                                            retorno = true;
                                        }
                                        else
                                        {
                                            retorno = false;
                                        }
                                    }
                                      else
                                      {
                                          retorno = true;
                                      }
                                  }
                                  else
                                  {
                                      retorno = false;
                                  }
                              }
                              else
                              {
                                  retorno = false;
                              }
                          }

                      }
                  





                }
            return retorno;
        }

        void MANTENIMIENTO_VENTA()
        {
            try
            { 
                    E_OBJMANT_VENTADET.ID_VENTA = string.Empty;
                    E_OBJMANT_VENTADET.SERIE = Session["SERIE"].ToString();
                    E_OBJMANT_VENTADET.TIPO_DOC = cboTIPO_DOC.SelectedValue.ToString();
                    E_OBJMANT_VENTADET.MONEDA = "S";
                    E_OBJMANT_VENTADET.VALOR_VENTA =Convert.ToDouble(lblSUBTOTAL.Text);
                    E_OBJMANT_VENTADET.IGV = Convert.ToDouble(lblIGV.Text);
                    E_OBJMANT_VENTADET.TOTAL = Convert.ToDouble(lblTOTAL.Text);
                    E_OBJMANT_VENTADET.SALDO = 0.00;
                    E_OBJMANT_VENTADET.ID_SEDE = Session["SEDE"].ToString();
                    E_OBJMANT_VENTADET.ID_PEDIDO = null;
                    E_OBJMANT_VENTADET.ID_CLIENTE =txtID_CLIENTE.Text;
                    E_OBJMANT_VENTADET.CLIENTE = txtCLIENTE_VENTA.Text;
                    E_OBJMANT_VENTADET.ACCION = "1";

                    N_OBJVENTAS.MANTENIMIENTO_VENTA(E_OBJMANT_VENTADET); //AQUI CARGO LA VENTA
                    MANTENIMIENTO_VENTADETALLE();                        // AQUI CARGO EL DETALLE DE LA VENTA
                    MANTENIMIENTO_CAJA_KARDEX();//AQUI LLAMO A MI PROCEDIMIENTO PAR GENERAR EL INGRESO EN CAJA KARDEX
                    IMPRIMIR_SPOOL();   //
            }
            catch (Exception)
            {

                Response.Write("<script>window.alert('REGISTRA TODOS LOS CAMPOS NECESARIOS PARA LA VENTA');</script>"); 
            }


            LIMPIAR_VENTA();
            Response.Redirect("FRM_MENUVENTA.aspx"); /* ESTO PERMITE LIMPIAR O RECARGAR LA PAGINA Y ASI INICIALIZAR LA PAGINA*/
          
        }

        void MANTENIMIENTO_VENTADETALLE()
        {
            DataTable detalleVenta = (DataTable)Session["detalleBien"];

            try
            {
                for (int i = 0; i < dgvBIEN_VENTA.Rows.Count; i++)
                    {
                        E_OBJMANT_VENTADET.ID_VENTA = E_OBJMANT_VENTADET.ID_VENTA;
                        E_OBJMANT_VENTADET.ID_BIEN= dgvBIEN_VENTA.Rows[i].Cells[1].Text;
                        E_OBJMANT_VENTADET.ITEM = i + 1;
                        Label can= dgvBIEN_VENTA.Rows[i].FindControl("Label1") as Label;
                        E_OBJMANT_VENTADET.CANTIDAD = Convert.ToDouble(can.Text);
                        Label pre = dgvBIEN_VENTA.Rows[i].FindControl("Label2") as Label;
                        E_OBJMANT_VENTADET.PRECIO = Convert.ToDouble(pre.Text);
                        E_OBJMANT_VENTADET.IMPORTE = Convert.ToDouble(dgvBIEN_VENTA.Rows[i].Cells[5].Text);
                        E_OBJMANT_VENTADET.SALDO_CANTIDAD = 0.00;
                        //1 = VENTA_DIRECTA Y NECESITO GRABAR EL DETALLE DE PEDIDO Y EL DETALLE DE LA VENTA 
                        E_OBJMANT_VENTADET.GRABA_PEDIDO_DETALLE = "1";     

                        N_OBJVENTAS.MANTENIMIENTO_VENTADETALLE(E_OBJMANT_VENTADET);
                    }
            }
            catch (Exception )
            {

                Response.Write("<script>window.alert('NO ESCOGISTE NINGUN BIEN A VENDER');</script>"); 
            }
            
        }


        public void MANTENIMIENTO_CAJA_KARDEX()
        {
            try
            {
                    E_OBJCAJA_KARDEX.ID_MOVIMIENTO = string.Empty;
                    
                    if(cboTIPO_PAGO.SelectedItem.Text == "EFECTIVO")
                    {
                        E_OBJCAJA_KARDEX.DESCRIPCION = "VENTA DIRECTA";
                    }
                    else
                    {
                        E_OBJCAJA_KARDEX.DESCRIPCION = "VENTA DIRECTA "+ txtTIPO_PAGO.Text;
                    }
                    
                    E_OBJCAJA_KARDEX.ID_COMPVENT=E_OBJMANT_VENTADET.ID_VENTA; //id de la venta

                    E_OBJCAJA_KARDEX.ID_TIPOPAGO = cboTIPO_PAGO.SelectedValue.ToString(); // AQUI SE ANOTA EL PAGO POR EL CONCEPTO QUE SE ELIGIO
                    
                    E_OBJCAJA_KARDEX.ID_TIPOMOV="IPV"; //ingreso por venta 
                    E_OBJCAJA_KARDEX.IMPORTE=Convert.ToDouble(lblTOTAL.Text.ToString());
                    E_OBJCAJA_KARDEX.MONEDA="S";
                    E_OBJCAJA_KARDEX.TIPO_CAMBIO=Convert.ToDouble(Session["TIPO_CAMBIO"].ToString());
                    E_OBJCAJA_KARDEX.AMORTIZADO=Convert.ToDouble(lblTOTAL.Text.ToString());
                    E_OBJCAJA_KARDEX.ID_CAJA=Session["ID_CAJA"].ToString();
                    E_OBJCAJA_KARDEX.IMPORTE_CAJA = Convert.ToDouble(lblTOTAL.Text.ToString());
                    E_OBJCAJA_KARDEX.OPCION = 1;

                    N_OBJVENTAS.CAJA_KARDEX_MANTENIMIENTO(E_OBJCAJA_KARDEX);
            }
            catch (Exception)
            {
                
                throw;
            }
           

        }


        #endregion


        #region FUNCIONES

        //public DataTable CAPTURAR_TABLA_VENTA()
        //{
        //    DataTable dt = new DataTable();
        //    string ID_VENTA = E_OBJMANT_VENTADET.ID_VENTA;
        //    dt = N_OBJVENTAS.CAPTURAR_TABLA_VENTA(ID_VENTA);
        //    return dt;
        //}

        



        #endregion

        #region VALIDACIONES
      

        




        #endregion

        #region EVENTOS
        protected void cboCLASE_BIEN_SelectedIndexChanged(object sender, EventArgs e)
                {
                    LLENAR_MENU_BIENES(); // llamamos al procedimiento para llenar los botones 
                }

                protected void dgvBIEN_VENTA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
                {
                    dgvBIEN_VENTA.PageIndex = e.NewSelectedIndex;
                }
        

                protected void btnBIEN01_Click(object sender, EventArgs e)
                {
                    if (idbien[0] != null) { 
                    OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[0].ToString(),valor[0].ToString(),PRECIO_BIEN[0].ToString());
                        
                    }
                }

                protected void btnBIEN02_Click(object sender, EventArgs e)
                {
                    if (idbien[1] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[1].ToString(), valor[1].ToString(), PRECIO_BIEN[1].ToString());
                    }
                }

                protected void btnBIEN03_Click(object sender, EventArgs e)
                {
                    if (idbien[2] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[2].ToString(), valor[2].ToString(), PRECIO_BIEN[2].ToString());
                    }
                }

                protected void btnBIEN04_Click(object sender, EventArgs e)
                {
                    if (idbien[3] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[3].ToString(), valor[3].ToString(), PRECIO_BIEN[3].ToString());
                    }
                }

                protected void btnBIEN05_Click(object sender, EventArgs e)
                {
                    if (idbien[4] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[4].ToString(), valor[4].ToString(), PRECIO_BIEN[4].ToString());
                    }
                }

                protected void btnBIEN06_Click(object sender, EventArgs e)
                {
                    if (idbien[5] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[5].ToString(), valor[5].ToString(), PRECIO_BIEN[5].ToString());
                    }
                }

                protected void btnBIEN07_Click(object sender, EventArgs e)
                {
                    if (idbien[6] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[6].ToString(), valor[6].ToString(), PRECIO_BIEN[6].ToString());
                    }
                }

                protected void btnBIEN08_Click(object sender, EventArgs e)
                {
                    if (idbien[7] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[7].ToString(), valor[7].ToString(), PRECIO_BIEN[7].ToString());
                    }
                }

                protected void btnBIEN09_Click(object sender, EventArgs e)
                {
                    if (idbien[8] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[8].ToString(), valor[8].ToString(), PRECIO_BIEN[8].ToString());
                    }
                }

                protected void btnBIEN10_Click(object sender, EventArgs e)
                {
                    if (idbien[9] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[9].ToString(), valor[9].ToString(), PRECIO_BIEN[9].ToString());
                    }
                }

                protected void btnBIEN11_Click(object sender, EventArgs e)
                {
                    if (idbien[10] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[10].ToString(), valor[10].ToString(), PRECIO_BIEN[10].ToString());
                    }
                }

                protected void btnBIEN12_Click(object sender, EventArgs e)
                {
                    if (idbien[11] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[11].ToString(), valor[11].ToString(), PRECIO_BIEN[11].ToString());
                    }
                }

                protected void btnBIEN13_Click(object sender, EventArgs e)
                {
                    if (idbien[12] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[12].ToString(), valor[12].ToString(), PRECIO_BIEN[12].ToString());
                    }
                }

                protected void btnBIEN14_Click(object sender, EventArgs e)
                {
                    if (idbien[13] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[13].ToString(), valor[13].ToString(), PRECIO_BIEN[13].ToString());
                    }
                }

                protected void btnBIEN15_Click(object sender, EventArgs e)
                {
                    if (idbien[14] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[14].ToString(), valor[14].ToString(), PRECIO_BIEN[14].ToString());
                    }
                }

                protected void btnBIEN16_Click(object sender, EventArgs e)
                {
                    if (idbien[15] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[15].ToString(), valor[15].ToString(), PRECIO_BIEN[15].ToString());
                   }
                }
        #endregion

                protected void dgvBIEN_VENTA_RowDataBound(object sender, GridViewRowEventArgs e)
                {
                    
                }
                
                /* EVENTO QUE SIRVE PARA MOSTRAR EL POPUP */
                protected void btnBUSCAR_BIEN_Click(object sender, ImageClickEventArgs e)
                {
                    //ModalPopup.Show();
                }

                /*EVENTO PARA FILTRAR BIENES*/
                //protected void btnBUSCAR_Click(object sender, EventArgs e)
                //{
                //    FILTRAR_BIEN_XCODIGO_XDESCRIPCION(); 
                //}

                /* */
                protected void dgvFILTRADOBIENES_RowDataBound(object sender, GridViewRowEventArgs e)
                {
                    //if (e.Row.RowType == DataControlRowType.DataRow)
                    //{
                    //    string cod = dgvFILTRADOBIENES.DataKeys[e.Row.RowIndex].Values[0].ToString();
                    //    string desc = dgvFILTRADOBIENES.DataKeys[e.Row.RowIndex].Values[1].ToString();
                    //    string prec = dgvFILTRADOBIENES.DataKeys[e.Row.RowIndex].Values[2].ToString();
                    //    Image img = (Image)e.Row.FindControl("ImgOK");
                    //    string formato = String.Format("SimularClick('btnbandera');");
                    //    img.Attributes.Add("onclick", formato);

                    //}
                    
                }

                /* EVENTO PARA SELECCIONAR EL BIEN Y SALIR DEL POPUP */

                //protected void dgvFILTRADOBIENES_RowCommand(object sender, GridViewCommandEventArgs e)
                //{
                //    if(e.CommandName=="cmdOK")
                //    {
                        
                //        GridViewRow fila_sel = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                //        string CODIGO = dgvFILTRADOBIENES.DataKeys[fila_sel.RowIndex].Values[0].ToString();
                //        string DESCRIPCION  = dgvFILTRADOBIENES.DataKeys[fila_sel.RowIndex].Values[1].ToString();
                //        string PRECIO = dgvFILTRADOBIENES.DataKeys[fila_sel.RowIndex].Values[2].ToString();

                //        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(CODIGO, DESCRIPCION, PRECIO);
                //        //String formato = "<script>javascript:Actualizar();</script>";
                //        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", formato, false);    
                //        ModalPopup.Hide();
                        
                //    }

                //}

              

               
                protected void btnOK_Click(object sender, EventArgs e)
                {
                    if (VALIDAR_DATOS())
                    {

                        if (txtPAGA.Text.ToString() != string.Empty)
                        {
                            if (Convert.ToDouble(txtPAGA.Text) >= Convert.ToDouble(lblTOTAL.Text))
                            {
                                double TOTAL = Convert.ToDouble(lblTOTAL.Text);
                                PAGA = Convert.ToDouble(txtPAGA.Text);
                                VUELTO =Convert.ToDouble(Server.HtmlEncode(Convert.ToDouble(PAGA - TOTAL).ToString("N2")));
                            }
                            else
                            {
                                Response.Write("<script>window.alert('INGRESAR UN MONTO MAYOR AL MONTO TOTAL');</script>");
                            }
                        }

                        MANTENIMIENTO_VENTA();
                        cboTIPO_DOC.SelectedIndex=0;   //REGRESANDO EL TIPO DE DOC A BOLETA DE VENTA
                        Session["INDICE_TIPODOC"] = 0; // INICIALIZANDO LA SESSION DEL INDICE DE TIPODOC A 0
                    }
                    else
                    {
                        Response.Write("<script>window.alert('ERROR, NO SE SELECCIONARON NI SE LLENARON TODOS LOS DATOS CORRECTOS, VUELVA A INTENTARLO...');</script>");
                    }
                }
                

                protected void PRUEBA_Click(object sender, EventArgs e)
                {
                    txtPAGA.Text = dgvBIEN_VENTA.Rows[0].Cells[0].Text;
                    txtVUELTO.Text = dgvBIEN_VENTA.Rows[1].Cells[2].Text;
                }

                
                //protected void txtPAGA_TextChanged(object sender, EventArgs e)
                //{
                //    double TOTAL = Convert.ToDouble(lblTOTAL.Text);
                //    double VUELTO = Convert.ToDouble(txtPAGA.Text);
                //    txtVUELTO.Text =Convert.ToString( Server.HtmlEncode(Convert.ToDouble(VUELTO - TOTAL).ToString("N2")));
                //}

                

                

                        
            void OBTENER_DATOS_CLIENTE(string ID,string DESC, string RUCDNI)
                {
                    txtID_CLIENTE.Text = ID;
                    txtDESCRIPCION.Text = DESC;
                    txtCLIENTE.Text = RUCDNI;
                }

            







        /* ================================================ METODO PARA GENERAR TICKET 1 PARTE ===============================================================*/
        /* ================================================ METODO PARA GENERAR TICKET 1 PARTE ===============================================================*/
        /* ================================================ METODO PARA GENERAR TICKET 1 PARTE ===============================================================*/
        /* ================================================ METODO PARA GENERAR TICKET 1 PARTE ===============================================================*/

                public class CreaTicket
                {
                    public string impresora;
                    //{

                    string ticket = "";
                    string parte1, parte2;
                    //string impresora = "\\\\FARMACIA-PVENTA\\Generic / Text Only"; // nombre exacto de la impresora como esta en el panel de control
                    //string impresora = "Generic / Text Only"; // nombre exacto de la impresora como esta en el panel de control
                    // string impresora = NombreImpresora; // nombre exacto de la impresora como esta en el panel de control
                    int max, cort;
                    public void LineasGuion()
                    {
                        ticket = "----------------------------------------\n";   // agrega lineas separadoras -
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
                    }
                    public void LineasAsterisco()
                    {
                        ticket = "****************************************\n";   // agrega lineas separadoras *
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
                    }
                    public void LineasIgual()
                    {
                        ticket = "========================================\n";   // agrega lineas separadoras =
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
                    }
                    public void LineasTotales()
                    {
                        ticket = "                             -----------\n"; ;   // agrega lineas de total
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
                    }
                    public void EncabezadoVenta()
                    {
                        //ticket = "Articulo        Can    P.Unit    Importe\n";   // agrega lineas de  encabezados
                        ticket = "Cant       Articulo              Importe\n";   // agrega lineas de  encabezados
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    public void TextoIzquierda(string par1)                          // agrega texto a la izquierda
                    {
                        max = par1.Length;
                        if (max > 40)                                 // **********
                        {
                            cort = max - 40;
                            parte1 = par1.Remove(40, cort);        // si es mayor que 40 caracteres, lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        ticket = parte1 + "\n";
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    public void TextoDerecha(string par1)
                    {
                        ticket = "";
                        max = par1.Length;
                        if (max > 40)                                 // **********
                        {
                            cort = max - 40;
                            parte1 = par1.Remove(40, cort);           // si es mayor que 40 caracteres, lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        max = 40 - par1.Length;                     // obtiene la cantidad de espacios para llegar a 40
                        for (int i = 0; i < max; i++)
                        {
                            ticket += " ";                          // agrega espacios para alinear a la derecha
                        }
                        ticket += parte1 + "\n";                    //Agrega el texto
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    public void TextoCentro(string par1)
                    {
                        ticket = "";
                        max = par1.Length;
                        if (max > 40)                                 // **********
                        {
                            cort = max - 40;
                            parte1 = par1.Remove(40, cort);          // si es mayor que 40 caracteres, lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        max = (int)(40 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                        for (int i = 0; i < max; i++)                // **********
                        {
                            ticket += " ";                           // Agrega espacios antes del texto a centrar
                        }                                            // **********
                        ticket += parte1 + "\n";
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    public void TextoExtremos(string par1, string par2)
                    {
                        max = par1.Length;
                        if (max > 25)                                 // **********
                        {
                            cort = max - 25;
                            parte1 = par1.Remove(25, cort);          // si par1 es mayor que 18 lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        ticket = parte1;                             // agrega el primer parametro
                        max = par2.Length;
                        if (max > 25)                                 // **********
                        {
                            cort = max - 25;
                            parte2 = par2.Remove(25, cort);          // si par2 es mayor que 18 lo corta
                        }
                        else { parte2 = par2; }
                        max = 40 - (parte1.Length + parte2.Length);
                        for (int i = 0; i < max; i++)                 // **********
                        {
                            ticket += " ";                            // Agrega espacios para poner par2 al final
                        }                                             // **********
                        ticket += parte2 + "\n";                     // agrega el segundo parametro al final
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    public void AgregaTotales(string par1, double total)
                    {
                        max = par1.Length;
                        if (max > 25)                                 // **********
                        {
                            cort = max - 25;
                            parte1 = par1.Remove(25, cort);          // si es mayor que 25 lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        ticket = parte1;
                        parte2 = total.ToString("");
                        max = 40 - (parte1.Length + parte2.Length);
                        for (int i = 0; i < max; i++)                // **********
                        {
                            ticket += " ";                           // Agrega espacios para poner el valor de moneda al final
                        }                                            // **********
                        ticket += parte2 + "\n";
                        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    //public void AgregaArticulo(string par1, int cant, double precio, double total)
                    //{
                    //    if (cant.ToString().Length <= 3 && precio.ToString("c").Length <= 10 && total.ToString("c").Length <= 11) // valida que cant precio y total esten dentro de rango
                    //    {
                    //        max = par1.Length;
                    //        if (max > 16)                                 // **********
                    //        {
                    //            cort = max - 16;
                    //            parte1 = par1.Remove(16, cort);          // corta a 16 la descripcion del articulo
                    //        }
                    //        else { parte1 = par1; }                      // **********
                    //        ticket = parte1;                             // agrega articulo
                    //        max = (3 - cant.ToString().Length) + (16 - parte1.Length);
                    //        for (int i = 0; i < max; i++)                // **********
                    //        {
                    //            ticket += " ";                           // Agrega espacios para poner el valor de cantidad
                    //        }
                    //        ticket += cant.ToString();                   // agrega cantidad
                    //        max = 10 - (precio.ToString("").Length);
                    //        for (int i = 0; i < max; i++)                // **********
                    //        {
                    //            ticket += " ";                           // Agrega espacios
                    //        }                                            // **********
                    //        ticket += precio.ToString(""); // agrega precio
                    //        max = 11 - (total.ToString().Length);
                    //        for (int i = 0; i < max; i++)                // **********
                    //        {
                    //            ticket += " ";                           // Agrega espacios
                    //        }                                            // **********
                    //        ticket += total.ToString("") + "\n"; // agrega precio
                    //        RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Valores fuera de rango");
                    //        RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n"); // imprime texto
                    //    }
                    //}
                    //*****************************+

                    //public void AgregaArticulo(string cant, string par1, double precio, double total)
                    public void AgregaArticulo(string cant, string par1, string total)
                    {
                        //if (cant.ToString().Length <= 7 && precio.ToString("c").Length <= 10 && total.ToString("c").Length <= 18) // valida que cant precio y total esten dentro de rango
                        if (cant.ToString().Length <= 7 && total.ToString().Length <= 15) // valida que cant precio y total esten dentro de rango
                        {

                            ticket = "";
                            max = (7 - cant.ToString().Length);

                            for (int i = 0; i < max; i++)                // **********
                            {
                                ticket += " ";                           // Agrega espacios para poner el valor de cantidad
                            }
                            ticket += cant.ToString();                   // agrega cantidad
                            //**************************************************************+
                            max = par1.Length;
                            if (max > 18)                                 // **********
                            {
                                cort = max - 18;
                                parte1 = par1.Remove(18, cort);          // corta a 16 la descripcion del articulo
                            }
                            else { parte1 = par1; }                      // **********
                            ticket += " " + parte1.ToString(); // agrega articulo

                            max = 15 - (total.ToString().Length);
                            for (int i = 0; i < max; i++)                // **********
                            {
                                ticket += " ";                           // Agrega espacios
                            }                                            // **********
                            ticket += total.ToString() + "\n"; // agrega total linea
                            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                        }
                        else
                        {
                            String formato = String.Format("<script>javascript:mensaje('VALORES FUERA DE RANGO');</script>");

                           // MessageBox.Show("Valores fuera de rango");
                            RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n"); // imprime texto
                        }
                    }
                    //***************************************+
                    public void CortaTicket()
                    {
                        string corte = "\x1B" + "m";                  // caracteres de corte
                        string avance = "\x1B" + "d" + "\x09";        // avanza 9 renglones
                        RawPrinterHelper.SendStringToPrinter(impresora, avance); // avanza
                        RawPrinterHelper.SendStringToPrinter(impresora, corte); // corta
                    }
                    public void AbreCajon()
                    {
                        string cajon0 = "\x1B" + "p" + "\x00" + "\x0F" + "\x96";                  // caracteres de apertura cajon 0
                        string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96";                 // caracteres de apertura cajon 1
                        RawPrinterHelper.SendStringToPrinter(impresora, cajon0); // abre cajon0
                        //RawPrinterHelper.SendStringToPrinter(impresora, cajon1); // abre cajon1
                    }
                }

                


        /*===============================================================================================================================================*/
        /* ================================================ METODOS TICKET 2 PARTE ===============================================================*/

                public class RawPrinterHelper
                {
                    // Structure and API declarions:
                    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                    public class DOCINFOA
                    {
                        [MarshalAs(UnmanagedType.LPStr)]
                        public string pDocName;
                        [MarshalAs(UnmanagedType.LPStr)]
                        public string pOutputFile;
                        [MarshalAs(UnmanagedType.LPStr)]
                        public string pDataType;
                    }
                    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

                    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool ClosePrinter(IntPtr hPrinter);

                    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

                    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool EndDocPrinter(IntPtr hPrinter);

                    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool StartPagePrinter(IntPtr hPrinter);

                    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool EndPagePrinter(IntPtr hPrinter);

                    [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
                    public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

                    // SendBytesToPrinter()
                    // When the function is given a printer name and an unmanaged array
                    // of bytes, the function sends those bytes to the print queue.
                    // Returns true on success, false on failure.
                    public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
                    {
                        Int32 dwError = 0, dwWritten = 0;
                        IntPtr hPrinter = new IntPtr(0);
                        DOCINFOA di = new DOCINFOA();
                        bool bSuccess = false; // Assume failure unless you specifically succeed.

                        di.pDocName = "My C#.NET RAW Document";
                        di.pDataType = "RAW";

                        // Open the printer.
                        if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                        {
                            // Start a document.
                            if (StartDocPrinter(hPrinter, 1, di))
                            {
                                // Start a page.
                                if (StartPagePrinter(hPrinter))
                                {
                                    // Write your bytes.
                                    bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                                    EndPagePrinter(hPrinter);
                                }
                                EndDocPrinter(hPrinter);
                            }
                            ClosePrinter(hPrinter);
                        }
                        // If you did not succeed, GetLastError may give more information
                        // about why not.
                        if (bSuccess == false)
                        {
                            dwError = Marshal.GetLastWin32Error();
                        }
                        return bSuccess;
                    }

                    public static bool SendStringToPrinter(string szPrinterName, string szString)
                    {
                        IntPtr pBytes;
                        Int32 dwCount;
                        // How many characters are in the string?
                        dwCount = szString.Length;
                        // Assume that the printer is expecting ANSI text, and then convert
                        // the string to ANSI text.
                        pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                        // Send the converted ANSI string to the printer.
                        SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                        Marshal.FreeCoTaskMem(pBytes);
                        return true;
                    }
                }

               

                public void p_actualizar_cantidad(String cod, Double cantidad, Double precio)
                {
                    DataTable dt = (DataTable)Session["detalleBien"];
                    DataRow row;
                    row = dt.Rows.Find(cod);
                    row.BeginEdit();
                    row["PRECIO"] = precio;
                    row["CANT"] = cantidad;
                    row["IMPORTE"] = Convert.ToDouble(row["PRECIO"]) * Convert.ToDouble(row["CANT"]);
                    row.EndEdit();
                    row.AcceptChanges();
                    LLENAR_GRILLA();
                }
                
                //protected void dgvBIEN_VENTA_RowEditing(object sender, GridViewEditEventArgs e)
                //{
                //    dgvBIEN_VENTA.EditIndex = e.NewEditIndex;
                //    LLENAR_GRILLA();
                //}

                protected void dgvBIEN_VENTA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
                {
                    dgvBIEN_VENTA.EditIndex = -1;
                    LLENAR_GRILLA();
                }

                protected void dgvBIEN_VENTA_RowUpdating(object sender, GridViewUpdateEventArgs e)
                {
                    String cod = dgvBIEN_VENTA.Rows[e.RowIndex].Cells[1].Text.ToString();
                    TextBox txtcant = (TextBox)dgvBIEN_VENTA.Rows[e.RowIndex].FindControl("txtcan");
                    TextBox txtprecc = (TextBox)dgvBIEN_VENTA.Rows[e.RowIndex].FindControl("txtprec");
                    Double cantidad = Convert.ToDouble(txtcant.Text);
                    Double precio = Convert.ToDouble(txtprecc.Text);
                    p_actualizar_cantidad(cod, cantidad,precio);
                    dgvBIEN_VENTA.EditIndex = -1;
                    LLENAR_GRILLA();
                    ACTUALIZAR_TOTALES();
                }

                protected void dgvBIEN_VENTA_RowDeleting(object sender, GridViewDeleteEventArgs e)
                {
                    String cod = dgvBIEN_VENTA.Rows[e.RowIndex].Cells[1].Text.ToString();
                    Eliminar_Registro(cod);
                    dgvBIEN_VENTA.EditIndex = -1;
                    LLENAR_GRILLA();
                    ACTUALIZAR_TOTALES();
                }

                public void Eliminar_Registro(String cod)
                {
                    DataTable dt = (DataTable)Session["detalleBien"];
                    DataRow row;
                    row = dt.Rows.Find(cod);
                    row.Delete();
                    row.AcceptChanges();
                    LLENAR_GRILLA();
                    ACTUALIZAR_TOTALES();
                }

                

                protected void dgvBIEN_VENTA_PageIndexChanging(object sender, GridViewPageEventArgs e)
                {
                    dgvBIEN_VENTA.PageIndex = e.NewPageIndex;
                    LLENAR_GRILLA();
                }

                protected void cboTIPO_DOC_SelectedIndexChanged(object sender, EventArgs e)
                {
                    Session["INDICE_TIPODOC"] = cboTIPO_DOC.SelectedIndex;
                }

                protected void btnVUELTO_Click(object sender, EventArgs e)
                {
                    if (txtPAGA.Text.ToString() != string.Empty)
                    {
                        if (Convert.ToDouble(txtPAGA.Text) >= Convert.ToDouble(lblTOTAL.Text))
                        {
                            double TOTAL = Convert.ToDouble(lblTOTAL.Text);
                            double VUELTO = Convert.ToDouble(txtPAGA.Text);
                            txtVUELTO.Text = Convert.ToString(Server.HtmlEncode(Convert.ToDouble(VUELTO - TOTAL).ToString("N2")));
                        }
                        else
                        {
                            Response.Write("<script>window.alert('INGRESAR UN MONTO MAYOR AL MONTO TOTAL');</script>");
                        }
                    }
                    

                }

                

                protected void btnBUSCAR_BIEN_Click1(object sender, ImageClickEventArgs e)
                {
                    Session["LLAMABIEN"] = "1";
                    Response.Redirect("FRM_CONSULTA_BIEN.aspx");
                    
                }

                protected void btnBUSCAR_CLIENTE_Click(object sender, EventArgs e)
                {
                    Session["LLAMABIEN"] = "1";
                    Response.Redirect("FRM_MANTENIMIENTO_CLIENTE.aspx");
                }

                protected void cboTIPO_PAGO_SelectedIndexChanged(object sender, EventArgs e)
                {
                    if(cboTIPO_PAGO.SelectedItem.Text=="EFECTIVO") //SI SE SELECCIONA PAGO EN EFECTIVO EL TEXBOX SE DEBE BLOQUEAR YA Q NO NECESITO INGRESAR NADA COMO DATO
                    {
                        txtTIPO_PAGO.ReadOnly = true;
                    }
                    else   //DE LO CONTRARIO SI SE REQUIERE INGRESAR DATOS EN EL TEXBOX PARA DEFINIR LA OPERACION REALIZADA
                    {
                        txtTIPO_PAGO.ReadOnly = false;
                    }
                }

                protected void btnBIEN17_Click(object sender, EventArgs e)
                {
                    if (idbien[16] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[16].ToString(), valor[16].ToString(), PRECIO_BIEN[16].ToString());
                    }
                }

                protected void btnBIEN18_Click(object sender, EventArgs e)
                {
                    if (idbien[17] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[17].ToString(), valor[17].ToString(), PRECIO_BIEN[17].ToString());
                    }
                }

                protected void btnBIEN19_Click(object sender, EventArgs e)
                {
                    if (idbien[18] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[18].ToString(), valor[18].ToString(), PRECIO_BIEN[18].ToString());
                    }
                }

                protected void btnBIEN20_Click(object sender, EventArgs e)
                {
                    if (idbien[19] != null)
                    {
                        OBTENER_ID_BIEN_Y_LLENAR_GRILLA(idbien[19].ToString(), valor[19].ToString(), PRECIO_BIEN[19].ToString());
                    }
                }

                protected void dgvBIEN_VENTA_SelectedIndexChanged(object sender, EventArgs e)
                {

                }

                

                

                

                

                
                

                

                

                

                

                



        /* ================================================ FIN METODO PARA GENERAR TICKET ===============================================================*/
        /* ================================================ FIN METODO PARA GENERAR TICKET ===============================================================*/

    }
}