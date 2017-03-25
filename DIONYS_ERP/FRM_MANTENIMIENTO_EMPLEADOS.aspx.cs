using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Runtime.InteropServices;

namespace DIONYS_ERP
{
    public partial class FRM_MANTENIMIENTO_EMPLEADOS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                cboBUSCAR.SelectedIndex = 0; //ESTO ES PARA QUE EL CONTROL ESTE SELECCIONADO EN TODOS AL CARGAR LA PAGINA
                txtFILTRO_DATA.Text = string.Empty;

                P_LISTAR_DEPARTAMENTO();                                           //AQUI CARGAMOS LA LISTA DE DEPARTAMENTOS
                cboE_DEPARTAMENTO_SelectedIndexChanged(sender, e);
                cboE_PROVINCIA_SelectedIndexChanged(sender, e);

                P_LISTAR_CARGO(); //AQUI LISTAMOS LOS CARGOS
                cboE_CARGO.SelectedIndex = 0;
                P_LISTAR_AREA(); //AQUI LISTAMOS A TODAS LAS AREAS 
                cboE_AREA.SelectedIndex = 0;
                P_LISTAR_SEDE(); //AQUI LISTAMOS A TODAS LAS SEDES
                cboE_SEDE.SelectedIndex = 0;
                LISTA_FILTRAR_EMPLEADOS(); //CON ESTO FILTRO LOS DATOS DE MI GRIDVIEW SEGUN MIS PARAMETROS
            }
        }


        //=====================================================================================================================================================================
        #region OBJETOS
        N_VENTA OBJVENTA = new N_VENTA();
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();
        E_MANT_EMPLEADOS E_OBJEMPLEADO = new E_MANT_EMPLEADOS();
        #endregion

        #region PROCEDIMIENTOS
        void LISTA_FILTRAR_EMPLEADOS()
        {
            string OPCION_FILTRO = cboBUSCAR.SelectedIndex.ToString();
            string DATO = txtFILTRO_DATA.Text.ToString();
            DataTable LISTA = (DataTable)OBJVENTA.LISTAR_FILTRAR_EMPLEADOS(OPCION_FILTRO, DATO);
            dgvLISTAR_EMPLEADOS.DataSource = LISTA;
            dgvLISTAR_EMPLEADOS.DataBind();
        }

        void P_LISTAR_DEPARTAMENTO()
        {
            DataTable dt = OBJVENTA.LISTAR_DEPARTAMENTO();
            cboE_DEPARTAMENTO.DataSource = dt;
            cboE_DEPARTAMENTO.DataValueField = "UBIDEP";
            cboE_DEPARTAMENTO.DataTextField = "UBIDEN";

            cboE_DEPARTAMENTO.DataBind();
        }
        void P_LISTAR_PROVINCIA(string depart)
        {
            DataTable dt = OBJVENTA.LISTAR_PROVINCIA(depart);
            cboE_PROVINCIA.DataSource = dt;
            cboE_PROVINCIA.DataValueField = "UBIPRV";
            cboE_PROVINCIA.DataTextField = "UBIPRN";

            cboE_PROVINCIA.DataBind();
        }
        void P_LISTAR_DISTRITO(string prov)
        {
            DataTable dt = OBJVENTA.LISTAR_DISTRITO(prov);
            cboE_DISTRITO.DataSource = dt;
            cboE_DISTRITO.DataValueField = "UBIDST";
            cboE_DISTRITO.DataTextField = "UBIDSN";
            cboE_DISTRITO.DataBind();
        }

        void P_LISTAR_CARGO()
        {
            DataTable dt = (DataTable)OBJVENTA.LISTAR_CARGOS();
            cboE_CARGO.DataSource = dt;
            cboE_CARGO.DataValueField = "ID_CARGO";
            cboE_CARGO.DataTextField = "DESCRIPCION";
            cboE_CARGO.DataBind();
        }
        void P_LISTAR_AREA()
        {
            DataTable dt = (DataTable)OBJVENTA.LISTAR_AREAS();
            cboE_AREA.DataSource = dt;
            cboE_AREA.DataValueField = "ID_AREA";
            cboE_AREA.DataTextField = "DESCRIPCION";
            cboE_AREA.DataBind();
        }
        void P_LISTAR_SEDE()
        {
            DataTable dt = (DataTable)OBJVENTA.LISTAR_SEDE();
            cboE_SEDE.DataSource = dt;
            cboE_SEDE.DataValueField = "ID_SEDE";
            cboE_SEDE.DataTextField = "DESCRIPCION";
            cboE_SEDE.DataBind();
        }

        void MANTENIMIENTO_EMPLEADOS(int ACCION)
        {
                    E_OBJEMPLEADO.ID_EMPLEADO = string.Empty;
                    E_OBJEMPLEADO.NOMBRE = txtE_NOMBRES.Text.ToString();
                    E_OBJEMPLEADO.APELLIDOS = txtE_APELLIDOS.Text.ToString();
                    E_OBJEMPLEADO.DNI_USUARIO = txtE_DNI.Text.ToString();
                    E_OBJEMPLEADO.CONTRASENA = txtE_CONTRASENA.Text.ToString();
                    E_OBJEMPLEADO.DIRECCION = txtE_DIRECCION.Text.ToString();
                    E_OBJEMPLEADO.FECHA_NAC = txtE_FECHANAC.Text.ToString();
                    E_OBJEMPLEADO.TELEFONO = txtE_TELEFONO.Text.ToString();
                    E_OBJEMPLEADO.MOVIL = txtE_MOVIL.Text.ToString();
                    E_OBJEMPLEADO.EMAIL = txtE_EMAIL.Text.ToString();
                    E_OBJEMPLEADO.ESTADO = true;
                    E_OBJEMPLEADO.UBIDST = cboE_DISTRITO.SelectedValue.ToString();
                    E_OBJEMPLEADO.ID_CARGO = cboE_CARGO.SelectedValue.ToString();
                    E_OBJEMPLEADO.ID_SEDE = cboE_SEDE.SelectedValue.ToString();
                    E_OBJEMPLEADO.ID_AREA = cboE_AREA.SelectedValue.ToString();
                    E_OBJEMPLEADO.ACCION = ACCION;

                    OBJVENTA.MANTENIMIENTO_EMPLEADOS(E_OBJEMPLEADO);
            
            
        }

        void LIMPIAR_CONTROLES()
        {
            txtE_NOMBRES.Text = string.Empty;
            txtE_APELLIDOS.Text = string.Empty;
            txtE_DNI.Text = string.Empty;
            txtE_CONTRASENA.Text = string.Empty;
            txtE_DIRECCION.Text = string.Empty;
            txtE_FECHANAC.Text = string.Empty;
            txtE_TELEFONO.Text = string.Empty;
            txtE_MOVIL.Text = string.Empty;
            txtE_EMAIL.Text = string.Empty;
            cboE_DEPARTAMENTO.SelectedValue = "00";
            cboE_PROVINCIA.SelectedValue = "0000";
            cboE_DISTRITO.SelectedValue = "000000";
            cboE_CARGO.SelectedValue = "000";
            cboE_AREA.SelectedValue = "000";
            cboE_SEDE.SelectedValue = "000";
        }
        #endregion 
        //=====================================================================================================================================================================
        #region FUNCIONES
        private bool F_VALIDAR_MANTEMPLEADOS() //FUNCION PARA VALIDAR LOS DATOS DE INGRESO DE LOS EMPLEADOS
        {
            bool RESULTADO;
                if(txtE_NOMBRES.Text.ToString() != string.Empty && txtE_APELLIDOS.Text !=string.Empty && txtE_DNI.Text.ToString()!= string.Empty &&
                    txtE_CONTRASENA.Text.ToString()!=string.Empty && txtE_DIRECCION.Text.ToString()!=string.Empty && txtE_FECHANAC.Text.ToString() != string.Empty &&
                   txtE_TELEFONO.Text.ToString() != string.Empty && txtE_MOVIL.Text.ToString() != string.Empty && txtE_EMAIL.Text.ToString() != string.Empty && cboE_DISTRITO.SelectedItem.Text != string.Empty &&
                   cboE_CARGO.SelectedItem.Text != string.Empty && cboE_SEDE.SelectedItem.Text != string.Empty && cboE_AREA.SelectedItem.Text != string.Empty)
                {
                    RESULTADO = true;
                }
                else
                {
                    RESULTADO = false;
                }
            return RESULTADO;
        }

        private DataTable OBTENER_DEPAR_PROV_POR_DIST(string DISTRITO)
        {
            DataTable CONSULTA_DIST;
            CONSULTA_DIST = OBJVENTA.OBTENER_DEPAR_PROV_POR_DIST(DISTRITO);
            return CONSULTA_DIST;
        }
        #endregion
        //=====================================================================================================================================================================
        #region EVENTOS
        protected void cboSEDE_TRAB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            LISTA_FILTRAR_EMPLEADOS(); //CON ESTO FILTRO LOS DATOS DE MI GRIDVIEW SEGUN MIS PARAMETROS
        }

        protected void cboE_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            P_LISTAR_PROVINCIA(cboE_DEPARTAMENTO.SelectedValue.ToString());
            cboE_PROVINCIA_SelectedIndexChanged(sender, e);
        }

        protected void cboE_PROVINCIA_SelectedIndexChanged(object sender, EventArgs e)
        {
            P_LISTAR_DISTRITO(cboE_PROVINCIA.SelectedValue.ToString());
        }
        
        //EVENTO PARA OBTENER EL INDICE SELECIONADO DE LA GRILLA 
        protected void dgvLISTAR_EMPLEADOS_SelectedIndexChanged(object sender, EventArgs e) 
        {
            /* == ACTIVAR CONTROLES DE EDICION == */

            //txtPRE.Enabled = true;
            //btnBOTON.Enabled = true;
            /* ================================== */
            string ID_EMPLEADO = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[1].Text;
            string NOMBRES = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[2].Text;
            string APELLIDOS = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[3].Text;
            string DNI = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[4].Text;
            string CONTRASENA =dgvLISTAR_EMPLEADOS.SelectedRow.Cells[5].Text;
            string DIRECCION =dgvLISTAR_EMPLEADOS.SelectedRow.Cells[6].Text;
            string FECHA_NAC=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[7].Text;
            string TELEFONO=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[8].Text;
            string MOVIL=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[9].Text;
            string EMAIL=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[10].Text;
            string ESTADO = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[11].Text;
            string UBIDST=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[12].Text;
            string ID_CARGO=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[13].Text;
            string ID_SEDE=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[14].Text;
            string ID_AREA=dgvLISTAR_EMPLEADOS.SelectedRow.Cells[15].Text;

            P_LISTAR_DEPARTAMENTO();
            DataTable CONSULTA_DIST;
            CONSULTA_DIST = OBJVENTA.OBTENER_DEPAR_PROV_POR_DIST(UBIDST);

            

            txtE_NOMBRES.Text = NOMBRES;
            txtE_APELLIDOS.Text = APELLIDOS;
            txtE_DNI.Text = DNI;
            txtE_CONTRASENA.Text = CONTRASENA;
            txtE_DIRECCION.Text = DIRECCION;
            txtE_FECHANAC.Text = FECHA_NAC;
            txtE_TELEFONO.Text = TELEFONO;
            txtE_MOVIL.Text = MOVIL;
            txtE_EMAIL.Text = EMAIL;
            cboE_DEPARTAMENTO.SelectedValue = CONSULTA_DIST.Rows[0][0].ToString();
            P_LISTAR_PROVINCIA(cboE_DEPARTAMENTO.SelectedValue.ToString());
            cboE_PROVINCIA_SelectedIndexChanged(sender,e);
            cboE_PROVINCIA.SelectedValue = CONSULTA_DIST.Rows[0][2].ToString();

            P_LISTAR_DISTRITO(cboE_PROVINCIA.SelectedValue.ToString());
            
            //cboE_PROVINCIA.DataValueField = CONSULTA_DIST.Rows[0][2].ToString(); ;
            cboE_DISTRITO.SelectedValue = UBIDST;
            //cboE_DISTRITO.DataValueField = COD_DIST;

            P_LISTAR_CARGO();
            //cboE_DISTRITO.SelectedValue = UBIDST;
            cboE_CARGO.SelectedValue= ID_CARGO;
            P_LISTAR_SEDE();
            cboE_SEDE.SelectedValue = ID_SEDE;
            P_LISTAR_AREA();
            cboE_AREA.SelectedValue = ID_AREA; 
        }
        protected void btnE_ELIMINAR_Click(object sender, EventArgs e)
        {
            E_OBJEMPLEADO.ID_EMPLEADO = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[1].Text;
            E_OBJEMPLEADO.NOMBRE = txtE_NOMBRES.Text.ToString();
            E_OBJEMPLEADO.APELLIDOS = txtE_APELLIDOS.Text.ToString();
            E_OBJEMPLEADO.DNI_USUARIO = txtE_DNI.Text.ToString();
            E_OBJEMPLEADO.CONTRASENA = txtE_CONTRASENA.Text.ToString();
            E_OBJEMPLEADO.DIRECCION = txtE_DIRECCION.Text.ToString();
            E_OBJEMPLEADO.FECHA_NAC = txtE_FECHANAC.Text.ToString().Substring(0,10);
            E_OBJEMPLEADO.TELEFONO = txtE_TELEFONO.Text.ToString();
            E_OBJEMPLEADO.MOVIL = txtE_MOVIL.Text.ToString();
            E_OBJEMPLEADO.EMAIL = txtE_EMAIL.Text.ToString();
            E_OBJEMPLEADO.ESTADO = false;
            E_OBJEMPLEADO.UBIDST = cboE_DISTRITO.SelectedValue.ToString();
            E_OBJEMPLEADO.ID_CARGO = cboE_CARGO.SelectedValue.ToString();
            E_OBJEMPLEADO.ID_SEDE = cboE_SEDE.SelectedValue.ToString();
            E_OBJEMPLEADO.ID_AREA = cboE_AREA.SelectedValue.ToString();
            E_OBJEMPLEADO.ACCION = 3;

            OBJVENTA.MANTENIMIENTO_EMPLEADOS(E_OBJEMPLEADO);
            LISTA_FILTRAR_EMPLEADOS();
            LIMPIAR_CONTROLES();
        }
        
        protected void btnE_GUARDAR_Click(object sender, EventArgs e)
        {
            if (F_VALIDAR_MANTEMPLEADOS())
            {
                MANTENIMIENTO_EMPLEADOS(1); //EJECUTAMOS EL MANTENIMIENTO REGISTRAR NUEVO EMPLEADO CON LA ACCION 1
                LIMPIAR_CONTROLES();
            }
            else
            {
                Response.Write("<script>window.alert('INGRESAR TODOS LOS DATOS DEL EMPLEADO');</script>");
            }
        }

        protected void btnE_LIMPIAR_Click(object sender, EventArgs e)
        {
            LIMPIAR_CONTROLES(); //CON ESTO LIMPIO TODOS LOS CONTROLES
        }

        protected void btnE_EDITAR_Click(object sender, EventArgs e)
        {
            if (F_VALIDAR_MANTEMPLEADOS())//esto me valida si todos los campos estan llenos para ejecutar la modificacion
            {
                if (dgvLISTAR_EMPLEADOS.Rows.Count != 0)
                {

                    E_OBJEMPLEADO.ID_EMPLEADO = dgvLISTAR_EMPLEADOS.SelectedRow.Cells[1].Text;
                    E_OBJEMPLEADO.NOMBRE = txtE_NOMBRES.Text.ToString();
                    E_OBJEMPLEADO.APELLIDOS = txtE_APELLIDOS.Text.ToString();
                    E_OBJEMPLEADO.DNI_USUARIO = txtE_DNI.Text.ToString();
                    E_OBJEMPLEADO.CONTRASENA = txtE_CONTRASENA.Text.ToString();
                    E_OBJEMPLEADO.DIRECCION = txtE_DIRECCION.Text.ToString();
                    E_OBJEMPLEADO.FECHA_NAC = txtE_FECHANAC.Text.ToString().Substring(0,10);
                    E_OBJEMPLEADO.TELEFONO = txtE_TELEFONO.Text.ToString();
                    E_OBJEMPLEADO.MOVIL = txtE_MOVIL.Text.ToString();
                    E_OBJEMPLEADO.EMAIL = txtE_EMAIL.Text.ToString();
                    E_OBJEMPLEADO.UBIDST = cboE_DISTRITO.SelectedValue;
                    E_OBJEMPLEADO.ID_CARGO = cboE_CARGO.SelectedValue;
                    E_OBJEMPLEADO.ID_SEDE = cboE_SEDE.SelectedValue;
                    E_OBJEMPLEADO.ID_AREA = cboE_AREA.SelectedValue;
                    E_OBJEMPLEADO.ACCION = 2; // CON ESTA OPCION AGO LA EDICION

                    OBJVENTA.MANTENIMIENTO_EMPLEADOS(E_OBJEMPLEADO);
                    LISTA_FILTRAR_EMPLEADOS();
                    
                }

            }
        }
        #endregion

        

       

        

        
        //=====================================================================================================================================================================

       

        
    }
}