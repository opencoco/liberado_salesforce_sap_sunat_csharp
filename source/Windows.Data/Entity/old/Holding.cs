using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class Holding
    {
        public Holding()
        {
            CotiCliente = new HashSet<CotiCliente>();
            Empresa = new HashSet<Empresa>();
            HoldingUsuario = new HashSet<HoldingUsuario>();
        }

        public int IIdHolding { get; set; }
        public string VcNombreComercial { get; set; }
        public string VcRuc { get; set; }
        public string VcRazonSocial { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public short? SiEstado { get; set; }

        public virtual ICollection<CotiCliente> CotiCliente { get; set; }
        public virtual ICollection<Empresa> Empresa { get; set; }
        public virtual ICollection<HoldingUsuario> HoldingUsuario { get; set; }
    }
}
