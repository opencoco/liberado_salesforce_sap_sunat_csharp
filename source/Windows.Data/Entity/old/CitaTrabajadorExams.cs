using System;
using System.Collections.Generic;

namespace Natclar.Data.Entity
{
    public partial class CitaTrabajadorExams
    {
        public int IIdCitaTrabajadorExam { get; set; }
        public int? IIdCitaTrabajador { get; set; }
        public int IIdPrueba { get; set; }
        public bool? BOpcional { get; set; }
        public bool? BAdicional { get; set; }
        public bool? BConvalidable { get; set; }
        public bool? BAexcepcion { get; set; }
        public decimal? DePrecio { get; set; }
        public bool? BOtro { get; set; }
        public int? IIdCitaTrabaTitular { get; set; }

        public virtual CitaTrabajador IIdCitaTrabajadorNavigation { get; set; }
    }
}
