using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaSedeInsumoLote
    {
        public int IIdEmpresaSedeInsumoLote { get; set; }
        public int IIdEmpresaSedeInsumo { get; set; }
        public string VcRegistroSanitario { get; set; }
        public string VcNumeroLote { get; set; }
        public int InCantidad { get; set; }
        public DateTime DtExpiracion { get; set; }
        public string VcDrogueria { get; set; }
        public string VcFabricante { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public short SiEstado { get; set; }

        public virtual EmpresaSedeInsumo IIdEmpresaSedeInsumoNavigation { get; set; }
    }
}
