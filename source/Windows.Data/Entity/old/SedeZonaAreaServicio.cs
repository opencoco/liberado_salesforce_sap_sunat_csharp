using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class SedeZonaAreaServicio
    {
        public SedeZonaAreaServicio()
        {
            DefPruebaSedeZonaAreaServicio = new HashSet<DefPruebaSedeZonaAreaServicio>();
        }

        public int IIdSedeZonaAreaServ { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdServicio { get; set; }
        public int? IIdSede { get; set; }
        public int? IIdZona { get; set; }
        public int? IIdArea { get; set; }

        public virtual Area IIdAreaNavigation { get; set; }
        public virtual Sede IIdSedeNavigation { get; set; }
        public virtual Servicio IIdServicioNavigation { get; set; }
        public virtual Zona IIdZonaNavigation { get; set; }
        public virtual ICollection<DefPruebaSedeZonaAreaServicio> DefPruebaSedeZonaAreaServicio { get; set; }
    }
}
