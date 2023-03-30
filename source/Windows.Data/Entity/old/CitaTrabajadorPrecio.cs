using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaTrabajadorPrecio
    {
        public int IIdCitaTrabaPrecio { get; set; }
        public int IIdCitaTrabajador { get; set; }
        public int IIdCitaTrabaTitular { get; set; }
        public int IIdProtocolo { get; set; }
        public int? IIdPrueba { get; set; }
        public bool BObligatorio { get; set; }
        public bool BOpcional { get; set; }
        public bool BOtro { get; set; }
        public bool BAexepcion { get; set; }
        public bool BConvalidable { get; set; }
        public decimal DePrecio { get; set; }
        public short SiEstadoPago { get; set; }
        public string VcCentroCosto { get; set; }
        public int? IIdProtConceptoFacturable { get; set; }

        public virtual CitaTrabajadorTitular IIdCitaTrabaTitularNavigation { get; set; }
        public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
        public virtual ProtocoloConceptoFacturable IIdProtConceptoFacturableNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
    }
}
