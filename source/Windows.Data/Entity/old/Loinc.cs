using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Loinc
    {
        public Loinc()
        {
            DefPrueba = new HashSet<DefPrueba>();
        }

        public int IIdLoinc { get; set; }
        public string VcLoincNum { get; set; }
        public string VcComponent { get; set; }
        public string VcProperty { get; set; }
        public string VcTimeAspct { get; set; }
        public string VcSystem { get; set; }
        public string VcScaleTyp { get; set; }
        public string VcMethodTyp { get; set; }
        public string VcClass { get; set; }
        public string VcShortname { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public bool? BEsOcupacional { get; set; }

        public virtual ICollection<DefPrueba> DefPrueba { get; set; }
    }
}
