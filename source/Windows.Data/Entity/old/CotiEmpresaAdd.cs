using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiEmpresaAdd
    {
        public int IIdCotEmpAdd { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short SiTipo { get; set; }
        public byte[] VbFile { get; set; }
        public int? IIdCotiEmpresa { get; set; }
        public DateTime DtCreacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }

        public virtual CotiEmpresa IIdCotiEmpresaNavigation { get; set; }
    }
}
