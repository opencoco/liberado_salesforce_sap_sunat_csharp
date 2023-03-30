using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class View1
    {
        public int IIdCitaTrabajador { get; set; }
        public string NvFormula { get; set; }
        public int? IIdPruebaAnterior { get; set; }
        public int IIdPruebaPosterior { get; set; }
        public int? IIdProtocolo { get; set; }
    }
}
