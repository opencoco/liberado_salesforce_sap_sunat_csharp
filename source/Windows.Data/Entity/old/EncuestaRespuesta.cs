using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class EncuestaRespuesta
    {
        public int IIdEncuestaRespuesta { get; set; }
        public int IIdEncuesta { get; set; }
        public int IIdEncuestaPregunta { get; set; }
        public int? IIdEncuestaPregProp { get; set; }
        public string VcRptaTexto { get; set; }
        public DateTime? DtRptaDateTime { get; set; }
        public bool? BRptaBit { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdusuarioModificacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdPersona { get; set; }

        public virtual Encuesta IIdEncuestaNavigation { get; set; }
        public virtual EncuestaPreguntaProp IIdEncuestaPregPropNavigation { get; set; }
        public virtual EncuestaPregunta IIdEncuestaPreguntaNavigation { get; set; }
        public virtual Persona IIdPersonaNavigation { get; set; }
    }
}
