using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;

namespace DIONYS_ERP
{
    public partial class FRM_REIMPRESIONES : System.Web.UI.Page
    {
        public String MON = "";
        public String WEB = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //CON ESTO PUEDO VERIFICO SI TENGO UNA CAJA ABIERTA  Y SINO ES ASI, ABRIR UNA CAJA NUEVA
                if (Session["ID_CAJA"].ToString() == string.Empty)
                {
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }

                LISTA_VENTAS_ANULADAS_ACTIVAS("1");
            }
        }

        #region MyRegion
        N_VENTA OBJVENTA = new N_VENTA();
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();
        E_VENTA_Y_DETALLE E_OBJVENTA = new E_VENTA_Y_DETALLE();
        E_CAJA_KARDEX E_OBJ_CAJAKARDEX = new E_CAJA_KARDEX();
        #endregion

        void LISTA_VENTAS_ANULADAS_ACTIVAS(string VER)
        {
            DataTable dt = new DataTable();
            string SERIE = Session["SERIE"].ToString();
            string SEDE=Session["SEDE"].ToString();
            string IDCAJA = Session["ID_CAJA"].ToString();

            string OPCION_BUSQUEDA;
            if(cboV_OPCIONBUSCAR.SelectedIndex==1)
            {
                OPCION_BUSQUEDA = "2"; //BUSQUEDA POR ID_VENTA
                
            }
            else
            {
                if(cboV_OPCIONBUSCAR.SelectedIndex==2)
                {
                    OPCION_BUSQUEDA = "3";
                }
                else
                {
                    OPCION_BUSQUEDA = "1";
                }
            }

            string DATO = txtBUSCARVENTA.Text;

            dt = OBJVENTA.LISTADO_VENTAS_ACTIVAS_ANULADAS(SERIE, SEDE, VER, OPCION_BUSQUEDA, DATO, IDCAJA);
            dgvLISTADOVENTAS.DataSource = dt;
            dgvLISTADOVENTAS.DataBind();
        }

        protected void btnBUSCARVENTA_Click(object sender, EventArgs e)
        {

            if (rdbLISTAOPCIONES.SelectedIndex == 0)
            {
                LISTA_VENTAS_ANULADAS_ACTIVAS("1");
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 1)
            {
                LISTA_VENTAS_ANULADAS_ACTIVAS("2");
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 2)
            {
                LISTA_VENTAS_ANULADAS_ACTIVAS("3");
            }

            Session.Add("EJEMPLO", txtBUSCARVENTA.Text);
        }

        protected void rdbLISTAOPCIONES_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rdbLISTAOPCIONES.SelectedIndex == 0)
            {
                LISTA_VENTAS_ANULADAS_ACTIVAS("1");
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 1)
            {
                LISTA_VENTAS_ANULADAS_ACTIVAS("2");
            }
            if (rdbLISTAOPCIONES.SelectedIndex == 2)
            {
                LISTA_VENTAS_ANULADAS_ACTIVAS("3");
            }


        }
        

        protected void dgvLISTADOVENTAS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdIMPRIMIR")
            {
                if(rdbLISTAOPCIONES.SelectedIndex == 0){
                GridViewRow fila = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string ID_VENTA = dgvLISTADOVENTAS.DataKeys[fila.RowIndex].Values[0].ToString();
               // P_IMPRIMIR(ID_VENTA);
                IMPRIMIR_SPOOL(ID_VENTA); //IMPRIME EL SPOOL GUARDADO
                }

                if (rdbLISTAOPCIONES.SelectedIndex == 2)
                {
                    GridViewRow fila = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    if (dgvLISTADOVENTAS.DataKeys[fila.RowIndex].Values[4].ToString() == string.Empty)
                    {
                        string ID_VENTA = dgvLISTADOVENTAS.DataKeys[fila.RowIndex].Values[0].ToString();
                        //P_IMPRIMIR(ID_VENTA);
                        IMPRIMIR_SPOOL(ID_VENTA); //IMPRIME EL SPOOL GUARDADO
                    }



                }
            }
            if((txtCLAVE_AUTO.Text.ToString() != string.Empty) && (txtCLAVE_AUTO.Text.ToString() == "CODDIONYS2017"))
            {
                    if(e.CommandName =="cmdANULAR")
                    {
                        if(rdbLISTAOPCIONES.SelectedIndex == 0)
                        {
                            GridViewRow fila = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                            string ID_VENTA = dgvLISTADOVENTAS.DataKeys[fila.RowIndex].Values[0].ToString();

                            //ESTA FUNCION SOLO ANULA UNA TRANSACCION EN CAJA KARDEX POR VENTA POR ESO LE MANDO EL ID_VENTA;
                            //FALTA ANALIZAR SI ESTA VENTA TIENE MUCHAS AMORTIZACIONES EN CAJA KARDEX, EN ESE CASO TENDRIA QUE ANULAR TODAS ELLAS CON UN FOR
                            ANULAR_VENTA_CAJAKARDEX( ID_VENTA);//ANULANDO EL INGRESO EN CAJA DE ESA VENTA
                            // ===========================================================================================

                            OBJVENTA.ELIMINAR_VENTA(ID_VENTA); //ANULANDO LA VENTA 
                            LISTA_VENTAS_ANULADAS_ACTIVAS("1"); //LAMANDO A MI STORE PARA LISTAR LAS VENTAS ACTIVAS
                    
                        }
                        if (rdbLISTAOPCIONES.SelectedIndex == 2)
                        {
                            GridViewRow fila = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                            if(dgvLISTADOVENTAS.DataKeys[fila.RowIndex].Values[4].ToString() == string.Empty){ //AQUI VERIFICO SI EL CAMPO DE FECHA DE ANULADO ESTA VACIO ES DECIR NO ESTA ANULADO                           
                                    string ID_VENTA = dgvLISTADOVENTAS.DataKeys[fila.RowIndex].Values[0].ToString();

                                    //ESTA FUNCION SOLO ANULA UNA TRANSACCION EN CAJA KARDEX POR VENTA POR ESO LE MANDO EL ID_VENTA;
                                    //FALTA ANALIZAR SI ESTA VENTA TIENE MUCHAS AMORTIZACIONES EN CAJA KARDEX, EN ESE CASO TENDRIA QUE ANULAR TODAS ELLAS CON UN FOR
                                    ANULAR_VENTA_CAJAKARDEX(ID_VENTA);//ANULANDO EL INGRESO EN CAJA DE ESA VENTA
                                    // ===========================================================================================

                                    OBJVENTA.ELIMINAR_VENTA(ID_VENTA);
                                    LISTA_VENTAS_ANULADAS_ACTIVAS(Convert.ToString(rdbLISTAOPCIONES.SelectedIndex + 1));
                            }
                        }
                    }
            }

            
        }


        public void ANULAR_VENTA_CAJAKARDEX( string ID_VENTA)
        {
                string ID_MOV_CAJAKARDEX;
                string FECHA_ANULADO;

                DataTable DT=new DataTable();
                    DT =OBJVENTA.OBTENER_ID_COMPVENT_CAJAKARDEX(ID_VENTA,"I");
                    FECHA_ANULADO = DT.Rows[0]["FECHA_ANULADO"].ToString();
                    ID_MOV_CAJAKARDEX = DT.Rows[0]["ID_MOVIMIENTO"].ToString();

                    if (FECHA_ANULADO == string.Empty) //si es vacio es porque el movimiento de caja kardex aun no se anulado
                    {
                             E_OBJ_CAJAKARDEX.ID_MOVIMIENTO = ID_MOV_CAJAKARDEX;
                             E_OBJ_CAJAKARDEX.DESCRIPCION = string.Empty;
                             E_OBJ_CAJAKARDEX.ID_COMPVENT = string.Empty;
                             E_OBJ_CAJAKARDEX.ID_TIPOMOV = string.Empty;
                             E_OBJ_CAJAKARDEX.ID_TIPOPAGO = string.Empty;
                             E_OBJ_CAJAKARDEX.IMPORTE = 0.00;
                             E_OBJ_CAJAKARDEX.MONEDA = "D";
                             E_OBJ_CAJAKARDEX.TIPO_CAMBIO = 0.00;
                             E_OBJ_CAJAKARDEX.AMORTIZADO = 0.00;
                             E_OBJ_CAJAKARDEX.ID_CAJA = string.Empty;
                             E_OBJ_CAJAKARDEX.IMPORTE_CAJA = 0.00;
                             E_OBJ_CAJAKARDEX.OPCION = 2;
                             OBJVENTA.CAJA_KARDEX_MANTENIMIENTO(E_OBJ_CAJAKARDEX);
                    }
                 
        }

