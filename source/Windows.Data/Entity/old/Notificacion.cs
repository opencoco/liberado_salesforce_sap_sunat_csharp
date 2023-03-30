using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Notificacion
    {
        public int IIdNotificacion { get; set; }
        public byte? TiTipo { get; set; }
        public byte? TiPrioridad { get; set; }
        public string TxMensajeCorreo { get; set; }
        public string VcMensajeSms { get; set; }
        public int? IIdMensajePlantilla { get; set; }
        public int IIdUsuario { get; set; }
        public int? IIdDocumento { get; set; }
        public DateTime? DtVisto { get; set; }

        public virtual Documento IIdDocumentoNavigation { get; set; }
        public virtual MensajePlantilla IIdMensajePlantillaNavigation { get; set; }
        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
