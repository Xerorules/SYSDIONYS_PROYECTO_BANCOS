using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Data;

namespace DIONYS_ERP.SERVICES
{
    /// <summary>
    /// Descripción breve de SA_PRODUCTO_XNOMBRE
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    
    public class SA_PRODUCTO_XNOMBRE : System.Web.Services.WebService
    {
        
        public SA_PRODUCTO_XNOMBRE()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(EnableSession = true)] //PARA PODER HACER USO DE SESSIONES EN WEB SERVICE TENEMOS QUE AGREGAR A [WebMethod] LO SIGUIENTE [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        
        public string[] productos(string prefix)
        {
            string CLASE="";
            List<string> clientes = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_AUTOCOMPLETAR_FILTRO_BIEN", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DESCRIPCION", prefix);
                    
                    // ESTO ES PARA VERIFICAR EN QUE SERIE SE ESTA TRABAJANDO Y POR LO TANTO SABREMOS QUE TIPO DE PRODUCTOS HAY QUE FILTRAR
                    if (Session["ID_PUNTOVENTA"].ToString() == "PV001" || Session["ID_PUNTOVENTA"].ToString() == "PV002" || Session["ID_PUNTOVENTA"].ToString() == "PV004" || Session["ID_PUNTOVENTA"].ToString() == "PV006" || Session["ID_PUNTOVENTA"].ToString() == "PV007")
                    {
                        CLASE = "1";
                    }
                    else
                    {
                        if (Session["ID_PUNTOVENTA"].ToString() == "PV003" || Session["ID_PUNTOVENTA"].ToString() == "PV008" || Session["ID_PUNTOVENTA"].ToString() == "PV009")
                        {
                            CLASE = "2";
                        }
                        else
                        {
                            CLASE = "3";
                        }
                        
                    }
                    //========================================================================================================================
                    
                    cmd.Parameters.AddWithValue("@OPCION_CLASE",CLASE);
                    cmd.Parameters.AddWithValue("@ID_EMPRESA", Session["ID_EMPRESA"].ToString());
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            clientes.Add(string.Format("{0}-{1}-{2}", sdr["DESCRIPCION"], sdr["ID_BIEN"], sdr["PRECIO"]));
                        }
                    }
                    conn.Close();
                }
                return clientes.ToArray();
            }
        }
    }
}
