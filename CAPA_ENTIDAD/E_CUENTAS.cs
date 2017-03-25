using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDAD
{
    public class E_CUENTAS
    {
        public int ID_CUENTASBANCARIAS { get; set; }
        public string ID_EMPRESA { get; set; }
        public string N_CUENTA { get; set; }
        public string N_CCI { get; set; }
        public string MONEDA { get; set; }
        public decimal SALDO_CONTABLE { get; set; }
        public decimal SALDO_DISPONIBLE { get; set; }
        public string SECTORISTA { get; set; }
        public string OFICINA { get; set; }
        public string TELEFONO { get; set; }
        public string EMAIL { get; set; }
        public string ESTADO { get; set; }
        public string ID_BANCOS { get; set; }
    }
    
}
