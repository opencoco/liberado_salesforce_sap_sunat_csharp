using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaTrabajadorTitular
    {
        public CitaTrabajadorTitular()
        {
            CitaTrabajadorPrecio = new HashSet<CitaTrabajadorPrecio>();
        }

        public int IIdCitaTrabaTitular { get; set; }
        public int IIdCitaTrabajador { get; set; }
        public int IIdEmpresa { get; set; }
        public int IIdEmpresaUnidad { get; set; }
        public int IIdProtocolo { get; set; }
        public int? IIdEmpresaZona { get; set; }
        public int? IIdEmpresaArea { get; set; }
        public int? IIdEmpresaPuesto { get; set; }

        public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
        public virtual EmpresaArea IIdEmpresaAreaNavigation { get; set; }
        public virtual Empresa IIdEmpresaNavigation { get; set; }
        public virtual EmpresaPuesto IIdEmpresaPuestoNavigation { get; set; }
        public virtual EmpresaUnidad IIdEmpresaUnidadNavigation { get; set; }
        public virtual EmpresaZona IIdEmpresaZonaNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual ICollection<CitaTrabajadorPrecio> CitaTrabajadorPrecio { get; set; }
    }
}
