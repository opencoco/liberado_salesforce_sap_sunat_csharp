using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Insumo
    {
        public Insumo()
        {
            DefPruebaInsumo = new HashSet<DefPruebaInsumo>();
            EmpresaSedeInsumo = new HashSet<EmpresaSedeInsumo>();
            SedeInsumo = new HashSet<SedeInsumo>();
        }

        public int IIdInsumo { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcCodigoMigracion { get; set; }
        public short? SiEstado { get; set; }
        public short? SiTipoPrueba { get; set; }

        public virtual ICollection<DefPruebaInsumo> DefPruebaInsumo { get; set; }
        public virtual ICollection<EmpresaSedeInsumo> EmpresaSedeInsumo { get; set; }
        public virtual ICollection<SedeInsumo> SedeInsumo { get; set; }
    }
}
