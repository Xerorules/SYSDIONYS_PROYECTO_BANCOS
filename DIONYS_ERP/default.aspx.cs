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
    public partial class FRM_INICIO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetNoStore();
                LISTAR_EMPRESA();
                cboEMPRESA_SelectedIndexChanged(sender, e);
                cboSEDE_SelectedIndexChanged(sender,e);
            }
        }

        #region OBJETOS
        N_LOGUEO OBJLOGUEO = new N_LOGUEO();
        E_LOGUEO OBJLOGUEOE = new E_LOGUEO();
        N_VENTA N_OBJVENTAS = new N_VENTA();
        E_MANTENIMIENTO_CAJA E_OBJ_MANTCAJA = new E_MANTENIMIENTO_CAJA();
        #endregion

        #region VARIABLES_GLOBALES
        private string VG_SEDE()
        {
            string var, sede;
            var = cboPUNTOVENTA.SelectedValue;
            sede = var.Substring(0, 3);

            return sede;
        }
        private string VG_SERIE()
        {
            string var, serie;
            var = cboPUNTOVENTA.SelectedValue;
            serie = var.Substring(4, 4);

            return serie;
        }
        private string VG_ID_PUNTOVENTA()
        {
            string var, ID_PUNTOVENTA;
            var = cboPUNTOVENTA.SelectedValue;
            ID_PUNTOVENTA = var.Substring(9, 5);

            return ID_PUNTOVENTA;
        }
        private string VG_SEDE_DESCRIPCION()
        {
            string var, SEDE_DESCRIPCION;
            var = cboPUNTOVENTA.SelectedValue;
            SEDE_DESCRIPCION = var.Substring(15, var.Length-15).ToString();

            return SEDE_DESCRIPCION;
        }
        
        #endregion

        #region PROCEDIMIENTOS
       
        private void LISTAR_EMPRESA()
        {
            cboEMPRESA.DataSource = OBJLOGUEO.LISTAR_EMPRESA();
            cboEMPRESA.DataValueField = "ID_EMPRESA";
            cboEMPRESA.DataTextField = "DESCRIPCION";
            cboEMPRESA.DataBind();
        }
        private void LISTAR_SEDE(string ID_EMPRESA)
        {
            cboSEDE.DataSource = OBJLOGUEO.LISTAR_SEDE(ID_EMPRESA);
            cboSEDE.DataValueField = "ID_SEDE";
            cboSEDE.DataTextField = "DESCRIPCION";
            cboSEDE.DataBind();
        } 
        private void LISTA_PUNTOVENTA(string ID_SEDE)
        {
            cboPUNTOVENTA.DataSource = OBJLOGUEO.PUNTO_VENTA(ID_SEDE);
            cboPUNTOVENTA.DataValueField = "PK_PUNTO_VENTA";
            cboPUNTOVENTA.DataTextField = "DESCRIPCION";
            cboPUNTOVENTA.DataBind();
        }



        #endregion

        #region ESTRUCTURA

        void ESTRUCTURA_DETALLEBIEN()
        {
            DataTable vPdt_detBien = new DataTable();
            DataColumn colum = vPdt_detBien.Columns.Add("ID_BIEN", typeof(String));
            colum.Unique = true;
            vPdt_detBien.Columns.Add(new DataColumn("CANT", typeof(double)));
            vPdt_detBien.Columns.Add(new DataColumn("DESCRIPCION", typeof(String)));
            vPdt_detBien.Columns.Add(new DataColumn("PRECIO", typeof(Double)));
            vPdt_detBien.Columns.Add(new DataColumn("IMPORTE", typeof(Double)));
            vPdt_detBien.PrimaryKey = new DataColumn[] { vPdt_detBien.Columns[0] };
            Session["detalleBien"] = vPdt_detBien;
        }

        #endregion

        protected void btnINGRESAR_Click(object sender, EventArgs e)
        {
            VALIDAR_USUARIO();
            
        }
        #region FUNCIONES

       private void VALIDAR_USUARIO()
        {
            string USUARIO =  txtDNI_USUARIO.Text.ToString();
            string CONTRASENA = txtCLAVE.Text.ToString();
            string ID_SEDE = VG_SEDE();
            DataTable dt = OBJLOGUEO.VALIDAR_USUARIO(USUARIO,CONTRASENA,ID_SEDE);
            
           if(dt.Rows.Count!=0) //SI TIENE DATO ES PORQUE SI ENCONTRO EL USUARIO
           {
                    #region DECLARACION DE VARIABLES GLOBALES
                    /*DECLARANDO MIS VARIABLES QUE SE VAN A USAR EN TODO MI PROYECTO - PARA SER USADO EN LA AYUDA DE BIEN*/
                    ESTRUCTURA_DETALLEBIEN(); //CONSTRUYENDO LOS DETALLES DE LA TABLA DETALLE
                    Session.Add("ID_BIEN", string.Empty);
                    Session.Add("DESCRIPCION_BIEN", string.Empty);
                    Session.Add("PRECIO_BIEN", string.Empty);
                    Session.Add("LLAMABIEN", "0");
                    Session.Add("SERIE", VG_SERIE());
                    Session.Add("SEDE", VG_SEDE());
                    Session.Add("SEDE_DESCRIPCION", VG_SEDE_DESCRIPCION());
                    Session.Add("ID_PUNTOVENTA",VG_ID_PUNTOVENTA()); //OBTENEMOS EL ID DEL PUNTO DE VENTA
                    Session.Add("PUNTOVENTA", cboPUNTOVENTA.SelectedItem.Text);
                    Session.Add("INDICE_TIPODOC", 0); //ESE SESSION ME INDICA QUE TIPO DOC ESTA SELECCIONADO EN EL FORMULARIO DE VENTA
                    Session.Add("COCINA_LISTA", string.Empty); //SESSION PARA MANIPULAR EL INDICE DEL ITEM DE MI DATALIST DE MI PANTALLA DE COCINA
                    Session.Add("COCINA_FILA", string.Empty);//SESSION PARA MANIPULAR EL INDICE DE MI GRIDVIEW DE MI PANTALLA COCINA
                    
                    Session.Add("ID_CLIENTE", string.Empty);
                    Session.Add("DESCRIPCION_CLIENTE", string.Empty);
                    Session.Add("RUCDNI_CLIENTE", string.Empty);
                    Session.Add("TIPO_DOC", string.Empty);
                    Session.Add("ID_EMPLEADO",dt.Rows[0]["ID_EMPLEADO"].ToString());
                    Session.Add("NOMBRE_EMPLEADO", dt.Rows[0]["NOMBRE"].ToString());
                    Session.Add("USUARIO_EMPLEADO",dt.Rows[0]["DNI_USUARIO"].ToString());
                    Session.Add("EMPLEADO", dt.Rows[0]["EMPLEADO"].ToString());
                    Session.Add("EMPLEADO_CARGO",dt.Rows[0]["ID_CARGO"].ToString());
                    Session.Add("ID_CAJA",string.Empty); //AQUI GUARDO MI ID CAJA PARA UTILIZARLO DESPUES DE REALIZAR EL LOGEO
                    Session.Add("TIPO_CAMBIO", string.Empty);
                    Session.Add("ID_EMPRESA",cboEMPRESA.SelectedValue.ToString()); //AQUI OBTENGO EL DATO DE LA EMPRESA 
                    Session.Add("NOMBRE_EMPRESA", cboEMPRESA.SelectedItem.Text); //AQUI OBTENGO EL NOMBRE DE LA EMPRESA 
                    Session.Add("ID_VENTA",string.Empty);//CREO UNA SESSION PARA MI ID_VENTA QUE SERA UTILIZADO EN LA PAGINA DE VENTA_DETALLADA
                    Session.Add("ID_COMPRA", string.Empty);//CREO UNA SESSION PARA MI ID_COMPRA QUE SERA UTILIZADO EN LA PAGINA DE COMPRAS
                    Session.Add("P_TIPODOCUMENTO",string.Empty);//ESTO ME SIRVE PARA GUARDAR EL TIPO DE DOCUMENTO PARA UTILIZARLO EN EL FORMULARIO DE VENTAS, ME SIRVE COMO PARAMETRO DE FILTRO DE CLIENTES
                    
                    //SESSIONES Y VARIABLES GLOBALES PARA EL FORMULARIO FRM_CUENTAS_POR_COBRAR Y FRM_MOVIMIENTOS_CAJA
                   
                    //===============================================================================================
                    #endregion

                    lblMENSAJE_ERROR.Text = "BIENVENIDO(A)...";
                    VALIDAR_ASIGNAR_CAJA(); //AQUI ESTOY VALIDANDO CAJA POR EL USUARIO LOGUEADO Y ASIGANDOLE LA VARIABLE SESSION EL VALOR DE ID_CAJA
                    VALIDAR_TIPO_CAMBIO();//AQUI LLAMAMOS A ESTA FUNCION PARA VALIDAR Y OBTENER EL TIPO DE CAMBIO ACTUAL
                    Response.Redirect("FRM_PRINCIPAL.aspx");


           }
           else
           {
                lblMENSAJE_ERROR.Text = ".:. ERROR, VUELVA A INTENTAR ...";
           }
        }

        private void VALIDAR_TIPO_CAMBIO()
       {
           Session["TIPO_CAMBIO"] = N_OBJVENTAS.CONSULTAR_TIPO_CAMBIO().Rows[0]["TIPO_CAMBIO"].ToString(); 
       }

        private void VALIDAR_ASIGNAR_CAJA()
       {
           E_OBJ_MANTCAJA.ID_CAJA = string.Empty;
           E_OBJ_MANTCAJA.SALDO_INICIAL = 0.00;
           E_OBJ_MANTCAJA.ID_EMPLEADO = Session["ID_EMPLEADO"].ToString();
           E_OBJ_MANTCAJA.ID_PUNTOVENTA = Session["ID_PUNTOVENTA"].ToString();
           E_OBJ_MANTCAJA.OBSERVACION = string.Empty;
           E_OBJ_MANTCAJA.OPCION = 3; //OPCION 3 ES PARA VALIDAR LOS DATOS
           DataTable DT = new DataTable();
           DT = N_OBJVENTAS.MANTENIMIENTO_CAJA(E_OBJ_MANTCAJA);

           if (DT.Rows.Count != 0) //SI ES DIFERENTE DE 0 ES PORQUE EL USUARIO  ID_USUARIO Y EL PUNTO DE VENTA ID_PUNTOVENTA TIENE CAJA APERTURADA
           {
               Session["ID_CAJA"] = DT.Rows[0]["ID_CAJA"].ToString(); //AQUI ASIGNO EL VALOR DE ID_CAJA
               
           }


       }

       
        #endregion

        protected void cboEMPRESA_SelectedIndexChanged(object sender, EventArgs e)
        {
            LISTAR_SEDE(cboEMPRESA.SelectedValue.ToString());
            cboSEDE_SelectedIndexChanged(sender, e);
        }

        protected void cboSEDE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LISTA_PUNTOVENTA(cboSEDE.SelectedValue.ToString());
        }

        #region EVENTOS

        #endregion



    }
}