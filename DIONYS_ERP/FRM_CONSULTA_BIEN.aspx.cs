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

namespace DIONYS_ERP
{
    public partial class FRM_CONSULTA_BIEN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region OBJETOS
        N_VENTA N_OBJVENTAS = new N_VENTA();
        E_VENTA E_OBJVENTAS = new E_VENTA();
        E_MANT_CLIENTE E_OBJMANT_CLIENTE = new E_MANT_CLIENTE();
        E_VENTA_Y_DETALLE E_OBJMANT_VENTADET = new E_VENTA_Y_DETALLE();
        #endregion

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            FILTRAR_BIEN_XCODIGO_XDESCRIPCION(); 
        }
        void FILTRAR_BIEN_XCODIGO_XDESCRIPCION()
        {
            DataTable dt = new DataTable();
            if(Session["SERIE"].ToString()!= "0003")
            {
                E_OBJVENTAS.OPCION_CLASE_BUSCAR = "1"; //SI ES DIFERENTE DE LA SERIE 0003 FILTRO LOS BIENES DE SERVICIO
            }
            else
            {
                E_OBJVENTAS.OPCION_CLASE_BUSCAR = "2"; //SINO FILTRO LOS BIENES DE CONSUMO
            }
            if (cboOPCION.SelectedIndex == 1)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = txtBUSCARBIEN.Text;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = null;
            }
            if (cboOPCION.SelectedIndex == 2)
            {
                E_OBJVENTAS.ID_BIEN_BUSCAR = null;
                E_OBJVENTAS.DESCRIPCION_BUSCAR = txtBUSCARBIEN.Text;
            }

            dt = N_OBJVENTAS.FILTRAR_BIEN_XCODIGO_XDESCRIPCION(E_OBJVENTAS);
            dgvFILTRADOBIENES.DataSource = dt;
            dgvFILTRADOBIENES.DataBind();
        }

        protected void dgvFILTRADOBIENES_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdOK")
            {

                GridViewRow fila_sel = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string CODIGO = dgvFILTRADOBIENES.DataKeys[fila_sel.RowIndex].Values[0].ToString();
                string DESCRIPCION = dgvFILTRADOBIENES.DataKeys[fila_sel.RowIndex].Values[1].ToString();
                string PRECIO = dgvFILTRADOBIENES.DataKeys[fila_sel.RowIndex].Values[2].ToString();

                //OBTENER_ID_BIEN_Y_LLENAR_GRILLA(CODIGO, DESCRIPCION, PRECIO);

                Session["ID_BIEN"] = CODIGO;
                Session["DESCRIPCION_BIEN"] = DESCRIPCION;
                Session["PRECIO_BIEN"] = PRECIO;
                Response.Redirect("FRM_MENUVENTA.aspx");
            }
        }
        void OBTENER_ID_BIEN_Y_LLENAR_GRILLA(string ID_BIEN, string DESCRIPCION, string PRECIO)
        {

            DataTable dt = (DataTable)Session["detalleBien"];

            try
            {
                DataRow row = dt.NewRow();
                row["ID_BIEN"] = ID_BIEN;
                row["CANT"] = Convert.ToDouble("1");
                row["DESCRIPCION"] = DESCRIPCION;
                row["PRECIO"] = PRECIO;
                row["IMPORTE"] = Convert.ToDouble(row["PRECIO"]) * Convert.ToDouble(row["CANT"]); ;
                dt.Rows.Add(row);
                dt.AcceptChanges();

                //LLENAR_GRILLA();
                //ACTUALIZAR_TOTALES();
            }
            catch (Exception)
            {
                Response.Write("<script>window.alert('EL BIEN YA ESTA EN LA LISTA');</script>");

            }


        }

        protected void btnCANCELAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("FRM_MENUVENTA.aspx");
        }



    }
}