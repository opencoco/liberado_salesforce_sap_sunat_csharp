using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class EpisodioCodigoDatoRes
    {
        public string VcNombre { get; set; }
        public string VcValor { get; set; }
        public decimal? DeValor { get; set; }
        public DateTime? DtValor { get; set; }
        public string JsonValor { get; set; }
        public string VcCodigoDato { get; set; }
        public string VcCodigoFicha { get; set; }
        public bool? BEsDecimal { get; set; }
        public string VcValorDescripcion { get; set; }
        public string VcCodigoPrueba { get; set; }

    }
}
