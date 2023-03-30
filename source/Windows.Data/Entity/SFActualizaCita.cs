using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class SFActualizaCita
    {
        public string id_orden { get; set; }
        public string Status { get; set; }
        public string SourceSystemIdentifier { get; set; }
        public string StartDate { get; set; }
        public string Establecimiento__c { get; set; }
        public string Priority { get; set; }
        public string Estado_del_pago_de_la_cita__c { get; set; }
        public string DateSigned { get; set; }
        public string Estado_de_la_atencion__c { get; set; }

    }
}
