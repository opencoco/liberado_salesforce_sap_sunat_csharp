using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProveedorUnidad
    {
        public int IIdProveedorUnidad { get; set; }
        public int? IIdProveedorRelacion { get; set; }
        public int IIdEmpresaUnidad { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiTipo { get; set; }
        public short? SiEstado { get; set; }
    }
}
