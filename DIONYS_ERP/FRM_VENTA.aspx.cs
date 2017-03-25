using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.HtmlReportRender;

namespace DIONYS_ERP
{
    public partial class FRM_VENTA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["ID_CAJA"].ToString() == string.Empty)
                {
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }
                //si la empresa se logea entonces selecciono como primeras opciones los recibos
                if (Session["ID_EMPRESA"].ToString() == "004")
                {
                    cboV_TIPO_DOC.SelectedIndex = 2; //debe estar seleccionado el documento recibo
                    cboV_SERIE.SelectedIndex = 10; //debe estar seleccionado la opcion 0000 como serie
                    ESTADO_TRANSACCION(1);//CON ESTO COLOCO EL FORMULARIO EN ESTADO DE CONSULTA
                }
                else
                {
                    ESTADO_TRANSACCION(1);//CON ESTO COLOCO EL FORMULARIO EN ESTADO DE CONSULTA
                }
                // ============================================================================

                if (Session["ID_VENTA"].ToString() == string.Empty)
                {
                    DataTable DT = new DataTable();
                    DT = N_OBJVENTA.OBTENER_ULTIMO_REGISTRO_VENTA(Session["SEDE"].ToString(), Session["SERIE"].ToString()); //AQUI OBTENEMOS LOS DATOS DEL ULTIMO REGISTRO DE VENTA GENERADO
                    if (DT.Rows.Count > 0)
                    {
                        Session["ID_VENTA"] = DT.Rows[0]["V_ID_VENTA"].ToString(); //AQUI GUARDO EL DATO ID_VENTA PARA PODER CONSULTAR_VENTA
                        CONSULTAR_VENTA(DT.Rows[0]["V_ID_VENTA"].ToString(),Session["SEDE"].ToString());
                    }
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = N_OBJVENTA.OBTENER_ULTIMO_REGISTRO_VENTA(Session["SEDE"].ToString(), Session["SERIE"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        CONSULTAR_VENTA(Session["ID_VENTA"].ToString(), Session["SEDE"].ToString());
                    }
                }
            }
           
        }

        #region DECLARACION DE OBJETOS
        E_VENTA_DETALLADA E_OBJVENTA = new E_VENTA_DETALLADA();
        N_VENTA N_OBJVENTA = new N_VENTA();
        #endregion

        #region PROCEDIMIENTOS
        

        void  CONSULTAR_VENTA(string ID_VENTA,string ID_SEDE)
        {
            DataTable DT = new DataTable();
            DT = N_OBJVENTA.CAPTURAR_TABLA_VENTA(ID_VENTA,ID_SEDE);
            
            if(DT.Rows.Count > 0 )
            {
                //RESULTADOS PUESTOS 
                txtV_IDVENTA.Text = DT.Rows[0]["V_ID_VENTA"].ToString();
                cboV_TIPO_DOC.SelectedValue=DT.Rows[0]["V_TIPO_DOC"].ToString();
                cboV_SERIE.SelectedItem.Text=DT.Rows[0]["V_SERIE"].ToString();
                txtV_NUMERO.Text=DT.Rows[0]["V_NUMERO"].ToString();
                txtV_IDCLIENTE.Text =DT.Rows[0]["V_ID_CLIENTE"].ToString();
                txtV_CLIENTEDESCRIPCION.Text=DT.Rows[0]["C_DESCRIPCION"].ToString();
                txtV_RUC.Text = DT.Rows[0]["C_RUC_DNI"].ToString();
                txtV_DIRECCION.Text = DT.Rows[0]["C_DIRECCION"].ToString();
                txtV_FECHA.Text = DT.Rows[0]["V_FECHA"].ToString();
                txtV_FECHAANULADO.Text=DT.Rows[0]["V_FECHA_ANULADO"].ToString();
                rdbMONEDA.SelectedValue=DT.Rows[0]["V_MONEDA"].ToString();
                txtV_CLIENTE_OPCIONAL.Text=DT.Rows[0]["V_CLIENTE"].ToString();
                txtV_IDPEDIDO.Text=DT.Rows[0]["V_ID_PEDIDO"].ToString();
                txtV_SUBTOTAL.Text=DT.Rows[0]["V_VALOR_VENTA"].ToString();
                txtV_IGV.Text=DT.Rows[0]["V_IGV"].ToString();
                txtV_TOTAL.Text = DT.Rows[0]["V_TOTAL"].ToString();

                OBTENER_ULTIMO_REGISTRO_DETALLE(ID_VENTA); // CON ESTO RECUPERO LOS DATOS DE LA GRILLA Y LO LLENO EN MI GRILLA
            }
            
        }
        void OBTENER_ULTIMO_REGISTRO_DETALLE(string ID_VENTA)
        {
            DataTable DT = new DataTable();
            DT = N_OBJVENTA.CAPTURAR_TABLA_VENTADETALLE(ID_VENTA);
            //RESULTADOS PUESTOS 
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string ID_BIEN = DT.Rows[i]["VD_ID_BIEN"].ToString();
                string DESCRIPCION = DT.Rows[i]["B_DESCRIPCION"].ToString();
                double PRECIO = Convert.ToDouble(DT.Rows[i]["VD_CANTIDAD"].ToString());
                double CANTIDAD = Convert.ToDouble(DT.Rows[i]["VD_PRECIO"].ToString());

                OBTENER_ID_BIEN_Y_LLENAR_GRILLA(ID_BIEN, DESCRIPCION, PRECIO, CANTIDAD);
            }

        }
       


        void ESTADO_TRANSACCION(int ESTADO)
        {
            if (ESTADO == 1) //ESTADO CONSULTA
            {

                //LIMPIARDO CONTROLES

                cboV_TIPO_DOC.SelectedIndex = 0;
                cboV_SERIE.SelectedIndex = 0;
                txtV_NUMERO.Text = string.Empty;
                txtV_FECHA.Text = string.Empty;
                txtV_IDVENTA.Text = string.Empty;
                txtV_IDCLIENTE.Text = string.Empty;
                txtV_CLIENTEDESCRIPCION.Text = string.Empty;
                txtV_RUC.Text = string.Empty;
                rdbMONEDA.SelectedIndex = -1;
                txtV_DIRECCION.Text = string.Empty;
                txtV_CLIENTE_OPCIONAL.Text = string.Empty;
                txtV_IDPEDIDO.Text = string.Empty;
                txtPRODUCTO.Text = string.Empty;
                txtPRODUCTO_DESCRIPCION.Text = string.Empty;
                txtPRECIO.Text = string.Empty;
                txtCANTIDAD.Text = string.Empty;
                txtV_TOTAL.Text = string.Empty;
                txtV_IGV.Text = string.Empty;
                txtV_SUBTOTAL.Text = string.Empty;
                DataTable dt = (DataTable)Session["detalleBien"];
                dt.Clear();

                LLENAR_GRILLA();
                dgvDETALLE_VENTA.Enabled = false;

                //====================

                cboV_TIPO_DOC.Enabled = false;
                cboV_SERIE.Enabled = false;
                txtV_NUMERO.ReadOnly = true;
                if (Session["ID_EMPRESA"].ToString() == "004") //SI LA EMPRESA EN GALERIA ENTONCES SE ADAPTA UNA NUEVA PLANTILLA PARA ESTA EMPRESA 
                {
                    lblFECHA.Text = "FECHA VENCIMIENTO";
                    txtV_FECHA.ReadOnly = true;
                }
                else
                {
                    lblFECHA.Text = "FECHA";
                    txtV_FECHA.ReadOnly = true;
                }
                txtV_FECHAANULADO.ReadOnly = true;
                txtV_IDCLIENTE.ReadOnly = true;
                txtV_CLIENTEDESCRIPCION.ReadOnly = true;
                txtV_RUC.ReadOnly = true;
                rdbMONEDA.Enabled = false;
                txtV_DIRECCION.ReadOnly = true;
                btnV_NUEVO.Enabled = true;
                btnV_GRABAR.Enabled = false;
                btnV_CANCELAR.Enabled = false;
                btnV_ANULAR.Enabled = true;
                btnV_IMPRIMIR.Enabled = true;
                btnV_CONSULTAR.Enabled = true;
                txtV_CLIENTE_OPCIONAL.ReadOnly = true;
                txtV_IDPEDIDO.ReadOnly = true;
                txtPRODUCTO.ReadOnly = true;
                txtPRODUCTO_DESCRIPCION.ReadOnly = true;
                txtPRECIO.ReadOnly = true;
                txtCANTIDAD.ReadOnly = true;
                btnV_AGREGARPRODUCTO.Enabled = false;
                txtV_TOTAL.ReadOnly = true;
                txtV_IGV.ReadOnly = true;
                txtV_SUBTOTAL.ReadOnly = true;
                dt.Clear();

                LLENAR_GRILLA();
                dgvDETALLE_VENTA.Enabled = true;

            }
            if (ESTADO == 2) //ESTADO NUEVO
            {
                //LIMPIARDO CONTROLES

                cboV_TIPO_DOC.SelectedIndex = 0;
                cboV_SERIE.SelectedIndex = 0;
                txtV_NUMERO.Text = string.Empty;
                txtV_FECHA.Text = string.Empty;
                txtV_IDVENTA.Text = string.Empty;
                txtV_IDCLIENTE.Text = string.Empty;
                txtV_CLIENTEDESCRIPCION.Text = string.Empty;
                txtV_RUC.Text = string.Empty;
                rdbMONEDA.SelectedIndex = 0;
                txtV_DIRECCION.Text = string.Empty;
                txtV_CLIENTE_OPCIONAL.Text = string.Empty;
                txtV_IDPEDIDO.Text = string.Empty;
                txtPRODUCTO.Text = string.Empty;
                txtPRODUCTO_DESCRIPCION.Text = string.Empty;
                txtPRECIO.Text = string.Empty;
                txtCANTIDAD.Text = string.Empty;
                txtV_TOTAL.Text = string.Empty;
                txtV_IGV.Text = string.Empty;
                txtV_SUBTOTAL.Text = string.Empty;
                DataTable dt = (DataTable)Session["detalleBien"];
                dt.Clear();

                LLENAR_GRILLA();
                dgvDETALLE_VENTA.Enabled = false;

                //====================

                cboV_TIPO_DOC.Enabled = true;
                cboV_SERIE.Enabled = true;
                txtV_NUMERO.ReadOnly = true;
                if (Session["ID_EMPRESA"].ToString() == "004") //SI LA EMPRESA EN GALERIA ENTONCES SE ADAPTA UNA NUEVA PLANTILLA PARA ESTA EMPRESA 
                {
                    lblFECHA.Text = "FECHA VENCIMIENTO";
                    txtV_FECHA.ReadOnly = false;
                }
                else
                {
                    lblFECHA.Text = "FECHA";
                    txtV_FECHA.ReadOnly = false;
                }

                txtV_FECHAANULADO.ReadOnly = true;
                txtV_IDCLIENTE.ReadOnly = false;
                txtV_CLIENTEDESCRIPCION.ReadOnly = false;
                txtV_RUC.ReadOnly = false;
                rdbMONEDA.Enabled = true;
                txtV_DIRECCION.ReadOnly = true;
                btnV_NUEVO.Enabled = false;
                btnV_GRABAR.Enabled = true;
                btnV_CANCELAR.Enabled = true;
                btnV_ANULAR.Enabled = false;
                btnV_IMPRIMIR.Enabled = false;
                btnV_CONSULTAR.Enabled = false;  
                txtV_CLIENTE_OPCIONAL.ReadOnly = false;
                txtV_IDPEDIDO.ReadOnly = true;
                txtPRODUCTO.ReadOnly = false;
                txtPRODUCTO_DESCRIPCION.ReadOnly = false;
                txtPRECIO.ReadOnly = false;
                txtCANTIDAD.ReadOnly = false;
                btnV_AGREGARPRODUCTO.Enabled = true;
                txtV_TOTAL.ReadOnly = true;
                txtV_IGV.ReadOnly = true;
                txtV_SUBTOTAL.ReadOnly = true;
                dgvDETALLE_VENTA.Enabled = true;

                txtV_FECHA.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void MANTENIMIENTO_VENTA(string ID_VENTA,string ACCION)
        {
            try
            {
                E_OBJVENTA.ID_VENTA = ID_VENTA;
                E_OBJVENTA.FECHA = txtV_FECHA.Text;
                E_OBJVENTA.SERIE = cboV_SERIE.SelectedItem.Text;
                E_OBJVENTA.TIPO_DOC =cboV_TIPO_DOC.SelectedValue.ToString();
                E_OBJVENTA.MONEDA = rdbMONEDA.SelectedValue.ToString();
                E_OBJVENTA.VALOR_VENTA = Convert.ToDouble(txtV_SUBTOTAL.Text);
                E_OBJVENTA.IGV = Convert.ToDouble(txtV_IGV.Text);
                E_OBJVENTA.TOTAL = Convert.ToDouble(txtV_TOTAL.Text);
                E_OBJVENTA.ID_SEDE = Session["SEDE"].ToString();
                E_OBJVENTA.ID_PEDIDO = null;
                E_OBJVENTA.ID_CLIENTE =txtV_IDCLIENTE.Text;
                E_OBJVENTA.CLIENTE =  txtV_CLIENTE_OPCIONAL.Text;
                E_OBJVENTA.SALDO = Convert.ToDouble(txtV_TOTAL.Text);
                E_OBJVENTA.ACCION = ACCION;

                N_OBJVENTA.MANTENIMIENTO_VENTA_DETALLADA(E_OBJVENTA); //AQUI CARGO LA VENTA
                if (ACCION != "3") //SI ES DIFERENTE DE ANULAR ENTONCES SE EJECUTA LA EDICION Y EL INSERT 
                {
                    MANTENIMIENTO_VENTADETALLE();                        // AQUI CARGO EL DETALLE DE LA VENTA
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        void MANTENIMIENTO_VENTADETALLE()
        {
            DataTable detalleVenta = (DataTable)Session["detalleBien"];

            try
            {
                for (int i = 0; i < dgvDETALLE_VENTA.Rows.Count; i++)
                {
                    E_OBJVENTA.ID_VENTA = E_OBJVENTA.ID_VENTA;
                    E_OBJVENTA.ID_BIEN = dgvDETALLE_VENTA.Rows[i].Cells[1].Text;
                    E_OBJVENTA.ITEM = i + 1;
                    Label can = dgvDETALLE_VENTA.Rows[i].FindControl("Label1") as Label;
                    E_OBJVENTA.CANTIDAD = Convert.ToDouble(can.Text);
                    Label pre = dgvDETALLE_VENTA.Rows[i].FindControl("Label2") as Label;
                    E_OBJVENTA.PRECIO = Convert.ToDouble(pre.Text);
                    E_OBJVENTA.IMPORTE = Convert.ToDouble(dgvDETALLE_VENTA.Rows[i].Cells[5].Text);
                    E_OBJVENTA.SALDO_CANTIDAD = Convert.ToDouble(can.Text); //el saldo cantidad tiene q ser igual a la cantidad para poder realizar despachos

                    N_OBJVENTA.VENTADETALLE_DETALLADA(E_OBJVENTA);
                }
            }
            catch (Exception)
            {

                Response.Write("<script>window.alert('NO ESCOGISTE NINGUN BIEN A VENDER');</script>");
            }

        }
        void OBTENER_ID_BIEN_Y_LLENAR_GRILLA(string ID_BIEN, string DESCRIPCION, double PRECIO,double CANTIDAD)
        {

            DataTable dt = (DataTable)Session["detalleBien"];

            try
            {
                DataRow row = dt.NewRow();
                row["ID_BIEN"] = ID_BIEN;
                row["CANT"] = Convert.ToDouble(CANTIDAD);
                row["DESCRIPCION"] = DESCRIPCION;
                row["PRECIO"] = Convert.ToDouble(PRECIO);
                row["IMPORTE"] = Convert.ToDouble(row["PRECIO"]) * Convert.ToDouble(row["CANT"]); ;
                dt.Rows.Add(row);
                dt.AcceptChanges();

                LLENAR_GRILLA();
                ACTUALIZAR_TOTALES();
            }
            catch (Exception)
            {

                // Response.Write("<script>window.alert('EL BIEN YA ESTA EN LA LISTA');</script>");

            }


        }

        void LLENAR_GRILLA()
        {
            DataTable dt = (DataTable)Session["detalleBien"];
            dgvDETALLE_VENTA.DataSource = dt;
            dgvDETALLE_VENTA.DataBind();
        }


        void ACTUALIZAR_TOTALES()
        {
            double subTotal, igv, total = 0;
            DataTable dt = (DataTable)Session["detalleBien"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                total = total + Convert.ToDouble(dt.Rows[i][4].ToString());

            }
            subTotal = (total / 1.18);
            igv = total - subTotal;


            txtV_SUBTOTAL.Text = subTotal.ToString("N2");
            txtV_IGV.Text = igv.ToString("N2");
            txtV_TOTAL.Text = total.ToString("N2");


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

        public bool VALIDAR_VENTA()
        {
            bool RESULTADO = false;
        if(dgvDETALLE_VENTA.Rows.Count > 0 )
         {
            if(cboV_TIPO_DOC.SelectedValue == "FT") //SI ES FACTURA
            {
                if(txtV_IDCLIENTE.Text != string.Empty)
                {
                    if(txtV_FECHA.Text != string.Empty)
                    {
                        if(rdbMONEDA.SelectedIndex != -1)
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
                    RESULTADO = false;
                }
            }

            if (cboV_TIPO_DOC.SelectedValue == "BV") //SI ES BOLETA ENTONCES
            {
                double total = Convert.ToDouble(txtV_TOTAL.Text);
                if (total < 700)
                {
                    if (txtV_FECHA.Text != string.Empty)
                    {
                        if (rdbMONEDA.SelectedIndex != -1)
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
                    if(total>=700)
                    {
                        if(txtV_IDCLIENTE.Text != string.Empty)
                        {
                                if (txtV_FECHA.Text != string.Empty)
                                {
                                    if (rdbMONEDA.SelectedIndex != -1)
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
                            RESULTADO = false;
                        }
                        
                    }
                }


            }

            if (cboV_TIPO_DOC.SelectedValue == "RA" || cboV_TIPO_DOC.SelectedValue == "RG" || cboV_TIPO_DOC.SelectedValue == "RR") //SI ES RECIBO ENTONCES
            {
                if (txtV_FECHA.Text != string.Empty)
                {
                    if (rdbMONEDA.SelectedIndex != -1)
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
            //if (cboV_TIPO_DOC.SelectedValue == "TB") //SI ES TICKET BOLETA
            //{
            //    if(Convert.ToDouble(txtV_TOTAL.Text)>=700) //tiene q escoger un cliente si el ticket boleta es >= que 700
            //    {
            //        if (txtV_IDCLIENTE.Text != string.Empty) //si esque tiene un cliente escogido entonces continuara
            //        {
            //            if (txtV_FECHA.Text != string.Empty)
            //            {
            //                if (rdbMONEDA.SelectedIndex != -1)
            //                {
            //                    RESULTADO = true;
            //                }
            //                else
            //                {
            //                    RESULTADO = false;
            //                }
            //            }
            //            else
            //            {
            //                RESULTADO = false;
            //            }
            //        }
            //        else
            //        {
            //            RESULTADO = false;
            //        }
                     
                    
            //    }
            //    else //no tiene lña obligacion de escoger un cliente si el ticket boleta es < que 700
            //    {
            //          if (txtV_FECHA.Text != string.Empty)
            //           {
            //               if (rdbMONEDA.SelectedIndex != -1)
            //                {
            //                    RESULTADO = true;
            //                }
            //                else
            //                {
            //                    RESULTADO = false;
            //                }
            //            }
            //            else
            //            {
            //                RESULTADO = false;
            //            }
            //   }
                    
            //}
            //if (cboV_TIPO_DOC.SelectedValue == "TF") //SI ES TICKET FACTURA
            //{
            //    if(Convert.ToDouble(txtV_TOTAL.Text) <= 700) //SI ES <= 700 ES PERMITIDO
            //    {
            //        if (txtV_FECHA.Text != string.Empty)
            //        {
            //            if (rdbMONEDA.SelectedIndex != -1)
            //            {
            //                RESULTADO = true;
            //            }
            //            else
            //            {
            //                RESULTADO = false;
            //            }
            //        }
            //        else
            //        {
            //            RESULTADO = false;
            //        }
            //    }
            //    else
            //    {
            //        RESULTADO= false;
            //    }
                    
            //   }
            //}
            //else // SINO SIGNIFICA QUE NO HAY DATOS EN LA GRILLA
            //{
            //RESULTADO = false;
            }
            return RESULTADO;
        }

        public void IMPRIMIR_SPOOL_TF_TB()
        {

            DataTable DATOS_VENTA = new DataTable();                         //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA
            string ID_VENTA = Session["ID_VENTA"].ToString();                   // ESTO PERMITE GENERAR LA VARIABLE DEL ID_VENTA
            DATOS_VENTA = N_OBJVENTA.CAPTURAR_TABLA_VENTA(ID_VENTA,Session["SEDE"].ToString());        //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO

            DataTable DATOS_VENTADETALLE = new DataTable();                  //ESTO ME PERMITE CREAR EL DATATABLE PARA LLAMAR A LOS DATOS DE MI VENTA_DETALLE
            string COD_VENTA = Session["ID_VENTA"].ToString();            // ESTO PERMITE GENERAR LA VARIABLE DEL ID_VENTA
            DATOS_VENTADETALLE = N_OBJVENTA.CAPTURAR_TABLA_VENTADETALLE(COD_VENTA); //ESTO ME PERMITE ALMACENAR TODOS LOS DATOS EN UN DATATABLE PARA PODER ACCEDER A ELLO EN TODO MOMENTO


            //LIMPIANDO MI SPOOL SI ESQUE UBIERA IMPRESIONES PENDIENTES
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "2");
            // ========================================================================================

            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][36].ToString(), "1"); //aqui va el nombre de la empresa


            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RUC: " + DATOS_VENTA.Rows[0][37].ToString(), "1");              //aqui va el ruc de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA EMPRESA
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                          // imprime una linea de guiones
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][28].ToString(), "1"); //aqui va el nombre de la sede de la empresa 
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][29].ToString(), "1"); //aqui va la direccion de la sede de la empresa
            //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DE LA SEDE
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0]["S_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["S_UBIDEN"].ToString(), "1");
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "MAQ REG : " + DATOS_VENTA.Rows[0][48].ToString(), "1");          //AQUI SE COLOCA EL NOMBRE DE LA MAQUINA REGISTRADORA
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][4].ToString(), "1");   //aqui va la fecha

            string TIP_DOC;
            TIP_DOC = DATOS_VENTA.Rows[0][3].ToString();/* AQUI BA EL NOMBRE  DEL TIPO DE DOCUMENTO */

            //P_SERIE_Y_NUMERO_CORRELATIVO_POR_PTOVENTA(TIP_DOC, CBOPTOVENTA.Text);
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TICKET: " + TIP_DOC + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString(), "1"); //aqui va el tipo_documento / el numero de serie / y el numero correlativo

            if (DATOS_VENTA.Rows[0]["V_ID_CLIENTE"] != DBNull.Value)   //ESTO ME PERMITE IMPRIMIR LOS DATOS CLIENTES SI ESQUE EXISTIERA UN CLIENTE
            {
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");                              // imprime una linea de guiones
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CLIENTE: " + DATOS_VENTA.Rows[0]["C_DESCRIPCION"].ToString(), "1"); //OBTENIENDO EL NOMBRE DEL CLIENTE
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "RUC/DNI: " + DATOS_VENTA.Rows[0]["C_RUC_DNI"].ToString(), "1"); //OBTENIENDO EL RUC DEL CLIENTE
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0]["C_DIRECCION"].ToString(), "1"); //OBTENIENDO LA DIRECCION DEL CLIENTE
                //AQUI ESTOY OBTENENIENDO EL NOMBRE DE DISTRITO PROVINCIA Y DEPARTAMENTO DEL CLIENTE
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0]["C_UBIDSN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIPRN"].ToString() + "-" + DATOS_VENTA.Rows[0]["C_UBIDEN"].ToString(), "1");
            }

            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");


            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CANT   DETALLE                IMPORTE", "1");
            for (int i = 0; i < DATOS_VENTADETALLE.Rows.Count; i++)
            {
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTADETALLE.Rows[i][3].ToString() + "   " + DATOS_VENTADETALLE.Rows[i][7].ToString() + "   " + DATOS_VENTADETALLE.Rows[i][5].ToString(), "1");
            }

            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");

            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "SUBTOTAL: S/. " + DATOS_VENTA.Rows[0]["V_VALOR_VENTA"].ToString(), "1"); //obtenemos el sub_total
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "IGV: S/. " + DATOS_VENTA.Rows[0]["V_IGV"].ToString(), "1");  //obtenemos el igv
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TOTAL: S/. " + DATOS_VENTA.Rows[0]["V_TOTAL"].ToString(), "1"); //obtenemos el total
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "P.V: " + Session["PUNTOVENTA"].ToString(), "1"); // obtenemos el punto de venta
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CAJERO: " + DATOS_VENTA.Rows[0][28].ToString(), "1"); //obtenemos la descripcion del cajero



            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][41].ToString(), "1"); //aqui obtenemos el email de la empresa
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][42].ToString(), "1"); //aqui obtenemos la pagina web de la empresa

            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString(), "1"); //obtenemos la descripcion del cajero
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "AGRADECEMOS SU PREFERENCIA!!!", "1"); // imprime en el centro "Venta mostrador"
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "VUELVA PRONTO!! LO ESPERAMOS!!", "1");
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
            if (DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString() != string.Empty)
            {
                N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString(), "1");
            }
            N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1");

            //METODO PARA EMITIR TICKET INDIVIDUALES POR PRODUCTO QUE ESTAN CONFIGURADOS EN LA TABLA BIEN

            for (int f = 0; f < DATOS_VENTADETALLE.Rows.Count; f++)
            {
                if (DATOS_VENTADETALLE.Rows[f]["B_EMITE_TICKET"].Equals(true))
                {
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][36].ToString(), "1"); //aqui va el nombre de la empresa
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][28].ToString(), "1"); //nombre de la sede
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "TICKET DESPACHO", "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "REFERENCIA: " + DATOS_VENTA.Rows[0][3].ToString() + " " + DATOS_VENTA.Rows[0][1].ToString() + "-" + DATOS_VENTA.Rows[0][2].ToString(), "1"); //aqui va el tipo_documento / el numero de serie / y el numero correlativo
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "**" + DATOS_VENTADETALLE.Rows[f]["VD_CANTIDAD"].ToString() + "**", "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTADETALLE.Rows[f]["B_DESCRIPCION"].ToString(), "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), string.Empty, "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "-", "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "ATENCION: " + DATOS_VENTA.Rows[0]["V_CLIENTE"].ToString(), "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), DATOS_VENTA.Rows[0][4].ToString(), "1");   //aqui va la fecha Y EL ID_VENTA
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "ID_VENTA: " + DATOS_VENTA.Rows[0][0].ToString(), "1");//aqui va el codigo de venta
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "AGRADECEMOS SU PREFERENCIA!!!", "1");
                    N_OBJVENTA.SPOOL_ETIQUETERA(Session["ID_PUNTOVENTA"].ToString(), "CORTATICKET", "1");
                }
            }
        }
        #endregion

       

        #region EVENTOS
         protected void btnV_NUEVO_Click(object sender, EventArgs e)
        {
            
            ESTADO_TRANSACCION(2); //LLAMAMOS AL PROCEDIMIENTO PARA GENERAR UN NUEVO REGISTRO
            //if (Session["SERIE"].ToString() != "0005") //AQUI VERIFICO EL PUNTO DE VENTA SELECCIONADO Y LA SERIE ASIGNADA PARA REALIZAR LA DESIGNACION DE LAS SERIES RESPECTIVAS
            //{
                cboV_SERIE.SelectedItem.Text = Session["SERIE"].ToString(); //AQUI IGUALO EL VALOR DE LA SERIE AL COMBO
                cboV_SERIE.Enabled = false;
            //}
            //CON ESTO GENERO EL NUMERO CORRELATIVO DE MI VENTA DETALLADA
            txtV_NUMERO.Text = N_OBJVENTA.CONSULTAR_NUMERO_CORRELATIVO_VENTA(Session["SEDE"].ToString(), cboV_SERIE.SelectedItem.Text, cboV_TIPO_DOC.SelectedValue.ToString()).Rows[0]["NUMERO"].ToString();
            Session["P_TIPODOCUMENTO"] = cboV_TIPO_DOC.SelectedValue.ToString();    
        }

        protected void btnV_CANCELAR_Click(object sender, EventArgs e)
         {
             ESTADO_TRANSACCION(1); //CON ESTO CONTROLAMOS LA ACTIVIDAD O INACTIVIDAD DE LOS CONTROLES
                                    // OBTENER_ULTIMO_REGISTRO_VENTA(); //CON ESTO OBTENGO EL ULTIMO REGISTRO PARA MOSTRARLO EN LA VENTA POR DEFECTO
             CONSULTAR_VENTA(Session["ID_VENTA"].ToString(), Session["SEDE"].ToString());
         }

        protected void dgvBIEN_VENTA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            dgvDETALLE_VENTA.PageIndex = e.NewSelectedIndex;
        }

        protected void dgvBIEN_VENTA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String cod = dgvDETALLE_VENTA.Rows[e.RowIndex].Cells[1].Text.ToString();
            Eliminar_Registro(cod);
            dgvDETALLE_VENTA.EditIndex = -1;
            LLENAR_GRILLA();
            ACTUALIZAR_TOTALES();
        }
        
        protected void btnV_AGREGARPRODUCTO_Click(object sender, EventArgs e)
        {
            if(txtPRODUCTO.Text != string.Empty && txtPRECIO.Text != string.Empty && txtCANTIDAD.Text!=string.Empty)
            {
                OBTENER_ID_BIEN_Y_LLENAR_GRILLA(txtPRODUCTO.Text,txtPRODUCTO_DESCRIPCION.Text,Convert.ToDouble(txtPRECIO.Text),Convert.ToDouble(txtCANTIDAD.Text));
                txtPRODUCTO.Text = string.Empty;
                txtPRECIO.Text = string.Empty;
                txtCANTIDAD.Text = string.Empty;
                txtPRODUCTO_DESCRIPCION.Text = string.Empty;
            }
            
        }

        protected void cboV_SERIE_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtV_NUMERO.Text = N_OBJVENTA.CONSULTAR_NUMERO_CORRELATIVO_VENTA(Session["SEDE"].ToString(),cboV_SERIE.SelectedItem.Text,cboV_TIPO_DOC.SelectedValue.ToString()).Rows[0]["NUMERO"].ToString();
        }

        protected void cboV_TIPO_DOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtV_NUMERO.Text = N_OBJVENTA.CONSULTAR_NUMERO_CORRELATIVO_VENTA(Session["SEDE"].ToString(), cboV_SERIE.SelectedItem.Text, cboV_TIPO_DOC.SelectedValue.ToString()).Rows[0]["NUMERO"].ToString();
            Session["P_TIPODOCUMENTO"] = cboV_TIPO_DOC.SelectedValue.ToString();//AQUI GUARDO EL TIPO DE DOCUMENTO PARA MI FILTRO DE CLIENTES

            //ESTO LIMPIA MIS DATOS SI ESQUE CAMBIO DE TIPO DE DOCUMENTO
            txtV_IDCLIENTE.Text = string.Empty;
            txtV_CLIENTEDESCRIPCION.Text = string.Empty;
            txtV_DIRECCION.Text = string.Empty;
            txtV_RUC.Text = string.Empty;
            txtPRODUCTO.Text = string.Empty;
            txtPRODUCTO_DESCRIPCION.Text = string.Empty;
            txtPRECIO.Text = string.Empty;
            txtCANTIDAD.Text = string.Empty;
            DataTable dt = (DataTable)Session["detalleBien"];
            dt.Clear();

            LLENAR_GRILLA();
            //============================================================
        }

        protected void btnV_GRABAR_Click(object sender, EventArgs e)
        {
           if(VALIDAR_VENTA())
           {
                MANTENIMIENTO_VENTA(string.Empty,"1"); //graba la venta
                ESTADO_TRANSACCION(1);
                CONSULTAR_VENTA(E_OBJVENTA.ID_VENTA,Session["SEDE"].ToString()); //CON ESTO OBTENGO EL ULTIMO REGISTRO PARA MOSTRARLO EN LA VENTA POR DEFECTO
                Session["ID_VENTA"] = E_OBJVENTA.ID_VENTA;
           }
           else
           {
               Response.Write("<script>window.alert('ERROR, NO SE PUEDE GRABAR PORQUE HAY DATOS INCORRECTOS');</script>");
           }
        }

        protected void txtPRODUCTO_DESCRIPCION_TextChanged(object sender, EventArgs e)
        {
           //if(txtPRODUCTO.Text.ToString().Length < 2)
           //{
           //    txtPRECIO.Text = string.Empty;
           //}
        }

        protected void btnV_ANULAR_Click(object sender, EventArgs e)
        {
            if (txtV_FECHAANULADO.Text != string.Empty)
            {
                Response.Write("<script>window.alert('ERROR, ESTA VENTA YA HA SIDO ANULADO ANTERIORMENTE');</script>");
            }
            else
            {
                //ANTES DE ANULA LA VENTA TENGO QUE VERIFICAR QUE LA VENTA NO TENGA AMORTIZACIONES NI DESPACHOS PARCIALES
                N_OBJVENTA.ELIMINAR_VENTA(txtV_IDVENTA.Text); //ANULANDO LA VENTA SELECCIONADA
                
                //CON ESTO ANULO LA VENTA DEL CONTROL DE GALERIA PARA MANTENER EL ORDEN
                if (Session["SEDE"].ToString() == "004")
                {
                    N_OBJVENTA.ACTUALIZAR_MODIFICACIONES_CONTROL_GALERIA(txtV_IDVENTA.Text,"1");
                }
               //=============================================================================

                CONSULTAR_VENTA(txtV_IDVENTA.Text, Session["SEDE"].ToString()); //CON ESTO OBTENGO EL ULTIMO REGISTRO PARA MOSTRARLO EN LA VENTA POR DEFECTO


                
            }
            
        }

        protected void dgvBIEN_VENTA_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnV_CONSULTAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("FRM_CONSULTAR_BUSCAR_VENTA.aspx");
        }

        protected void btnV_IMPRIMIR_Click(object sender, EventArgs e)
        {
               if(txtV_FECHAANULADO.Text == string.Empty) //solo se imprimira las ventas que no estan anuladas
               {

                    if(cboV_TIPO_DOC.SelectedValue=="FT")
                    {
                        string IDVENTA = txtV_IDVENTA.Text;
                        String url = String.Format("REPORTES/FRM_REPORTE_FACTURA.aspx?IDVENTA={0}", IDVENTA);
                       // Response.Redirect(url);
                        string s = "window.open('" + url + "', 'popup_window', 'width=700,height=400,left=10%,top=10%,resizable=yes');"; //con esto muestro la venta en una nueva ventana 
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                    }
                    if (cboV_TIPO_DOC.SelectedValue == "BV")
                    {
                        string IDVENTA = txtV_IDVENTA.Text;
                        String url = String.Format("REPORTES/FRM_REPORTE_BOLETA.aspx?IDVENTA={0}", IDVENTA);
                       // Response.Redirect(url);
                        string s = "window.open('" + url + "', 'popup_window', 'width=700,height=400,left=10%,top=10%,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
                    if (cboV_TIPO_DOC.SelectedValue == "RA" || cboV_TIPO_DOC.SelectedValue == "RG" || cboV_TIPO_DOC.SelectedValue == "RR")
                    {
                        string ID_COMPVTA = txtV_IDVENTA.Text;
                        string ID_EMPRESA = Session["ID_EMPRESA"].ToString();
                        String url = String.Format("REPORTES/FRM_REPORTE_RECIBOS.aspx?ID_COMPVTA={0}&ID_EMPRESA={1}",ID_COMPVTA,ID_EMPRESA);
                        // Response.Redirect(url);
                        string s = "window.open('" + url + "', 'popup_window', 'width=700,height=400,left=10%,top=10%,resizable=yes');";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    }
               }
        }

        #endregion
    }
}