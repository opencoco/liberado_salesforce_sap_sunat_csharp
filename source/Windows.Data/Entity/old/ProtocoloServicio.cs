using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class ProtocoloServicio
    {
        public int IdProtocoloServicio { get; set; }
        public int IIdProtocolo { get; set; }
        public int IIdServicio { get; set; }

        public virtual Protocolo IIdProtocoloNavigation { get; set; }
        public virtual Servicio IIdServicioNavigation { get; set; }
    }
}
