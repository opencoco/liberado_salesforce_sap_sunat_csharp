using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class DocFin
    {

        public long? iIdCitaSunatDet { get; set; }
        public long? iIdCita { get; set; }
        public long? iIdCitaSunatCab { get; set; }
        public string nvGUID { get; set; }
        public string vcExtension { get; set; }
        public int? iIdSubClaseDocumento { get; set; }
        public short? siTipoDoc { get; set; }
        public string vcRuta { get; set; }
        public string vcNombre { get; set; }
        public MemoryStream mem { get; set; }
        public string vcTipo { get; set; }
        //public long? iIdProcedimientoEra { get; set; }
        public int? idUsuario { get; set; }
        public string vcExtensionCorta { get; set; }
        public short? SiTipoComprobante { get; set; }
        

    }
}
