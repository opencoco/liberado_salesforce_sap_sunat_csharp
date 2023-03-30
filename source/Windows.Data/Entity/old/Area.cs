using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Area
    {
        public Area()
        {
            EmpresaArea = new HashSet<EmpresaArea>();
            Puesto = new HashSet<Puesto>();
            SedeZonaArea = new HashSet<SedeZonaArea>();
            SedeZonaAreaServicio = new HashSet<SedeZonaAreaServicio>();
        }

        public int IIdArea { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdZona { get; set; }
        public string VcCodigoChaman { get; set; }
        public short? SiEstado { get; set; }
        public bool? BDeLaClinica { get; set; }

        public virtual Zona IIdZonaNavigation { get; set; }
        public virtual ICollection<EmpresaArea> EmpresaArea { get; set; }
        public virtual ICollection<Puesto> Puesto { get; set; }
        public virtual ICollection<SedeZonaArea> SedeZonaArea { get; set; }
        public virtual ICollection<SedeZonaAreaServicio> SedeZonaAreaServicio { get; set; }
    }
}
