using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class DefFichaCampo
    {
        public DefFichaCampo()
        {
            DefFichaCampoOpcion = new HashSet<DefFichaCampoOpcion>();
            MatrizInfoPruebaCampo = new HashSet<MatrizInfoPruebaCampo>();
            ProcedimientoCampo = new HashSet<ProcedimientoCampo>();
        }

        public int IIdCampo { get; set; }
        public string VcNombre { get; set; }
        public string VcDescripcion { get; set; }
        public short SiFormato { get; set; }
        public byte TiLongitudEntera { get; set; }
        public byte? TiLongitudDecimal { get; set; }
        public short? SiUnidadMedida { get; set; }
        public decimal? DeValorMinimo { get; set; }
        public decimal? DeValorMaximo { get; set; }
        public bool? BObligatorio { get; set; }
        public bool? BSensible { get; set; }
        public bool? BComparable { get; set; }
        public DateTime DtCreacion { get; set; }
        public DateTime? DtModificacion { get; set; }
        public int IIdUsuarioCreacion { get; set; }
        public int? IIdUsuarioModificacion { get; set; }
        public int IIdFicha { get; set; }
        public short? SiEstado { get; set; }
        public bool? BParaFormulas { get; set; }
        public short? SiTipoDato { get; set; }
        public bool? BVisible { get; set; }
        public short? SiOrden { get; set; }
        public string NvFormula { get; set; }

        public virtual DefFicha IIdFichaNavigation { get; set; }
        public virtual ICollection<DefFichaCampoOpcion> DefFichaCampoOpcion { get; set; }
        public virtual ICollection<MatrizInfoPruebaCampo> MatrizInfoPruebaCampo { get; set; }
        public virtual ICollection<ProcedimientoCampo> ProcedimientoCampo { get; set; }
    }
}
