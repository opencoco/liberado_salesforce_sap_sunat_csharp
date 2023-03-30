using System;
using System.Collections.Generic;
using System.IO;

namespace ACME.Data.Entity
{
    public partial class CertificadoDigital
    {

        public byte[]? Certificado { get; set; }
        public string Clave { get; set; }

        

    }
}
