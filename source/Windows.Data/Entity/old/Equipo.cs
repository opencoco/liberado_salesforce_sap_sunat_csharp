using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Equipo
    {
        public Equipo()
        {
            DefPruebaEquipo = new HashSet<DefPruebaEquipo>();
            EquipoCalibracion = new HashSet<EquipoCalibracion>();
            SedeEquipo = new HashSet<SedeEquipo>();
        }

        public int IIdEquipo { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcCodigoMigracion { get; set; }
        public short? SiEstado { get; set; }
        public short? SiTipoPrueba { get; set; }
        public short? SiTipoEquipo { get; set; }

        public virtual ICollection<DefPruebaEquipo> DefPruebaEquipo { get; set; }
        public virtual ICollection<EquipoCalibracion> EquipoCalibracion { get; set; }
        public virtual ICollection<SedeEquipo> SedeEquipo { get; set; }
    }
}
