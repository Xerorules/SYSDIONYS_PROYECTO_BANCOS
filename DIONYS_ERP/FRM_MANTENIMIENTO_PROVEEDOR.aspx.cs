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

namespace DIONYS_ERP
{
    public partial class FRM_MANTENIMIENTO_PROVEEDOR : System.Web.UI.Page
    {
        string vFILTRO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                P_LISTAR_DEPARTAMENTO();                                           //AQUI CARGAMOS LA LISTA DE DEPARTAMENTOS
                
                cboMP_DEPARTAMENTO_SelectedIndexChanged(sender, e);
                cboMP_PROVINCIA_SelectedIndexChanged(sender, e);

                FILTRO_LISTAR_DEPARTAMENTO();                                      //AQUI CARGO LA LISTA DE DEPARTAMENTOS  PARA EL FILTRO
                cboFILTRO_DEPARTAMENTO_SelectedIndexChanged(sender, e);
                cboFILTRO_PROVINCIA_SelectedIndexChanged(sender, e);

                ESTADO_TRANSACCION(1);//CON ESTO COLOCO EL FORMULARIO EN ESTADO DE CONSULTA
                vFILTRO = " ESTADO = 1";
                CONCATENAR_CONDICION();
                CARGAR_DATOS(vFILTRO);
            }
        }

        protected void btnV_GRABAR_Click(object sender, EventArgs e)
        {
            if (VALIDAR_PROVEEDOR())
            {
                MANTENIMIENTO_PROVEEDOR(true,1); //GRABAR UN PROVEEDOR
                ESTADO_TRANSACCION(1);
                vFILTRO = " ESTADO = 1";
                CARGAR_DATOS(vFILTRO);
            }
            else
            {
                Response.Write("<script>window.alert('ERROR, NO SE PUEDE GRABAR PORQUE HAY DATOS INCORRECTOS');</script>");
            }
        }
        #region OBJETOS
        E_MANT_PROVEEDOR E_OBJPROVEEDOR = new E_MANT_PROVEEDOR();
        N_VENTA N_OBJPROVEEDOR = new N_VENTA();
        #endregion

        #region FUNCIONES

        #endregion

        #region PROCEDIMIENTOS
        protected void cboMP_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            P_LISTAR_PROVINCIA(cboMP_DEPARTAMENTO.SelectedValue.ToString());
            cboMP_PROVINCIA_SelectedIndexChanged(sender, e);
        }

        protected void cboMP_PROVINCIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            P_LISTAR_DISTRITO(cboMP_PROVINCIA.SelectedValue.ToString());
        }

        public void CARGAR_DATOS(string pCONDICION)
        {
            dgvLISTADO_PROVEEDOR.DataSource = N_OBJPROVEEDOR.CONSULTA_LISTA_PROVEEDORES(pCONDICION);
            dgvLISTADO_PROVEEDOR.DataBind();
        }

        public string CONCATENAR_CONDICION()
        {
            if (txtFILTRO_DESCRIPCION.Text != string.Empty)
            {
                vFILTRO = " DESCRIPCION LIKE '%"+txtFILTRO_DESCRIPCION.Text+"%'";
            }

            if (txtFILTRO_RUCDNI.Text != string.Empty)
            {
                vFILTRO += " AND RUC_DNI LIKE '%" + txtFILTRO_RUCDNI.Text + "%'";
            }
            if (cboFILTRO_TIPOPROVEEDOR.SelectedIndex != 0)
            {
                vFILTRO += " AND TIPO_PROVEEDOR = '" + cboFILTRO_TIPOPROVEEDOR.SelectedValue + "'";
            }
            if (cboFILTRO_DEPARTAMENTO.SelectedIndex != 0)
            {
                vFILTRO += " AND UBIDEN = '" + cboFILTRO_DEPARTAMENTO.SelectedItem.Text + "'";
            }
            if (cboFILTRO_PROVINCIA.SelectedIndex != 0)
            {
                vFILTRO += " AND UBIPRN = '" + cboFILTRO_PROVINCIA.SelectedItem.Text + "'";
            }
            if (cboFILTRO_DISTRITO.SelectedIndex != 0)
            {
                vFILTRO += " AND UBIDSN = '" + cboFILTRO_DISTRITO.SelectedItem.Text + "'";
            }



            return vFILTRO;
        }
        void CONSULTAR_ULTIMO_PROVEEDOR( string ID_PROVEEDOR)
        {
            DataTable DT = new DataTable();
            DT = N_OBJPROVEEDOR.CONSULTA_ULTIMO_PROVEEDOR(ID_PROVEEDOR);

            //RESULTADOS PUESTOS 
            txtID_PROVEEDOR.Text=DT.Rows[0]["ID_PROVEEDOR"].ToString();
            rdbMP_TIPOPROVEEDOR.SelectedValue = DT.Rows[0]["TIPO_PROVEEDOR"].ToString();
            rdbMP_ORIGENPROV.SelectedValue = DT.Rows[0]["ORIGEN_PROVEEDOR"].ToString();
            txtMP_DESCRIPCION.Text = DT.Rows[0]["DESCRIPCION"].ToString();
            txtMP_RUCDNI.Text = DT.Rows[0]["RUC_DNI"].ToString();
            txtMP_DIRECCION.Text = DT.Rows[0]["DIRECCION"].ToString();
            txtMP_TELEFONO1.Text = DT.Rows[0]["TELEFONO_1"].ToString();
            txtMP_TELEFONO2.Text = DT.Rows[0]["TELEFONO_2"].ToString();
            txtMP_MOVIL.Text = DT.Rows[0]["MOVIL"].ToString();
            txtMP_FNACIMIENTO.Text = DT.Rows[0]["FECHA_NAC"].ToString();
            txtMP_FULTIMACOMPRA.Text = DT.Rows[0]["FECHA_ULTCOMPRA"].ToString();
            txtMP_EMAIL.Text = DT.Rows[0]["EMAIL"].ToString();
            txtMP_WEBSITE.Text = DT.Rows[0]["WEB_SITE"].ToString();

            P_LISTAR_DEPARTAMENTO();
            cboMP_DEPARTAMENTO.SelectedItem.Text = DT.Rows[0]["UBIDEN"].ToString();

            P_LISTAR_PROVINCIA(cboMP_DEPARTAMENTO.SelectedValue.ToString());
            cboMP_PROVINCIA.SelectedItem.Text = DT.Rows[0]["UBIPRN"].ToString();

            P_LISTAR_DISTRITO(cboMP_PROVINCIA.SelectedValue.ToString());
            cboMP_DISTRITO.SelectedItem.Text = DT.Rows[0]["UBIDSN"].ToString();
            
            
            

        }
        public bool VALIDAR_PROVEEDOR()
        {
            bool RESULTADO = false;
            if (rdbMP_TIPOPROVEEDOR.SelectedIndex != -1 && rdbMP_ORIGENPROV.SelectedIndex != -1 && txtMP_DESCRIPCION.Text != string.Empty && txtMP_RUCDNI.Text != string.Empty && txtMP_DIRECCION.Text != string.Empty &&
              txtMP_TELEFONO1.Text!=string.Empty && txtMP_EMAIL.Text != string.Empty)
          {
              RESULTADO = true;
          }
          else
          {
              RESULTADO = false;
          }
            return RESULTADO;
        }

        public void MANTENIMIENTO_PROVEEDOR(bool ESTADO,int ACCION)
        {
            
            try 
	        {	        
		        E_OBJPROVEEDOR.ID_PROVEEDOR = string.Empty;
                E_OBJPROVEEDOR.TIPO_PROVEEDOR =rdbMP_TIPOPROVEEDOR.SelectedValue.ToString();
                E_OBJPROVEEDOR.ORIGEN_PROVEEDOR = rdbMP_ORIGENPROV.SelectedValue.ToString();
                E_OBJPROVEEDOR.DESCRIPCION =txtMP_DESCRIPCION.Text;
                E_OBJPROVEEDOR.RUC_DNI=txtMP_RUCDNI.Text;
                E_OBJPROVEEDOR.DIRECCION=txtMP_DIRECCION.Text;
                E_OBJPROVEEDOR.TELEFONO_1= txtMP_TELEFONO1.Text;
                E_OBJPROVEEDOR.TELEFONO_2=txtMP_TELEFONO2.Text;
                E_OBJPROVEEDOR.MOVIL=txtMP_MOVIL.Text;
                E_OBJPROVEEDOR.FECHA_NAC= txtMP_FNACIMIENTO.Text;
                E_OBJPROVEEDOR.EMAIL=txtMP_EMAIL.Text;
                E_OBJPROVEEDOR.WEB_SITE= txtMP_WEBSITE.Text;;
                E_OBJPROVEEDOR.ESTADO=ESTADO;
                E_OBJPROVEEDOR.UBIDST=cboMP_DISTRITO.SelectedValue;
                E_OBJPROVEEDOR.ACCION=ACCION;
                N_OBJPROVEEDOR.MANTENIMIENTO_PROVEEDOR(E_OBJPROVEEDOR);

	        }
	        catch (Exception)
	        {

                Response.Write("<script>window.alert('SE ESTA INGRESANDO DATOS QUE YA EXISTEN');</script>");
	        }
            
        }

        void ESTADO_TRANSACCION(int ESTADO)
        {
            if (ESTADO == 1) //ESTADO CONSULTA
            {

                //LIMPIANDO CONTROLES

                rdbMP_TIPOPROVEEDOR.SelectedIndex = -1;
                rdbMP_ORIGENPROV.SelectedIndex = -1;
                txtMP_DESCRIPCION.Text = string.Empty;
                txtMP_RUCDNI.Text = string.Empty;
                txtMP_DIRECCION.Text = string.Empty;
                txtMP_TELEFONO1.Text = string.Empty;
                txtMP_FNACIMIENTO.Text = string.Empty;
                txtMP_EMAIL.Text = string.Empty;
                cboMP_DEPARTAMENTO.SelectedIndex = 0;
                cboMP_PROVINCIA.SelectedIndex = 0;
                cboMP_DISTRITO.SelectedIndex = 0;
                txtMP_TELEFONO2.Text = string.Empty;
                txtMP_WEBSITE.Text = string.Empty;
                txtMP_MOVIL.Text = string.Empty;
                txtID_PROVEEDOR.Text = string.Empty;
                txtMP_FULTIMACOMPRA.Text = string.Empty;
                txtFILTRO_DESCRIPCION.Text = string.Empty;
                txtFILTRO_RUCDNI.Text = string.Empty;
                cboFILTRO_TIPOPROVEEDOR.SelectedIndex = 0;
                cboFILTRO_DEPARTAMENTO.SelectedIndex = 0;
                cboFILTRO_PROVINCIA.SelectedIndex = 0;
                cboFILTRO_DISTRITO.SelectedIndex = 0;
                //====================

                rdbMP_TIPOPROVEEDOR.Enabled = false;
                rdbMP_ORIGENPROV.Enabled = false;
                txtMP_DESCRIPCION.ReadOnly = true;
                txtMP_RUCDNI.ReadOnly = true;
                txtMP_DIRECCION.ReadOnly = true;
                txtMP_TELEFONO1.ReadOnly = true;
                txtMP_FNACIMIENTO.ReadOnly = true;
                txtMP_EMAIL.ReadOnly = true;
                cboMP_DEPARTAMENTO.Enabled = false;
                cboMP_PROVINCIA.Enabled = false;
                cboMP_DISTRITO.Enabled = false;
                txtMP_TELEFONO2.ReadOnly = true;
                txtMP_WEBSITE.ReadOnly = true;
                txtMP_MOVIL.ReadOnly = true;
                txtMP_FULTIMACOMPRA.ReadOnly = true;
                txtID_PROVEEDOR.ReadOnly = true;
                dgvLISTADO_PROVEEDOR.Enabled = true;
                txtFILTRO_DESCRIPCION.ReadOnly = false;
                txtFILTRO_RUCDNI.ReadOnly = false;
                cboFILTRO_TIPOPROVEEDOR.Enabled = true;
                cboFILTRO_DEPARTAMENTO.Enabled = true;
                cboFILTRO_PROVINCIA.Enabled = true;
                cboFILTRO_DISTRITO.Enabled = true;
                btnMP_NUEVO.Enabled = true;
                btnMP_GRABAR.Enabled = false;
                btnMP_CANCELAR.Enabled = false;
                btnMP_ANULAR.Enabled = true;
            }
            if (ESTADO == 2) //ESTADO NUEVO
            {
                //LIMPIARDO CONTROLES

                rdbMP_TIPOPROVEEDOR.SelectedIndex = -1;
                rdbMP_ORIGENPROV.SelectedIndex = -1;
                txtMP_DESCRIPCION.Text = string.Empty;
                txtMP_RUCDNI.Text = string.Empty;
                txtMP_DIRECCION.Text = string.Empty;
                txtMP_TELEFONO1.Text = string.Empty;
                txtMP_FNACIMIENTO.Text = string.Empty;
                txtMP_EMAIL.Text = string.Empty;
                cboMP_DEPARTAMENTO.SelectedItem.Text = "-- SELECCIONAR --";
                cboMP_PROVINCIA.SelectedItem.Text = "-- SELECCIONAR --";
                cboMP_DISTRITO.SelectedItem.Text = "-- SELECCIONAR --";
                txtMP_TELEFONO2.Text = string.Empty;
                txtMP_WEBSITE.Text = string.Empty;
                txtMP_MOVIL.Text = string.Empty;
                txtMP_FULTIMACOMPRA.Text = string.Empty;
                txtID_PROVEEDOR.Text = string.Empty;
                txtFILTRO_DESCRIPCION.Text = string.Empty;
                txtFILTRO_RUCDNI.Text = string.Empty;
                cboFILTRO_TIPOPROVEEDOR.SelectedIndex = 0;
                cboFILTRO_DEPARTAMENTO.SelectedIndex = 0;
                cboFILTRO_PROVINCIA.SelectedIndex = 0;
                cboFILTRO_DISTRITO.SelectedIndex = 0;

                //==================================================

                rdbMP_TIPOPROVEEDOR.Enabled = true;
                rdbMP_ORIGENPROV.Enabled = true;
                txtMP_DESCRIPCION.ReadOnly = false;
                txtMP_RUCDNI.ReadOnly = false;
                txtMP_DIRECCION.ReadOnly = false;
                txtMP_TELEFONO1.ReadOnly = false;
                txtMP_FNACIMIENTO.ReadOnly = false;
                txtMP_EMAIL.ReadOnly = false;
                cboMP_DEPARTAMENTO.Enabled = true;
                cboMP_PROVINCIA.Enabled = true;
                cboMP_DISTRITO.Enabled = true;
                txtMP_TELEFONO2.ReadOnly = false;
                txtMP_WEBSITE.ReadOnly = false;
                txtMP_MOVIL.ReadOnly = false;
                txtMP_FULTIMACOMPRA.ReadOnly = true;
                txtID_PROVEEDOR.ReadOnly = true;
                dgvLISTADO_PROVEEDOR.Enabled = false;
                txtFILTRO_DESCRIPCION.ReadOnly = false;
                txtFILTRO_RUCDNI.ReadOnly = false;
                cboFILTRO_TIPOPROVEEDOR.Enabled = true;
                cboFILTRO_DEPARTAMENTO.Enabled = true;
                cboFILTRO_PROVINCIA.Enabled = true;
                cboFILTRO_DISTRITO.Enabled = true;
                btnMP_NUEVO.Enabled = false;
                btnMP_GRABAR.Enabled = true;
                btnMP_CANCELAR.Enabled = true;
                btnMP_ANULAR.Enabled = false;
            }
        }
        #endregion

        protected void btnV_NUEVO_Click(object sender, EventArgs e)
        {
            ESTADO_TRANSACCION(2); //LLAMAMOS AL PROCEDIMIENTO PARA GENERAR UN NUEVO REGISTRO
                                  //CON ESTO GENERO EL NUMERO CORRELATIVO DE MI VENTA DETALLADA

        }

        protected void btnV_CANCELAR_Click(object sender, EventArgs e)
        {
            ESTADO_TRANSACCION(1);
        }

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            vFILTRO= " ESTADO = 1";
            CARGAR_DATOS(CONCATENAR_CONDICION());
        }


        void P_LISTAR_DEPARTAMENTO()
        {
            DataTable dt = N_OBJPROVEEDOR.LISTAR_DEPARTAMENTO();
            cboMP_DEPARTAMENTO.DataSource = dt;
            cboMP_DEPARTAMENTO.DataValueField = "UBIDEP";
            cboMP_DEPARTAMENTO.DataTextField = "UBIDEN";

            cboMP_DEPARTAMENTO.DataBind();
        }
        void P_LISTAR_PROVINCIA(string depart)
        {
            DataTable dt = N_OBJPROVEEDOR.LISTAR_PROVINCIA(depart);
            cboMP_PROVINCIA.DataSource = dt;
            cboMP_PROVINCIA.DataValueField = "UBIPRV";
            cboMP_PROVINCIA.DataTextField = "UBIPRN";

            cboMP_PROVINCIA.DataBind();
        }
        void P_LISTAR_DISTRITO(string prov)
        {
            DataTable dt = N_OBJPROVEEDOR.LISTAR_DISTRITO(prov);
            cboMP_DISTRITO.DataSource = dt;
            cboMP_DISTRITO.DataValueField = "UBIDST";
            cboMP_DISTRITO.DataTextField = "UBIDSN";

            cboMP_DISTRITO.DataBind();
        }

        protected void dgvLISTADO_PROVEEDOR_SelectedIndexChanged(object sender, EventArgs e)
        {

            CONSULTAR_ULTIMO_PROVEEDOR(dgvLISTADO_PROVEEDOR.SelectedRow.Cells[1].Text);
        }

        protected void btnMP_ANULAR_Click(object sender, EventArgs e)
        {
                ANULAR_PROVEEDOR_SELECCIONADO();
                vFILTRO = " ESTADO = 1";
                CARGAR_DATOS(vFILTRO);
                ESTADO_TRANSACCION(1);
            
        }
        public void ANULAR_PROVEEDOR_SELECCIONADO()
        {
            string ID_PROVEEDOR = txtID_PROVEEDOR.Text;
            N_OBJPROVEEDOR.ANULAR_PROVEEDOR(ID_PROVEEDOR);

        }



        void FILTRO_LISTAR_DEPARTAMENTO()
        {
            DataTable dt = N_OBJPROVEEDOR.LISTAR_DEPARTAMENTO();
            cboFILTRO_DEPARTAMENTO.DataSource = dt;
            cboFILTRO_DEPARTAMENTO.DataValueField = "UBIDEP";
            cboFILTRO_DEPARTAMENTO.DataTextField = "UBIDEN";

            cboFILTRO_DEPARTAMENTO.DataBind();
        }
        void FILTRO_LISTAR_PROVINCIA(string depart)
        {
            DataTable dt = N_OBJPROVEEDOR.LISTAR_PROVINCIA(depart);
            cboFILTRO_PROVINCIA.DataSource = dt;
            cboFILTRO_PROVINCIA.DataValueField = "UBIPRV";
            cboFILTRO_PROVINCIA.DataTextField = "UBIPRN";

            cboFILTRO_PROVINCIA.DataBind();
        }
        void FILTRO_LISTAR_DISTRITO(string prov)
        {
            DataTable dt = N_OBJPROVEEDOR.LISTAR_DISTRITO(prov);
            cboFILTRO_DISTRITO.DataSource = dt;
            cboFILTRO_DISTRITO.DataValueField = "UBIDST";
            cboFILTRO_DISTRITO.DataTextField = "UBIDSN";

            cboFILTRO_DISTRITO.DataBind();
        }

        protected void cboFILTRO_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            FILTRO_LISTAR_PROVINCIA(cboFILTRO_DEPARTAMENTO.SelectedValue.ToString());
            cboFILTRO_PROVINCIA_SelectedIndexChanged(sender, e);
        }

        protected void cboFILTRO_PROVINCIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            FILTRO_LISTAR_DISTRITO(cboFILTRO_PROVINCIA.SelectedValue.ToString());
        }

       

        

        #region EVENTOS
        
        #endregion
    }
}
