using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPA_ENTIDAD;
using CAPA_NEGOCIO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DIONYS_ERP
{
    public partial class FRM_CHEQUE_FOTOS : System.Web.UI.Page
    {
        string carpeta = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string res = Session["ID_EMPRESA"].ToString();
                if (res == "")
                {
                    Response.Redirect("default.aspx");
                }

            }
            catch
            {
                Response.Redirect("default.aspx");
            }
            /*---CARGAR IMAGEN------*/
            imgSubida.Width = 0;
            imgSubida.Width = 0;
            imgSubida.ImageUrl = "";

            carpeta = Server.MapPath("MODULOBANCOS_FOTOSCHEQUES");
            hfruta.Value = carpeta;
            /*----------------------*/
        }

        N_VENTA OBJVENTA = new N_VENTA();
        E_CHEQUES OBJCHEQUE = new E_CHEQUES();

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string sExt = string.Empty;
            string sName = string.Empty;

            imgSubida.Width = 0;
            imgSubida.Width = 0;
            imgSubida.ImageUrl = "";

            if (uploadFile.HasFile)
            {
                sName = uploadFile.FileName;
                sExt = Path.GetExtension(sName);
                Session["Imagen"] = TXTCHEQUE.Text + sExt;
                txtimagen.Text = Session["Imagen"].ToString();

                string archivo = Path.GetFileName(uploadFile.PostedFile.FileName);

                if (ValidaExtension(sExt))
                {

                    uploadFile.SaveAs(Server.MapPath("MODULOBANCOS_FOTOSCHEQUES") + "\\" + TXTCHEQUE.Text + sExt);// MapPath("~/MODULOBANCOS_FOTOSCHEQUES/" + TXTCHEQUE.Text + sExt));
                    imgSubida.Height = 50;
                    imgSubida.Width = 100;
                    imgSubida.ImageUrl = "/MODULOBANCOS_FOTOSCHEQUES/" + TXTCHEQUE.Text + sExt;
                    //string carpeta_final = Path.Combine(hfruta.Value, TXTCHEQUE.Text + sExt);
                    //uploadFile.PostedFile.SaveAs(carpeta_final);

                    string fn = System.IO.Path.GetFileName(uploadFile.PostedFile.FileName);
                    string SaveLocation = @"C:\\inetpub\\wwwroot\\MODULOBANCOS_FOTOSCHEQUES" + "\\" + TXTCHEQUE.Text + sExt;
                    uploadFile.PostedFile.SaveAs(SaveLocation);

                    lblMensaje.Text = "Archivo cargado correctamente.";

                    /*
                     TextBox2.Text = archivo;
                    string carpeta_final = Path.Combine(carpeta, archivo);
                    FileUpload1.PostedFile.SaveAs(carpeta_final);
                     */
                }
                else
                    lblMensaje.Text = "El archivo no es de tipo imagen.";
            }
            else
                lblMensaje.Text = "Seleccione el archivo que desea subir.";
        }


        private Boolean ValidaExtension(string sExtension)
        {
            Boolean rel = false;
            switch (sExtension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".bmp":
                    rel = true;
                    break;
                default:
                    rel = false;
                    break;

            }
            return rel;
        }

        protected void btnBUSCAR_Click(object sender, EventArgs e)
        {
            string cod_cheque = TXTCHEQUE.Text;
            try
            {
                DataTable tb = OBJVENTA.NCONSULTA_CHEQUE_IMAGEN(cod_cheque);
                TXTCLI.Text = tb.Rows[0][0].ToString();
                TCTNCH.Text = tb.Rows[0][1].ToString();
                TXTIMPO.Text = tb.Rows[0][2].ToString();
                TXTFEC1.Text = tb.Rows[0][4].ToString();
                try
                {

                    string rutaFisica = Server.MapPath("MODULOBANCOS_FOTOSCHEQUES"); 
                    string sName = tb.Rows[0][3].ToString();

                    if (File.Exists(Server.MapPath(tb.Rows[0][3].ToString())))
                    {

                        imgSubida.ImageUrl = tb.Rows[0][3].ToString();
                        imgSubida.Height = 50;
                        imgSubida.Width = 100;

                    }
                    else
                    {

                        imgSubida.ImageUrl = "/MODULOBANCOS_FOTOSCHEQUES/ImagenNoDisponible.jpg";
                        imgSubida.Height = 50;
                        imgSubida.Width = 100;
                    }



                }
                catch
                {
                    imgSubida.ImageUrl = "";
                }

            }
            catch { Response.Write("<script>alert('NO EXISTEN CHEQUES CON ESE CODIGO')</script>"); }

        }

        protected void btnSUBIRE_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
            string var = Session["Imagen"].ToString();

            try
            {
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAR_IMAGEN_CHEQUE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IMAGEN_PATH", "/MODULOBANCOS_FOTOSCHEQUES/" + Session["Imagen"].ToString());
                cmd.Parameters.AddWithValue("@COD_CHEQUE", TXTCHEQUE.Text);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                Response.Write("<script>alert('HUBO UN ERROR AL CARGAR LA IMAGEN')</script>");
            }

            TXTCHEQUE.Text = "";
            TXTCLI.Text = "";
            TXTIMPO.Text = "";
            lblMensaje.Text = "";
            txtimagen.Text = "";
            TCTNCH.Text = "";

        }
    }
}