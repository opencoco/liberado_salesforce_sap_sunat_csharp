using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class EpisodiosEntregable
    {

        public string VcCodigoCompatible { get; set; }
        public int? IIdMensajePlantilla { get; set; }
        public int? IIdCitaTrabaTitular { get; set; }
        public int? IIdSubClaseDocumento { get; set; }
        public bool? BIncluirFirma { get; set; }
        public string Clase { get; set; }
    }
}
