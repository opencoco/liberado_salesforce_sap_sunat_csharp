using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CotiEmpresaSede
    {
        public int IIdCotiEmpSede { get; set; }
        public int? IIdCotiEmpresa { get; set; }
        public int? IIdSede { get; set; }
        public short? SiEstado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
    }
}
