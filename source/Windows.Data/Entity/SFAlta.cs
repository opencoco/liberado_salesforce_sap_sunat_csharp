using System;
using System.Collections.Generic;

namespace ACME.Data.Entity
{
    public partial class SFAlta
    {
        public string EXAM_COD_ATENCION { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string TipoDocumento { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string Sexo { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string TelefonoFijo { get; set; }
        public string GradoInstruccion { get; set; }
        public string EstadoCivil { get; set; }
        public string Validado { get; set; }
        public string Cesado { get; set; }
        public string FechaCesado { get; set; }
        public DateTime? EXAM_FECHA_INICIO { get; set; }
        public string EXAM_COD_COMPAÑIA { get; set; }
        public string EXAM_COD_CONTRATA { get; set; }
        public string EXAM_COD_UNIDAD { get; set; }
        public DateTime? Trabajador_Fecha_Creacion { get; set; }
        public string Trabajador_Origen { get; set; }
        public DateTime? Trabajador_Fecha_Modificacion { get; set; }
        public string Trabajador_Usuario_Modificacion { get; set; }
        public short? SiEstado { get; set; }
        public string IdPaciente { get; set; }
    }
}
