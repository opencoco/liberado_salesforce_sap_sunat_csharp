//using ACME.Domain.Entity;
using ACME.Data;
using ACME.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace ACME.Data.Contracts
{
    public interface ISUNATManager
    {
        Task<object> RegistraPadron(DataTable dt, int offset, int limit);
    }
}
