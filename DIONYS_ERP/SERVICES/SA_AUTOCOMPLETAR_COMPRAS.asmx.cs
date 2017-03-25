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
    /// Descripción breve de SA_FILTRAR_PROVEEDOR_XDESCRIPCION
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class SA_FILTRAR_PROVEEDOR_XDESCRIPCION : System.Web.Services.WebService
    {
        public SA_FILTRAR_PROVEEDOR_XDESCRIPCION()
        { 
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] FILTRO_PROVEEDOR(string prefix)
        {
            //string TIPOCLI = "";
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_AUTOCOMPLETAR_FILTRO_PROVEEDOR", conn))
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

                    //cmd.Parameters.AddWithValue("@TIPO_PROVEEDOR", TIPOCLI);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}-{1}-{2}-{3}-{4}", sdr["DESCRIPCION"], sdr["ID_PROVEEDOR"], sdr["RUC_DNI"], sdr["DIRECCION"],sdr["ORIGEN_PROVEEDOR"]));
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] FILTRAR_PROVEEDOR_XRUCDNI(string prefix)
        {
            //string TIPOPROV = "";
            List<string> clientes = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_AUTOCOMPLETAR_FILTRO_PROVEEDOR_XRUCDNI", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RUC_DNI", prefix);

                    // ESTO ES PARA VERIFICAR EN QUE SERIE SE ESTA TRABAJANDO Y POR LO TANTO SABREMOS QUE TIPO DE PRODUCTOS HAY QUE FILTRAR
                    //if (Session["P_TIPODOCUMENTO"].ToString() == "FT")
                    //{
                    //    TIPOPROV = "PJ";
                    //}
                    //else
                    //{
                    //    TIPOPROV = "PN";
                    //}
                    //cmd.Parameters.AddWithValue("@TIPO_PROVEEDOR", TIPOPROV);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            clientes.Add(string.Format("{0}-{1}-{2}-{3}-{4}", sdr["RUC_DNI"], sdr["ID_PROVEEDOR"], sdr["DESCRIPCION"], sdr["DIRECCION"], sdr["ORIGEN_PROVEEDOR"]));
                        }
                    }
                    conn.Close();
                }
                return clientes.ToArray();
            }
        }


        [WebMethod(EnableSession = true)] //PARA PODER HACER USO DE SESSIONES EN WEB SERVICE TENEMOS QUE AGREGAR A [WebMethod] LO SIGUIENTE [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public string[] productos(string prefix)
        {
            string CLASE = "3";
            List<string> clientes = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["sql"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand("SP_AUTOCOMPLETAR_FILTRO_BIEN", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DESCRIPCION", prefix);
                    cmd.Parameters.AddWithValue("@OPCION_CLASE", CLASE);
                    cmd.Parameters.AddWithValue("@ID_EMPRESA",Session["ID_EMPRESA"].ToString());
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
