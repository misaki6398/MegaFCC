using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaEcmBackEnd.Models.FccmAtomic.Repositorys
{
    public interface IUnitOfWork : IDisposable
    {
        FsiRtListDataRepositroy FsiRtListDataRepositroy { get; }
        void Commit();
    }
}