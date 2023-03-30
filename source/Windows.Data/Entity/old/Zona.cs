using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Zona
    {
        public Zona()
        {
            Area = new HashSet<Area>();
            EmpresaZona = new HashSet<EmpresaZona>();
            SedeZona = new HashSet<SedeZona>();
            SedeZonaArea = new HashSet<SedeZonaArea>();
            SedeZonaAreaServicio = new HashSet<SedeZonaAreaServicio>();
        }

        public int IIdZona { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short? SiEstado { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcCodigoChaman { get; set; }

        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<EmpresaZona> EmpresaZona { get; set; }
        public virtual ICollection<SedeZona> SedeZona { get; set; }
        public virtual ICollection<SedeZonaArea> SedeZonaArea { get; set; }
        public virtual ICollection<SedeZonaAreaServicio> SedeZonaAreaServicio { get; set; }
    }
}
