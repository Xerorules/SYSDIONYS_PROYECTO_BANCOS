using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDAD
{
    public class E_MOVIMIENTOS
    {
        public string id_mov { get; set; }
        public string id_concepto_banc { get; set; }
        public string fecha { get; set; }
        public string lugar { get; set; }
        public string tipo_mov { get; set; }
        public string operacion { get; set; }
        public string descripcion { get; set; }
        public decimal importe { get; set; }
        public decimal itf { get; set; }
        public decimal saldoc { get; set; }
        public decimal saldod { get; set; }
        public string id_cuentasbancarias { get; set; }
        public string id_cliente { get; set; }
        public decimal saldo { get; set; }
        public string observacion { get; set; }
    }
}
