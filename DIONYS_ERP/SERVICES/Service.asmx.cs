using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;

namespace DIONYS_ERP.SERVICES
{
    /// <summary>
    /// Descripción breve de Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        public Service()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetCustomers(string prefix)
        {
            //string TIPOCLI = "";
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_AUTOCOMPLETAR_FILTRO_CLIENTES", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DESCRIPCION", prefix);
                    
                    // ESTO ES PARA VERIFICAR EN QUE SERIE SE ESTA TRABAJANDO Y POR LO TANTO SABREMOS QUE TIPO DE PRODUCTOS HAY QUE FILTRAR
                    //if (Session["P_TIPODOCUMENTO"].ToString() == "FT" || Session["P_TIPODOCUMENTO"].ToString() == "TF")
                    //{
                    //    TIPOCLI = "PJ";
                    //}
                    //else
                    //{
                    //    TIPOCLI = "PN";
                    //}

                    //cmd.Parameters.AddWithValue("@TIPO_CLIENTE", TIPOCLI);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}-{2}-{3}", sdr["DESCRIPCION"], sdr["ID_CLIENTE"], sdr["RUC_DNI"], sdr["DIRECCION"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }

        

    }
}
