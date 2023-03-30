using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class VariablesDemografica
    {
        public int IIdVariableDemografica { get; set; }
        public string VcNombreAmigable { get; set; }
        public string VcCampo { get; set; }
        public short SiFormato { get; set; }
        public byte? TiLongitudEntera { get; set; }
        public byte? TiLongitudDecimal { get; set; }
        public bool? BObligatorio { get; set; }
    }
}
