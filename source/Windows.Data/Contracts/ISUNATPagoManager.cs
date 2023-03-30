//using ACME.Domain.Entity;
using ACME.Data;
using ACME.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace ACME.Data.Contracts
{
    public interface ISUNATPagoManager
    {
        #region sap
        //Contado
        Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosContadoPend();
        Task<SAPAsientoContadoCab> getSAPAsientosContadoCab(int iIdCitaSunatCab, string vcTipoDoc);
        Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosContadoDet(int iIdCitaSunatCab);
        Task<bool> updateSAPAsientosContado(int iIdCitaSunatCab, string DocEntry, string TransId);
        //NC Contado
        Task<ICollection<SAPAsientoContadoNCPen>> getSAPAsientosContadoPend_NC();
        Task<SAPAsientoContadoCab> getSAPAsientosContadoCab_NC(int iIdCitaSunatCab, int? iIdCitaSunatDet, bool bCabecera);
        Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosContadoDet_NC(int iIdCitaSunatCab, int? iIdCitaSunatDet, bool bCabecera);
        Task<bool> updateSAPAsientosContado_NC(int iIdCitaSunatCab, int? iIdCitaSunatDet, bool bCabecera, string DocEntry, string TransId);

        //Credito
        Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosCreditoPend();
        Task<SAPAsientoContadoCab> getSAPAsientosCreditoCab(long iIdValoCitaSunatCab);
        Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosCreditoDet(long iIdValoCitaSunatCab);
        Task<bool> updateSAPAsientosCredito(string DocEntry, string TransId);

        //ND Crédito
        Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosCredito_ND_Pend();
        Task<SAPAsientoContadoCab> getSAPAsientosCredito_ND_Cab(long iIdValoCitaSunatCab, string DocEntry);
        Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosCredito_ND_Det(long iIdValoCitaSunatCab, string DocEntry);
        //NC Crédito
        Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosCredito_NC_Pend();
        Task<SAPAsientoContadoCab> getSAPAsientosCredito_NC_Cab(long iIdValoCitaSunatCab, string DocEntry);
        Task<ICollection<SAPAsientoContadoDet>> getSAPAsientosCredito_NC_Det(long iIdValoCitaSunatCab, string DocEntry);
        //Asientos Baja
        Task<ICollection<SAPAsientoContadoPen>> getSAPAsientosaja_Pend();
        Task<bool> updateSAPAsientosBaja(string TransId);

        #endregion sap

        Task<DocFin> ConsultarXmlNotaCredito(NotaCreditoPendiente item, int iIdUsuario);
        Task<CitaSunatCabecera> ConsultarSunatCabecera(int iIdCitaSunatCab, int iIdUsuario);
        Task<ICollection<EpisodiosEntregable>> getEpisodioxEntregablesPend(string vcCodEpisodioSync, int iIdusuario);
        Task<ICollection<EpisodiosEnColaDocs>> getEpisodiosEnColaParaEntregables();
        Task<object> UpdateEpisodioEntregableRep(string vcCodEpisodioSync);
        
        Task<object> InsertDocumentNotaCredito(DocFin doc);
        Task<DocFin> ConsultarPdfNotaCredito(NotaCreditoPendiente item, int iIdUsuario);
        Task<ICollection<NotaCreditoPendiente>> NotaCreditoPendDocs(int iIdUsuario);
        //Task<ICollection<int>> RegistrarComprobantes();
        Task<DocFin> ConsultarPdf(CitaSunatCabecera citaSunat, int iIdUsuario);
        Task<DocFin> ConsultarXml(CitaSunatCabecera citaSunat, int iIdUsuario);
        Task<object> InsertDocument(DocFin doc);
        Task<IEnumerable<DocsxRecuperar>> DocsXGenerar(int iIdUsuario);
       
        Task<ICollection<PersonaTmp>> GetPersonasSinclaveTemporalAsync();
    }
}