/*==================================================================================================================================================================*/
        void IMPRIMIR_SPOOL(string ID_VENTA)
        {
            DataTable DATOS_VENTA = new DataTable();                         //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA

            DATOS_VENTA = OBJVENTA.CAPTURAR_TABLA_VENTA(ID_VENTA, Session["SEDE"].ToString());        //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO

            DataTable DATOS_VENTADETALLE = new DataTable();                  //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA_DETALLE

            DATOS_VENTADETALLE = OBJVENTA.CAPTURAR_TABLA_VENTADETALLE(ID_VENTA); //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO


            //LIMPIANDO MI SPOOL SI ESQUE UBIERA IMPRESIONES PENDIENTES
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "2");
            // ========================================================================================
            

            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][36].ToString(),"1"); //aqui va el nombre de la empresa
            //Ticket1.TextoCentro(DATOS_VENTA.Rows[0][40].ToString());        //aqui va la direccion de la empresa


            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"RUC: " + DATOS_VENTA.Rows[0][37].ToString(),"1");              //aqui va el ruc de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA EMPRESA
            //OBJVENTA.SPOOL_ETIQUETERA(DATOS_VENTA.Rows[0]["U_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["U_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["U_UBIDEN"].ToString());   
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                        // imprime una linea de guiones
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][28].ToString(),"1"); //aqui va el nombre de la sede de la empresa 
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][29].ToString(),"1"); //aqui va la direccion de la sede de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA SEDE
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0]["S_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIDEN"].ToString(),"1");
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                        // imprime una linea de guiones
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"MAQ REG : " + DATOS_VENTA.Rows[0][48].ToString(),"1");          //AQUI SE COLOCA EL NOMBRE DE LA MAQUINA REGISTRADORA
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][4].ToString(),"1");   //aqui va la fecha

            string TIP_DOC;
            TIP_DOC = DATOS_VENTA.Rows[0][3].ToString();/* AQUI BA EL NOMBRE  DEL TIPO DE DOCUMENTO */

            //P_SERIE_Y_NUMERO_CORRELATIVO_POR_PTOVENTA(TIP_DOC, CBOPTOVENTA.Text);
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"TICKET: " + TIP_DOC + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString(),"1"); //aqui va el tipo_documento / el numero de serie / y el numero correlativo
            if (DATOS_VENTA.Rows[0]["V_ID_CLIENTE"] != DBNull.Value)   //ESTO ME PERMITE IMPRIMIR LOS DATOS CLIENTES SI ESQUE EXISTIERA UN CLIENTE
            {
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                         // imprime una linea de guiones
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CLIENTE: " + DATOS_VENTA.Rows[0]["C_DESCRIPCION"].ToString(),"1"); //OBTENIENDO EL NOMBRE DEL CLIENTE
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"RUC/DNI: " + DATOS_VENTA.Rows[0]["C_RUC_DNI"].ToString(),"1"); //OBTENIENDO EL RUC DEL CLIENTE
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0]["C_DIRECCION"].ToString(),"1"); //OBTENIENDO LA DIRECCION DEL CLIENTE
                //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DEL CLIENTE
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0]["C_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIDEN"].ToString(),"1");

            }
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"-","1");

            //DGVPEDIDO["MONEDA", fila].Value.ToString();

            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CANT   DETALLE                IMPORTE","1");
            for (int i = 0; i < DATOS_VENTADETALLE.Rows.Count; i++)
            {
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString()," " + DATOS_VENTADETALLE.Rows[i][3].ToString() + "   " + DATOS_VENTADETALLE.Rows[i][7].ToString()+"    " + DATOS_VENTADETALLE.Rows[i][5].ToString(),"1");
            }

            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"-","1");

            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "SUBTOTAL:   " + DATOS_VENTA.Rows[0][6].ToString(), "1"); //obtenemos el sub_total
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "IGV:       " + DATOS_VENTA.Rows[0][7].ToString(), "1");  //obtenemos el igv
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL:      " + DATOS_VENTA.Rows[0][8].ToString(), "1"); //obtenemos el total
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty,"1");
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"P.V: " + Session["PUNTOVENTA"].ToString(),"1"); // obtenemos el punto de venta
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CAJERO: " + DATOS_VENTA.Rows[0][28].ToString(),"1"); //obtenemos la descripcion del cajero



            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][41].ToString(),"1"); //aqui obtenemos el email de la empresa
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][42].ToString(),"1"); //aqui obtenemos la pagina web de la empresa

            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"-","1");
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString(),"1"); //obtenemos la descripcion del cajero
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"AGRADECEMOS SU PREFERENCIA!!!","1"); // imprime en el centro "Venta mostrador"
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"VUELVA PRONTO!! LO ESPERAMOS!!","1");
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty,"1");
            if (DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString() != string.Empty)
            {
                OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString(),"1");
            }
            OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1");

            //METODO PARA EMITIR TICKET INDIVIDUALES POR PRODUCTO QUE ESTAN CONFIGURADOS EN LA TABLA BIEN

            //for (int f = 0; f < DATOS_VENTADETALLE.Rows.Count; f++)
            //{
            //    if (DATOS_VENTADETALLE.Rows[f]["B_EMITE_TICKET"].Equals(true))
            //    {

            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][36].ToString(),"1"); //aqui va el nombre de la empresa
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][28].ToString(),"1"); //nombre de la sede
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"TICKET DESPACHO","1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"REFERENCIA: " + DATOS_VENTA.Rows[0][3].ToString() + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString(),"1"); //aqui va el tipo_documento / el numero de serie / y el numero correlativo
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),string.Empty,"1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"**" + DATOS_VENTADETALLE.Rows[f]["VD_CANTIDAD"].ToString() + "**","1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTADETALLE.Rows[f]["B_DESCRIPCION"].ToString(),"1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString()," ","1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString(),"1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),DATOS_VENTA.Rows[0][4].ToString() + "     " + DATOS_VENTA.Rows[0][0].ToString(),"1");   //aqui va la fecha
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString(),"1"); //obtenemos el id_venta
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"AGRADECEMOS SU PREFERENCIA!!!","1");
            //        OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(),"CORTATICKET","1");
            //    }
            //}


            //=============================================================================================

        }


        /*==============IMPRIMIR =============================*/
        void P_IMPRIMIR(string ID_VENTA)
        {
            DataTable DATOS_VENTA = new DataTable();                         //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA

            DATOS_VENTA = OBJVENTA.CAPTURAR_TABLA_VENTA(ID_VENTA, Session["SEDE"].ToString());        //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO

            DataTable DATOS_VENTADETALLE = new DataTable();                  //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA_DETALLE

            DATOS_VENTADETALLE = OBJVENTA.CAPTURAR_TABLA_VENTADETALLE(ID_VENTA); //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO

           
            //string cantidad;
            //string descripcion;
            //string total;
            //double totalLinea = 90.51;

            CreaTicket Ticket1 = new CreaTicket();
            Ticket1.impresora = "BIXOLON SRP-270";

            Ticket1.TextoCentro(DATOS_VENTA.Rows[0][36].ToString()); //aqui va el nombre de la empresa
            //Ticket1.TextoCentro(DATOS_VENTA.Rows[0][40].ToString());        //aqui va la direccion de la empresa
            

            Ticket1.TextoCentro("RUC: " + DATOS_VENTA.Rows[0][37].ToString());              //aqui va el ruc de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA EMPRESA
            //Ticket1.TextoCentro(DATOS_VENTA.Rows[0]["U_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["U_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["U_UBIDEN"].ToString());   
            Ticket1.LineasGuion();                          // imprime una linea de guiones
            Ticket1.TextoCentro(DATOS_VENTA.Rows[0][28].ToString()); //aqui va el nombre de la sede de la empresa 
            Ticket1.TextoCentro(DATOS_VENTA.Rows[0][29].ToString()); //aqui va la direccion de la sede de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA SEDE
            Ticket1.TextoCentro(DATOS_VENTA.Rows[0]["S_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIDEN"].ToString());
            Ticket1.LineasGuion();                          // imprime una linea de guiones
            Ticket1.TextoCentro("MAQ REG : " + DATOS_VENTA.Rows[0][48].ToString());          //AQUI SE COLOCA EL NOMBRE DE LA MAQUINA REGISTRADORA
            Ticket1.TextoCentro(DATOS_VENTA.Rows[0][4].ToString());   //aqui va la fecha

            string TIP_DOC;
            TIP_DOC = DATOS_VENTA.Rows[0][3].ToString();/* AQUI BA EL NOMBRE  DEL TIPO DE DOCUMENTO */

            //P_SERIE_Y_NUMERO_CORRELATIVO_POR_PTOVENTA(TIP_DOC, CBOPTOVENTA.Text);
            Ticket1.TextoCentro("Ticket: " + TIP_DOC + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString()); //aqui va el tipo_documento / el numero de serie / y el numero correlativo
            if (DATOS_VENTA.Rows[0]["V_ID_CLIENTE"] != DBNull.Value)   //ESTO ME PERMITE IMPRIMIR LOS DATOS CLIENTES SI ESQUE EXISTIERA UN CLIENTE
            {
                Ticket1.LineasGuion();                          // imprime una linea de guiones
                Ticket1.TextoCentro("CLIENTE: "+DATOS_VENTA.Rows[0]["C_DESCRIPCION"].ToString()); //OBTENIENDO EL NOMBRE DEL CLIENTE
                Ticket1.TextoCentro("RUC/DNI: "+DATOS_VENTA.Rows[0]["C_RUC_DNI"].ToString()); //OBTENIENDO EL RUC DEL CLIENTE
                Ticket1.TextoCentro(DATOS_VENTA.Rows[0]["C_DIRECCION"].ToString()); //OBTENIENDO LA DIRECCION DEL CLIENTE
                //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DEL CLIENTE
                Ticket1.TextoCentro(DATOS_VENTA.Rows[0]["C_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIDEN"].ToString());
           
            }
            Ticket1.LineasGuion();

            //DGVPEDIDO["MONEDA", fila].Value.ToString();

            Ticket1.TextoIzquierda("CANT   DETALLE                IMPORTE");
            for (int i = 0; i < DATOS_VENTADETALLE.Rows.Count; i++)
            {
                Ticket1.TextoExtremos(" " + DATOS_VENTADETALLE.Rows[i][3].ToString() + "   " + DATOS_VENTADETALLE.Rows[i][7].ToString(), MON + DATOS_VENTADETALLE.Rows[i][5].ToString());
            }

            Ticket1.LineasTotales();

            Ticket1.TextoExtremos("SUBTOTAL:", MON + DATOS_VENTA.Rows[0][6].ToString()); //obtenemos el sub_total
            Ticket1.TextoExtremos("IGV: ", MON + DATOS_VENTA.Rows[0][7].ToString());  //obtenemos el igv
            Ticket1.TextoExtremos("TOTAL: ", MON + DATOS_VENTA.Rows[0][8].ToString()); //obtenemos el total
            Ticket1.TextoCentro(" ");
            Ticket1.TextoCentro("P.V: " + DATOS_VENTA.Rows[0][47].ToString()); // obtenemos el punto de venta
            Ticket1.TextoCentro("CAJERO: " + DATOS_VENTA.Rows[0][28].ToString()); //obtenemos la descripcion del cajero
            


            Ticket1.TextoCentro(DATOS_VENTA.Rows[0][41].ToString()); //aqui obtenemos el email de la empresa
            Ticket1.TextoCentro(DATOS_VENTA.Rows[0][42].ToString()); //aqui obtenemos la pagina web de la empresa

            Ticket1.LineasGuion();
            Ticket1.TextoCentro("ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString()); //obtenemos la descripcion del cajero
            Ticket1.TextoCentro("AGRADECEMOS SU PREFERENCIA!!!"); // imprime en el centro "Venta mostrador"
            Ticket1.TextoCentro("VUELVA PRONTO!! LO ESPERAMOS!!");
            Ticket1.TextoCentro(" ");
            if (DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString() != string.Empty)
            {
                Ticket1.TextoCentro("ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString());
            }
            Ticket1.CortaTicket();

            //METODO PARA EMITIR TICKET INDIVIDUALES POR PRODUCTO QUE ESTAN CONFIGURADOS EN LA TABLA BIEN
            
            for (int f = 0; f < DATOS_VENTADETALLE.Rows.Count;f++)
            {
                if(DATOS_VENTADETALLE.Rows[f]["B_EMITE_TICKET"].Equals(true))
                {

                    Ticket1.TextoCentro(DATOS_VENTA.Rows[0][36].ToString()); //aqui va el nombre de la empresa
                    Ticket1.TextoCentro(DATOS_VENTA.Rows[0][28].ToString()); //nombre de la sede
                    Ticket1.LineasGuion();
                    Ticket1.TextoCentro("TICKET DESPACHO");
                    Ticket1.TextoCentro("REFERENCIA: " + DATOS_VENTA.Rows[0][3].ToString() + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString()); //aqui va el tipo_documento / el numero de serie / y el numero correlativo
                    Ticket1.LineasGuion();
                    Ticket1.TextoCentro(" ");
                    Ticket1.TextoCentro("**" + DATOS_VENTADETALLE.Rows[f]["VD_CANTIDAD"].ToString() + "**");
                    Ticket1.TextoCentro(DATOS_VENTADETALLE.Rows[f]["B_DESCRIPCION"].ToString());
                    Ticket1.TextoCentro(" ");
                    Ticket1.LineasGuion();
                    Ticket1.TextoCentro("ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString());
                    Ticket1.TextoIzquierda(DATOS_VENTA.Rows[0][4].ToString() + "     " + DATOS_VENTA.Rows[0][0].ToString());   //aqui va la fecha
                    Ticket1.TextoDerecha("ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString()); //obtenemos el id_venta
                    Ticket1.TextoCentro("AGRADECEMOS SU PREFERENCIA!!!");
                    Ticket1.CortaTicket();
                }
            }


            //=============================================================================================

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

        
/*==================================================================================================================================================================*/
        


    }
}

