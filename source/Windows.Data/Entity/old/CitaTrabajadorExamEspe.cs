using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaTrabajadorExamEspe
    {
        public int IIdCitaTrabajadorExamEspe { get; set; }
        public int IIdProtocolo { get; set; }
        public int IIdCitaTrabajador { get; set; }
        public decimal? DePrecio { get; set; }
        public int? IIdCitaTrabaTitular { get; set; }

        public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
    }
}
