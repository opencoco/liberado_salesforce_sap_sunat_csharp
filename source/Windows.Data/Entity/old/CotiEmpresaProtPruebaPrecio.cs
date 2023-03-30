using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiEmpresaProtPruebaPrecio
    {
        public int IIdCotEmpPruebaProtPrecio { get; set; }
        public int IIdCotEmpPruebaProtocolo { get; set; }
        public int IIdProtocolo { get; set; }
        public int IIdPrueba { get; set; }
        public int? IIdCotiEmpresa { get; set; }
        public decimal? DePrecio { get; set; }
        public DateTime DtCreacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public string VcNota { get; set; }

        public virtual CotiEmpresa IIdCotiEmpresaNavigation { get; set; }
        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual DefPrueba IIdPruebaNavigation { get; set; }
    }
}
