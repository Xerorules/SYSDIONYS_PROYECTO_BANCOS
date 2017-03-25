using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;


namespace DIONYS_ERP
{
    public partial class LOGEO : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {

                Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                Response.Cache.SetAllowResponseInBrowserHistory(false) ;
                Response.Cache.SetNoStore() ;

                LISTA_PUNTOVENTA();

            }

        }
        #region OBJETOS
            N_LOGUEO OBJLOGUEO = new N_LOGUEO();
            E_LOGUEO OBJLOGUEOE = new E_LOGUEO();
        #endregion

        #region VARIABLES_GLOBALES
             public string VG_SEDE()
            {
                string var, sede;
                var = cboPUNTOVENTA.SelectedValue;
                sede = var.Substring(0, 3);

                return sede;
            }
            public string VG_SERIE()
            {
                string var, serie;
                var = cboPUNTOVENTA.SelectedValue;
                serie = var.Substring(4, 4);

                return serie;
            }
        #endregion

        #region PROCEDIMIENTOS
        void LISTA_PUNTOVENTA()
        {
            DataTable dt = new DataTable();
            dt = OBJLOGUEO.PUNTO_VENTA(cboPUNTOVENTA.SelectedValue);
            cboPUNTOVENTA.DataSource = dt;
            cboPUNTOVENTA.DataValueField = "PK_PUNTO_VENTA";
            cboPUNTOVENTA.DataTextField = "DESCRIPCION";
            cboPUNTOVENTA.DataBind();
        }

        #endregion

        #region Validar Logueo

        public void VALIDAR_LOGUEO()
            {
                FRM_VENTA OBJFRMVENTA = new FRM_VENTA();

                string puntovta1 = "CAJA BOLETERIA 1 - TOBOGANES";
                string puntovta2 = "CAJA BOLETERIA 2 - TOBOGANES";
                string puntovta3 = "CAJA RESTAURANTE - TOBOGANES";
                string puntovta4 = "CAJA BOLETERIA 1 - LEÑAS";

                
                if ((cboPUNTOVENTA.SelectedItem.Text == puntovta1 && txtCLAVE.Text == "PV00120") || 
                    (cboPUNTOVENTA.SelectedItem.Text == puntovta2 && txtCLAVE.Text == "PV00230") || 
                    (cboPUNTOVENTA.SelectedItem.Text == puntovta3 && txtCLAVE.Text == "PV00340") || 
                    (cboPUNTOVENTA.SelectedItem.Text == puntovta4 && txtCLAVE.Text == "PV00450"))
                {

                    #region DECLARACION DE VARIABLES GLOBALES
                    /*DECLARANDO MIS VARIABLES QUE SE VAN A USAR EN TODO MI PROYECTO - PARA SER USADO EN LA AYUDA DE BIEN*/
                    ESTRUCTURA_DETALLEBIEN(); //CONSTRUYENDO LOS DETALLES DE LA TABLA DETALLE
                    Session.Add("ID_BIEN", string.Empty);
                    Session.Add("DESCRIPCION_BIEN", string.Empty);
                    Session.Add("PRECIO_BIEN", string.Empty);
                    Session.Add("LLAMABIEN", "0");
                    Session.Add("SERIE",VG_SERIE());
                    Session.Add("SEDE", VG_SEDE());
                    Session.Add("PUNTOVENTA",cboPUNTOVENTA.SelectedItem.Text);
                    Session.Add("INDICE_TIPODOC",0); //ESE SESSION ME INDICA QUE TIPO DOC ESTA SELECCIONADO EN EL FORMULARIO DE VENTA
                    Session.Add("COCINA_LISTA", string.Empty); //SESSION PARA MANIPULAR EL INDICE DEL ITEM DE MI DATALIST DE MI PANTALLA DE COCINA
                    Session.Add("COCINA_FILA", string.Empty);//SESSION PARA MANIPULAR EL INDICE DE MI GRIDVIEW DE MI PANTALLA COCINA

                    Session.Add("ID_CLIENTE", string.Empty);
                    Session.Add("DESCRIPCION_CLIENTE",string.Empty);
                    Session.Add("RUCDNI_CLIENTE",string.Empty);
                    Session.Add("TIPO_DOC",string.Empty);
                    #endregion

                    
                    if (VG_SEDE().Equals("001"))
                    {
                        Session.Add("SEDE_DESCRIPCION", "TOBOGANES DE SANTA ANA");
                    }
                    else
                    {
                        Session.Add("SEDE_DESCRIPCION", "CLUB LAS LEÑAS");
                    }
                    Response.Redirect("FRM_MENUVENTA.aspx");
                    
                }
                
            }

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
                VALIDAR_LOGUEO();
                
            }
        #region FUNCIONES
        
        #endregion

        #region EVENTOS

        #endregion

    }
}