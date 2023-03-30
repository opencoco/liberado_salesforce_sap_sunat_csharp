using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaUnidad
    {
        public EmpresaUnidad()
        {
            CitaEmpresaUnidad = new HashSet<CitaEmpresaUnidad>();
            CitaTrabajadorTitular = new HashSet<CitaTrabajadorTitular>();
            EmpresaUnidadTrabajador = new HashSet<EmpresaUnidadTrabajador>();
            EmpresaUnidadUsuario = new HashSet<EmpresaUnidadUsuario>();
            EmpresaZona = new HashSet<EmpresaZona>();
        }

        public int IIdEmpresaUnidad { get; set; }
        public int IIdEmpresa { get; set; }
        public short SiAlturaLabor { get; set; }
        public string VcNombre { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public string VcDescripcion { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual ICollection<CitaEmpresaUnidad> CitaEmpresaUnidad { get; set; }
        public virtual ICollection<CitaTrabajadorTitular> CitaTrabajadorTitular { get; set; }
        public virtual ICollection<EmpresaUnidadTrabajador> EmpresaUnidadTrabajador { get; set; }
        public virtual ICollection<EmpresaUnidadUsuario> EmpresaUnidadUsuario { get; set; }
        public virtual ICollection<EmpresaZona> EmpresaZona { get; set; }
    }
}
