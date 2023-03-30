using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaTrabajador
    {
        public EmpresaTrabajador()
        {
            EmpresaRl = new HashSet<EmpresaRl>();
            EmpresaUnidadTrabajador = new HashSet<EmpresaUnidadTrabajador>();
        }

        public int IIdTrabajador { get; set; }
        public short SiEstado { get; set; }
        public int IIdPersona { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int? IIdEmpresa { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Persona IIdPersonaNavigation { get; set; }
        public virtual ICollection<EmpresaRl> EmpresaRl { get; set; }
        public virtual ICollection<EmpresaUnidadTrabajador> EmpresaUnidadTrabajador { get; set; }
    }
}
