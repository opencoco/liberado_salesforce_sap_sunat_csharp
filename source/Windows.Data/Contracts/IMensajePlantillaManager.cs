//using ACME.Domain.Entity;
using ACME.Data;
using ACME.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ACME.Data.Contracts
{
    public interface IMensajePlantillaManager : IRepository<MensajePlantilla>
    {
        Task<object> GetMensajePlantillaAsync(UrlQueryParameters urlQueryParameters);

    }
}
