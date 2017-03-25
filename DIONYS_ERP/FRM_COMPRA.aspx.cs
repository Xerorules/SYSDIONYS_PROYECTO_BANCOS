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
    public partial class FRM_COMPRA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ID_CAJA"].ToString() == string.Empty)
                {
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }

                ESTADO_TRANSACCION(1);//CON ESTO COLOCO EL FORMULARIO EN ESTADO DE CONSULTA
                if (Session["ID_COMPRA"].ToString() == string.Empty)
                {
                    DataTable DT = new DataTable();
                    DT = N_OBJCOMPRA.OBTENER_ULTIMO_REGISTRO_COMPRA(Session["SEDE"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        Session["ID_COMPRA"] = DT.Rows[0]["C_ID_COMPRA"].ToString(); //AQUI GUARDO EL DATO ID_COMPRA PARA PODER HACER LA CONSULTAR_COMPRA
                        CONSULTAR_COMPRA(DT.Rows[0]["C_ID_COMPRA"].ToString());
                    }
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = N_OBJCOMPRA.OBTENER_ULTIMO_REGISTRO_COMPRA(Session["SEDE"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        CONSULTAR_COMPRA(Session["ID_COMPRA"].ToString());
                    }
                }
            }
        }

        #region OBJETOS
        E_COMPRAS E_OBJCOMPRA = new E_COMPRAS();
        N_VENTA N_OBJCOMPRA = new N_VENTA();
        #endregion


        #region PROCEDIMIENTOS
        void CONSULTAR_COMPRA(string ID_COMPRA)
        {
            DataTable DT = new DataTable();
            DT = N_OBJCOMPRA.CAPTURAR_TABLA_COMPRA(ID_COMPRA);
            
            if (DT.Rows.Count > 0)
            {
                //RESULTADOS PUESTOS 
                txtC_IDCOMPRA.Text = DT.Rows[0]["C_ID_COMPRA"].ToString();
                cboC_TIPO_DOC.SelectedValue = DT.Rows[0]["C_TIPO_DOC"].ToString();
                txtC_SERIE.Text = DT.Rows[0]["C_SERIE"].ToString();
                txtC_NUMERO.Text = DT.Rows[0]["C_NUMERO"].ToString();
                txtC_IDPROVEEDOR.Text = DT.Rows[0]["C_ID_PROVEEDOR"].ToString();
                txtC_PROVDESCRIPCION.Text = DT.Rows[0]["P_DESCRIPCION"].ToString();
                txtC_PROV_RUC_DNI.Text = DT.Rows[0]["P_RUC_DNI"].ToString();
                txtC_PROVDIRECCION.Text = DT.Rows[0]["P_DIRECCION"].ToString();
                txtC_PROVORIGEN.Text = DT.Rows[0]["P_ORIGEN_PROVEEDOR"].ToString();
                txtC_FECHA.Text = DT.Rows[0]["C_FECHA"].ToString();
                txtC_FECHAANULADO.Text = DT.Rows[0]["C_FECHA_ANULADO"].ToString();
                rdbMONEDA.SelectedValue = DT.Rows[0]["C_MONEDA"].ToString();
                txtC_OBSERVACIONES.Text = DT.Rows[0]["C_OBSERVACIONES"].ToString();
                txtC_SUBTOTAL.Text = DT.Rows[0]["C_VALOR_VENTA"].ToString();
                txtC_IGV.Text = DT.Rows[0]["C_IGV"].ToString();
                txtC_TOTAL.Text = DT.Rows[0]["C_TOTAL"].ToString();
            
                OBTENER_ULTIMO_REGISTRO_DETALLE(ID_COMPRA); // CON ESTO RECUPERO LOS DATOS DE LA GRILLA Y LO LLENO EN MI GRILLA
            }
           
        }
        void OBTENER_ULTIMO_REGISTRO_DETALLE(string ID_COMPRA)
        {
            DataTable DT = new DataTable();
            DT = N_OBJCOMPRA.CAPTURAR_TABLA_COMPRADETALLE(ID_COMPRA);
            //RESULTADOS PUESTOS 
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string ID_BIEN = DT.Rows[i]["CD_ID_BIEN"].ToString();
                string DESCRIPCION = DT.Rows[i]["B_DESCRIPCION"].ToString();
                double PRECIO = Convert.ToDouble(DT.Rows[i]["CD_CANTIDAD"].ToString());
                double CANTIDAD = Convert.ToDouble(DT.Rows[i]["CD_PRECIO"].ToString());

                OBTENER_ID_BIEN_Y_LLENAR_GRILLA(ID_BIEN, DESCRIPCION, PRECIO, CANTIDAD);
            }

        }
        void OBTENER_NUMERO_CORRELATIVO()
        {

        }
        public bool VALIDAR_REGISTRO_COMPRA(string SERIE, string NUMERO) //CON ESTO VALIDAMOS SI ESQUE EL REGISTRO A GRABAR YA EXISTE O NO EN LA TABLA DE COMPRAS
        {
            bool resultado=true; // SI ES TRUE SIGNIIFICA QUE NO HAY REGISTROS IDENTICOS
            
            DataTable DT = new DataTable();
            DT = N_OBJCOMPRA.VALIDAR_REGCOMPRA(SERIE, NUMERO);
            if(DT.Rows.Count >0 )
            {
                resultado = false;
            }else{
                resultado=true;
            }
            return resultado;
        }

        void ESTADO_TRANSACCION(int ESTADO)
        {
            if (ESTADO == 1) //ESTADO CONSULTA
            {

                //LIMPIARDO CONTROLES

                cboC_TIPO_DOC.SelectedIndex = 0;
                txtC_SERIE.Text = string.Empty;
                txtC_NUMERO.Text = string.Empty;
                txtC_FECHA.Text = string.Empty;
                txtC_IDCOMPRA.Text = string.Empty;
                txtC_IDPROVEEDOR.Text = string.Empty;
                txtC_PROVDESCRIPCION.Text = string.Empty;
                txtC_PROV_RUC_DNI.Text = string.Empty;
                rdbMONEDA.SelectedIndex = -1;
                txtC_PROVDIRECCION.Text = string.Empty;
                txtC_PROVORIGEN.Text = string.Empty;
                txtC_OBSERVACIONES.Text = string.Empty;
                txtPRODUCTO.Text = string.Empty;
                txtPRODUCTO_DESCRIPCION.Text = string.Empty;
                txtPRECIO.Text = string.Empty;
                txtCANTIDAD.Text = string.Empty;
                txtC_TOTAL.Text = string.Empty;
                txtC_IGV.Text = string.Empty;
                txtC_SUBTOTAL.Text = string.Empty;
                DataTable dt = (DataTable)Session["detalleBien"];
                dt.Clear();

                LLENAR_GRILLA();
                dgvDETALLE_COMPRA.Enabled = false;

                //====================

                cboC_TIPO_DOC.Enabled = false;
                txtC_SERIE.ReadOnly = true;
                txtC_NUMERO.ReadOnly = true;
                txtC_FECHA.ReadOnly = true;
                txtC_FECHAANULADO.ReadOnly = true;
                txtC_IDPROVEEDOR.ReadOnly = true;
                txtC_PROVDESCRIPCION.ReadOnly = true;
                txtC_PROV_RUC_DNI.ReadOnly = true;
                rdbMONEDA.Enabled = false;
                txtC_PROVDIRECCION.ReadOnly = true;
                btnC_NUEVO.Enabled = true;
                btnC_GRABAR.Enabled = false;
                btnC_CANCELAR.Enabled = false;
                btnC_ANULAR.Enabled = true;
                btnC_CONSULTAR.Enabled = true;
                txtC_OBSERVACIONES.ReadOnly = true;
                txtC_PROVORIGEN.ReadOnly = true;
                txtPRODUCTO.ReadOnly = true;
                txtPRODUCTO_DESCRIPCION.ReadOnly = true;
                txtPRECIO.ReadOnly = true;
                txtCANTIDAD.ReadOnly = true;
                btnC_AGREGARPRODUCTO.Enabled = false;
                txtC_TOTAL.ReadOnly = true;
                txtC_IGV.ReadOnly = true;
                txtC_SUBTOTAL.ReadOnly = true;
                dt.Clear();

                LLENAR_GRILLA();
                dgvDETALLE_COMPRA.Enabled = true;

            }
            if (ESTADO == 2) //ESTADO NUEVO
            {
                //LIMPIARDO CONTROLES

                cboC_TIPO_DOC.SelectedIndex = 0;
                txtC_SERIE.Text = string.Empty;
                txtC_NUMERO.Text = string.Empty;
                txtC_FECHA.Text = string.Empty;
                txtC_IDCOMPRA.Text = string.Empty;
                txtC_IDPROVEEDOR.Text = string.Empty;
                txtC_PROVDESCRIPCION.Text = string.Empty;
                txtC_PROV_RUC_DNI.Text = string.Empty;
                rdbMONEDA.SelectedIndex = 0;
                txtC_PROVDIRECCION.Text = string.Empty;
                txtC_PROVORIGEN.Text = string.Empty;
                txtC_OBSERVACIONES.Text = string.Empty;
                txtPRODUCTO.Text = string.Empty;
                txtPRODUCTO_DESCRIPCION.Text = string.Empty;
                txtPRECIO.Text = string.Empty;
                txtCANTIDAD.Text = string.Empty;
                txtC_TOTAL.Text = string.Empty;
                txtC_IGV.Text = string.Empty;
                txtC_SUBTOTAL.Text = string.Empty;
                DataTable dt = (DataTable)Session["detalleBien"];
                dt.Clear();

                LLENAR_GRILLA();
                dgvDETALLE_COMPRA.Enabled = false;

                //====================

                cboC_TIPO_DOC.Enabled = true;
                txtC_SERIE.ReadOnly = false;
                txtC_NUMERO.ReadOnly = false;
                txtC_FECHA.ReadOnly = true;
                txtC_FECHAANULADO.ReadOnly = true;
                txtC_IDPROVEEDOR.ReadOnly = false;
                txtC_PROVDESCRIPCION.ReadOnly = false;
                txtC_PROV_RUC_DNI.ReadOnly = false;
                rdbMONEDA.Enabled = true;
                txtC_PROVDIRECCION.ReadOnly = true;
                btnC_NUEVO.Enabled = false;
                btnC_GRABAR.Enabled = true;
                btnC_CANCELAR.Enabled = true;
                btnC_ANULAR.Enabled = false;
                btnC_CONSULTAR.Enabled = false;
                txtC_OBSERVACIONES.ReadOnly = false;
                txtC_PROVORIGEN.ReadOnly = true;
                txtPRODUCTO.ReadOnly = false;
                txtPRODUCTO_DESCRIPCION.ReadOnly = false;
                txtPRECIO.ReadOnly = false;
                txtCANTIDAD.ReadOnly = false;
                btnC_AGREGARPRODUCTO.Enabled = true;
                txtC_TOTAL.ReadOnly = true;
                txtC_IGV.ReadOnly = true;
                txtC_SUBTOTAL.ReadOnly = true;
                dgvDETALLE_COMPRA.Enabled = true;

                txtC_FECHA.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void MANTENIMIENTO_COMPRA(string ID_COMPRA, string ACCION)
        {
            try
            {
                E_OBJCOMPRA.ID_COMPRA = ID_COMPRA;
                E_OBJCOMPRA.SERIE = txtC_SERIE.Text;
                E_OBJCOMPRA.NUMERO = txtC_NUMERO.Text;
                E_OBJCOMPRA.TIPO_DOC = cboC_TIPO_DOC.SelectedValue.ToString();
                E_OBJCOMPRA.MONEDA = rdbMONEDA.SelectedValue.ToString();
                E_OBJCOMPRA.VALOR_VENTA = Convert.ToDouble(txtC_SUBTOTAL.Text);
                E_OBJCOMPRA.IGV = Convert.ToDouble(txtC_IGV.Text);
                E_OBJCOMPRA.TOTAL = Convert.ToDouble(txtC_TOTAL.Text);
                E_OBJCOMPRA.ID_SEDE = Session["SEDE"].ToString();
                E_OBJCOMPRA.ID_PROVEEDOR = txtC_IDPROVEEDOR.Text;
                E_OBJCOMPRA.OBSERVACIONES = txtC_OBSERVACIONES.Text;
                E_OBJCOMPRA.SALDO = Convert.ToDouble(txtC_TOTAL.Text);
                E_OBJCOMPRA.ACCION = ACCION;

                N_OBJCOMPRA.MANTENIMIENTO_COMPRA(E_OBJCOMPRA); //AQUI CARGO LA COMPRA
                if (ACCION != "3") //SI ES DIFERENTE DE ANULAR ENTONCES SE EJECUTA LA EDICION Y EL INSERT 
                {
                    MANTENIMIENTO_COMPRADETALLE();                        // AQUI CARGO EL DETALLE DE LA COMPRA
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        void MANTENIMIENTO_COMPRADETALLE()
        {

            try
            {
                for (int i = 0; i < dgvDETALLE_COMPRA.Rows.Count; i++)
                {
                    E_OBJCOMPRA.ID_COMPRA = E_OBJCOMPRA.ID_COMPRA;
                    E_OBJCOMPRA.ID_BIEN = dgvDETALLE_COMPRA.Rows[i].Cells[1].Text;
                    E_OBJCOMPRA.ITEM = i + 1;
                    Label can = dgvDETALLE_COMPRA.Rows[i].FindControl("Label1") as Label;
                    E_OBJCOMPRA.CANTIDAD = Convert.ToDouble(can.Text);
                    Label pre = dgvDETALLE_COMPRA.Rows[i].FindControl("Label2") as Label;
                    E_OBJCOMPRA.PRECIO = Convert.ToDouble(pre.Text);
                    E_OBJCOMPRA.IMPORTE = Convert.ToDouble(dgvDETALLE_COMPRA.Rows[i].Cells[5].Text);
                    E_OBJCOMPRA.SALDO_CANTIDAD = Convert.ToDouble(can.Text); //el saldo cantidad tiene q ser igual que le importe para que luego sea amortizado y pagado por el monto total o por partes

                    N_OBJCOMPRA.MANTENIMIENTO_COMPRADETALLE(E_OBJCOMPRA);
                }
            }
            catch (Exception)
            {

                Response.Write("<script>window.alert('NO ESCOGISTE NINGUN BIEN DE LA COMPRA');</script>");
            }

        }
        void OBTENER_ID_BIEN_Y_LLENAR_GRILLA(string ID_BIEN, string DESCRIPCION, double PRECIO, double CANTIDAD)
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
            dgvDETALLE_COMPRA.DataSource = dt;
            dgvDETALLE_COMPRA.DataBind();
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


            txtC_SUBTOTAL.Text = subTotal.ToString("N2");
            txtC_IGV.Text = igv.ToString("N2");
            txtC_TOTAL.Text = total.ToString("N2");


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

        public bool VALIDAR_COMPRA()
        {
            bool RESULTADO = false;
            if (dgvDETALLE_COMPRA.Rows.Count > 0)
            {
                if (cboC_TIPO_DOC.SelectedValue == "FT") //SI ES FACTURA
                {
                    if (txtC_SERIE.Text != string.Empty && txtC_NUMERO.Text != string.Empty)
                    {
                        if (txtC_IDPROVEEDOR.Text != string.Empty)
                        {
                            if (txtC_FECHA.Text != string.Empty)
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
                    else
                    {
                        RESULTADO = false;
                    }

                }

                if (cboC_TIPO_DOC.SelectedValue == "BV") //SI ES BOLETA ENTONCES
                {
                    if (txtC_SERIE.Text != string.Empty && txtC_NUMERO.Text != string.Empty)
                    {
                        if (txtC_FECHA.Text != string.Empty)
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
            return RESULTADO;
        }



        protected void btnC_NUEVO_Click(object sender, EventArgs e)
        {
            ESTADO_TRANSACCION(2); //LLAMAMOS AL PROCEDIMIENTO PARA GENERAR UN NUEVO REGISTRO
            //CON ESTO GENERO EL NUMERO CORRELATIVO DE MI VENTA DETALLADA
            Session["P_TIPODOCUMENTO"] = cboC_TIPO_DOC.SelectedValue.ToString();    
        }

        protected void btnC_GRABAR_Click(object sender, EventArgs e)
        {
           if(VALIDAR_REGISTRO_COMPRA(txtC_SERIE.Text,txtC_NUMERO.Text)) // SI ES PASA ES QUE ES VERDADERO Y POR LO TANTO SE HACE LA COMPRA CON LA SERIE Y EL NUMERO RESPECTIVO
           {
                if (VALIDAR_COMPRA())
                {
                    MANTENIMIENTO_COMPRA(string.Empty, "1"); //graba la venta
                    ESTADO_TRANSACCION(1);
                    CONSULTAR_COMPRA(E_OBJCOMPRA.ID_COMPRA); //CON ESTO OBTENGO EL ULTIMO REGISTRO PARA MOSTRARLO EN LA VENTA POR DEFECTO
                    COMPLETAR_SERIE_NUMERO();
                    Session["ID_COMPRA"] = E_OBJCOMPRA.ID_COMPRA;
                }
                else
                {
                    Response.Write("<script>window.alert('ERROR, NO SE PUEDE GRABAR PORQUE HAY DATOS INCORRECTOS');</script>");
                }
           }
           else
           {
               Response.Write("<script>window.alert('ERROR, LA COMPRA CON ESTA SERIE Y NUMERO YA EXISTEN, VUELVA A INTENTARLO...');</script>");
           }
            
        }

        void COMPLETAR_SERIE_NUMERO()
        {
            int cantSERIE = txtC_SERIE.Text.ToString().Length;
            int cantNUMERO =txtC_NUMERO.Text.ToString().Length;
            string cad="";
            if(cantSERIE<=4)
            {
                for(int i=0;i<4-cantSERIE;i++)
                {
                    cad=cad + "0";
                }
                
            }
            txtC_SERIE.Text = cad + txtC_SERIE.Text;
        }


        protected void btnC_CANCELAR_Click(object sender, EventArgs e)
        {
            ESTADO_TRANSACCION(1); //CON ESTO CONTROLAMOS LA ACTIVIDAD O INACTIVIDAD DE LOS CONTROLES
            // OBTENER_ULTIMO_REGISTRO_VENTA(); //CON ESTO OBTENGO EL ULTIMO REGISTRO PARA MOSTRARLO EN LA VENTA POR DEFECTO
            CONSULTAR_COMPRA(Session["ID_COMPRA"].ToString());
        }

        protected void dgvDETALLE_COMPRA_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            dgvDETALLE_COMPRA.PageIndex = e.NewSelectedIndex;
        }

        protected void dgvDETALLE_COMPRA_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String cod = dgvDETALLE_COMPRA.Rows[e.RowIndex].Cells[1].Text.ToString();
            Eliminar_Registro(cod);
            dgvDETALLE_COMPRA.EditIndex = -1;
            LLENAR_GRILLA();
            ACTUALIZAR_TOTALES();
        }

        protected void btnC_AGREGARPRODUCTO_Click(object sender, EventArgs e)
        {
            if (txtPRODUCTO.Text != string.Empty && txtPRECIO.Text != string.Empty && txtCANTIDAD.Text != string.Empty)
            {
                OBTENER_ID_BIEN_Y_LLENAR_GRILLA(txtPRODUCTO.Text, txtPRODUCTO_DESCRIPCION.Text, Convert.ToDouble(txtPRECIO.Text), Convert.ToDouble(txtCANTIDAD.Text));
                txtPRODUCTO.Text = string.Empty;
                txtPRECIO.Text = string.Empty;
                txtCANTIDAD.Text = string.Empty;
                txtPRODUCTO_DESCRIPCION.Text = string.Empty;
            }
        }

        protected void cboC_TIPO_DOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["P_TIPODOCUMENTO"] = cboC_TIPO_DOC.SelectedValue.ToString();//AQUI GUARDO EL TIPO DE DOCUMENTO PARA MI FILTRO DE CLIENTES
            //ESTO LIMPIA MIS DATOS SI ESQUE CAMBIO DE TIPO DE DOCUMENTO
            txtC_IDPROVEEDOR.Text = string.Empty;
            txtC_PROVDESCRIPCION.Text = string.Empty;
            txtC_PROVDIRECCION.Text = string.Empty;
            txtC_PROV_RUC_DNI.Text = string.Empty;
            txtC_OBSERVACIONES.Text = string.Empty;
            txtPRODUCTO.Text = string.Empty;
            txtPRODUCTO_DESCRIPCION.Text = string.Empty;
            txtPRECIO.Text = string.Empty;
            txtCANTIDAD.Text = string.Empty;
            rdbMONEDA.SelectedIndex=0;
            DataTable dt = (DataTable)Session["detalleBien"];
            dt.Clear();

            LLENAR_GRILLA();
            //============================================================
        }

        protected void btnC_ANULAR_Click(object sender, EventArgs e)
        {
            if (txtC_FECHAANULADO.Text != string.Empty)
            {
                Response.Write("<script>window.alert('ERROR, ESTA VENTA YA HA SIDO ANULADO ANTERIORMENTE');</script>");
            }
            else
            {
                N_OBJCOMPRA.ANULAR_COMPRA(txtC_IDCOMPRA.Text); //ANULANDO LA VENTA SELECCIONADA
                CONSULTAR_COMPRA(txtC_IDCOMPRA.Text); //CON ESTO OBTENGO EL ULTIMO REGISTRO PARA MOSTRARLO EN LA VENTA POR DEFECTO
            }

        }
        #endregion  

        protected void btnC_CONSULTAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("FRM_CONSULTAR_BUSCAR_COMPRA.aspx");
        }

    }
}