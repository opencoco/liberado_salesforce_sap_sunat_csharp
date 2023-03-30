using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EmpresaSedeInsumoSrv
    {
        public int IIdEmpresaSedeInsumoSrv { get; set; }
        public int IIdEmpresaSedeInsumo { get; set; }
        public int IIdServicio { get; set; }
        public int IIdEmpresa { get; set; }

        public virtual EmpresaSedeInsumo IIdEmpresaSedeInsumoNavigation { get; set; }
        public virtual Servicio IIdServicioNavigation { get; set; }
    }
}
