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
    public partial class FRM_MANTENIMIENTO_CLIENTE : System.Web.UI.Page
    {
        string vFILTRO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                P_LISTAR_DEPARTAMENTO();                                           //AQUI CARGAMOS LA LISTA DE DEPARTAMENTOS
                
                cboMC_DEPARTAMENTO_SelectedIndexChanged(sender, e);
                cboMC_PROVINCIA_SelectedIndexChanged(sender, e);

                FILTRO_LISTAR_DEPARTAMENTO();                                      //AQUI CARGO LA LISTA DE DEPARTAMENTOS  PARA EL FILTRO
                cboFILTRO_DEPARTAMENTO_SelectedIndexChanged(sender, e);
                cboFILTRO_PROVINCIA_SelectedIndexChanged(sender, e);

                ESTADO_TRANSACCION(1); //CON ESTO COLOCO EL FORMULARIO EN ESTADO DE CONSULTA
                vFILTRO = " ESTADO = 1";
                CONCATENAR_CONDICION();
                CARGAR_DATOS(vFILTRO);
            }
        }

        protected void btnV_GRABAR_Click(object sender, EventArgs e)
        {
            if (VALIDAR_VENTA())
            {
                MANTENIMIENTO_CLIENTE(true,"1"); //graba UN CLIENTE
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
        E_MANT_CLIENTE E_OBJCLIENTE = new E_MANT_CLIENTE();
        N_VENTA N_OBJCLIENTE = new N_VENTA();
        #endregion

        #region FUNCIONES

        #endregion

        #region PROCEDIMIENTOS
        protected void cboMC_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            P_LISTAR_PROVINCIA(cboMC_DEPARTAMENTO.SelectedValue.ToString());
            cboMC_PROVINCIA_SelectedIndexChanged(sender, e);
        }

        protected void cboMC_PROVINCIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            P_LISTAR_DISTRITO(cboMC_PROVINCIA.SelectedValue.ToString());
        }

        public void CARGAR_DATOS(string pCONDICION)
        {
            dgvLISTADO_CLIENTE.DataSource = N_OBJCLIENTE.CONSULTA_LISTA_CLIENTES(pCONDICION);
            dgvLISTADO_CLIENTE.DataBind();
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
            if (cboFILTRO_TIPOCLIENTE.SelectedIndex != 0)
            {
                vFILTRO += " AND TIPO_CLIENTE = '" + cboFILTRO_TIPOCLIENTE.SelectedValue + "'";
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
        void CONSULTAR_ULTIMO_CLIENTE( string ID_CLIENTE)
        {
            DataTable DT = new DataTable();
            DT = N_OBJCLIENTE.CONSULTA_ULTIMO_CLIENTE(ID_CLIENTE);

            //RESULTADOS PUESTOS 
            txtID_CLIENTE.Text=DT.Rows[0]["ID_CLIENTE"].ToString();
            rdbMC_TIPOCLIENTE.SelectedValue = DT.Rows[0]["TIPO_CLIENTE"].ToString();
            txtMC_DESCRIPCION.Text = DT.Rows[0]["DESCRIPCION"].ToString();
            txtMC_RUCDNI.Text = DT.Rows[0]["RUC_DNI"].ToString();
            txtMC_DIRECCION.Text = DT.Rows[0]["DIRECCION"].ToString();
            txtMC_TELEFONO1.Text = DT.Rows[0]["TELEFONO_1"].ToString();
            txtMC_TELEFONO2.Text = DT.Rows[0]["TELEFONO_2"].ToString();
            txtMC_MOVIL.Text = DT.Rows[0]["MOVIL"].ToString();
            txtMC_FNACIMIENTO.Text = DT.Rows[0]["FECHA_NAC"].ToString();
            txtMC_FULTIMAVENTA.Text = DT.Rows[0]["FECHA_ULTVENTA"].ToString();
            txtMC_EMAIL.Text = DT.Rows[0]["EMAIL"].ToString();
            txtMC_WEBSITE.Text = DT.Rows[0]["WEB_SITE"].ToString();

            P_LISTAR_DEPARTAMENTO();
            cboMC_DEPARTAMENTO.SelectedItem.Text = DT.Rows[0]["UBIDEN"].ToString();

            P_LISTAR_PROVINCIA(cboMC_DEPARTAMENTO.SelectedValue.ToString());
            cboMC_PROVINCIA.SelectedItem.Text = DT.Rows[0]["UBIPRN"].ToString();

            P_LISTAR_DISTRITO(cboMC_PROVINCIA.SelectedValue.ToString());
            cboMC_DISTRITO.SelectedItem.Text = DT.Rows[0]["UBIDSN"].ToString();
            
            
            

        }
        public bool VALIDAR_VENTA()
        {
            bool RESULTADO = false;
          if(rdbMC_TIPOCLIENTE.SelectedIndex != -1 && txtMC_DESCRIPCION.Text != string.Empty && txtMC_RUCDNI.Text != string.Empty && txtMC_DIRECCION.Text != string.Empty &&
              txtMC_TELEFONO1.Text!=string.Empty && txtMC_EMAIL.Text != string.Empty)
          {
              RESULTADO = true;
          }
          else
          {
              RESULTADO = false;
          }
            return RESULTADO;
        }

        public void MANTENIMIENTO_CLIENTE(bool ESTADO,string ACCION)
        {
            
            try 
	        {	        
		        E_OBJCLIENTE.ID_CLIENTE = string.Empty;
                E_OBJCLIENTE.TIPO_CLIENTE =rdbMC_TIPOCLIENTE.SelectedValue.ToString();
                E_OBJCLIENTE.DESCRIPCION =txtMC_DESCRIPCION.Text;
                E_OBJCLIENTE.RUC_DNI=txtMC_RUCDNI.Text;
                E_OBJCLIENTE.DIRECCION=txtMC_DIRECCION.Text;
                E_OBJCLIENTE.TELEFONO_1= txtMC_TELEFONO1.Text;
                E_OBJCLIENTE.TELEFONO_2=txtMC_TELEFONO2.Text;
                E_OBJCLIENTE.MOVIL=txtMC_MOVIL.Text;
                E_OBJCLIENTE.FECHA_NAC= txtMC_FNACIMIENTO.Text;
                E_OBJCLIENTE.EMAIL=txtMC_EMAIL.Text;
                E_OBJCLIENTE.WEB_SITE= txtMC_WEBSITE.Text;;
                E_OBJCLIENTE.ESTADO=ESTADO;
                E_OBJCLIENTE.UBIDST=cboMC_DISTRITO.SelectedValue;
                E_OBJCLIENTE.ACCION=ACCION;
                N_OBJCLIENTE.MANTENIMIENTO_CLIENTE(E_OBJCLIENTE);

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

                rdbMC_TIPOCLIENTE.SelectedIndex = -1;
                txtMC_DESCRIPCION.Text = string.Empty;
                txtMC_RUCDNI.Text = string.Empty;
                txtMC_DIRECCION.Text = string.Empty;
                txtMC_TELEFONO1.Text = string.Empty;
                txtMC_FNACIMIENTO.Text = string.Empty;
                txtMC_EMAIL.Text = string.Empty;
                cboMC_DEPARTAMENTO.SelectedIndex = 0;
                cboMC_PROVINCIA.SelectedIndex = 0;
                cboMC_DISTRITO.SelectedIndex = 0;
                txtMC_TELEFONO2.Text = string.Empty;
                txtMC_WEBSITE.Text = string.Empty;
                txtMC_MOVIL.Text = string.Empty;
                txtID_CLIENTE.Text = string.Empty;
                txtMC_FULTIMAVENTA.Text = string.Empty;
                txtFILTRO_DESCRIPCION.Text = string.Empty;
                txtFILTRO_RUCDNI.Text = string.Empty;
                cboFILTRO_TIPOCLIENTE.SelectedIndex = 0;
                cboFILTRO_DEPARTAMENTO.SelectedIndex = 0;
                cboFILTRO_PROVINCIA.SelectedIndex = 0;
                cboFILTRO_DISTRITO.SelectedIndex = 0;
                //====================

                rdbMC_TIPOCLIENTE.Enabled = false;
                txtMC_DESCRIPCION.ReadOnly = true;
                txtMC_RUCDNI.ReadOnly = true;
                txtMC_DIRECCION.ReadOnly = true;
                txtMC_TELEFONO1.ReadOnly = true;
                txtMC_FNACIMIENTO.ReadOnly = true;
                txtMC_EMAIL.ReadOnly = true;
                cboMC_DEPARTAMENTO.Enabled = false;
                cboMC_PROVINCIA.Enabled = false;
                cboMC_DISTRITO.Enabled = false;
                txtMC_TELEFONO2.ReadOnly = true;
                txtMC_WEBSITE.ReadOnly = true;
                txtMC_MOVIL.ReadOnly = true;
                txtMC_FULTIMAVENTA.ReadOnly = true;
                txtID_CLIENTE.ReadOnly = true;
                dgvLISTADO_CLIENTE.Enabled = true;
                txtFILTRO_DESCRIPCION.ReadOnly = false;
                txtFILTRO_RUCDNI.ReadOnly = false;
                cboFILTRO_TIPOCLIENTE.Enabled = true;
                cboFILTRO_DEPARTAMENTO.Enabled = true;
                cboFILTRO_PROVINCIA.Enabled = true;
                cboFILTRO_DISTRITO.Enabled = true;
                btnMC_NUEVO.Enabled = true;
                btnMC_GRABAR.Enabled = false;
                btnMC_CANCELAR.Enabled = false;
                btnMC_ANULAR.Enabled = true;
            }
            if (ESTADO == 2) //ESTADO NUEVO
            {
                //LIMPIARDO CONTROLES

                rdbMC_TIPOCLIENTE.SelectedIndex = -1;
                txtMC_DESCRIPCION.Text = string.Empty;
                txtMC_RUCDNI.Text = string.Empty;
                txtMC_DIRECCION.Text = string.Empty;
                txtMC_TELEFONO1.Text = string.Empty;
                txtMC_FNACIMIENTO.Text = string.Empty;
                txtMC_EMAIL.Text = string.Empty;
                cboMC_DEPARTAMENTO.SelectedItem.Text = "-- SELECCIONAR --";
                cboMC_PROVINCIA.SelectedItem.Text = "-- SELECCIONAR --";
                cboMC_DISTRITO.SelectedItem.Text = "-- SELECCIONAR --";
                txtMC_TELEFONO2.Text = string.Empty;
                txtMC_WEBSITE.Text = string.Empty;
                txtMC_MOVIL.Text = string.Empty;
                txtMC_FULTIMAVENTA.Text = string.Empty;
                txtID_CLIENTE.Text = string.Empty;
                txtFILTRO_DESCRIPCION.Text = string.Empty;
                txtFILTRO_RUCDNI.Text = string.Empty;
                cboFILTRO_TIPOCLIENTE.SelectedIndex = 0;
                cboFILTRO_DEPARTAMENTO.SelectedIndex = 0;
                cboFILTRO_PROVINCIA.SelectedIndex = 0;
                cboFILTRO_DISTRITO.SelectedIndex = 0;

                //==================================================

                rdbMC_TIPOCLIENTE.Enabled = true;
                txtMC_DESCRIPCION.ReadOnly = false;
                txtMC_RUCDNI.ReadOnly = false;
                txtMC_DIRECCION.ReadOnly = false;
                txtMC_TELEFONO1.ReadOnly = false;
                txtMC_FNACIMIENTO.ReadOnly = false;
                txtMC_EMAIL.ReadOnly = false;
                cboMC_DEPARTAMENTO.Enabled = true;
                cboMC_PROVINCIA.Enabled = true;
                cboMC_DISTRITO.Enabled = true;
                txtMC_TELEFONO2.ReadOnly = false;
                txtMC_WEBSITE.ReadOnly = false;
                txtMC_MOVIL.ReadOnly = false;
                txtMC_FULTIMAVENTA.ReadOnly = true;
                txtID_CLIENTE.ReadOnly = true;
                dgvLISTADO_CLIENTE.Enabled = false;
                txtFILTRO_DESCRIPCION.ReadOnly = true;
                txtFILTRO_RUCDNI.ReadOnly = true;
                cboFILTRO_TIPOCLIENTE.Enabled = false;
                cboFILTRO_DEPARTAMENTO.Enabled = false;
                cboFILTRO_PROVINCIA.Enabled = false;
                cboFILTRO_DISTRITO.Enabled = false;
                btnMC_NUEVO.Enabled = false;
                btnMC_GRABAR.Enabled = true;
                btnMC_CANCELAR.Enabled = true;
                btnMC_ANULAR.Enabled = false;
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
            DataTable dt = N_OBJCLIENTE.LISTAR_DEPARTAMENTO();
            cboMC_DEPARTAMENTO.DataSource = dt;
            cboMC_DEPARTAMENTO.DataValueField = "UBIDEP";
            cboMC_DEPARTAMENTO.DataTextField = "UBIDEN";

            cboMC_DEPARTAMENTO.DataBind();
        }
        void P_LISTAR_PROVINCIA(string depart)
        {
            DataTable dt = N_OBJCLIENTE.LISTAR_PROVINCIA(depart);
            cboMC_PROVINCIA.DataSource = dt;
            cboMC_PROVINCIA.DataValueField = "UBIPRV";
            cboMC_PROVINCIA.DataTextField = "UBIPRN";

            cboMC_PROVINCIA.DataBind();
        }
        void P_LISTAR_DISTRITO(string prov)
        {
            DataTable dt = N_OBJCLIENTE.LISTAR_DISTRITO(prov);
            cboMC_DISTRITO.DataSource = dt;
            cboMC_DISTRITO.DataValueField = "UBIDST";
            cboMC_DISTRITO.DataTextField = "UBIDSN";

            cboMC_DISTRITO.DataBind();
        }

        protected void dgvLISTADO_CLIENTE_SelectedIndexChanged(object sender, EventArgs e)
        {

            CONSULTAR_ULTIMO_CLIENTE(dgvLISTADO_CLIENTE.SelectedRow.Cells[1].Text);
        }

        protected void btnMC_ANULAR_Click(object sender, EventArgs e)
        {
                ANULAR_CLIENTE_SELECCIONADO();
                vFILTRO = " ESTADO = 1";
                CARGAR_DATOS(vFILTRO);
                ESTADO_TRANSACCION(1);
            
        }
        public void ANULAR_CLIENTE_SELECCIONADO()
        {
            string ID_CLIENTE = txtID_CLIENTE.Text;
            N_OBJCLIENTE.ANULAR_CLIENTE(ID_CLIENTE);

        }



        void FILTRO_LISTAR_DEPARTAMENTO()
        {
            DataTable dt = N_OBJCLIENTE.LISTAR_DEPARTAMENTO();
            cboFILTRO_DEPARTAMENTO.DataSource = dt;
            cboFILTRO_DEPARTAMENTO.DataValueField = "UBIDEP";
            cboFILTRO_DEPARTAMENTO.DataTextField = "UBIDEN";

            cboFILTRO_DEPARTAMENTO.DataBind();
        }
        void FILTRO_LISTAR_PROVINCIA(string depart)
        {
            DataTable dt = N_OBJCLIENTE.LISTAR_PROVINCIA(depart);
            cboFILTRO_PROVINCIA.DataSource = dt;
            cboFILTRO_PROVINCIA.DataValueField = "UBIPRV";
            cboFILTRO_PROVINCIA.DataTextField = "UBIPRN";

            cboFILTRO_PROVINCIA.DataBind();
        }
        void FILTRO_LISTAR_DISTRITO(string prov)
        {
            DataTable dt = N_OBJCLIENTE.LISTAR_DISTRITO(prov);
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