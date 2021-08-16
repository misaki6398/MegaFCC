using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  MegaEcmBackEnd.Models.MegaEcm.Repositorys
{
    public interface IUnitOfWork : IDisposable
    {
        TfAlertsRepository TfAlertsRepository { get; }
        TfCasesRepository TfCasesRepository { get; }
        void Commit();
    }
}