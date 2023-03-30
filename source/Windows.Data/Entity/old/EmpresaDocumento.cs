using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaDocumento
    {
        public int IIdEmpresaDocumento { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }

        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual Empresa IIdEmpresaNavigation { get; set; }
    }
}
