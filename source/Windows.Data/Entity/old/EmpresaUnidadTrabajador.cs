using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaUnidadTrabajador
    {
        public int IIdEmpresaUnidad { get; set; }
        public int IIdTrabajador { get; set; }
        public DateTime? DtEraInicio { get; set; }
        public DateTime? DtEraFin { get; set; }
        public int IIdEmpresaPuesto { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual EmpresaPuesto IIdEmpresaPuestoNavigation { get; set; }
        public virtual EmpresaUnidad IIdEmpresaUnidadNavigation { get; set; }
        public virtual EmpresaTrabajador IIdTrabajadorNavigation { get; set; }
    }
}
