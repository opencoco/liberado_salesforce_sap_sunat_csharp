using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaRl
    {
        public int IIdEmpresaRl { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdTrabajador { get; set; }
        public short? SiEstado { get; set; }
        public int? IIdDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual EmpresaTrabajador IIdTrabajadorNavigation { get; set; }
    }
}
