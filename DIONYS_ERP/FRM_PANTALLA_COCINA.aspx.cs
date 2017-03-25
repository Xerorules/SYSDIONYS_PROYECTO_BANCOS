using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;

namespace DIONYS_ERP
{
    public partial class FRM_PANTALLA_COCINA : System.Web.UI.Page
    {
        private string ESTADO_PEDIDO;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!Page.IsPostBack)
            {
                ACTUALIZAR_FECHA_ATENCION();
            }
        }

        #region OBJETOS
        N_VENTA N_OBJVENTAS = new N_VENTA();
        E_VENTA E_OBJVENTAS = new E_VENTA();
        E_PEDIDO E_OBJPEDIDO = new E_PEDIDO();

        E_MANT_CLIENTE E_OBJMANT_CLIENTE = new E_MANT_CLIENTE();
        E_VENTA_Y_DETALLE E_OBJMANT_VENTADET = new E_VENTA_Y_DETALLE();
        #endregion

        void LISTAR_PEDIDO()
        {

            DataList1.DataSource = N_OBJVENTAS.LISTA_PEDIDOS(Session["SEDE"].ToString());
            DataList1.DataBind();
        }

        void MANTENIMIENTO_PEDIDO_ANULAR()
        {
            try
            {
                E_OBJPEDIDO.ID_PEDIDO = txtNUM_ANULADO.Text.ToString();
                E_OBJPEDIDO.ID_EMPLEADO = Session["ID_EMPLEADO"].ToString();
                E_OBJPEDIDO.CLIENTE = string.Empty;
                E_OBJPEDIDO.VALOR_VENTA = 0.00;
                E_OBJPEDIDO.IGV = 0.00;
                E_OBJPEDIDO.TOTAL = 0.00;
                E_OBJPEDIDO.MONEDA = string.Empty;
                E_OBJPEDIDO.OBSERVACION = string.Empty;
                E_OBJPEDIDO.FECHA_ANULADO = string.Empty;
                E_OBJPEDIDO.FECHA_ATENDIDO = string.Empty;
                E_OBJPEDIDO.ID_SEDE = string.Empty;
                E_OBJPEDIDO.ACCION = "3";

                N_OBJVENTAS.MANTENIMIENTO_PEDIDO(E_OBJPEDIDO);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        void ACTUALIZAR_FECHA_ATENCION() //ESTA FUNCION QUE ES UN STORE ME CHEQUEA SI HAY PEDIDOS POR ATENDER EN COCINA Y DEPENDIENDO DE ELLO ME FILTRA MI DATALIST
        {
            DataTable dt = new DataTable();
            dt = N_OBJVENTAS.LISTA_PEDIDOS(Session["SEDE"].ToString());

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string ID_PEDIDO = dt.Rows[x]["ID_PEDIDO"].ToString();
                N_OBJVENTAS.ACTUALIZAR_FECHA_ATENCION(ID_PEDIDO);
            }
            LISTAR_PEDIDO(); //LLENA 
            LISTAR_PEDIDO_DETALLE();

        }

        void LISTAR_PEDIDO_DETALLE()
        {
            TimeSpan DEMORA_MINUTO ;
            DateTime varFECHA_PEDIDO;
            
            for (int x = 0; x < DataList1.Items.Count; x++)
            {
                Label PEDIDO = (Label)DataList1.Items[x].FindControl("lblNUM_PEDIDO");
                
                
                //CALCULANDO LOS MINUTOS QUE DEMORA EL PEDIDO EN DESPACHARSE
                Label FECHA_PEDIDO = (Label)DataList1.Items[x].FindControl("lblFECHA_PEDIDO");

                varFECHA_PEDIDO = Convert.ToDateTime(FECHA_PEDIDO.Text.ToString());

                DEMORA_MINUTO = DateTime.Now - varFECHA_PEDIDO;

                Label lblDEMORA_MINUTO = (Label)DataList1.Items[x].FindControl("lblDEMORA_MINUTO");

                //con esto me salen todos los minutos horas y segundos que los pedidos estan en preparacion
                lblDEMORA_MINUTO.Text = DEMORA_MINUTO.Hours.ToString() + ":" + DEMORA_MINUTO.Minutes.ToString() + ":" + DEMORA_MINUTO.Seconds.ToString();
                //===========================================================
                string ID_PEDIDO = PEDIDO.Text;
                GridView dgv = (GridView)DataList1.Items[x].FindControl("dgvDETALLE_PEDIDO");
                dgv.DataSource = N_OBJVENTAS.LISTA_PEDIDOS_DETALLE(ID_PEDIDO);
                dgv.DataBind();
            }

        }

        //ESTE EVENTO ME RECORRE TODAS LA FILAS DE MI GRIDVIEW Y ME COMPARA LOS DATOS PARA GENERAR MIS ICONOS
        protected void dgvDETALLE_PEDIDO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string FECHA_PREPARADO = e.Row.Cells[5].Text;

            if (FECHA_PREPARADO != "FECHA_PREPARADO" )
            {
                if (FECHA_PREPARADO != "&nbsp;")
                {
                    
                    if(FECHA_PREPARADO != string.Empty)
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        e.Row.ForeColor = System.Drawing.Color.White;
                        //ImageButton boton = (ImageButton)((Control)e.Row.FindControl("imgESTADO"));
                        //boton.ImageUrl = "ICONOS/OK.png";
                    }
                    
                }
                if (FECHA_PREPARADO == "&nbsp;")
                {

                    if (FECHA_PREPARADO != string.Empty)
                    {
                        e.Row.BackColor = System.Drawing.Color.White;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                        //ImageButton boton = (ImageButton)((Control)e.Row.FindControl("imgESTADO"));
                        //boton.ImageUrl = "ICONOS/OK.png";
                    }

                }
            }

        }

        protected void dgvDETALLE_PEDIDO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdATENDER")
            {
                ESTADO_PEDIDO = string.Empty;
                GridViewRow fila = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string varFECHA_PREPARADO = fila.Cells[5].Text.ToString();
                if (varFECHA_PREPARADO != string.Empty && varFECHA_PREPARADO != "&nbsp;") //si el campo fecha preparado es diferente de nulo (esta preparado) entonces puedo despachar
                {
                    Session["COCINA_FILA"] = fila.RowIndex.ToString();
                    ESTADO_PEDIDO = "ESTADO DESPACHADO";
                }
            }
            if (e.CommandName == "cmdESTADO")
            {
                ESTADO_PEDIDO = string.Empty;
                GridViewRow fila = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string varFECHA_PREPARADO = fila.Cells[5].Text.ToString();
                //LBLPRUEBA.Text = fila.Cells[5].Text;
                
                    if (varFECHA_PREPARADO == string.Empty || varFECHA_PREPARADO == "&nbsp;") //si el campo fecha preparado es nulo (falta preparar) entonces puedo cambiar de estado a preparado
                    {
                        Session["COCINA_FILA"] = fila.RowIndex.ToString();
                        ESTADO_PEDIDO = "ESTADO PREPARADO";

                        
                    }
                    if (varFECHA_PREPARADO != "&nbsp;")
                    {
                        Session["COCINA_FILA"] = fila.RowIndex.ToString();
                        ESTADO_PEDIDO = "ESTADO DESHACER";

                        
                    }
                
                
            }
        }

        //CON ESTE EVENTO GENERO Y OBTENGO EL ITEM QUE SERA SELECCIONADA POSTERIORMENTE
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e) 
        {
            //AQUI OBTENGO EL ITEM SELECIONADO
            DataListItem LISTITEM = e.Item;
            //AQUI GUARDO MI ITEM EN UNA SESSION
            Session["COCINA_LISTA"] = LISTITEM.ItemIndex.ToString();
            //AQUI LLAMO A MI PROCEDIMIENTO ACTUALIZAR
            if (ESTADO_PEDIDO != string.Empty)
            {
                ACTUALIZAR_FECHA_ESTADO_PEDIDO();
            }
        }
        private void ACTUALIZAR_FECHA_ESTADO_PEDIDO() //AQUI  ACTUALIZO LA FECHA DE PREPARADO DE MIS REGISTROS DE MI GRIDVIEW PEDIDO_DETALLE
        {
            // AQUI GENERO UN GRIDVIEW A TRAVEZ DE UN CONTROL PARA PODER UTLIZARLO LUEGO Y PODER ACCEDER A SUS DATOS
            GridView DGVDETALLE_PEDIDO = (GridView)DataList1.Items[Convert.ToInt32(Session["COCINA_LISTA"].ToString())].FindControl("dgvDETALLE_PEDIDO");

            //AQUI CAPTURO EL VALOR DE MI GRIDVIEW DE LA FILA X Y DE LA COLUMNA X OSEA CAPTURO EL CODIGO DEL BIEN
            string ID_BIEN = DGVDETALLE_PEDIDO.Rows[Convert.ToInt32( Session["COCINA_FILA"].ToString())].Cells[1].Text.ToString();

            //OBTENIENDO EL ID_PEDIDO
            Label lblID_PEDIDO = (Label)DataList1.Items[Convert.ToInt32(Session["COCINA_LISTA"].ToString())].FindControl("lblNUM_PEDIDO");
            string ID_PEDIDO = lblID_PEDIDO.Text.ToString();
            //OBTENIENDO EL ITEM DEL PRODUCTO
            //LBLPRUEBA.Text=DGVDETALLE_PEDIDO.Rows[Convert.ToInt32(Session["COCINA_FILA"].ToString())].Cells[4].Text.ToString();
            int ITEM =Convert.ToInt32( DGVDETALLE_PEDIDO.Rows[Convert.ToInt32(Session["COCINA_FILA"].ToString())].Cells[4].Text.ToString());

            if (ESTADO_PEDIDO == "ESTADO PREPARADO")
            {
                //CON ESTO LLAMO A MI PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA FECHA DE PREPARADO
                N_OBJVENTAS.ACTUALIZA_FECHA_ESTADO_PEDIDO(ID_PEDIDO, ID_BIEN, ITEM, ESTADO_PEDIDO);
            }
            else
            {
             if(ESTADO_PEDIDO == "ESTADO DESHACER"){
                    //CON ESTO LLAMO A MI PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA FECHA DE DESPACHADO
                                    N_OBJVENTAS.ACTUALIZA_FECHA_ESTADO_PEDIDO(ID_PEDIDO, ID_BIEN, ITEM, ESTADO_PEDIDO);
             }
             else
             {
                 //CON ESTO LLAMO A MI PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA FECHA DE DESPACHADO
                 N_OBJVENTAS.ACTUALIZA_FECHA_ESTADO_PEDIDO(ID_PEDIDO, ID_BIEN, ITEM, ESTADO_PEDIDO);
             }
                
            }
            ACTUALIZAR_FECHA_ATENCION();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
           
            ACTUALIZAR_FECHA_ATENCION();
        }

        protected void btnANULAR_PEDIDO_Click(object sender, EventArgs e)
        {
            MANTENIMIENTO_PEDIDO_ANULAR();//ANULAMOS EL PEDIDO SELECCIONADO
            txtNUM_ANULADO.Text = string.Empty;
        }



       


       
        

    }
}