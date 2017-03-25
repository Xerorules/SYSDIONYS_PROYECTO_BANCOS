using CAPA_NEGOCIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIONYS_ERP
{
    public partial class Frm_CONTROLGALERIA_ESTADOSTIENDAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ID_EMPRESA"].ToString() != "004")
                {
                    Response.Redirect("FRM_PRINCIPAL.aspx");
                }
                rdbLISTAGALERIAS.SelectedIndex = 0;
                //CARGAR FILTROS
                rdbLISTAGALERIAS_SelectedIndexChanged(sender, e);
                // ===========================
                //cboPROPIETARIOS.SelectedIndex=0;
                cboPROPIETARIOS_SelectedIndexChanged(sender, e);
                //cboTIENDAS.SelectedIndex=0;
                //cboCONDICION.SelectedIndex = 0;
            }
        }

        #region OBJETOS_INSTANCIAR
        N_VENTA GALERIA = new N_VENTA();
        #endregion

        protected void btnGUARDAR_Click(object sender, EventArgs e)
        {

        }

        public void CARGAR_PROPIETARIOS(string NOMGALERIA)
        {
            DataTable dt = new DataTable();
            dt = GALERIA.LISTA_PROPIETARIOS(NOMGALERIA);
            cboPROPIETARIOS.DataSource = dt;
            cboPROPIETARIOS.DataValueField = "DESCRIPCION_D";
            cboPROPIETARIOS.DataTextField = "DESCRIPCION";
            cboPROPIETARIOS.DataBind();
        }

        public void CARGAR_TIENDAS(String GALERIA_OPC, String PROPIETARIO)
        {
            DataTable dt = new DataTable();
            dt = GALERIA.LISTA_TIENDASxPROPIETARIO(GALERIA_OPC, PROPIETARIO);
            cboTIENDAS.DataSource = dt;
            cboTIENDAS.DataValueField = "DESCRIPCION_T";
            cboTIENDAS.DataTextField = "DESCRIPCION";

            cboTIENDAS.DataBind();
        }

        protected void rdbLISTAGALERIAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CARGAR_PROPIETARIOS(rdbLISTAGALERIAS.SelectedItem.Text);
            cboPROPIETARIOS_SelectedIndexChanged(sender, e);
        }

        protected void cboPROPIETARIOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CARGAR_TIENDAS(rdbLISTAGALERIAS.SelectedItem.Text, cboPROPIETARIOS.SelectedValue.ToString());
        }

        protected void imgBtnCONSULTAR_Click(object sender, ImageClickEventArgs e)
        {
            gvDATOSCONTROL_ESTADOGALERIA.DataSource = GALERIA.ESTADOS_GALERIA(rdbLISTAGALERIAS.SelectedItem.Text, cboPROPIETARIOS.SelectedValue.ToString(), cboTIENDAS.SelectedValue.ToString());
            gvDATOSCONTROL_ESTADOGALERIA.DataBind();

        }

        protected void gvDATOSCONTROL_ESTADOGALERIA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox checkBox_Disponible = e.Row.Cells[4].Controls[0] as CheckBox;
                //checkBox_Disponible.Enabled = true;

                CheckBox checkBox_Ocupado = e.Row.Cells[5].Controls[0] as CheckBox;
                //checkBox_Ocupado.Enabled = true;

                CheckBox checkBox_Mantenimiento = e.Row.Cells[6].Controls[0] as CheckBox;
                //checkBox_Mantenimiento.Enabled = true;

                    if (checkBox_Disponible.Checked == true )
                    {
                        checkBox_Ocupado.Checked=false;
                        checkBox_Mantenimiento.Checked = false;
                    }
                    if (checkBox_Ocupado.Checked == true)
                    {
                        checkBox_Disponible.Checked = false;
                        checkBox_Mantenimiento.Checked = false;
                    }
                    if (checkBox_Mantenimiento.Checked == true)
                    {
                        checkBox_Disponible.Checked = false;
                        checkBox_Ocupado.Checked = false;
                    }





                    for (int i = 1; i < 5; i++)
                    {
                                e.Row.Cells[i].BackColor = System.Drawing.Color.OliveDrab;
                                //e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                           
                                //e.Row.Cells[i].BackColor = System.Drawing.Color.Firebrick;
                                //e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                            
                        
                    }
            }



            //if (e.Row.RowType==DataControlRowType.DataRow)
            //{

            //    Image img=(Image)e.Row.FindControl("CGESTADO");
            //    String cod = e.Row.Cells[16].Text.Trim();
            //    String ruta = String.Format("ICONOS/{OCUPADO}.png",cod);
            //    String formato = String.Format
            //}



            

            
        }

        protected void gvDATOSCONTROL_ESTADOGALERIA_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //ACTIVAR LOS CONTROLES

            gvDATOSCONTROL_ESTADOGALERIA.EditIndex = e.NewEditIndex;

            gvDATOSCONTROL_ESTADOGALERIA.DataSource = GALERIA.ESTADOS_GALERIA(rdbLISTAGALERIAS.SelectedItem.Text, cboPROPIETARIOS.SelectedValue.ToString(), cboTIENDAS.SelectedValue.ToString());
            gvDATOSCONTROL_ESTADOGALERIA.DataBind();
        }

        protected void gvDATOSCONTROL_ESTADOGALERIA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDATOSCONTROL_ESTADOGALERIA.EditIndex = -1;

            gvDATOSCONTROL_ESTADOGALERIA.DataSource = GALERIA.ESTADOS_GALERIA(rdbLISTAGALERIAS.SelectedItem.Text, cboPROPIETARIOS.SelectedValue.ToString(), cboTIENDAS.SelectedValue.ToString());
            gvDATOSCONTROL_ESTADOGALERIA.DataBind();
        }

        protected void gvDATOSCONTROL_ESTADOGALERIA_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Get the GridView Row.
            GridViewRow row = gvDATOSCONTROL_ESTADOGALERIA.Rows[e.RowIndex];

            //Get the HobbyId from the DataKey property.
            bool disponible = (row.Cells[4].Controls[0] as CheckBox).Checked;
            bool ocupado = (row.Cells[5].Controls[0] as CheckBox).Checked;
            bool mantenimiento = (row.Cells[6].Controls[0] as CheckBox).Checked;

            //extraigo el codigo de tienda para la actualizacion
            int codigo = Convert.ToInt32(gvDATOSCONTROL_ESTADOGALERIA.DataKeys[e.RowIndex].Values[0]);
            
            //ejecuto la sentencia de actualizacion
            if (disponible == true)
            {
                GALERIA.ACTUALIZAR_ESTADOGALERIA("DISPONIBLE",codigo);
            }
            if (ocupado == true)
            {
                GALERIA.ACTUALIZAR_ESTADOGALERIA("OCUPADO", codigo);
            }
            if (mantenimiento == true)
            {
                GALERIA.ACTUALIZAR_ESTADOGALERIA("MANTENIMIENTO", codigo);
            }
            
            

            //marco como no edicion
            gvDATOSCONTROL_ESTADOGALERIA.EditIndex = -1;

            gvDATOSCONTROL_ESTADOGALERIA.DataSource = GALERIA.ESTADOS_GALERIA(rdbLISTAGALERIAS.SelectedItem.Text, cboPROPIETARIOS.SelectedValue.ToString(), cboTIENDAS.SelectedValue.ToString());
            gvDATOSCONTROL_ESTADOGALERIA.DataBind();
        }

    }
}