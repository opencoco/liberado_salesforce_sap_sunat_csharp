using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaEmpresaProtocolo
    {
        public CitaEmpresaProtocolo()
        {
            CitaEmpresaProtocoloOpc = new HashSet<CitaEmpresaProtocoloOpc>();
        }

        public int IIdCitaEmpresaProtocolo { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdProtocolo { get; set; }
        public bool BEspecial { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual ICollection<CitaEmpresaProtocoloOpc> CitaEmpresaProtocoloOpc { get; set; }
    }
}
