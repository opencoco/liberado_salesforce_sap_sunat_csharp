using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaArea
    {
        public EmpresaArea()
        {
            CitaEmpresaUnidad = new HashSet<CitaEmpresaUnidad>();
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            EmpresaPuesto = new HashSet<EmpresaPuesto>();
        }

        public int IIdEmpresaArea { get; set; }
        public int IIdEmpresaZona { get; set; }
        public int? IIdArea { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }

        public virtual Area IIdAreaNavigation { get; set; }
        public virtual EmpresaZona IIdEmpresaZonaNavigation { get; set; }
        public virtual ICollection<CitaEmpresaUnidad> CitaEmpresaUnidad { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<EmpresaPuesto> EmpresaPuesto { get; set; }
    }
}
