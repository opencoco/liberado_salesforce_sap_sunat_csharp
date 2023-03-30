using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EncuestaPreguntaProp
    {
        public EncuestaPreguntaProp()
        {
            EncuestaRespuesta = new HashSet<EncuestaRespuesta>();
        }

        public int IIdEncuestaPregProp { get; set; }
        public int IIdEncuestaPregunta { get; set; }
        public string VcValor { get; set; }
        public string VcTexto { get; set; }
        public int? IOrden { get; set; }

        public virtual EncuestaPregunta IIdEncuestaPreguntaNavigation { get; set; }
        public virtual ICollection<EncuestaRespuesta> EncuestaRespuesta { get; set; }
    }
}
