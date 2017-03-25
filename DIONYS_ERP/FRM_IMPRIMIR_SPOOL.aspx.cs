using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIONYS_ERP
{
    public partial class FRM_IMPRIMIR_SPOOL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIMPRIMIRSPOOL_Click(object sender, EventArgs e)
        {
            FRM_MENUVENTA obj = new FRM_MENUVENTA();
            obj.IMPRIMIR_SPOOL();   
        }
    }
}