//using ACME.Domain.Entity;
using ACME.Data;
using ACME.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ACME.Data.Contracts
{
    public interface ICitaTrabajadorManager //: IRepository<CitaTrabajador> 
    {
        Task<long> InsertaDocumento(DocFin doc);
        Task<long> InsertaEntregableaEpisodio(DocFin doc, long docid, string codEpisodio);
        Task<IEnumerable<EpisodioColaMerge>> ListEpisodiosColaMerge();
        Task<IEnumerable<EpisodioEntregableMerge>> ListEpisodiosEntregables(string vcCodEpisodioSync);
        Task<IEnumerable<CitaTrabajadorData>> ListCitaTrabajadorData();
        Task<object> SincronizaCitaData();
        Task<object> UpdateFilemedioCompleto(string vcCodEpisodioSync);
        Task<CertificadoDigital> GetEpisodiosEntregableDigitalSignature(string vcCodEpisodioSync, int iIdSubClaseDocumento);

    }
}
