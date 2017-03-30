using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DIONYS_ERP.SERVICES
{
    /// <summary>
    /// Descripción breve de AUTOCOMPLETAR_MOVIMIENTOS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class AUTOCOMPLETAR_MOVIMIENTOS : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] AUTOCOMPLETAR_CUENTAS(string prefix)
        {
            List<string> cuentas = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("AUTOCOMPLETAR_CUENTAS", conn))
                {
                    string id_emp = Session["ID_EMPRESA"].ToString();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_EMPRESA", id_emp);
                    cmd.Parameters.AddWithValue("@DATO", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            cuentas.Add(string.Format("{0}-{1}", sdr["N_CUENT_A"], sdr["ID_CUENTASBANCARIAS"]));
                        }
                    }
                    conn.Close();
                }
                return cuentas.ToArray();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] AUTOCOMPLETAR_CLIENTES(string prefix)
        {
            List<string> cuentas = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("AUTOCOMPLETAR_CLIENTES", conn))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DATO", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            cuentas.Add(string.Format("{0}-{1}", sdr["DESCRIPCION"], sdr["ID_CLIENTE"]));
                        }
                    }
                    conn.Close();
                }
                return cuentas.ToArray();
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] AUTOCOMPLETAR_LUGAR(string prefix)
        {
            List<string> cuentas = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("AUTOCOMPLETAR_LUGAR", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DATO", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            cuentas.Add(string.Format("{0}-{1}", sdr["LUGAR"], sdr["LUGAR"]));
                        }
                    }
                    conn.Close();
                }
                return cuentas.ToArray();
            }
        }

    }
}
