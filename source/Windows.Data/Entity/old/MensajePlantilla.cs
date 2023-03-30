using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class MensajePlantilla
    {
        public MensajePlantilla()
        {
            Notificacion = new HashSet<Notificacion>();
        }

        public int IIdMensajePlantilla { get; set; }
        public string VcNombre { get; set; }
        public string VcMensaje { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string JsonParam { get; set; }
        public short? SiEstado { get; set; }

        public virtual ICollection<Notificacion> Notificacion { get; set; }
    }
}
