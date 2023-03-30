using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EncuestaPregunta
    {
        public EncuestaPregunta()
        {
            EncuestaPreguntaProp = new HashSet<EncuestaPreguntaProp>();
            EncuestaRespuesta = new HashSet<EncuestaRespuesta>();
            InverseIIdDependeDeNavigation = new HashSet<EncuestaPregunta>();
        }

        public int IIdEncuestaPregunta { get; set; }
        public int IIdDependeDe { get; set; }
        public short SiTipo { get; set; }
        public bool? BObligatorio { get; set; }
        public string VcNombre { get; set; }
        public int? IOrden { get; set; }
        public int IIdEncuesta { get; set; }

        public virtual EncuestaPregunta IIdDependeDeNavigation { get; set; }
        public virtual Encuesta IIdEncuestaNavigation { get; set; }
        public virtual ICollection<EncuestaPreguntaProp> EncuestaPreguntaProp { get; set; }
        public virtual ICollection<EncuestaRespuesta> EncuestaRespuesta { get; set; }
        public virtual ICollection<EncuestaPregunta> InverseIIdDependeDeNavigation { get; set; }
    }
}
