using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Models
{
    public class Lote
    {
        public int cod_lote { get; set; }
        public string lote { get; set; }
        public int cod_produto { get; set; }
        public DateTime validade { get; set; }
        public string cod_barra { get; set; }
    }
}
