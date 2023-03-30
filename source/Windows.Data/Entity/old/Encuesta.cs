using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Encuesta
    {
        public Encuesta()
        {
            EncuestaPregunta = new HashSet<EncuestaPregunta>();
            EncuestaRespuesta = new HashSet<EncuestaRespuesta>();
        }

        public int IIdEncuesta { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public short SiEstado { get; set; }
        public int IMinutosAntesCita { get; set; }
        public int? IOrden { get; set; }
        public int IIdServicio { get; set; }

        public virtual Servicio IIdServicioNavigation { get; set; }
        public virtual ICollection<EncuestaPregunta> EncuestaPregunta { get; set; }
        public virtual ICollection<EncuestaRespuesta> EncuestaRespuesta { get; set; }
    }
}
