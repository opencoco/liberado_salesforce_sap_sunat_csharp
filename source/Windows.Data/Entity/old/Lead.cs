using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Lead
    {
        public int IIdLead { get; set; }
        public string VcNombres { get; set; }
        public string VcApellidos { get; set; }
        public string VcCorreo { get; set; }
        public string VcCelular { get; set; }
        public int? IIdPais { get; set; }
        public int? IIdProvincia { get; set; }
        public short? SiPara { get; set; }
        public DateTime? DtCreado { get; set; }
        public DateTime? DtModificado { get; set; }
        public bool? BContactado { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcComentario { get; set; }
        public short? SiSegmento { get; set; }
    }
}
