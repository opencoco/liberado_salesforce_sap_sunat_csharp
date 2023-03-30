using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class UsuarioPassword
    {
        public int IIdUsuarioPassword { get; set; }
        public int IIdUsuario { get; set; }
        public string NvPassword { get; set; }
        public DateTime? DtCreado { get; set; }
        public DateTime? DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public byte[] VbPasswordHash { get; set; }
        public byte[] VbPasswordSalt { get; set; }

        public virtual Usuario IIdUsuarioNavigation { get; set; }
    }
}
