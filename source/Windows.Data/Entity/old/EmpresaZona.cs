using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaZona
    {
        public EmpresaZona()
        {
            CitaEmpresaUnidad = new HashSet<CitaEmpresaUnidad>();
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            EmpresaArea = new HashSet<EmpresaArea>();
        }

        public int IIdEmpresaZona { get; set; }
        public int? IIdZona { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdEmpresaUnidad { get; set; }
        public string VcNombre { get; set; }
        public short? SiEstado { get; set; }
        public string VcDescripcion { get; set; }

        public virtual EmpresaUnidad IIdEmpresaUnidadNavigation { get; set; }
        public virtual Zona IIdZonaNavigation { get; set; }
        public virtual ICollection<CitaEmpresaUnidad> CitaEmpresaUnidad { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<EmpresaArea> EmpresaArea { get; set; }
    }
}
