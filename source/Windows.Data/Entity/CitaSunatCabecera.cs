using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class CitaSunatCabecera
    {
        public CitaSunatCabecera()
        {
            //CitaSunatDetalle = new HashSet<CitaSunatDetalle>();
        }

        public int IIdCitaSunatCab { get; set; }
        public int IIdCita { get; set; }
        public string VcSunatSerie { get; set; }
        public int ISunatCorrelativo { get; set; }
        public string VcSunatTipoComprobante { get; set; }
        public string VcSunatMotivo { get; set; }
        public string VcSunatIdentificador { get; set; }
        public bool? BOkSunat { get; set; }
        public bool? BOkGeneracionComprobantePdf { get; set; }
        public bool? BOkGeneracionComprobanteXml { get; set; }
        public short SiSunatTipo { get; set; }
        public string VcSunatProveedor { get; set; }
        public DateTime DtCreacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public string VcTipoDoc { get; set; }


        //public virtual ICollection<CitaSunatDetalle> CitaSunatDetalle{ get; set; }

    }
}
