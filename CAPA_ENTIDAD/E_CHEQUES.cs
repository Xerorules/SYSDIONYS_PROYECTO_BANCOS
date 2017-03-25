using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPA_ENTIDAD
{
    public class E_CHEQUES
    {
        public string id_cheque { get; set; }
        public string fecha_giro { get; set; }
        public string fecha_cobro { get; set; }
        public string numero { get; set; }
        public string id_banco { get; set; }
        public decimal importe { get; set; }
        public string moneda { get; set; }
        public string estado { get; set; }
        public string id_cliente { get; set; }
        public string id_empresa { get; set; }
    }
}
