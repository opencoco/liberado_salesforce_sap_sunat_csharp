using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaSedeInsumo
    {
        public EmpresaSedeInsumo()
        {
            EmpresaSedeInsumoLote = new HashSet<EmpresaSedeInsumoLote>();
            EmpresaSedeInsumoSrv = new HashSet<EmpresaSedeInsumoSrv>();
        }

        public int IIdEmpresaSedeInsumo { get; set; }
        public int IIdSede { get; set; }
        public int IIdInsumo { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public bool? BDescontarenCitas { get; set; }
        public short SiEstado { get; set; }
        public int IIdEmpresa { get; set; }
        public short? SiPara { get; set; }

        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual Insumo IIdInsumoNavigation { get; set; }
        public virtual ICollection<EmpresaSedeInsumoLote> EmpresaSedeInsumoLote { get; set; }
        public virtual ICollection<EmpresaSedeInsumoSrv> EmpresaSedeInsumoSrv { get; set; }
    }
}
