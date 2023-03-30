using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaEmpresaProtocoloOpc
    {
        public int IIdCitaEmpresaProtocoloOpc { get; set; }
        public int IIdCitaEmpresaProtocolo { get; set; }
        public int IIdPrueba { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }

        public virtual CitaEmpresaProtocolo IIdCitaEmpresaProtocoloNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
