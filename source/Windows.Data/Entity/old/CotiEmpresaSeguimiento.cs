using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiEmpresaSeguimiento
    {
        public int IIdSeguimeintoSolicitud { get; set; }
        public DateTime? DtRegistro { get; set; }
        public byte? TiMotivo { get; set; }
        public string VcDescripcion { get; set; }
        public int? IIdCotiEmpresa { get; set; }
        public int IIdUsuario { get; set; }
        public int? IIdCotiCliente { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual CotiCliente IIdCotiClienteNavigation { get; set; }
        public virtual CotiEmpresa IIdCotiEmpresaNavigation { get; set; }
    }
}
