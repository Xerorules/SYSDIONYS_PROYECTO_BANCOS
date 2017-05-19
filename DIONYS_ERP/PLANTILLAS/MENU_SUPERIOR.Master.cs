using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIONYS_ERP.PLANTILLAS
{
    public partial class MENU_SUPERIOR : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblID_EMPRESA.Text = Session["ID_EMPRESA"].ToString();
                lblEMPRESA.Text = Session["NOMBRE_EMPRESA"].ToString();
                lblID_SEDE.Text = Session["SEDE"].ToString();
                lblSEDE.Text = Session["SEDE_DESCRIPCION"].ToString();
                lblID_PVENTA.Text = Session["ID_PUNTOVENTA"].ToString();
                lblPVENTA.Text = Session["PUNTOVENTA"].ToString();
                lblID_USUARIO.Text = Session["USUARIO_EMPLEADO"].ToString();
                lblFECHA.Text = DateTime.Now.ToShortDateString();
                LOGINUSUARIO.Text = Session["NOMBRE_EMPLEADO"].ToString();
                
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
            

        }



        public Label ID_EMPRESA { get { return lblID_EMPRESA; } set { lblID_EMPRESA = value; } }
        public Label EMPRESA { get { return lblEMPRESA; } set { lblEMPRESA = value; } }
        public Label ID_SEDE { get { return lblID_SEDE; } set { lblID_SEDE = value; } }
        public Label SEDE { get { return lblSEDE; } set { lblSEDE = value; } }
        public Label ID_PVENTA { get { return lblID_PVENTA; } set { lblID_PVENTA = value; } }
        public Label PVENTA { get { return lblPVENTA; } set { lblPVENTA = value; } }
        public Label USUARIO { get { return lblID_USUARIO; } set { lblID_USUARIO = value; } }
        public Label FECHA { get { return lblFECHA; } set { lblFECHA = value; } }


       
    }
}