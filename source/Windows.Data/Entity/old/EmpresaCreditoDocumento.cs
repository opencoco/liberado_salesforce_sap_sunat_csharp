using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaCreditoDocumento
    {
        public int IIdEmpresaCredDoc { get; set; }
        public int IIdEmpresaSolCredito { get; set; }
        public int IIdDocumento { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }
        public short? SiTipo { get; set; }

        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual EmpresaCredito IIdEmpresaSolCreditoNavigation { get; set; }
    }
}
