using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class SFDatosMedicos
    {
        public string EXAM_COD_ATENCION { get; set; }
        public string Campo { get; set; }
        public string Dato { get; set; }
        public string Dato_Campo_Codigo { get; set; }
        public string Dato_Campo_Tipo { get; set; }
        public bool Dato_Tipo_Clasificacion { get; set; }
    }
}
