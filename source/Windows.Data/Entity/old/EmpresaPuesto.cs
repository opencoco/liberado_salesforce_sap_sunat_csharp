using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaPuesto
    {
        public EmpresaPuesto()
        {
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            EmpresaUnidadTrabajador = new HashSet<EmpresaUnidadTrabajador>();
        }

        public int IIdEmpresaPuesto { get; set; }
        public int IIdEmpresaArea { get; set; }
        public int? IIdPuesto { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }

        public virtual EmpresaArea IIdEmpresaAreaNavigation { get; set; }
        public virtual Puesto IIdPuestoNavigation { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<EmpresaUnidadTrabajador> EmpresaUnidadTrabajador { get; set; }
    }
}
