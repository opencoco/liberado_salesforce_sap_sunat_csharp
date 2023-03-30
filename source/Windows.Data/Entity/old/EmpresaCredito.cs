using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaCredito
    {
        public EmpresaCredito()
        {
            EmpresaCreditoDocumento = new HashSet<EmpresaCreditoDocumento>();
        }

        public int IIdEmpresaSolCredito { get; set; }
        public short SiDiasCredito { get; set; }
        public byte TiDocumentoParaFacturar { get; set; }
        public byte? TiTipoRelacionComercial { get; set; }
        public int IIdEmpresa { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstadoCredito { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual ICollection<EmpresaCreditoDocumento> EmpresaCreditoDocumento { get; set; }
    }
}
