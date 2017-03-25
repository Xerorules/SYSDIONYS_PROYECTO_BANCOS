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
using System.Runtime.InteropServices;
using System.Globalization;

namespace DIONYS_ERP
{
    public partial class FRM_CERRAR_CAJA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ID_CAJA"].ToString() == string.Empty )
                {
                    HABILITAR_CONTROLES(2); //HABILITA LOS CONTROLES PARA PODER HABILITAR UN CAJA

                    //ESTO ES PARA CARGAR EL SALDO ANTERIOR DE LA CORRESPONDIENTE CAJA

                    DataTable DT= new DataTable(); 

                    DT=OBJ_N_MANTCAJA.OBTENER_SALDO_CAJA(Session["ID_PUNTOVENTA"].ToString());
                    if(DT.Rows.Count != 0 )
                    {
                        txtSALDO_INICIAL.Text = DT.Rows[0][0].ToString();
                        txtSALDO_FINAL.Text = DT.Rows[0][0].ToString();
                    }
                    

                    //================================================================
                }
                else
                {
                    HABILITAR_CONTROLES(1); // CON ESTA OPCION SE PONE EN ESTADO DE CONSULTA DE CAJA
                    CONSULTAR_CAJA();
                }
                
            }

        }

        #region OBJETOS
        N_VENTA OBJ_N_MANTCAJA = new N_VENTA();
        E_MANTENIMIENTO_CAJA OBJ_E_MANTCAJA = new E_MANTENIMIENTO_CAJA();
        #endregion

        public void CONSULTAR_CAJA()
        {
            DataTable dt = new DataTable();
            dt = OBJ_N_MANTCAJA.CONSULTAR_CAJA(Session["ID_CAJA"].ToString());

            txtFECHA_APERTURA.Text = dt.Rows[0]["FECHA_INICIAL"].ToString(); //AQUI RECUPERO LA FECHA DE APERTURA QUE SE HISO EN UN PRINCIPIO
            txtFECHA_CIERRE.Text = DateTime.Now.ToString();
            txtID_CAJA.Text = dt.Rows[0]["ID_CAJA"].ToString();
            txtSALDO_INICIAL.Text = dt.Rows[0]["SALDO_INICIAL"].ToString();
            txtSALDO_FINAL.Text = dt.Rows[0]["SALDO_FINAL"].ToString();
            txtOBSERVACIONES.Text = dt.Rows[0]["OBSERVACION"].ToString();
        }

        protected void btnCERRARCAJA_Click(object sender, EventArgs e)
        {
            string ID_ADMIN="";
            if (Session["ID_EMPRESA"].ToString() == "001") 
            {
                ID_ADMIN = "PV005";
            }
            else
            {
                if (Session["ID_EMPRESA"].ToString() == "003")
                {
                    ID_ADMIN = "PV010";
                }
                else
                {
                    if (Session["ID_EMPRESA"].ToString() == "004")
                    {
                        ID_ADMIN = "PV011";
                    }
                }
                
            }
            DataTable dt = OBJ_N_MANTCAJA.VALIDAR_EXISTENCIA_CAJAADMINISTRACION(ID_ADMIN);

            if(dt.Rows.Count > 0)
            {
                    if (txtFECHA_APERTURA.Text.ToString() != string.Empty && txtFECHA_CIERRE.Text.ToString() != string.Empty &&
                        txtID_CAJA.Text.ToString() != string.Empty && txtSALDO_INICIAL.Text.ToString() != string.Empty && txtSALDO_FINAL.Text.ToString() != string.Empty)
                    {
                        OBJ_E_MANTCAJA.ID_CAJA = txtID_CAJA.Text.ToString();
                        OBJ_E_MANTCAJA.SALDO_INICIAL = Convert.ToDouble(txtSALDO_INICIAL.Text.ToString());
                        OBJ_E_MANTCAJA.OBSERVACION = txtOBSERVACIONES.Text.ToString();
                        OBJ_E_MANTCAJA.ID_EMPLEADO = Session["ID_EMPLEADO"].ToString();
                        OBJ_E_MANTCAJA.ID_PUNTOVENTA = Session["ID_PUNTOVENTA"].ToString();
                        OBJ_E_MANTCAJA.OPCION = 2;


                        //AQUI TENEMOS EL CODIGO PARA PASAR LA INFORMACION DE CADA CAJA A LA CAJA ADMINISTRACION
                        //=====================================================================================
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ".";
                        provider.NumberGroupSeparator = ",";
                        provider.NumberGroupSizes = new int[] { 2 };

                        OBJ_N_MANTCAJA.MOVIMIENTOS_XDIA_CAJAS(Session["NOMBRE_EMPLEADO"].ToString(), Session["ID_CAJA"].ToString(), Convert.ToDouble(Session["TIPO_CAMBIO"].ToString()), "1", (-1) * Convert.ToDouble(txtSALDO_FINAL.Text.ToString(), provider), Session["SEDE"].ToString());
                        OBJ_N_MANTCAJA.MOVIMIENTOS_XDIA_CAJAS(Session["NOMBRE_EMPLEADO"].ToString(), Session["ID_CAJA"].ToString(), Convert.ToDouble(Session["TIPO_CAMBIO"].ToString()), "2", (Convert.ToDouble(txtSALDO_FINAL.Text.ToString(), provider)), Session["SEDE"].ToString());

                        // ====================================================================================

                        OBJ_N_MANTCAJA.MANTENIMIENTO_CAJA(OBJ_E_MANTCAJA);
                        Session["ID_CAJA"] = string.Empty;
                        string id = Session["ID_CAJA"].ToString();
                        HABILITAR_CONTROLES(1); // CON ESTA OPCION SE PONE EN ESTADO DE CONSULTA DE CAJA

                        Response.Redirect("FRM_PRINCIPAL.aspx");
                    }
            }
            else
            {
                Response.Write("<script>window.alert('NO PUEDE CERRAR CAJA, PORQUE NO HAY UNA CAJA ADMINISTRACION ABIERTA');</script>");
            }
        }

        
        protected void btnABRIRCAJA_Click(object sender, EventArgs e)
        {
            DataTable DT = OBJ_N_MANTCAJA.VALIDAR_RESTRICCIONES_ABRIR_CAJA(Session["ID_PUNTOVENTA"].ToString());

            if(DT.Rows.Count == 0)
            {
                        if (txtSALDO_INICIAL.Text.ToString() != string.Empty)
                        {
                            OBJ_E_MANTCAJA.ID_CAJA = string.Empty;
                            OBJ_E_MANTCAJA.SALDO_INICIAL = Convert.ToDouble(txtSALDO_INICIAL.Text.ToString());
                            OBJ_E_MANTCAJA.OBSERVACION = txtOBSERVACIONES.Text.ToString();
                            OBJ_E_MANTCAJA.ID_EMPLEADO = Session["ID_EMPLEADO"].ToString();
                            OBJ_E_MANTCAJA.ID_PUNTOVENTA = Session["ID_PUNTOVENTA"].ToString();
                            OBJ_E_MANTCAJA.OPCION = 1;

                            OBJ_N_MANTCAJA.MANTENIMIENTO_CAJA(OBJ_E_MANTCAJA);
                            Session["ID_CAJA"] = OBJ_E_MANTCAJA.ID_CAJA;
                            Session["SALDO_INICIAL_CAJA"] = txtSALDO_INICIAL.Text.ToString();
                            HABILITAR_CONTROLES(1);
                            CONSULTAR_CAJA();
            
                
                            Response.Redirect("FRM_PRINCIPAL.aspx");
                        }
            }else
            {
                Response.Write("<script>window.alert('NO PUEDE ABRIR CAJA, PORQUE EXISTE UNA CAJA ABIERTA EN ESTE PUNTO DE VENTA');</script>");
            }

            


            
        }


        public void HABILITAR_CONTROLES(int ESTADO)
        {
            if (ESTADO == 1) //ESTADO 1 SIGNIIFCA QUE ESTA EN CONSULTA CIERRE DE CAJA
            {
                txtFECHA_APERTURA.ReadOnly = true;
                txtFECHA_CIERRE.Text = DateTime.Now.ToString();
                txtFECHA_CIERRE.ReadOnly = true;
                txtID_CAJA.ReadOnly = true;
                txtSALDO_INICIAL.ReadOnly = true;
                txtSALDO_FINAL.ReadOnly = true;
                txtOBSERVACIONES.ReadOnly = true;
                btnABRIRCAJA.Enabled = false;
                btnCERRARCAJA.Enabled = true;
            }
            if(ESTADO == 2) // ESTADO 2 INDICA QUE VA A APERTURAR CAJA
            {
                txtFECHA_APERTURA.Text = DateTime.Now.ToString();
                txtFECHA_APERTURA.ReadOnly = true;
                txtFECHA_CIERRE.ReadOnly = true;
                txtID_CAJA.ReadOnly = true;
                txtSALDO_INICIAL.ReadOnly = false;
                txtSALDO_FINAL.ReadOnly = true;
                txtOBSERVACIONES.ReadOnly = false;
                btnABRIRCAJA.Enabled = true;
                btnCERRARCAJA.Enabled = false;
            }

        }

        protected void btnSALIR_Click(object sender, EventArgs e)
        {
            Response.Redirect("FRM_PRINCIPAL.aspx");
        }
    }
}