using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaEmpresaUnidad
    {
        public int IIdCitaEmpresaUnidad { get; set; }
        public int IIdCita { get; set; }
        public int IIdEmpresaUnidad { get; set; }
        public string VcCentroCosto { get; set; }
        public int? IIdEmpresaZona { get; set; }
        public int? IIdEmpresaArea { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }

        public virtual Cita IIdCitaNavigation { get; set; }
        public virtual EmpresaArea IIdEmpresaAreaNavigation { get; set; }
        public virtual EmpresaUnidad IIdEmpresaUnidadNavigation { get; set; }
        public virtual EmpresaZona IIdEmpresaZonaNavigation { get; set; }
    }
}
